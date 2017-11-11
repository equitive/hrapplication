using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management
{
    class Employee
    {//Properties
        private string empId;
        private string password;
        private string fname;
        private string lname;
        private string address;
        private string email;
        private string status;
        private string department;
        private string jobTitle;
        private int vacationDays;
        private int sickTime;

        

        //constructors
        public Employee()
        {
            empId = "";
            password = "";
            fname = "";
            lname = "";
            address = "";
            email = "";
            status = "";
            department = "";
            jobTitle = "";
            vacationDays = 0;
            sickTime = 0;

        }//end of default constructor

        public Employee(string eid, string ps, string fn, string ln, string ad, string em, string stat, string dep, string jt, int vac, int sic)
        {
            empId = eid;
            password = ps;
            fname = fn;
            lname = ln;
            address = ad;
            email = em;
            status = stat;
            department = dep;
            jobTitle = jt;
            vacationDays = vac;
            sickTime = sic;

        }
        //get and set custID
        public string EmpId
        {
            get { return empId; }
            set { empId = value; }
        }
        //get and set password
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        //get and set fname
        public string Fname
        {
            get { return fname; }
            set { fname = value; }
        }
        //get and set lname
        public string Lname
        {
            get { return lname; }
            set { lname = value; }

        }
        //get and set address
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        //get and set email
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public string Department
        {
            get { return department; }
            set { department = value; }
        }
        public string JobTitle
        {
            get { return jobTitle; }
            set { jobTitle = value; }
        }
        public int VacationDays
        {
            get { return vacationDays; }
            set { vacationDays = value; }
        }
        public int SickTime
        {
            get { return sickTime; }
            set { sickTime = value; }
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

            OleDbConnection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=      ;Persist Security Info=False;";


        }//end DBSetup()


        /******************************SELECT METHOD***********************************************/
        public void SelectDB(string id)
        {
            DBSetup();

            cmd = "Select * from Employees where CustID = '" + id + "'";
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
                Password = dr.GetValue(1) + "";
                Fname = dr.GetValue(2) + "";
                Lname = dr.GetValue(3) + "";
                Address = dr.GetValue(4) + "";
                Email = dr.GetValue(5) + "";
                Status = dr.GetValue(6) + "";
                Department = dr.GetValue(7) + "";
                JobTitle = dr.GetValue(8) + "";
                VacationDays = int.Parse(dr.GetValue(9) + "");
                SickTime = int.Parse(dr.GetValue(10) + "");

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
        public void InsertDB(string eid, string ps, string fn, string ln, string ad, string em, string st, string dep, string jt, int vac, int sit)
        {
            DBSetup();

            empId = eid;
            password = ps;
            fname = fn;
            lname = ln;
            address = ad;
            email = em;
            status = st;
            department = dep;
            jobTitle = jt;
            vacationDays = vac;
            sickTime = sit;

            cmd = "INSERT into Employees values('" + EmpId + "'," +
                                               "'" + Password + "'," +
                                               "'" + Fname + "'," +
                                               "'" + Lname + "'," +
                                              "'" + Address + "'," +
                                              "'" + Email + "'," +
                                               "'" + Status + "'," +
                                               "'" + Department + "'," +
                                              "'" + JobTitle + "'," +
                                              "'" + VacationDays + "'," +
                                               "'" + SickTime + "')";

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


            cmd = "Update Employees set Password = '" + Password + "'," +
                                      " FirstName = '" + Fname + "'," +
                                      " LastName = '" + Lname + "'," +
                                      " Address = '" + Address + "'," +
                                      " Email = '" + Email + "'" +
                                      " Status = '" + Status + "'," +
                                      " Department = '" + Department + "'," +
                                      " JobTitle = '" + JobTitle + "'," +
                                      " VacationDays = '" + VacationDays + "'" +
                                      " SickTime = '" + SickTime + "'" +
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

            cmd = "Delete from Employees where EmpId ='" + empId + "'";

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
