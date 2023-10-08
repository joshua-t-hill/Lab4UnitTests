using Lab4;
using Lab4.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests;
[TestClass]
public class UnitTestUpdateAirport
{
    /// <summary>
    /// Test: Check that an airport with Id of min length (3 chars) can be updated in DB.
    /// </summary>
    [TestMethod]
    public void UpdateAirportTestIdMin()
    {
        BusinessLogic businessLogic = new();
        Airport testAirport = new("ARP", "UpdateCity", DateTime.Now, 5); //Create default Airport object: ID="KATW", City="Appleton", DateVisited=DateTime.Now, Rating=5

        //"ARP" should already be in DB, but better to check anyways.
        if (!businessLogic.Airports.Contains(testAirport))
        {
            businessLogic.AddAirport(testAirport.Id, testAirport.City, testAirport.DateVisited.ToString(), testAirport.Rating.ToString());
        }

        ErrorHandling response = businessLogic.EditAirport("ARP", "UpdateCity", "02/02/2002", "2");

        Assert.AreEqual(Constants.ErrorType.NoError, response.ErrorType);
    }

    /// <summary>
    /// Test: Check that an airport with Id of max length (4 chars) can be updated in DB.
    /// </summary>
    [TestMethod]
    public void UpdateAirportTestIdMax()
    {
        BusinessLogic businessLogic = new();
        Airport testAirport = new(); //Create default Airport object: ID="KATW", City="Appleton", DateVisited=DateTime.Now, Rating=5

        //"KATW should already be in DB, but better to check anyways.
        if (!businessLogic.Airports.Contains(testAirport))
        {
            businessLogic.AddAirport(testAirport.Id, testAirport.City, testAirport.DateVisited.ToString(), testAirport.Rating.ToString());
        }

        ErrorHandling response = businessLogic.EditAirport("KATW", "UpdateCity", "02/02/2002", "2");

        Assert.AreEqual(Constants.ErrorType.NoError, response.ErrorType);
    }

    /// <summary>
    /// Test: Check that an attempt to edit with a malformed Id gives the IncorrectIdFormat error 
    /// (instead of IdNotFound, as that would indicate a search on the DB was attempted).
    /// </summary>
    [TestMethod]
    public void UpdateAirportTestIdFormatFailure()
    {
        BusinessLogic businessLogic = new();

        ErrorHandling response = businessLogic.EditAirport("INCORRECT FORMAT", "", "", "");

        Assert.AreEqual(Constants.ErrorType.IncorrectIdFormat, response.ErrorType);
    }

    /// <summary>
    /// Test: Check that an attempt to edit with a empty string Id gives the IncorrectIdFormat error 
    /// (instead of IdNotFound, as that would indicate a search on the DB was attempted).
    /// </summary>
    [TestMethod]
    public void UpdateAirportTestIdEmptyString()
    {
        BusinessLogic businessLogic = new();

        ErrorHandling response = businessLogic.EditAirport("", "", "", "");

        Assert.AreEqual(Constants.ErrorType.IncorrectIdFormat, response.ErrorType);
    }

    /// <summary>
    /// Test: Check that attempts to edit a non-existent airport fails with correct error code.
    /// </summary>
    [TestMethod]
    public void UpdateAirportTestAirportDoesNotExist()
    {
        BusinessLogic businessLogic = new();
        Airport testAirport = new("DNE", "", DateTime.Now, 5);

        //"DNE" shouldn't be in DB, but better to check anyways.
        if (businessLogic.Airports.Contains(testAirport))
        {
            businessLogic.DeleteAirport(testAirport.Id);
        }

        ErrorHandling response = businessLogic.EditAirport(testAirport.Id, "", "", "");

        Assert.AreEqual(Constants.ErrorType.AirportNotFound, response.ErrorType);
    }

    /// <summary>
    /// Test: Check that an airport with City of min length (1 char) can be updated in DB.
    /// </summary>
    [TestMethod]
    public void UpdateAirportTestCityMin()
    {
        BusinessLogic businessLogic = new();
        Airport testAirport = new(); //Create default Airport object: ID="KATW", City="Appleton", DateVisited=DateTime.Now, Rating=5

        //"KATW should already be in DB, but better to check anyways.
        if (!businessLogic.Airports.Contains(testAirport))
        {
            businessLogic.AddAirport(testAirport.Id, testAirport.City, testAirport.DateVisited.ToString(), testAirport.Rating.ToString());
        }

        ErrorHandling response = businessLogic.EditAirport(testAirport.Id, "A", "02/02/2222", "2");

        Assert.AreEqual(Constants.ErrorType.NoError, response.ErrorType);
    }

    /// <summary>
    /// Test: Check that an airport with City of max length (25 char) can be updated in DB.
    /// </summary>
    [TestMethod]
    public void UpdateAirportTestCityMax()
    {
        BusinessLogic businessLogic = new();
        Airport testAirport = new(); //Create default Airport object: ID="KATW", City="Appleton", DateVisited=DateTime.Now, Rating=5

        //"KATW should already be in DB, but better to check anyways.
        if (!businessLogic.Airports.Contains(testAirport))
        {
            businessLogic.AddAirport(testAirport.Id, testAirport.City, testAirport.DateVisited.ToString(), testAirport.Rating.ToString());
        }

        ErrorHandling response = businessLogic.EditAirport(testAirport.Id, "ABCDEFGHIJKLMNOPQRSTUVWXY", "03/03/3333", "3");

        Assert.AreEqual(Constants.ErrorType.NoError, response.ErrorType);
    }

