using Lab4.Model.Interfaces;
using Npgsql;
using System.Collections.ObjectModel;

namespace Lab4.Model;
/// <summary>
/// Back-end class.
/// </summary>
internal class Database : IDataBase
{
    private readonly ObservableCollection<Airport> airports = new();
    private readonly string connString = "";

    public ObservableCollection<Airport> Airports { get { return airports; } }

    /// <summary>
    /// Default constructor. Gets the list of Airport objects from DB text file.
    /// </summary>
    public Database()
    {
        connString = GetConnectionString(); //will only connect to single database and table; set connection string immediately on generation

        CreateAirportsTable(); //check if table exists and create it if not; error-prevention

        try
        {
            airports = new ObservableCollection<Airport>(SelectAllAirports()); //leverage Lab1 list
        }
        catch (Exception e) //Silent error
        {
            Console.WriteLine("Error occurred while attempting to access SQL DB: {0}", e.Message);
        }
    }

    /// <summary>
    /// Send delete query to SQL server.
    /// </summary>
    /// <param name="id"> ID of airport to delete </param>
    /// <returns> Instance of ErrorHandling class indicating for error-checking purposes </returns>
    public ErrorHandling DeleteAirport(string id)
    {
        try
        {
            int numDeleted = CreateAndExecuteSqlQuery(true, Constants.SQLDeleteAirportsTableString, id, "", null, null);

            if (numDeleted > 0)  //the airport was found and deleted
            {
                SelectAllAirports(); //repopulate airports to reflect changes
                return new ErrorHandling(Constants.ErrorType.NoError, "");
            }
            else                //airport was not found, report error
            {
                return new ErrorHandling
                (
                    Constants.ErrorType.IdNotFound,
                    Constants.IdNotFoundMessage,
                    id
                );
            }
        }
        catch
        {
            return new ErrorHandling
            (
                Constants.ErrorType.DatabaseError,
                Constants.DatabaseErrorMessage
            );
        }
    }

    /// <summary>
    /// Add new airport to airports DB table.
    /// </summary>
    /// <param name="airport"> the new airport to be added to DB </param>
    /// <returns> Instance of ErrorHandling class indicating for error-checking purposes </returns>
    public ErrorHandling InsertAirport(Airport airport)
    {
        try
        {
            CreateAndExecuteSqlQuery
            (
                false,
                Constants.SQLInsertAirportsTableString,
                airport.Id,
                airport.City,
                airport.DateVisited,
                airport.Rating
            );

            SelectAllAirports(); //repopulate airports to reflect changes
            return new ErrorHandling(Constants.ErrorType.NoError, "");
        }
        catch
        {
            return new ErrorHandling
            (
                Constants.ErrorType.DatabaseError,
                Constants.DatabaseErrorMessage
            );
        }
    }

    /// <summary>
    /// Read in SQL SELECT query results; store in airports list.
    /// </summary>
    /// <returns> collection of all airports from database; empty collection if nothing in database </returns>
    public ObservableCollection<Airport> SelectAllAirports()
    {
        airports.Clear();

        using var conn = new NpgsqlConnection(connString);
        conn.Open();

        using var cmd = new NpgsqlCommand(Constants.SQLSelectAllAirportsString, conn);
        using var reader = cmd.ExecuteReader(); // used for SELECT statement, returns a forward-only traversable object
        while (reader.Read()) // each time through we get another row in the table
        {
            string id = reader.GetString(0);
            string city = reader.GetString(1);
            DateTime dateVisited = reader.GetDateTime(2);
            int rating = reader.GetInt32(3);
            Airport airportToAdd = new(id, city, dateVisited, rating);
            airports.Add(airportToAdd);
        }

        return airports;
    }

    /// <summary>
    /// Update existing airport in database table.
    /// </summary>
    /// <param name="airport"> airport class instance where ID points to the airport to update in DB </param>
    /// <returns> collection of all airports from database; empty collection if nothing in database </returns>
    public ErrorHandling UpdateAirport(Airport airport)
    {
        try
        {
            int numUpdated =
                CreateAndExecuteSqlQuery
                (
                    false,
                    Constants.SQLUpdateAirportsTableString,
                    airport.Id,
                    airport.City,
                    airport.DateVisited,
                    airport.Rating
                );

            if (numUpdated > 0)  //the airport was found and updated
            {
                SelectAllAirports(); //repopulate airports to reflect changes
                return new ErrorHandling(Constants.ErrorType.NoError, "");
            }
            else                //airport was not found, report error
            {
                return new ErrorHandling
                (
                    Constants.ErrorType.IdNotFound,
                    Constants.IdNotFoundMessage,
                    airport.Id
                );
            }
        }
        catch
        {
            return new ErrorHandling
            (
                Constants.ErrorType.DatabaseError,
                Constants.DatabaseErrorMessage
            );
        }
    }

    /// <summary>
    /// Consolidate calls to generate connection string.
    /// </summary>
    /// <returns> A connection string for use in SQL DB queries </returns>
    private static string GetConnectionString()
    {
        var connStringBuilder = new NpgsqlConnectionStringBuilder()
        {
            Host = Constants.DbHost,
            Port = Constants.DbPort,
            SslMode = SslMode.VerifyFull,
            Username = Constants.DbUser,
            Password = Constants.DbPass,
            Database = Constants.DbName,
            ApplicationName = Constants.DbApp,
            IncludeErrorDetail = true
        };

        return connStringBuilder.ConnectionString;
    }

    /// <summary>
    /// When this file is created, automatically check if table exists on SQL side. If not, create it.
    /// </summary>
    private void CreateAirportsTable()
    {
        try
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            new NpgsqlCommand(Constants.SQLCreateAirportTableNotExistsString, conn).ExecuteNonQuery();
        }
        catch (Exception e) //Silent error
        {
            Console.WriteLine("Error occurred while attempting to access SQL DB: {0}", e.Message);
        }
    }

    /// <summary>
    /// Consolidate calls to execute Npgsql queries.
    /// </summary>
    /// <param name="isDelete"> indicates that ID is the only value needed </param>
    /// <param name="query"> the query to run in string form </param>
    /// <param name="id"> an airport ID </param>
    /// <param name="city"> the airport City </param>
    /// <param name="dateVisited"> the date the airport was visited</param>
    /// <param name="rating"> airport rating </param>
    /// <returns> an integer indicating how many rows were changed in airports DB table </returns>
    private int CreateAndExecuteSqlQuery(bool isDelete, string query, string id,
                                                    string city, DateTime? dateVisited, int? rating)
    {
        using var conn = new NpgsqlConnection(connString);
        conn.Open();

        using var cmd = new NpgsqlCommand()
        {
            Connection = conn,
            CommandText = query
        };

        if (!isDelete) //if isDelete==true then skip these params as they aren't used.
        {
            cmd.Parameters.AddWithValue("city", city);
            cmd.Parameters.AddWithValue("dateVisited", dateVisited);
            cmd.Parameters.AddWithValue("rating", rating);
        }

        cmd.Parameters.AddWithValue("id", id); //always needed

        return cmd.ExecuteNonQuery(); //sends back the amount of rows changed; useful for delete and update
    }

    //No longer used as of Lab3; move to SQL usage made obsolete.
    public Airport SelectAirport(string id)
    {
        throw new NotImplementedException();
    }

}