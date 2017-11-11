using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management
{
    class Complaints
    {
        private string empId;
        private string date;
        private string description;



        //constructors
        public Complaints()
        {
            empId = "";
            date = "";
            description = "";

        }//end of default constructor

        public Complaints(string eid, string dt, string desc)
        {
            empId = eid;
            date = dt;
            description = desc;

        }
        //get and set empID
        public string EmpId
        {
            get { return empId; }
            set { empId = value; }
        }
        //get and set empID
        public string Date
        {
            get { return date; }
            set { date = value; }
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

            OleDbConnection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source= ????????????????    ;Persist Security Info=False;";


        }//end DBSetup()


        /******************************SELECT METHOD***********************************************/
        public void SelectDB(string eid)
        {
            DBSetup();

            cmd = "Select * from Complaints where EmpID = '" + eid + "'";
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
                EmpId = eid;
                Date = dr.GetValue(1) + "";
                Description = dr.GetValue(2) + "";
               

            }

            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);
            }
            finally
            {
                OleDbConnection.Close();
            }
        }//end SelectDB()

       


        /******************************Insert METHOD***********************************************/
        public void InsertDB(string eid, string dt, string desc)
        {
            DBSetup();

            empId = eid;
            date = dt;
            description = desc;
           
            cmd = "INSERT into Complaints values('" + EmpId + "'," +
                                               "'" + Date + "'," +
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

        

        /******************************DELETE METHOD***********************************************/
        public void DeleteDB()
        {

            cmd = "Delete from Complaints where CustID ='" + empId + "'";

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
