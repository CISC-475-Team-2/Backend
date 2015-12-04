using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace SeatingChartAPI.Controllers
{
    public class DBConnector
    {
        static string cString = "server=us-cdbr-azure-central-a.cloudapp.net;"+
                                "database=seatingChart;"+
                                "Trusted_Connection = yes"+
                                "uid=bd4630ac6f11d6;"+
                                "pwd=4089a061;";
        static SqlConnection sqlcon;

        public DBConnector()
        {
            sqlcon = new SqlConnection(cString);
        }

        public DBConnector(string server, string database, string trusted_connection, string uid, string pwd){
            sqlcon = new SqlConnection("server=" + server + ";" +
                                       "database=" + database + ";" +
                                       "Trusted_Connection=" + trusted_connection + ";" +
                                       "uid=" + uid + ";" +
                                       "pwd=" + pwd + ";");
        }

        public void connectToDB()
        {
            try
            {
                sqlcon.Open();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void disconnectFromDB()
        {
            try
            {
                sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public Dictionary<string, string> getUserData(string userName)
        {
            Dictionary<string, string> userSet = new Dictionary<string, string>();
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("SELECT * FROM agent_activedirectory WHERE userName =" + userName, sqlcon);
            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                userSet.Add("firstName", myReader["FirstName"].ToString() );
                userSet.Add("lastName", myReader["LastName"].ToString());
                userSet.Add("userName", myReader["Username"].ToString());
                userSet.Add("city", myReader["City"].ToString());
                userSet.Add("department", myReader["Department"].ToString());
                userSet.Add("email", myReader["Email"].ToString());
            }
            return userSet;
        }

        public string getConnectedUser(string physicalAddr)
        {
            Dictionary<string, string> portSet = new Dictionary<string, string>();
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("SELECT * FROM agent_internalwebsites WHERE DevicePhysicalAddress =" + physicalAddr, sqlcon);
            myReader = myCommand.ExecuteReader();
            return myReader["Username"].ToString();
        }

        public Dictionary<string, Dictionary<string, string>> getPortInfo()
        {
            Dictionary<string, Dictionary<string, string>> switches = new Dictionary<string, Dictionary<string, string>>();
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("SELECT * FROM agent_networkswitch", sqlcon);
            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                Dictionary<string, string> switchSet = new Dictionary<string, string>();
                switchSet.Add("port", myReader["SwitchPortName"].ToString());
                switchSet.Add("physicalAddress", myReader["DevicePhysicalAddress"].ToString());
                switches.Add(myReader["SwitchName"].ToString(), switchSet);

            }
            return switches;
        }
    }
}