using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using NUnit.Framework;

namespace SeatingChartAPI.Controllers
{
    [TestFixture]
    public class DBConnectorTest
    {
        [Test]
        public void DBConnectorTest1() // tests sucessful connection to database
        {
            DBConnector testConnection = new DBConnector();
            Assert.DoesNotThrow(new TestDelegate(testConnection.connectToDB));
            Assert.DoesNotThrow(new TestDelegate(testConnection.disconnectFromDB));
        }
        [Test]
        public void DBConnecterTest2() // tests failed connection to database
        {
            DBConnector testConnection2 = new DBConnector("notaserver.com","notadatabase","yes","notauid","notthepassword");
            Assert.Throws<System.Data.SqlClient.SqlException>(new TestDelegate(testConnection2.connectToDB));
        }
        //[Test]
        public void DBConnectorTest3() // tests data pulled from database
        {
            DBConnector testConnection = new DBConnector();
            try
            {
                testConnection.connectToDB();
                Dictionary<string, string> testinfo = new Dictionary<string, string>();
                testinfo = testConnection.getUserData("test"); // TODO: add this data to the DB so it doesn't fail :)
                Assert.AreEqual(testinfo["FirstName"], "test");
                Assert.AreEqual(testinfo["LastName"], "test");
                Assert.AreEqual(testinfo["UserName"], "test12");
                Assert.AreEqual(testinfo["City"], "testopolis");
                Assert.AreEqual(testinfo["Department"], "department of test");
                Assert.AreEqual(testinfo["Email"], "test@test.test");
            }
            catch
            {
                Assert.Fail();
            }
        }
    }

}