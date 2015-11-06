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

        public Dictionary<string, Dictionary<string, string>> loadData()
        {
            dbcon.connectToDB();
            Dictionary<string, Dictionary<string, string>> ports = dbcon.getPortInfo();
            Dictionary<string, Dictionary<string, string>> newPorts = new Dictionary<string, Dictionary<string, string>>();
            foreach (KeyValuePair<string, Dictionary<string, string>> entry in ports)
            {

                string user = dbcon.getConnectedUser(entry.Value["physicalAddress"]);
                Dictionary<string, string> userdata = dbcon.getUserData(user);
                foreach (KeyValuePair<string, string> data in entry.Value)
                {
                    userdata.Add(data.Key, data.Value);
                }
                newPorts.Add(entry.Key, userdata);
            }
            return newPorts;

        }

        public Dictionary<string, Dictionary<string, string>> handleNoise(Dictionary<string, Dictionary<string, string>> portData)
        {
            portData = removeConferenceRooms(portData);
            portData = removeMultiples(portData);
            return portData;
        }

        private Dictionary<string, Dictionary<string, string>> removeConferenceRooms(Dictionary<string, Dictionary<string, string>> portData)
        {
            return portData;
        }

        private Dictionary<string, Dictionary<string, string>> removeMultiples(Dictionary<string, Dictionary<string, string>> portData)
        {
            return portData;
        }

    }
}