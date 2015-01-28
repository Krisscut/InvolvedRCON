using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RconInvolved.Models;
using RconInvolved.Utils;

namespace RconInvolved.DataPersistance
{
    public static class Configuration
    {
        public static String CONFIGURATION_FILE_PATH = AppDomain.CurrentDomain.BaseDirectory + "Datas/Conf/";
        public static String CONFIGURATION_FILENAME = "RconInvolved.conf";
        public static XDocument xmlConfigFile;

        public static Dictionary<String, Dictionary<String, String>> myVar;

        public static void Initialize () {
            Logger.MonitoringLogger.Info("Configuration File Initialization");
            Logger.MonitoringLogger.Info("Configuration FIle path : " + CONFIGURATION_FILE_PATH);
            
            if (!GenerateConfFile())
            {
                Console.WriteLine("Reading configuration File");
                LoadConfigurationFile();
            }
        }

        public static bool GenerateConfFile() {
            //Generates Conf directory
            bool exists = Directory.Exists(CONFIGURATION_FILE_PATH);
            if (!exists) Directory.CreateDirectory(CONFIGURATION_FILE_PATH);

            exists = File.Exists(CONFIGURATION_FILE_PATH + CONFIGURATION_FILENAME);
            if (!exists)
            {
                File.Create(CONFIGURATION_FILE_PATH + CONFIGURATION_FILENAME);
                
                //Create basic tree structure
                xmlConfigFile = new XDocument();
                return true;
            }
            return false;
        }

        public static void LoadConfigurationFile() {
            xmlConfigFile = XDocument.Load(CONFIGURATION_FILE_PATH + CONFIGURATION_FILENAME);
        }

        public static XElement GetRootElementByName(String name)
        {
            return xmlConfigFile.Element(name);
        }

        public static void ReplaceRootElement(XElement element)
        {
            XElement oldElement = xmlConfigFile.Element(element.Name);
            if (oldElement != null)
            {
                oldElement.ReplaceWith(element);
            }
            else
            {
                xmlConfigFile.Add(element);
            }
            //Update root element to old it in case of crash
            SaveConfiguration();
        }

        public static void SaveConfiguration()
        {
            xmlConfigFile.Save(CONFIGURATION_FILE_PATH + CONFIGURATION_FILENAME);
        }
    }
}
