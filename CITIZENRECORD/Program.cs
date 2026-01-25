using Com.Wipro.Citizen.Bean;
using Com.Wipro.Citizen.Dao;
using Com.Wipro.Citizen.Service;
using Com.Wipro.Citizen.Util;
using Microsoft.Data.SqlClient;
using System;

//1 table Citizen_TBL
namespace Com.Wipro.Citizen.Util
{
    public class DBUtil
    {
        public static string constr = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=CITIZENDB;Integrated Security=True;TrustServerCertificate=True";
        public static SqlConnection GetDBConnection()
        {
            SqlConnection con = new SqlConnection(constr);
            return con;
        }
    }


    //2 custom exception
    public class InvalidCitizenException : Exception
    {
        public override string ToString()
        {
            return "INVALID CITIZEN DETAILS";
        }
    }
}


//3 bean class
namespace Com.Wipro.Citizen.Bean
{
    public class CitizenBean
    {
        public string CitizenID { get; set; }
        public string CitizenName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Status { get; set; }
    }

}


//4 DAO class
namespace Com.Wipro.Citizen.Dao
{

    public class CitizenDAO
    {
        public bool validateCitizen(string citizenID)
        {
            SqlConnection con = DBUtil.GetDBConnection();
            con.Open();
            string query = "SELECT COUNT(*) FROM Citizen_TBL WHERE Citizen_ID = @CitizenId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@CitizenId", citizenID);
            //ExecuteReader()-> select * from Citizen_TBL
            //ExecuteNonQuery() -> insert/delete/update
            int count = (int)cmd.ExecuteScalar();
            con.Close();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public CitizenBean viewCitizen(string citizenID)
        {
            SqlConnection con = DBUtil.GetDBConnection();
            con.Open();
            string query1 = "SELECT * FROM Citizen_TBL WHERE Citizen_ID = @CitizenId";
            SqlCommand cmd = new SqlCommand(query1, con);
            cmd.Parameters.AddWithValue("@CitizenId", citizenID);

            SqlDataReader dr = cmd.ExecuteReader();
            CitizenBean bean = null;

            if (dr.Read())
            {
                bean = new CitizenBean();
                bean.CitizenId = dr["Citizen_ID"].ToString();
                bean.CitizenName = dr["Citizen_Name"].ToString();
                bean.Age = Convert.ToInt32(dr["Age"]);
                bean.City = dr["City"].ToString();
                bean.Status = dr["Status"].ToString();
            }
            con.Close();
            return bean;

        }


        public bool registerCitizen(CitizenBean citizenBean)
        {
            SqlConnection con = DBUtil.GetDBConnection();
            con.Open();

            SqlCommand cmd =
                    new SqlCommand(
                        "INSERT INTO CITIZEN_TBL VALUES (@i,@n,@a,@c,@s)", con);

            cmd.Parameters.AddWithValue("@i", citizenBean.CitizenId);
            cmd.Parameters.AddWithValue("@n", citizenBean.CitizenName);
            cmd.Parameters.AddWithValue("@a", citizenBean.Age);
            cmd.Parameters.AddWithValue("@c", citizenBean.City);
            cmd.Parameters.AddWithValue("@s", citizenBean.Status);

            int rows = cmd.ExecuteNonQuery();
            con.Close();
            return rows > 0;

        }
    }

}


//5 citizen service
namespace Com.Wipro.Citizen.Service
{
    public class CitizenMain
    {
        CitizenDAO dao = new CitizenDAO();

        //view citizen
        public string viewCitizen(string citizenID)
        {

            //STEP1
            if (!dao.validateCitizen(citizenID))
            {
                return "CITIZEN NOT FOUND";
            }

            //STEP2
            CitizenBean bean = dao.viewCitizen(citizenID);

            //step3
            return bean.CitizenId + " | " +
                  bean.CitizenName + " | " +
                  bean.Age + " | " +
                  bean.City + " | " +
                  bean.Status;

        }

        public string register(CitizenBean citizenBean)
        {
            //step1
            if (citizenBean == null)
                return "INVALID";

            try
            {
                // Step 2 & 3: Validate ID uniqueness and age
                if (dao.validateCitizen(citizenBean.CitizenId) || citizenBean.Age < 18)
                    throw new InvalidCitizenException();

                // Step 4 & 5: Register citizen
                return dao.registerCitizen(citizenBean) ? "SUCCESS" : "FAILURE";
            }
            catch (InvalidCitizenException)
            {
                return "INVALID CITIZEN DETAILS";
            }

        }


        public static void Main(string[] args)
        {
            CitizenMain citizenMain = new CitizenMain();

            // Test Case 1: View Citizen (Valid)
            Console.WriteLine(citizenMain.viewCitizen("C10001"));


            // Test Case 2: Register Citizen (Valid)
            CitizenBean c1 = new CitizenBean();
            c1.CitizenId = "C10003";
            c1.CitizenName = "Arun";
            c1.Age = 29;
            c1.City = "Madurai";
            c1.Status = "Active";
            Console.WriteLine(citizenMain.register(c1));

            // Test Case 3: Register Citizen (Invalid Age)
            CitizenBean c2 = new CitizenBean();
            c2.CitizenId = "C10004";
            c2.CitizenName = "Kavi";
            c2.Age = 16;
            c2.City = "Salem";
            c2.Status = "Active";
            Console.WriteLine(citizenMain.register(c2));

            // Test Case 4: Invalid Citizen ID
            Console.WriteLine(citizenMain.viewCitizen("C99999"));


            // Test Case 5: Null Citizen
            Console.WriteLine(citizenMain.register(null));


        }
    }
}





