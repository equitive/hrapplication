using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management
{
    class PositionInfo
    {
        private string empId;
        private string jobTitle;
        private string jobDescription;
        private double salary;



        //constructors
        public PositionInfo()
        {
            empId = "";
            jobTitle = "";
            jobDescription = "";
            salary = 0;

        }//end of default constructor

        public PositionInfo(string eid, string jt, string jd, int sal)
        {
            empId = eid;
            jobTitle = jt;
            jobDescription = jd;
            salary = sal;

        }
        //get and set custID
        public string EmpId
        {
            get { return empId; }
            set { empId = value; }
        }
        //get and set jobTitle
        public string JobTitle
        {
            get { return jobTitle; }
            set { jobTitle = value; }
        }
        //get and set jobDescription
        public string JobDescription
        {
            get { return jobDescription; }
            set { jobDescription = value; }
        }
        //get and set salary
        public double Salary
        {
            get { return salary; }
            set { salary = value; }

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

            OleDbConnection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=         ;Persist Security Info=False;";


        }//end DBSetup()


        /******************************SELECT METHOD***********************************************/
        public void SelectDB(string id)
        {
            DBSetup();

            cmd = "Select * from PositionInfo where EmpId = '" + id + "'";
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
                JobTitle = dr.GetValue(1) + "";
                JobDescription = dr.GetValue(2) + "";
                Salary = Double.Parse(dr.GetValue(3) + "");
                


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
        public void InsertDB(string eid, string jt, string jd, double sal)
        {
            DBSetup();

            empId = eid;
            JobTitle = jt;
            JobDescription = jd;
            Salary = sal;
            

            cmd = "INSERT into PositionInfo values('" + EmpId + "'," +
                                               "'" + JobTitle + "'," +
                                               "'" + JobDescription + "'," +
                                               "'" + Salary + "')";

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


            cmd = "Update PositionInfo set JobTitle = '" + JobTitle + "'," +
                                      " JobDescription = '" + JobDescription + "'," +
                                      " Salary = '" + Salary + "'," +
                                      " where EmpId = '" + EmpId + "'";


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

        }//end InsertDB()

        /******************************DELETE METHOD***********************************************/
        public void DeleteDB()
        {

            cmd = "Delete from PositionInfo where EmpId ='" + EmpId + "'";

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
