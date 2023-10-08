namespace Lab4;
/// <summary>
/// Strings and warnings that come up repeatedly.
/// </summary>
public static class Constants
{
    //Airport object related errors; combine enum and strings to allow error checking by enum Integer an reporting specific details w/ message
    public enum ErrorType
    {
        NoError,                //(int)ErrorType.NoError = 0
        AirportNotFound,
        DuplicateAirport,
        IncorrectIdFormat,
        IncorrectCityFormat,
        IncorrectDateFormat,
        IncorrectRatingFormat,
        IdNotFound,
        DatabaseError
    }
    public const string AirPortNotFoundMessage = "An Airport matching the given ID value was not found.\nID entered: {0}";
    public const string DuplicateAirportMessage = "An Airport by this ID already exists. Airport not added to list.\nID entered: {0}";
    public const string IncorrectIdFormatMessage = "The format for entered ID was invalid. ID must be 3-4 characters long.\nID entered: {0}";
    public const string IncorrectCityFormatMessage = "The format for entered City was invalid. City must be 1-25 characters long.\nCity entered: {0}";
    public const string IncorrectDateFormatMessage = "The format for entered Date was invalid. Date must be formatted as MM/DD/YYYY.\nDate entered: {0}";
    public const string IncorrectRatingFormatMessage = "The format for entered Rating was invalid. Rating must be a number from 1-5.\nRating entered: {0}";
    public const string IdNotFoundMessage = "The given ID was not found in the database. No changes made.\nID entered: {0}";
    public const string DatabaseErrorMessage = "The database was unable to be reached. No changes made.";

    //Calculate statistics strings and values
    public enum CalcStatsRankNum
    {
        NumToBronze = 42,
        NumToSilver = 84,
        NumToGold = 125
    }
    public const string DefaultCalcStatsString = "{0} {1} visited: {2} airports remaining until achieving {3}.\n";
    public const string GoldCalcStatsString = "{0} {1} visited: Max rank of {2} achieved.\n";
    public const string Gold = "Gold";
    public const string Silver = "Silver";
    public const string Bronze = "Bronze";

    //Airport object min/max values
    public enum IdMinMax
    {
        MinIdLength = 3,
        MaxIdLength = 4
    }
    public enum CityMinMax
    {
        MinCityLength = 1,
        MaxCityLength = 25
    }
    public enum RatingMinMax
    {
        MinRatingNum = 1,
        MaxRatingNum = 5
    }

    //Default airport object information
    public const string DefaultAirportId = "KATW";
    public const string DefaultCity = "Appleton";

    //Database connection information
    public const string DbHost = "cs341-labserver-13086.5xj.cockroachlabs.cloud";
    public const int DbPort = 26257;
    public const string DbName = "airportdb";
    public const string DbApp = "lab3"; //optional
    public const string DbUser = "hill_joshuataylor_gm";
    public const string DbPass = "iZyAvF0iGFv553Ahf-yvGg"; //couldn't get string from secrets.json

    //SQL Queries and Commands
    public const string SQLSelectAllAirportsString = "SELECT id, city, date_visited, rating FROM airports";
    public const string SQLCreateAirportTableNotExistsString = "CREATE TABLE IF NOT EXISTS airports (id VARCHAR(4) PRIMARY KEY, city VARCHAR(25), date_visited TIMESTAMP, rating SMALLINT)";
    public const string SQLInsertAirportsTableString = "INSERT INTO airports (id, city, date_visited, rating) VALUES (@id, @city, @dateVisited, @rating)";
    public const string SQLUpdateAirportsTableString = "UPDATE airports SET city = @city, date_visited = @dateVisited, rating = @rating WHERE id = @id;";
    public const string SQLDeleteAirportsTableString = "DELETE FROM airports WHERE id = @id";
}