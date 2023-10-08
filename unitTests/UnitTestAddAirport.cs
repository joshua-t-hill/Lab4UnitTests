using Lab4;
using Lab4.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests;
[TestClass]
public class UnitTestAddAirport
{
    /// <summary>
    /// Test: Check that an airport Id of min length (3 chars) can be added to DB.
    /// </summary>
    [TestMethod]
    public void AddAirportTestIdMin()
    {
        BusinessLogic businessLogic = new();
        Airport testAirport = new("ARP", "CityName", DateTime.Now, 5);

        //Check if airport already in DB; if so delete it before running test.
        //This way the ID doesn't have to be changed each time this test is run.
        if (businessLogic.Airports.Contains(testAirport))
        {
            businessLogic.DeleteAirport(testAirport.Id);
        }

        ErrorHandling response = businessLogic.AddAirport(testAirport.Id, testAirport.City, testAirport.DateVisited.ToString(), testAirport.Rating.ToString());

        Assert.AreEqual(Constants.ErrorType.NoError, response.ErrorType);
    }

    /// <summary>
    /// Test: Check that an airport Id of max length (4 chars) can be added to DB.
    /// </summary>
    [TestMethod]
    public void AddAirportTestIdMax()
    {
        BusinessLogic businessLogic = new();
        Airport testAirport = new(); //Create default Airport object: ID="KATW", City="Appleton", DateVisited=DateTime.Now, Rating=5

        //Check if airport already in DB; if so delete it before running test.
        //This way the ID doesn't have to be changed each time this test is run.
        if (businessLogic.Airports.Contains(testAirport))
        {
            businessLogic.DeleteAirport(testAirport.Id);
        }

        ErrorHandling response = businessLogic.AddAirport(testAirport.Id, testAirport.City, testAirport.DateVisited.ToString(), testAirport.Rating.ToString());

        Assert.AreEqual(Constants.ErrorType.NoError, response.ErrorType);
    }


    /// <summary>
    /// Test: Check that an airport cannot be added if its Id already exists in DB.
    /// </summary>
    [TestMethod]
    public void AddAirportTestAirportAlreadyExists()
    {
        BusinessLogic businessLogic = new();

        //Airport with Id="KATW" will exist if test AddAirportTestIdMax() passed.
        ErrorHandling response = businessLogic.AddAirport("KATW", "", "", "");

        Assert.AreEqual(Constants.ErrorType.DuplicateAirport, response.ErrorType);
    }

    /// <summary>
    /// Test: An airport with a malformed Id cannot be added to DB.
    /// </summary>
    [TestMethod]
    public void AddAirportTestIdFormatFailure()
    {
        BusinessLogic businessLogic = new();

        ErrorHandling response = businessLogic.AddAirport("INCORRECT FORMAT", "", "", "");

        Assert.AreEqual(Constants.ErrorType.IncorrectIdFormat, response.ErrorType);
    }

    /// <summary>
    /// Test: An airport with an empty string Id cannot be added to DB.
    /// </summary>
    [TestMethod]
    public void AddAirportTestIdEmptyString()
    {
        BusinessLogic businessLogic = new();

        ErrorHandling response = businessLogic.AddAirport("", "", "", "");

        Assert.AreEqual(Constants.ErrorType.IncorrectIdFormat, response.ErrorType);
    }

    /// <summary>
    /// Test: Check that an airport City of min length (1 char) can be added to DB.
    /// </summary>
    [TestMethod]
    public void AddAirportTestCityMin()
    {
        BusinessLogic businessLogic = new();
        Airport testAirport = new("CMIN", "A", DateTime.Now, 5);

        //Check if airport already in DB; if so delete it before running test.
        //This way the ID doesn't have to be changed each time this test is run.
        if (businessLogic.Airports.Contains(testAirport))
        {
            businessLogic.DeleteAirport(testAirport.Id);
        }

        ErrorHandling response = businessLogic.AddAirport(testAirport.Id, testAirport.City, testAirport.DateVisited.ToString(), testAirport.Rating.ToString());

        Assert.AreEqual(Constants.ErrorType.NoError, response.ErrorType);
    }

