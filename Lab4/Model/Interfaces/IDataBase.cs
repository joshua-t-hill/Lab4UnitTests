using System.Collections.ObjectModel;

namespace Lab4.Model.Interfaces;
/// <summary>
/// Handles all database operations.
/// 
/// DO NOT add new methods to the interface; save it for the class.
/// </summary>
internal interface IDataBase
{
    /// <summary>
    /// Reads from database; stores it in airports.
    /// </summary>
    public ObservableCollection<Airport> SelectAllAirports();

    /// <summary>
    /// Returns Airport given id, null if not found.
    /// </summary>
    public Airport SelectAirport(string id);

    /// <summary>
    /// !!!Return type can be changed!!! 
    /// </summary>
    public ErrorHandling InsertAirport(Airport airport);

    /// <summary>
    /// !!!Return type can be changed!!! 
    /// </summary>
    public ErrorHandling DeleteAirport(string id);

    /// <summary>
    /// !!!Return type can be changed!!! 
    /// </summary>
    public ErrorHandling UpdateAirport(Airport airport);

}

