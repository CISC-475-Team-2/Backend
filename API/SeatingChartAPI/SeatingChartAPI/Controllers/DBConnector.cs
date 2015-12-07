using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace SeatingChartAPI.Controllers
{
    public class DBConnector
    {
        private string cString = "Data Source=localhost;Initial Catalog = WhereAt; User ID = seatingchart; Password = Password1";
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
                throw(ex);
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
            SqlCommand myCommand = new SqlCommand("SELECT * FROM agent_activedirectory WHERE Username ='" + userName+"'", sqlcon);
            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                userSet.Add("firstName", myReader["FirstName"].ToString() );
                userSet.Add("lastName", myReader["LastName"].ToString());
                userSet.Add("userName", myReader["Username"].ToString());
                userSet.Add("city", myReader["City"].ToString());
                userSet.Add("department", myReader["Department"].ToString());
                userSet.Add("email", myReader["Email"].ToString());
            }
            myReader.Close();
            return userSet;
        }

        public string getConnectedUser(string physicalAddr)
        {
            Dictionary<string, string> portSet = new Dictionary<string, string>();
            SqlCommand myCommand = new SqlCommand("SELECT * FROM Agent_InternalWebsites WHERE DevicePhysicalAddress ='" + physicalAddr+"'", sqlcon);
            SqlDataReader myReader = myCommand.ExecuteReader();
            string returnString= "";
            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    returnString = myReader["Username"].ToString();
                }
            }
            myReader.Close();
            return returnString;
        }

        public Dictionary<string, Dictionary<string, string>> getPortInfo()
        {
            Dictionary<string, Dictionary<string, string>> switches = new Dictionary<string, Dictionary<string, string>>();
            SqlCommand myCommand = new SqlCommand("SELECT * FROM agent_networkswitch", sqlcon);
            SqlDataReader  myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                Dictionary<string, string> switchSet;
                if (switches.ContainsKey(myReader["SwitchName"].ToString()))
                {
                    switchSet = switches[myReader["SwitchName"].ToString()];
                }
                else
                {
                    switchSet = new Dictionary<string, string>();
                }
                if (!switchSet.ContainsKey(myReader["SwitchPortName"].ToString()))
                {
                    switchSet.Add(myReader["SwitchPortName"].ToString(), myReader["DevicePhysicalAddress"].ToString());
                }

                if (switches.ContainsKey(myReader["SwitchName"].ToString()))
                {
                    switches[myReader["SwitchName"].ToString()] = switchSet;
                }
                else
                {
                    switches.Add(myReader["SwitchName"].ToString(), switchSet);
                }

            }
            myReader.Close();
            return switches;
        }
    }
}