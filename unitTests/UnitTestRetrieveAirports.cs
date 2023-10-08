using Lab4;
using Lab4.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests;
/// <summary>
/// Class to hold single test to check Retrieval of all airports in DB.
/// </summary>
[TestClass]
public class UnitTestRetrieveAirports
{
    /// <summary>
    /// Test: Check that all airports currently in DB can be retrieved.
    /// When an instance of BusinessLogic is created, a Database class is created with it automatically.
    /// The Airports property in BusinessLogic can then pull the list from the Database class instance.
    /// </summary>
    [TestMethod]
    public void RetrieveAllAirportsTest()
    {
        Boolean listNotEmptyOrNull = false;
        BusinessLogic businessLogic = new();

        if(businessLogic.Airports is not null && businessLogic.Airports.Count > 0)
        {
            listNotEmptyOrNull = true;

            //Console printing visible when selecting this test in test console.
            foreach (var airport in businessLogic.Airports)
            {
                Console.Write(airport.Id + ", ");
            }
        }

        Assert.AreEqual (listNotEmptyOrNull, true);
    }

}

