using Lab4.Model.Interfaces;
using System.Collections.ObjectModel;

namespace Lab4.Model;
/// <summary>
/// Middle tier; handles rules for what is allowable values before sending to database.
/// </summary>
public class BusinessLogic : IBusinessLogic
{
    private readonly Database database = new();
    public ObservableCollection<Airport> Airports { get { return database.Airports; } } //property field

    /// <summary>
    /// Checks for formatting and parsing errors before passing to DB.
    /// </summary>
    /// <param name="id"> airport ID </param>
    /// <param name="city"> airport City</param>
    /// <param name="dateVisited"> date airport was visited </param>
    /// <param name="rating"> airport Rating </param>
    /// <returns> Instance of ErrorHandling class indicating if an error occurred </returns>
    public ErrorHandling AddAirport(string id, string city, string dateVisited, string rating)
    {
        DateTime? parseDate = CorrectDateFormat(dateVisited);   //if null there was parse or format error
        int? parseRating = CorrectRatingFormat(rating);         //if null there was parse or format error

        if (!CorrectIDFormat(id))           //id = string 3-4 char long
        {
            return new ErrorHandling
            (
                Constants.ErrorType.IncorrectIdFormat,
                Constants.IncorrectIdFormatMessage,
                id
            );
        }
        else if (FindAirport(id))            //existing list check, no point in sending to SQL if Airport already in list
        {
            return new ErrorHandling
            (
                Constants.ErrorType.DuplicateAirport,
                Constants.DuplicateAirportMessage,
                id
            );
        }
        else if (!CorrectCityFormat(city))  //city = string max 25 char long
        {
            return new ErrorHandling
            (
                Constants.ErrorType.IncorrectCityFormat,
                Constants.IncorrectCityFormatMessage,
                city
            );
        }
        else if (parseDate == null)         //date = string in format MM/DD/YYYY
        {
            return new ErrorHandling
            (
                Constants.ErrorType.IncorrectDateFormat,
                Constants.IncorrectDateFormatMessage,
                dateVisited
            );
        }
        else if (parseRating == null)       //rating = int between 1-5
        {
            return new ErrorHandling
            (
                Constants.ErrorType.IncorrectRatingFormat,
                Constants.IncorrectRatingFormatMessage,
                rating
            );
        }

        return database.InsertAirport(new Airport(id, city, (DateTime)parseDate, (int)parseRating)); //Send to DB
    }

    /// <summary>
    /// Generate a formatted string after performing calculations.
    /// No Rank = 0-41 | Bronze = 42-84 | Silver = 85-124 | Gold = 125+
    /// </summary>
    /// <returns> generated string indicating what rank the user is at and how many airports until next rank if applicable </returns>
    public string CalculateStatistics()
    {
        int numAirports = database.Airports.Count; //size of list corresponds to number of airports
        string apString = numAirports > 1 ? "airports" : "airport"; //make plural if more than 1 airport
        int numUntilNextRank;

        switch (numAirports)
        {
            case >= (int)Constants.CalcStatsRankNum.NumToGold:
                return string.Format(Constants.GoldCalcStatsString, numAirports, apString, Constants.Gold);

            case >= (int)Constants.CalcStatsRankNum.NumToSilver:
                numUntilNextRank = (int)Constants.CalcStatsRankNum.NumToGold - numAirports;
                return string.Format(Constants.DefaultCalcStatsString, numAirports, apString, numUntilNextRank, Constants.Gold);

            case >= (int)Constants.CalcStatsRankNum.NumToBronze:
                numUntilNextRank = (int)Constants.CalcStatsRankNum.NumToSilver - numAirports;
                return string.Format(Constants.DefaultCalcStatsString, numAirports, apString, numUntilNextRank, Constants.Silver);

            default:
                numUntilNextRank = (int)Constants.CalcStatsRankNum.NumToBronze - numAirports;
                return string.Format(Constants.DefaultCalcStatsString, numAirports, apString, numUntilNextRank, Constants.Bronze);
        }
    }

    /// <summary>
    /// Asks for id, and deletes that airport if exists.
    /// </summary>
    /// <param name="id"> 3-4 letter ID code </param>
    /// <returns> Instance of ErrorHandling class indicating if an error occurred </returns>
    public ErrorHandling DeleteAirport(string id)
    {
        if (!CorrectIDFormat(id))   //id = string 3-4 char long
        {
            return new ErrorHandling
            (
                Constants.ErrorType.IncorrectIdFormat,
                Constants.IncorrectIdFormatMessage,
                id
            );
        }
        else if (!FindAirport(id)) //existing list check, no point in sending to SQL if Airport not in list
        {
            return new ErrorHandling
            (
                Constants.ErrorType.AirportNotFound,
                Constants.AirPortNotFoundMessage,
                id
            );
        }

        return database.DeleteAirport(id);
    }

