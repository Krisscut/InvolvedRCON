using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RconInvolved.Models;
using RconInvolved.Utils;
using System.Deployment.Application;
using System.Windows.Forms;

namespace RconInvolved.DataPersistance
{
    public static class Configuration
    {
        //Reference to the application
        public static RconInvolvedApp app = null;

        //Deployment vars
        public static String URL_CHANGELOG = "";
        public static ApplicationDeployment applicationDeployment = null;

        //Path vars
        public static String CONFIGURATION_FILE_PATH = AppDomain.CurrentDomain.BaseDirectory + "Data\\Conf\\";
        public static String CONFIGURATION_FILENAME = "RconInvolved.conf";
        public static String ROOT_NAME = "RconInvolved";

        //Conf vars
        public static XDocument xmlConfigFile;
        public static XElement xmlRoot;
        public static Dictionary<String, Dictionary<String, String>> myVar;

        public static void Initialize(RconInvolvedApp appValue)
        {
            app = appValue;
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
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    applicationDeployment = ApplicationDeployment.CurrentDeployment;
                    Version version = applicationDeployment.CurrentVersion;
                    URL_CHANGELOG = String.Format("http://krisscut.legtux.org/applications/RconInvolved/changelog/content/changelog_{0}_{1}_{2}.html", version.Major, version.Minor, version.Build);
                    Logger.MonitoringLogger.Warn("Application deployment data directory : " + ApplicationDeployment.CurrentDeployment.DataDirectory); 
                }
                else
                {
                    Logger.MonitoringLogger.Warn("This application is not network Deployed or run in debug mode"); 
                }
                
            }
            catch (Exception e)
            {
                Logger.ExceptionLogger.Error("Error while getting deployment info : \n" + e.ToString());
            }
        }

        public static bool GenerateConfFile() {
            //Generates Conf directory
            bool exists = Directory.Exists(CONFIGURATION_FILE_PATH);
            if (!exists)
            {
                Logger.MonitoringLogger.Warn("Generates Config File directory");
                Directory.CreateDirectory(CONFIGURATION_FILE_PATH);
            }

            exists = File.Exists(CONFIGURATION_FILE_PATH + CONFIGURATION_FILENAME);
            if (!exists)
            {
                Logger.MonitoringLogger.Warn("Generates Configuration File " + CONFIGURATION_FILE_PATH + CONFIGURATION_FILENAME);
                xmlConfigFile = new XDocument();
                xmlRoot = new XElement(ROOT_NAME);
                xmlConfigFile.Add(xmlRoot);

                FileStream fs = File.Create(CONFIGURATION_FILE_PATH + CONFIGURATION_FILENAME);
                fs.Close();
                SaveConfiguration();
                return true;
            }
            return false;
        }

        public static void LoadConfigurationFile() {
            try
            {
                xmlConfigFile = XDocument.Load(CONFIGURATION_FILE_PATH + CONFIGURATION_FILENAME);
                xmlRoot = xmlConfigFile.Element(ROOT_NAME);
                
                //Throw an exception while loading app --> end process
                if (xmlRoot == null)
                {
                    throw new System.InvalidOperationException("XmlConfiguration File problem: root can't be null");
                }
            }
            catch (Exception e)
            {
                DialogResult result = MessageBox.Show("Error when loading configuration file, Do you want to override it( restart needed) ? \n\n please considers to checks your parameters in " + CONFIGURATION_FILE_PATH + CONFIGURATION_FILENAME , 
                "Critical Error",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    File.Delete(CONFIGURATION_FILE_PATH + CONFIGURATION_FILENAME);
                    //Creates new config File
                    GenerateConfFile();
                    //Stop application
                    System.Environment.Exit(-1);
                }
                else
                {
                    //Throw exception to close the application
                    Logger.ExceptionLogger.Error("Error while loading Configuration file, please delete or checks "+ CONFIGURATION_FILE_PATH + CONFIGURATION_FILENAME +": \n" + e.ToString());
                    throw;
                }
            }
        }

        public static XElement GetRootElementByName(String name)
        {
            return xmlRoot.Element(name);
        }

        public static XElement GetElementByName(String nameRootElement, String nameChildElement)
        {
            XElement rootElement = GetRootElementByName(nameRootElement);
            if (rootElement == null) return null;
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
            XElement rootElement = GetRootElementByName(nameRootElement);

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
                ExceptionHandler.HandleException(e, "Error while saving configuration file");
                throw;
            }
            
        }
    }
}
