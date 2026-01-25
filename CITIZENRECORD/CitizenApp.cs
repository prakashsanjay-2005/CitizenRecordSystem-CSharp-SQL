using System;
using Microsoft.Data.SqlClient;

namespace Com.Wipro.Citizen.Bean
{
    public class CitizenBean
    {
        public string CitizenID { get; set; } = "";
        public string CitizenName { get; set; } = "";
        public int Age { get; set; }
        public string City { get; set; } = "";
        public string Status { get; set; } = "";
    }
}

namespace Com.Wipro.Citizen.Util
{
    public class DBUtil
    {
        public static string constr =
            "Data Source=localhost\\SQLEXPRESS;Initial Catalog=CitizenDB;Integrated Security=True;TrustServerCertificate=True";

        public static SqlConnection GetDBConnection()
        {
            return new SqlConnection(constr);
        }
    }

    public class InvalidCitizenException : Exception
    {
        public override string ToString()
        {
            return "INVALID CITIZEN DETAILS";
        }
    }
}

namespace Com.Wipro.Citizen.Dao
{
    using Com.Wipro.Citizen.Bean;
    using Com.Wipro.Citizen.Util;

    public class CitizenDAO
    {
        public bool validateCitizen(string citizenID)
        {
            using SqlConnection con = DBUtil.GetDBConnection();
            SqlCommand cmd = new SqlCommand(
                "SELECT COUNT(*) FROM CITIZEN_TBL WHERE Citizen_ID=@id", con);

            cmd.Parameters.AddWithValue("@id", citizenID);
            con.Open();
            return (int)cmd.ExecuteScalar() > 0;
        }

        public CitizenBean? viewCitizen(string citizenID)
        {
            using SqlConnection con = DBUtil.GetDBConnection();
            SqlCommand cmd = new SqlCommand(
                "SELECT Citizen_ID, Citizen_Name, Age, City, [Status] FROM CITIZEN_TBL WHERE Citizen_ID=@id",
                con);

            cmd.Parameters.AddWithValue("@id", citizenID);
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            if (!dr.Read()) return null;

            return new CitizenBean
            {
                CitizenID = dr["Citizen_ID"].ToString()!,
                CitizenName = dr["Citizen_Name"].ToString()!,
                Age = Convert.ToInt32(dr["Age"]),
                City = dr["City"].ToString()!,
                Status = dr["Status"].ToString()!
            };
        }

        public bool registerCitizen(CitizenBean b)
        {
            using SqlConnection con = DBUtil.GetDBConnection();
            SqlCommand cmd = new SqlCommand(
                "INSERT INTO CITIZEN_TBL (Citizen_ID,Citizen_Name,Age,City,[Status]) VALUES (@i,@n,@a,@c,@s)",
                con);

            cmd.Parameters.AddWithValue("@i", b.CitizenID);
            cmd.Parameters.AddWithValue("@n", b.CitizenName);
            cmd.Parameters.AddWithValue("@a", b.Age);
            cmd.Parameters.AddWithValue("@c", b.City);
            cmd.Parameters.AddWithValue("@s", b.Status);

            con.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}

namespace Com.Wipro.Citizen.Service
{
    using Com.Wipro.Citizen.Bean;
    using Com.Wipro.Citizen.Dao;
    using Com.Wipro.Citizen.Util;
    using Com.Wipro.Citizen.Service;




    public class CitizenMain
    {
        public static void Main(string[] args)

        {
            CitizenDAO dao = new CitizenDAO();

            Console.WriteLine(dao.validateCitizen("C10001"));

            CitizenBean c = new CitizenBean
            {
                CitizenID = "C10010",
                CitizenName = "Arun",
                Age = 25,
                City = "Madurai",
                Status = "Active"
            };

            Console.WriteLine(dao.registerCitizen(c));
            Console.ReadLine();
        }
    }
}