    /// <summary>
    /// Test: Check that an airport City of max length (25 char) can be added to DB.
    /// </summary>
    [TestMethod]
    public void AddAirportTestCityMax()
    {
        BusinessLogic businessLogic = new();
        Airport testAirport = new("CMAX", "ABCDEFGHIJKLMNOPQRSTUVWXY", DateTime.Now, 5);

        //Check if airport already in DB; if so delete it before running test.
        //This way the ID doesn't have to be changed each time this test is run.
        if (businessLogic.Airports.Contains(testAirport))
        {
            businessLogic.DeleteAirport(testAirport.Id);
        }

        ErrorHandling response = businessLogic.AddAirport(testAirport.Id, testAirport.City, testAirport.DateVisited.ToString(), testAirport.Rating.ToString());

        Assert.AreEqual(Constants.ErrorType.NoError, response.ErrorType);
    }

    /// <summary>
    /// Test: An airport with a malformed City cannot be added to DB.
    /// </summary>
    [TestMethod]
    public void AddAirportTestCityFormatFailure()
    {
        BusinessLogic businessLogic = new();

        ErrorHandling response = businessLogic.AddAirport("BAD", "ABCDEFGHIJKLMNOPQRSTUVWXYZ", "01/01/2001", "5");

        Assert.AreEqual(Constants.ErrorType.IncorrectCityFormat, response.ErrorType);
    }

    /// <summary>
    /// Test: An airport with a malformed DateVisited cannot be added to DB.
    /// </summary>
    [TestMethod]
    public void AddAirportTestDateFormatFailure()
    {
        BusinessLogic businessLogic = new();

        ErrorHandling response = businessLogic.AddAirport("DATE", "CityName", "INCORRECT FORMAT", "5");

        Assert.AreEqual(Constants.ErrorType.IncorrectDateFormat, response.ErrorType);
    }

    /// <summary>
    /// Test: Check that an airport Rating of min integer value (1) can be added to DB.
    /// </summary>
    [TestMethod]
    public void AddAirportTestRatingMin()
    {
        BusinessLogic businessLogic = new();
        Airport testAirport = new("RMIN", "CityName", DateTime.Now, 1);

        //Check if airport already in DB; if so delete it before running test.
        //This way the ID doesn't have to be changed each time this test is run.
        if (businessLogic.Airports.Contains(testAirport))
        {
            businessLogic.DeleteAirport(testAirport.Id);
        }

        ErrorHandling response = businessLogic.AddAirport(testAirport.Id, testAirport.City, testAirport.DateVisited.ToString(), testAirport.Rating.ToString());

        Assert.AreEqual(Constants.ErrorType.NoError, response.ErrorType);
    }

    /// <summary>
    /// Test: Check that an airport Rating of max integer value (5) can be added to DB.
    /// </summary>
    [TestMethod]
    public void AddAirportTestRatingMax()
    {
        BusinessLogic businessLogic = new();
        Airport testAirport = new("RMAX", "CityName", DateTime.Now, 5);

        //Check if airport already in DB; if so delete it before running test.
        //This way the ID doesn't have to be changed each time this test is run.
        if (businessLogic.Airports.Contains(testAirport))
        {
            businessLogic.DeleteAirport(testAirport.Id);
        }

        ErrorHandling response = businessLogic.AddAirport(testAirport.Id, testAirport.City, testAirport.DateVisited.ToString(), testAirport.Rating.ToString());

        Assert.AreEqual(Constants.ErrorType.NoError, response.ErrorType);
    }

    /// <summary>
    /// Test: An airport with a malformed Rating cannot be added to DB.
    /// </summary>
    [TestMethod]
    public void AddAirportTestRatingFormatFailure()
    {
        BusinessLogic businessLogic = new();

        ErrorHandling response = businessLogic.AddAirport("RATE", "CityName", "01/01/2001", "10");

        Assert.AreEqual(Constants.ErrorType.IncorrectRatingFormat, response.ErrorType);
    }

}