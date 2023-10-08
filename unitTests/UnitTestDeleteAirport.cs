using Lab4;
using Lab4.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests;
/// <summary>
/// Class to hold all Airport Delete unit tests.
/// </summary>
[TestClass]
public class UnitTestDeleteAirport
{
    /// <summary>
    /// Test: Check that an airport Id of min length (3 chars) can be deleted from DB.
    /// </summary>
    [TestMethod]
    public void DeleteAirportTestIdMin()
    {
        BusinessLogic businessLogic = new();
        Airport testAirport = new("ARP", "DeleteCity", DateTime.Now, 5); //Create default Airport object: ID="KATW", City="Appleton", DateVisited=DateTime.Now, Rating=5

        //"ARP" should already be in DB, but better to check anyways.
        if (!businessLogic.Airports.Contains(testAirport))
        {
            businessLogic.AddAirport(testAirport.Id, testAirport.City, testAirport.DateVisited.ToString(), testAirport.Rating.ToString());
        }

        ErrorHandling response = businessLogic.DeleteAirport(testAirport.Id);

        Assert.AreEqual(Constants.ErrorType.NoError, response.ErrorType);
    }

    /// <summary>
    /// Test: Check that an airport Id of max length (4 chars) can be deleted from DB.
    /// </summary>
    [TestMethod]
    public void DeleteAirportTestIdMax()
    {
        BusinessLogic businessLogic = new();
        Airport testAirport = new(); //Create default Airport object: ID="KATW", City="Appleton", DateVisited=DateTime.Now, Rating=5

        //"KATW should already be in DB, but better to check anyways.
        if (!businessLogic.Airports.Contains(testAirport))
        {
            businessLogic.AddAirport(testAirport.Id, testAirport.City, testAirport.DateVisited.ToString(), testAirport.Rating.ToString());
        }

        ErrorHandling response = businessLogic.DeleteAirport(testAirport.Id);

        Assert.AreEqual(Constants.ErrorType.NoError, response.ErrorType);
    }

    /// <summary>
    /// Test: Check that an attempt to delete with a malformed Id gives the IncorrectIdFormat error 
    /// (instead of IdNotFound, as that would indicate a search on the DB was attempted).
    /// </summary>
    [TestMethod]
    public void DeleteAirportTestIdFormatFailure()
    {
        BusinessLogic businessLogic = new();

        ErrorHandling response = businessLogic.DeleteAirport("INCORRECT FORMAT");

        Assert.AreEqual(Constants.ErrorType.IncorrectIdFormat, response.ErrorType);
    }

    /// <summary>
    /// Test: Check that an attempt to delete with an empty string Id gives the IncorrectIdFormat error 
    /// (instead of IdNotFound, as that would indicate a search on the DB was attempted).
    /// </summary>
    [TestMethod]
    public void DeleteAirportTestIdEmptyString()
    {
        BusinessLogic businessLogic = new();

        ErrorHandling response = businessLogic.DeleteAirport("");

        Assert.AreEqual(Constants.ErrorType.IncorrectIdFormat, response.ErrorType);
    }

    /// <summary>
    /// Test: Check that attempts to delete a non-existent airport fails with correct error code.
    /// </summary>
    [TestMethod]
    public void DeleteAirportTestAirportDoesNotExist()
    {
        BusinessLogic businessLogic = new();
        Airport testAirport = new("DNE", "", DateTime.Now, 5);

        //"DNE" shouldn't be in DB, but better to check anyways.
        if (businessLogic.Airports.Contains(testAirport))
        {
            businessLogic.DeleteAirport(testAirport.Id);
        }

        ErrorHandling response = businessLogic.DeleteAirport(testAirport.Id);

        Assert.AreEqual(Constants.ErrorType.AirportNotFound, response.ErrorType);
    }

}