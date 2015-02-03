using RconInvolved.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RconInvolved.DataPersistance
{
    public class SQLiteDatabase
    {
        String dbConnection;
        public static String DATABASE_LOCAL_FILE_PATH = "Data\\Databases\\";
        public static String DATABASE_FILE_PATH = AppDomain.CurrentDomain.BaseDirectory + DATABASE_LOCAL_FILE_PATH;  
        public static String SQLITE_DATABASE_FILENAME = "sqliteDatabase.sqlite";

        /// <summary>
        ///     Default Constructor for SQLiteDatabase Class.
        /// </summary>
        public SQLiteDatabase()
        {
            Logger.MonitoringLogger.Info("Instanciating Sqlite Database");
            dbConnection = "Data Source=" + DATABASE_FILE_PATH + SQLITE_DATABASE_FILENAME;
        }

        /// <summary>
        ///     Single Param Constructor for specifying the DB file.
        /// </summary>
        /// <param name="inputFile">The File containing the DB</param>
        public SQLiteDatabase(String inputFile)
        {
            Logger.MonitoringLogger.Info("Instanciating new Sqlite Database : " + inputFile);
            dbConnection = String.Format("Data Source={0}", inputFile);
        }
        
        public static void Initialize()
        {
            Logger.MonitoringLogger.Info("Initializing SQLite database");
            try
            {
                CreateSqliteFile();
                String dbToBuild = String.Format("Data Source={0}", DATABASE_FILE_PATH + SQLITE_DATABASE_FILENAME);
                String query = "CREATE TABLE IF NOT EXISTS profileList (rowID INTEGER PRIMARY KEY AUTOINCREMENT, profilName VARCHAR(200), hostname VARCHAR(200), port VARCHAR(20), password VARCHAR(200), autoReconnect VARCHAR(50))";
                CreateTableIfNotExists(dbToBuild, query);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, "Error while creating sqlite file");
                throw;
            }
        }

        private static void CreateSqliteFile()
        {
            bool exists = Directory.Exists(DATABASE_FILE_PATH);
            if (!exists)
            {
                Logger.MonitoringLogger.Warn("Create Database directory into : " + DATABASE_FILE_PATH);
                Directory.CreateDirectory(DATABASE_FILE_PATH);
            }

            exists = File.Exists(DATABASE_FILE_PATH + SQLITE_DATABASE_FILENAME);
            if (!exists)
            {
                Logger.MonitoringLogger.Warn("Create new SQL file : " + DATABASE_FILE_PATH + SQLITE_DATABASE_FILENAME);
                SQLiteConnection.CreateFile(DATABASE_FILE_PATH + SQLITE_DATABASE_FILENAME);
            }
        }

        public static void CreateTableIfNotExists(String dbInfo, String sqlQuery) {
            using (SQLiteConnection con = new SQLiteConnection(dbInfo))
            using (SQLiteCommand command = con.CreateCommand())
            {
                try
                {
                    con.Open();
                    command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='table_name';";
                    var name = command.ExecuteScalar();

                    // check account table exist or not 
                    // if exist do nothing 
                    if (name != null)
                        return;
                    // profileList does'nt exist
                    Logger.MonitoringLogger.Warn("Generate new table profileList");
                    command.CommandText = sqlQuery;
                    command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception e)
                {
                    Logger.ExceptionLogger.Error("Error in createTable if not Exists !");
                }
            }
        }



	    /// <summary>
	    ///     Single Param Constructor for specifying advanced connection options.
	    /// </summary>
	    /// <param name="connectionOpts">A dictionary containing all desired options and their values</param>
	    public SQLiteDatabase(Dictionary<String, String> connectionOpts)
	    {
		    String str = "";
		    foreach (KeyValuePair<String, String> row in connectionOpts)
		    {
			    str += String.Format("{0}={1}; ", row.Key, row.Value);
		    }
		    str = str.Trim().Substring(0, str.Length - 1);
		    dbConnection = str;
	    }

	    /// <summary>
	    ///     Allows the programmer to run a query against the Database.
	    /// </summary>
	    /// <param name="sql">The SQL to run</param>
	    /// <returns>A DataTable containing the result set.</returns>
	    public DataTable GetDataTable(string sql)
	    {
		    DataTable dt = new DataTable();
		    try
		    {
			    SQLiteConnection cnn = new SQLiteConnection(dbConnection);
			    cnn.Open();
			    SQLiteCommand mycommand = new SQLiteCommand(cnn);
			    mycommand.CommandText = sql;
			    SQLiteDataReader reader = mycommand.ExecuteReader();
			    dt.Load(reader);
			    reader.Close();
			    cnn.Close();
		    }
		    catch (Exception e)
		    {
			    throw new Exception(e.Message);
		    }
		    return dt;
	    }
	
	    /// <summary>
	    ///     Allows the programmer to interact with the database for purposes other than a query.
	    /// </summary>
	    /// <param name="sql">The SQL to be run.</param>
	    /// <returns>An Integer containing the number of rows updated.</returns>
	    public int ExecuteNonQuery(string sql)
	    {
		    SQLiteConnection cnn = new SQLiteConnection(dbConnection);
		    cnn.Open();
		    SQLiteCommand mycommand = new SQLiteCommand(cnn);
		    mycommand.CommandText = sql;
		    int rowsUpdated = mycommand.ExecuteNonQuery();
		    cnn.Close();
		    return rowsUpdated;
	    }

	    /// <summary>
	    ///     Allows the programmer to retrieve single items from the DB.
	    /// </summary>
	    /// <param name="sql">The query to run.</param>
	    /// <returns>A string.</returns>
	    public string ExecuteScalar(string sql)
	    {
		    SQLiteConnection cnn = new SQLiteConnection(dbConnection);
		    cnn.Open();
		    SQLiteCommand mycommand = new SQLiteCommand(cnn);
		    mycommand.CommandText = sql;
		    object value = mycommand.ExecuteScalar();
		    cnn.Close();
		    if (value != null)
		    {
			    return value.ToString();
		    }
		    return "";
	    }

	    /// <summary>
	    ///     Allows the programmer to easily update rows in the DB.
	    /// </summary>
	    /// <param name="tableName">The table to update.</param>
	    /// <param name="data">A dictionary containing Column names and their new values.</param>
	    /// <param name="where">The where clause for the update statement.</param>
	    /// <returns>A boolean true or false to signify success or failure.</returns>
	    public bool Update(String tableName, Dictionary<String, String> data, String where)
	    {
		    String vals = "";
		    Boolean returnCode = true;
		    if (data.Count >= 1)
		    {
			    foreach (KeyValuePair<String, String> val in data)
			    {
				    vals += String.Format(" {0} = '{1}',", val.Key.ToString(), val.Value.ToString());
			    }
			    vals = vals.Substring(0, vals.Length - 1);
		    }
		    try
		    {
			    this.ExecuteNonQuery(String.Format("update {0} set {1} where {2};", tableName, vals, where));
		    }
		    catch(Exception fail)
		    {
                Logger.ExceptionLogger.Error("Attempt to update database error :\n" + fail.ToString());
			    returnCode = false;
		    }
		    return returnCode;
	    }

	    /// <summary>
	    ///     Allows the programmer to easily delete rows from the DB.
	    /// </summary>
	    /// <param name="tableName">The table from which to delete.</param>
	    /// <param name="where">The where clause for the delete.</param>
	    /// <returns>A boolean true or false to signify success or failure.</returns>
	    public bool Delete(String tableName, String where)
	    {
		    Boolean returnCode = true;
            String query = String.Format("delete from {0} where {1};", tableName, where);
		    try
		    {
			    this.ExecuteNonQuery(query);
		    }
		    catch (Exception fail)
		    {
			    MessageBox.Show(fail.Message);
                Logger.ExceptionLogger.Error(String.Format("Attempt to delete from database error with this parameters : {0} \n {1};", query, fail.ToString()));
			    returnCode = false;
		    }
		    return returnCode;
	    }

	    /// <summary>
	    ///     Allows the programmer to easily insert into the DB
	    /// </summary>
	    /// <param name="tableName">The table into which we insert the data.</param>
	    /// <param name="data">A dictionary containing the column names and data for the insert.</param>
	    /// <returns>A boolean true or false to signify success or failure.</returns>
	    public bool Insert(String tableName, Dictionary<String, String> data)
	    {
		    String columns = "";
		    String values = "";
		    Boolean returnCode = true;
		    foreach (KeyValuePair<String, String> val in data)
		    {
			    columns += String.Format(" {0},", val.Key.ToString());
			    values += String.Format(" '{0}',", val.Value);
		    }
		    columns = columns.Substring(0, columns.Length - 1);
		    values = values.Substring(0, values.Length - 1);
		    try
		    {
			    this.ExecuteNonQuery(String.Format("insert into {0}({1}) values({2});", tableName, columns, values));
		    }
		    catch(Exception fail)
		    {
			    MessageBox.Show(fail.Message);
                Logger.ExceptionLogger.Error("Attempt to insert into database error :\n" + fail.ToString());
			    returnCode = false;
		    }
		    return returnCode;
	    }

	    /// <summary>
	    ///     Allows the programmer to easily delete all data from the DB.
	    /// </summary>
	    /// <returns>A boolean true or false to signify success or failure.</returns>
	    public bool ClearDB()
	    {
		    DataTable tables;
		    try
		    {
			    tables = this.GetDataTable("select NAME from SQLITE_MASTER where type='table' order by NAME;");
			    foreach (DataRow table in tables.Rows)
			    {
				    this.ClearTable(table["NAME"].ToString());
			    }
			    return true;
		    }
		    catch(Exception e)
		    {
                Logger.ExceptionLogger.Error("Attempt to ClearDB error :\n" + e.ToString());
			    return false;
		    }
	    }

	    /// <summary>
	    ///     Allows the user to easily clear all data from a specific table.
	    /// </summary>
	    /// <param name="table">The name of the table to clear.</param>
	    /// <returns>A boolean true or false to signify success or failure.</returns>
	    public bool ClearTable(String table)
	    {
		    try
		    {
			
			    this.ExecuteNonQuery(String.Format("delete from {0};", table));
			    return true;
		    }
		    catch(Exception e)
		    {
                Logger.ExceptionLogger.Error("Attempt to ClearTable error :\n" + e.ToString());
			    return false;
		    }
	    }       

    }
}