    /// <summary>
    /// Test: Check that an attempt to edit with a malformed City gives the IncorrectCityFormat error 
    /// (instead of NoError, as that would indicate the formatting checks didn't work).
    /// </summary>
    [TestMethod]
    public void UpdateAirportTestCityFormatFailure()
    {
        BusinessLogic businessLogic = new();
        Airport testAirport = new(); //Create default Airport object: ID="KATW", City="Appleton", DateVisited=DateTime.Now, Rating=5

        //"KATW should already be in DB, but better to check anyways.
        if (!businessLogic.Airports.Contains(testAirport))
        {
            businessLogic.AddAirport(testAirport.Id, testAirport.City, testAirport.DateVisited.ToString(), testAirport.Rating.ToString());
        }

        ErrorHandling response = businessLogic.EditAirport(testAirport.Id, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", "", "");

        Assert.AreEqual(Constants.ErrorType.IncorrectCityFormat, response.ErrorType);
    }

    /// <summary>
    /// Test: Check that an attempt to edit with a malformed Date gives the IncorrectDateFormat error 
    /// (instead of NoError, as that would indicate the formatting checks didn't work).
    /// </summary>
    [TestMethod]
    public void UpdateAirportTestDateFormatFailure()
    {
        BusinessLogic businessLogic = new();
        Airport testAirport = new(); //Create default Airport object: ID="KATW", City="Appleton", DateVisited=DateTime.Now, Rating=5

        //"KATW should already be in DB, but better to check anyways.
        if (!businessLogic.Airports.Contains(testAirport))
        {
            businessLogic.AddAirport(testAirport.Id, testAirport.City, testAirport.DateVisited.ToString(), testAirport.Rating.ToString());
        }

        ErrorHandling response = businessLogic.EditAirport(testAirport.Id, testAirport.City, "INCORRECT FORMAT", "");

        Assert.AreEqual(Constants.ErrorType.IncorrectDateFormat, response.ErrorType);
    }

    /// <summary>
    /// Test: Check that an airport with a Rating of min integer value (1) can be updated in DB.
    /// </summary>
    [TestMethod]
    public void UpdateAirportTestRatingMin()
    {
        BusinessLogic businessLogic = new();
        Airport testAirport = new(); //Create default Airport object: ID="KATW", City="Appleton", DateVisited=DateTime.Now, Rating=5

        //"KATW should already be in DB, but better to check anyways.
        if (!businessLogic.Airports.Contains(testAirport))
        {
            businessLogic.AddAirport(testAirport.Id, testAirport.City, testAirport.DateVisited.ToString(), testAirport.Rating.ToString());
        }

        ErrorHandling response = businessLogic.EditAirport(testAirport.Id, testAirport.City, testAirport.DateVisited.ToString(), "1");

        Assert.AreEqual(Constants.ErrorType.NoError, response.ErrorType);
    }

    /// <summary>
    /// Test: Check that an airport with a Rating of max integer value (5) can be updated in DB.
    /// </summary>
    [TestMethod]
    public void UpdateAirportTestRatingMax()
    {
        BusinessLogic businessLogic = new();
        Airport testAirport = new(); //Create default Airport object: ID="KATW", City="Appleton", DateVisited=DateTime.Now, Rating=5

        //"KATW should already be in DB, but better to check anyways.
        if (!businessLogic.Airports.Contains(testAirport))
        {
            businessLogic.AddAirport(testAirport.Id, testAirport.City, testAirport.DateVisited.ToString(), testAirport.Rating.ToString());
        }

        ErrorHandling response = businessLogic.EditAirport(testAirport.Id, testAirport.City, testAirport.DateVisited.ToString(), "5");

        Assert.AreEqual(Constants.ErrorType.NoError, response.ErrorType);
    }

    /// <summary>
    /// Test: Check that an attempt to edit with a malformed Rating gives the IncorrectRatingFormat error 
    /// (instead of NoError, as that would indicate the formatting checks didn't work).
    /// </summary>
    [TestMethod]
    public void UpdateAirportTestRatingFormatFailure()
    {
        BusinessLogic businessLogic = new();
        Airport testAirport = new(); //Create default Airport object: ID="KATW", City="Appleton", DateVisited=DateTime.Now, Rating=5

        //"KATW should already be in DB, but better to check anyways.
        if (!businessLogic.Airports.Contains(testAirport))
        {
            businessLogic.AddAirport(testAirport.Id, testAirport.City, testAirport.DateVisited.ToString(), testAirport.Rating.ToString());
        }

        ErrorHandling response = businessLogic.EditAirport(testAirport.Id, testAirport.City, testAirport.DateVisited.ToString(), "10");

        Assert.AreEqual(Constants.ErrorType.IncorrectRatingFormat, response.ErrorType);
    }

}
