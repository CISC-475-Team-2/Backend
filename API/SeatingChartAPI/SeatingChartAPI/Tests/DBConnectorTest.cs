using System.Collections.Generic;
using NUnit.Framework;
using System;

namespace SeatingChartAPI.Controllers
{
    [TestFixture]
    public class DBConnectorTest
    {
        [Test]
        public void DBConnectorTestConnection() // tests sucessful connection to database
        {
            DBConnector testConnection = new DBConnector();
            Assert.DoesNotThrow(new TestDelegate(testConnection.connectToDB));
            Assert.DoesNotThrow(new TestDelegate(testConnection.disconnectFromDB));
        }
        [Test]
        public void DBConnecterTestErrorHanding() // tests failed connection to database
        {
            DBConnector testConnection2 = new DBConnector("notaserver.com","notadatabase","yes","notauid","notthepassword");
            Assert.Throws<System.Data.SqlClient.SqlException>(new TestDelegate(testConnection2.connectToDB));
        }
        [Test]
        public void DBConnectorTestGetUsername() // tests data pulled from database
        {
            DBConnector testConnection = new DBConnector();
            try
            {
                testConnection.connectToDB();
                Assert.AreEqual("jbuena", testConnection.getConnectedUser("349ADEAA08EA"));
                testConnection.disconnectFromDB();
            }
            catch
            {
                Assert.Fail();
            }
        }
        [Test]
        public void DBConnectorTestGetData() // tests data pulled from database
        {
            DBConnector testConnection = new DBConnector();
            try
            {
                testConnection.connectToDB();
                Dictionary<string, string> testinfo = new Dictionary<string, string>();
                testinfo = testConnection.getUserData("jbuena");
                Assert.AreEqual(testinfo["firstName"], "Jamar");
                Assert.AreEqual(testinfo["lastName"], "Buena");
                Assert.AreEqual(testinfo["userName"], "jbuena");
                Assert.AreEqual(testinfo["city"], "Conshohocken");
                Assert.AreEqual(testinfo["department"], "Creative");
                Assert.AreEqual(testinfo["email"], "jbuena@emoneyadvisor.com");
                testConnection.disconnectFromDB();
            }
            catch
            {
                Assert.Fail();
            }
        }
    }

}