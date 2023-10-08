using System.Collections.ObjectModel;

namespace Lab4.Model.Interfaces;
/// <summary>
/// The "brains" of the program. All of these methods must ask the database to
/// do the actual operation (after checking that the fields are valid, no
/// duplications, etc.).
/// 
/// DO NOT add new methods to the interface; save it for the class.
/// </summary>
public interface IBusinessLogic
{

    public ObservableCollection<Airport> Airports { get; } //Property

    /// <summary>
    /// !!!Return type can be changed!!!
    /// </summary>
    public ErrorHandling AddAirport(string id, string city, string dateVisited, string rating);

    /// <summary>
    /// !!!Return type can be changed!!! 
    /// </summary>
    public ErrorHandling DeleteAirport(string id);

    /// <summary>
    /// !!!Return type can be changed!!! 
    /// </summary>
    public ErrorHandling EditAirport(string id, string city, string dateVisited, string rating);

    /// <summary>
    /// Null if not found.
    /// </summary>
    public bool FindAirport(string id);

    /// <summary>
    /// Checks how many airports the user has in their list and determines their rank.
    /// </summary>
    public string CalculateStatistics();

    /// <summary>
    /// Returns a list of all airports.
    /// </summary>
    public ObservableCollection<Airport> GetAirports();

}

