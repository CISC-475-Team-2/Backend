using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeatingChartAPI.Controllers
{
    public class JSONBuilder
    {
        DBConnector dbcon;

        public JSONBuilder()
        {
            dbcon = new DBConnector();
        }

        public Dictionary<string, Dictionary<string, Dictionary<string, string>>> loadData()
        {
            try
            {
                dbcon.connectToDB();
                Dictionary<string, Dictionary<string, string>> ports = dbcon.getPortInfo();
                Dictionary<string, Dictionary<string, Dictionary<string, string>>> newPorts = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
                foreach (KeyValuePair<string, Dictionary<string, string>> entry in ports)
                {
                    Dictionary<string, Dictionary<string, string>> portData = new Dictionary<string, Dictionary<string, string>>();
                    foreach (KeyValuePair<string, string> entry2 in entry.Value)
                    {
                        string user = dbcon.getConnectedUser(entry2.Value);
                        Dictionary<string, string> userdata;
                        if (user.Equals(""))
                        {
                            userdata = new Dictionary<string, string>();

                        }
                        else
                        {
                            userdata = dbcon.getUserData(user);
                        }
                        portData.Add(entry2.Key, userdata);

                    }
                    newPorts.Add(entry.Key, portData);
                }
                return newPorts;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                throw;
            }
            finally
            {
                try
                {
                    dbcon.disconnectFromDB();
                }
                catch
                {
                    Console.WriteLine("Unable to Disconnect from Database");
                }
            }

        }

        public Dictionary<string, Dictionary<string, Dictionary<string, string>>> handleNoise(Dictionary<string, Dictionary<string, Dictionary<string, string>>> portData)
        {
            portData = removeConferenceRooms(portData);
            portData = removeMultiples(portData);
            return portData;
        }

        private Dictionary<string, Dictionary<string, Dictionary<string, string>>> removeConferenceRooms(Dictionary<string, Dictionary<string, Dictionary<string, string>>> portData)
        {
            return portData;
        }

        private Dictionary<string, Dictionary<string, Dictionary<string, string>>> removeMultiples(Dictionary<string, Dictionary<string, Dictionary<string, string>>> portData)
        {
            return portData;
        }

        public void writeDictionaryToFile(Dictionary<string, Dictionary<string, Dictionary<string, string>>> portData)
        {
            
            string JSONString = "{";
            Boolean first = true;
            foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, string>>> entry in portData)
            {
                if (!first)
                {
                    JSONString = JSONString + ",";
                }
                else
                {
                    first = false;
                }
                JSONString = JSONString + "\"" + entry.Key + "\"" +":{";
                Boolean first2 = true;
                foreach (KeyValuePair<string, Dictionary<string, string>> entry2 in entry.Value)
                {
                    if (!first2)
                    {
                        JSONString = JSONString + ",";
                    }
                    else
                    {
                        first2 = false;
                    }
                    JSONString = JSONString + "\"" + entry2.Key + "\":{";
                    Boolean first3 = true;
                    foreach (KeyValuePair<string, string> entry3 in entry2.Value)
                    {
                        if (!first3)
                        {
                            JSONString = JSONString + ",";
                        }
                        else
                        {
                            first3 = false;
                        }
                        JSONString = JSONString +"\"" + entry3.Key+"\":\""+entry3.Value+"\"";
                    }
                    JSONString = JSONString + "}";
                }
                JSONString = JSONString + "}";
            }
            JSONString = JSONString + "}";
            System.IO.File.WriteAllText(@"C:\Users\Public\App_Data\seatingChartJSON.txt", JSONString);
        }

    }
}