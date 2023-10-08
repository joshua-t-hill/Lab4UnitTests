namespace Lab4.Model;
/// <summary>
/// Class for holding individual airport information to be kept in a list.
/// </summary>
[Serializable]
public class Airport
{
    private string id;
    private string city;
    private DateTime dateVisited;
    private int rating;

    public string Id
    {
        get { return id; }
        set { id = value; }
    }
    public string City
    {
        get { return city; }
        set { city = value; }
    }
    public DateTime DateVisited
    {
        get { return dateVisited; }
        set { dateVisited = value; }
    }
    public int Rating
    {
        get { return rating; }
        set { rating = value; }
    }

    /// <summary>
    /// Default to KATW, Appleton, date the program is running, 5.
    /// </summary>
    public Airport()
    {
        id = Constants.DefaultAirportId;
        city = Constants.DefaultCity;
        dateVisited = DateTime.Now;
        rating = (int)Constants.RatingMinMax.MaxRatingNum; //int value of enum corresponds to max rating num
    }

    /// <summary>
    /// Paramaterized constructor.
    /// </summary>
    public Airport(string id, string city, DateTime dateVisited, int rating)
    {
        this.id = id;
        this.city = city;
        this.dateVisited = dateVisited; //arbitrarily inserts the current time as it isn't user-specified.
        this.rating = rating;
    }

    /// <summary>
    /// Return true if obj is an Airport and has the same ID as this Airport.
    /// </summary>
    /// <param name="obj"> the Airport to be conmpared with </param>
    /// <returns> true if compared Airport id's are the same; false otherwise </returns>
    override public bool Equals(object obj)
    {
        if (obj == null || obj is not Airport) return false; //null check + type check

        Airport compAirport = obj as Airport; //confirmed not-null and is of type Airport; can safely cast to Airport

        return id.Equals(compAirport?.id); //compare id Strings
    }

}