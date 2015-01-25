using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RconInvolved.Models;

namespace RconInvolved.DataPersistance
{
    public class Configuration
    {
        private static String CONFIGURATION_FILE_PATH = AppDomain.CurrentDomain.BaseDirectory + "Datas/Conf/";
        private static String CONFIGURATION_FILENAME = "RconInvolved.conf";

        private Dictionary<String, Dictionary<String, String>> myVar;

        public Configuration() {
            Console.WriteLine("Configuration File Initialization");
            Console.WriteLine(CONFIGURATION_FILE_PATH);

            if (!GenerateConfFile())
            {
                Console.WriteLine("Reading configuration File");
                LoadConfigurationFile();
            }
        }

        protected bool GenerateConfFile(){
            //Generates Conf directory
            bool exists = Directory.Exists(CONFIGURATION_FILE_PATH);
            if (!exists) Directory.CreateDirectory(CONFIGURATION_FILE_PATH);

            exists = File.Exists(CONFIGURATION_FILE_PATH + CONFIGURATION_FILENAME);
            if (!exists)
            {
                File.Create(CONFIGURATION_FILE_PATH + CONFIGURATION_FILENAME);
                return true;
            }
            return false;
        }

        protected void LoadConfigurationFile() {
            var xdoc = XDocument.Load(CONFIGURATION_FILE_PATH + CONFIGURATION_FILENAME);
            /*
            _dictionary = xdoc.Descendants("data")
                  .ToDictionary(d => (string)d.Attribute("name"),
                                d => (string)d);
                              
            */


        }


        public void SaveConfiguration()
        {


        }
    }
}
