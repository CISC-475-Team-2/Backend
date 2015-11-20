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
        public void DBConnectorTest1()
        {
            DBConnector testConnection = new DBConnector();
            Assert.DoesNotThrow(new TestDelegate(testConnection.connectToDB));
            DBConnector testConnection2 = new DBConnector("notaserver.com","notadatabase","yes","notauid","notthepassword");
            Assert.Throws<Exception>(new TestDelegate(testConnection2.connectToDB));
        }
    }

}