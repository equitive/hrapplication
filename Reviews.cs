using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management
{
    class Reviews
    {
        private string empId;
        private string date;
        private string title;
        private string manager;
        private string description;



        //constructors
        public Reviews()
        {
            empId = "";
            date = "";
            title = "";
            manager = "";
            description = "";

        }//end of default constructor

        public Reviews(string eid, string dt, string tit, string man, string desc)
        {
            empId = eid;
            date = dt;
            title = tit;
            manager = man;
            description = desc;

        }
        //get and set empID
        public string EmpId
        {
            get { return empId; }
            set { empId = value; }
        }
        //get and set Date
        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        //get and set Title
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        //get and set Manager
        public string Manager
        {
            get { return manager; }
            set { manager = value; }
        }
        //get and set description
        public string Description
        {
            get { return description; }
            set { description = value; }

        }
        /*************************************************Data Base Elements*****************************************************/

        public System.Data.OleDb.OleDbDataAdapter OleDbDataAdapter;
        public System.Data.OleDb.OleDbCommand OleDbSelectCommand;
        public System.Data.OleDb.OleDbCommand OleDbInsertCommand;
        public System.Data.OleDb.OleDbCommand OleDbUpdateCommand;
        public System.Data.OleDb.OleDbCommand OleDbDeleteCommand;
        public System.Data.OleDb.OleDbConnection OleDbConnection;
        public string cmd;

        public void DBSetup()
        {
            /******************************************************DB SETUP FUNCTION*************************************************/
            /*This DBSetup() method instantiates all the objects needed to access a Database includig OleDbDataAdapter, which contains
             * 4 other objects(OlsDbSelectCommand, OlsDbInsertCommand, OleDbUpdateCommand, OleDbDeleteCommand.) And each command object 
             * contains a connection object and an SQL string object*****************************************************************/


            OleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter();
            OleDbSelectCommand = new System.Data.OleDb.OleDbCommand();
            OleDbInsertCommand = new System.Data.OleDb.OleDbCommand();
            OleDbUpdateCommand = new System.Data.OleDb.OleDbCommand();
            OleDbDeleteCommand = new System.Data.OleDb.OleDbCommand();
            OleDbConnection = new System.Data.OleDb.OleDbConnection();


            OleDbDataAdapter.DeleteCommand = OleDbDeleteCommand;
            OleDbDataAdapter.InsertCommand = OleDbInsertCommand;
            OleDbDataAdapter.SelectCommand = OleDbSelectCommand;
            OleDbDataAdapter.UpdateCommand = OleDbUpdateCommand;

            //The text below includes the database information and where it is located

            OleDbConnection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=            ;Persist Security Info=False;";


        }//end DBSetup()


        /******************************SELECT METHOD***********************************************/
        public void SelectDB(string id)
        {
            DBSetup();

            cmd = "Select * from Reviews where EmpId = '" + id + "'";
            OleDbDataAdapter.SelectCommand.CommandText = cmd;
            OleDbDataAdapter.SelectCommand.Connection = OleDbConnection;
            Console.WriteLine(cmd);

            try
            {
                Console.WriteLine("Before Open");
                OleDbConnection.Open();
                Console.WriteLine("After Open");
                System.Data.OleDb.OleDbDataReader dr;
                dr = OleDbDataAdapter.SelectCommand.ExecuteReader();
                Console.WriteLine("After Execute");
                dr.Read();
                EmpId = id;
                Date = dr.GetValue(1) + "";
                Title = dr.GetValue(2) + "";
                Manager = dr.GetValue(3) + "";
                Description = dr.GetValue(4) + "";
                
            }

            catch (Exception ex)
            {
                Console.WriteLine("hello" + ex);
            }
            finally
            {
                OleDbConnection.Close();
            }
        }//end SelectDB()

       


        /******************************Insert METHOD***********************************************/
        public void InsertDB(string eid, string dt, string tt, string man, string desc)
        {
            DBSetup();

            empId = eid;
            date = dt;
            title = tt;
            manager = man;
            description = desc;

            cmd = "INSERT into Reviews values('" + EmpId + "'," +
                                               "'" + Date + "'," +
                                               "'" + Title + "'," +
                                               "'" + Manager + "'," +
                                               "'" + Description + "')";

            OleDbDataAdapter.InsertCommand.CommandText = cmd;
            OleDbDataAdapter.InsertCommand.Connection = OleDbConnection;
            Console.WriteLine(cmd);

            try
            {
                OleDbConnection.Open();
                int n = OleDbDataAdapter.InsertCommand.ExecuteNonQuery();

                if (n == 1)
                    Console.WriteLine("Data Inserted");
                else
                    Console.WriteLine("Error: Inserting Data");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                OleDbConnection.Close();
            }

        }//end InsertDB()

        /******************************UPDATE METHOD***********************************************/
        public void UpdateDB()
        {


            cmd = "Update Reviews set Date = '" + Date + "'," +
                                      " TItle = '" + Title + "'," +
                                      " Manager = '" + Manager + "'," +
                                      " Description = '" + Description + "'," +
                                      " where Empid = '" + EmpId + "'";


            OleDbDataAdapter.UpdateCommand.CommandText = cmd;
            OleDbDataAdapter.UpdateCommand.Connection = OleDbConnection;
            Console.WriteLine(cmd);

            try
            {
                OleDbConnection.Open();
                Console.WriteLine("conection open");
                int n = OleDbDataAdapter.UpdateCommand.ExecuteNonQuery();

                if (n == 1)
                    Console.WriteLine("Data Updated");
                else
                    Console.WriteLine("Error: Updating Data");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                OleDbConnection.Close();
            }

        }//end UpdateDB()

        /******************************DELETE METHOD***********************************************/
        public void DeleteDB()
        {

            cmd = "Delete from Reviews where EmpId ='" + empId + "'";

            OleDbDataAdapter.DeleteCommand.CommandText = cmd;
            OleDbDataAdapter.DeleteCommand.Connection = OleDbConnection;
            Console.WriteLine(cmd);

            try
            {
                OleDbConnection.Open();
                int n = OleDbDataAdapter.DeleteCommand.ExecuteNonQuery();

                if (n == 1)
                    Console.WriteLine("Data Deleted");
                else
                    Console.WriteLine("Error: Delete Data");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                OleDbConnection.Close();
            }

        }//end DeleteDB()
    }
}
