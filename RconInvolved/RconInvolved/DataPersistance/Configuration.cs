using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RconInvolved.Models;
using RconInvolved.Utils;
using System.Windows;
using System.Deployment.Application;

namespace RconInvolved.DataPersistance
{
    public static class Configuration
    {
        public static String URL_CHANGELOG = "";
        public static ApplicationDeployment applicationDeployment = null;
        public static String CONFIGURATION_FILE_PATH = AppDomain.CurrentDomain.BaseDirectory + "Datas/Conf/";
        public static String CONFIGURATION_FILENAME = "RconInvolved.conf";
        public static XDocument xmlConfigFile;
        public static XElement xmlRoot;

        public static String ROOT_NAME = "RconInvolved";
        public static Dictionary<String, Dictionary<String, String>> myVar;

        public static void Initialize () {
            Logger.MonitoringLogger.Info("Configuration File Initialization");
            Logger.MonitoringLogger.Info("Configuration FIle path : " + CONFIGURATION_FILE_PATH);
            
            if (!GenerateConfFile())    //If there is already a config File
            {
                Logger.MonitoringLogger.Info("Reading configuration File");
                LoadConfigurationFile();
            }

            GetApplicationDeployment();
        }

        private static void GetApplicationDeployment()
        {
            try
            {
                applicationDeployment = ApplicationDeployment.CurrentDeployment;
                Version version = applicationDeployment.CurrentVersion;
                URL_CHANGELOG = String.Format("http://krisscut.legtux.org/applications/RconInvolved/changelog/content/changelog_{0}_{1}_{2}.html", version.Major, version.Minor, version.Revision );
            }
            catch (Exception e)
            {
                Logger.ExceptionLogger.Error("Error while getting deployment info : \n" + e.ToString());
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
                xmlRoot = new XElement(ROOT_NAME);
                xmlConfigFile.Add(xmlRoot);
                return true;
            }
            return false;
        }

        public static void LoadConfigurationFile() {
            try
            {
                xmlConfigFile = XDocument.Load(CONFIGURATION_FILE_PATH + CONFIGURATION_FILENAME);
                xmlRoot = xmlConfigFile.Element(ROOT_NAME);
                
                //Throw an exception 
                if (xmlRoot == null)
                {
                    throw new System.InvalidOperationException("XmlConfiguration File problem: root can't be null");
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, "Error when loading configure file, please considers to delete it or checks your parameters in " + CONFIGURATION_FILE_PATH + CONFIGURATION_FILENAME);
                throw;
            }
        }

        public static XElement GetRootElementByName(String name)
        {
            return xmlRoot.Element(name);
        }

        public static XElement GetElementByName(String nameRootElement, String nameChildElement)
        {
            XElement rootElement = xmlRoot.Element(nameRootElement);
            return rootElement.Element(nameChildElement);
        }

        public static void ReplaceRootElement(XElement element)
        {
            XElement oldElement = xmlRoot.Element(element.Name);
            if (oldElement != null)
            {
                oldElement.ReplaceWith(element);
            }
            else
            {
                xmlRoot.Add(element);
            }
            //Update root element to old it in case of crash
            SaveConfiguration();
        }

        public static void ReplaceChildRootElement(string nameRootElement, XElement element)
        {
            //Gets root element
            XElement rootElement = xmlRoot.Element(nameRootElement);

            //creates new root element if necessary
            if (rootElement == null)
            {
                rootElement = new XElement(nameRootElement);
                xmlRoot.Add(rootElement);
            }

            //Search for the old value
            XElement oldElement = rootElement.Element(element.Name);
            if (oldElement != null)
            {
                oldElement.ReplaceWith(element);
            }
            else
            {
                rootElement.Add(element);
            }

            //Replace old element with the new one
            xmlRoot.Element(nameRootElement).ReplaceWith(rootElement);

            //Update root element to old it in case of crash
            SaveConfiguration();
        }

        public static void SaveConfiguration()
        {
            try
            {
                xmlConfigFile.Element(ROOT_NAME).ReplaceWith(xmlRoot);
                xmlConfigFile.Save(CONFIGURATION_FILE_PATH + CONFIGURATION_FILENAME);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, "Error while saving configure file");
            }
            
        }
    }
}
