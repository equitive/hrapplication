using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management
{
    class TimeOff
    {
        private string empId;
        private string startDate;
        private string endDate;
        private string approve;
        private string type;



        //constructors
        public TimeOff()
        {
            empId = "";
            startDate = "";
            endDate = "";
            approve = "";
            type = "";

        }//end of default constructor

        public TimeOff(string eid, string sdt, string edt, string app, string typ)
        {
            empId = eid;
            startDate = sdt;
            endDate = edt;
            approve = app;
            type = typ;

        }
        //get and set custID
        public string EmpId
        {
            get { return empId; }
            set { empId = value; }
        }
        //get and set startDate
        public string StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        //get and set endDate
        public string EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        //get and set approve
        public string Approve
        {
            get { return approve; }
            set { approve = value; }
        }
        //get and set type
        public string Type
        {
            get { return type; }
            set { type = value; }
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

            OleDbConnection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=              ;Persist Security Info=False;";


        }//end DBSetup()


        /******************************SELECT METHOD***********************************************/
        public void SelectDB(string id)
        {
            DBSetup();

            cmd = "Select * from TimeOff where EmpId = '" + id + "'";
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
                StartDate = dr.GetValue(1) + "";
                EndDate = dr.GetValue(2) + "";
                Approve = dr.GetValue(3) + "";
                Type = dr.GetValue(4) + "";
                
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
        public void InsertDB(string eid, string sd, string ed, string ap, string ty)
        {
            DBSetup();

            empId = eid;
            startDate = sd;
            endDate = ed;
            approve = ap;
            type = ty;

            cmd = "INSERT into TimeOff values('" + EmpId + "'," +
                                               "'" + StartDate + "'," +
                                               "'" + EndDate + "'," +
                                               "'" + Approve + "'," +
                                               "'" + Type + "')";

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


            cmd = "Update TimeOff set StartDate = '" + StartDate + "'," +
                                      " EndDate = '" + EndDate + "'," +
                                      " Approve = '" + Approve + "'," +
                                      " where EmpID = '" + EmpId + "'";


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

        }//end Update()

        /******************************DELETE METHOD***********************************************/
        public void DeleteDB()
        {

            cmd = "Delete from TimeOff where EmpId ='" + empId + "'";

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