    /// <summary>
    /// Edit an existing airport entry.
    /// </summary>
    /// <param name="id"> 3-4 letter ID code </param>
    /// <param name="city"> the city the airport is located in </param>
    /// <param name="dateVisited"> the date the airport was visited </param>
    /// <param name="rating"> user rating of the airport </param>
    /// <returns> Instance of ErrorHandling class indicating if an error occurred </returns>
    public ErrorHandling EditAirport(string id, string city, string dateVisited, string rating)
    {
        DateTime? parseDate = CorrectDateFormat(dateVisited);   //if null there was parse or format error
        int? parseRating = CorrectRatingFormat(rating);         //if null there was parse or format error

        if (!CorrectIDFormat(id))           //id = string 3-4 char long
        {
            return new ErrorHandling
            (
                Constants.ErrorType.IncorrectIdFormat,
                Constants.IncorrectIdFormatMessage,
                id
            );
        }
        if (!FindAirport(id))               //existing list check, no point in sending to SQL if Airport not in list
        {
            return new ErrorHandling
            (
                Constants.ErrorType.AirportNotFound,
                Constants.AirPortNotFoundMessage,
                id
            );
        }
        else if (!CorrectCityFormat(city))  //city = string max 25 char long
        {
            return new ErrorHandling
            (
                Constants.ErrorType.IncorrectCityFormat,
                Constants.IncorrectCityFormatMessage,
                city
            );
        }
        else if (parseDate == null)         //date = string in format MM/DD/YYYY
        {
            return new ErrorHandling
            (
                Constants.ErrorType.IncorrectDateFormat,
                Constants.IncorrectDateFormatMessage,
                dateVisited
            );
        }
        else if (parseRating == null)       //rating = int between 1-5
        {
            return new ErrorHandling
            (
                Constants.ErrorType.IncorrectRatingFormat,
                Constants.IncorrectRatingFormatMessage,
                rating
            );
        }

        return database.UpdateAirport(new Airport(id, city, (DateTime)parseDate, (int)parseRating));
    }

    /// <summary>
    /// Searches for existing airport entry. Returns null if not found.
    /// </summary>
    /// <param name="id"> 3-4 letter ID code </param>
    /// <returns> true if airport with given ID found in list; false otherwise </returns>
    public bool FindAirport(string id)
    {
        Airport tempAirport = new() //make a dummy airport to assign ID to; the Equals() method only needs ID.
        {
            Id = id
        };

        return database.Airports.Contains(tempAirport);
    }

    /// <summary>
    /// Consolidate Id format check.
    /// </summary>
    /// <param name="id"> 3-4 letter ID code </param>
    /// <returns> true if correctly formatted; false otherwise. </returns>
    private static bool CorrectIDFormat(string id)
    {
        if (id == null)
        {
            return false;
        }

        return id.Length >= (int)Constants.IdMinMax.MinIdLength &&
                id.Length <= (int)Constants.IdMinMax.MaxIdLength;
    }

    /// <summary>
    /// Consolidate City format check.
    /// </summary>
    /// <param name="city"> the city the airport is located in </param>
    /// <returns> "True" if correctly formatted; false otherwise. </returns>
    private static bool CorrectCityFormat(string city)
    {
        if (city == null)
        {
            return false;
        }

        return city.Length >= (int)Constants.CityMinMax.MinCityLength &&
                city.Length <= (int)Constants.CityMinMax.MaxCityLength;
    }

    /// <summary>
    /// Consolidate DateVisited format check.
    /// </summary>
    /// <param name="dateVisited"></param>
    /// <returns> a DateTime parsed from string dateVisited param if no exception is thrown; null otherwise </returns>
    private static DateTime? CorrectDateFormat(string dateVisited)
    {
        try
        {
            DateTime parseDate = DateTime.Parse(dateVisited);
            return parseDate;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Consolidate Rating format check.
    /// </summary>
    /// <param name="rating"> user entered rating </param>
    /// <returns> int if parsing and format check succeeds; null otherwise </returns>
    private static int? CorrectRatingFormat(string rating)
    {
        try
        {
            int parseRating = int.Parse(rating);

            if (parseRating >= (int)Constants.RatingMinMax.MinRatingNum &&
                parseRating <= (int)Constants.RatingMinMax.MaxRatingNum)
            {
                return parseRating; //passes all checks
            }

            return null; //doesn't meet formatting requirements
        }
        catch
        {
            return null; //parse failure
        }
    }

    //Not needed for this lab. Left in because IBusinessLogic shouldn't be changed.
    public ObservableCollection<Airport> GetAirports()
    {
        throw new NotImplementedException();
    }

}