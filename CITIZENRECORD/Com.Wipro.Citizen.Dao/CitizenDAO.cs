using Com.Wipro.Citizen.Bean;
using Com.Wipro.Citizen.Util;
using Microsoft.Data.SqlClient;

namespace Com.Wipro.Citizen.Dao
{
    public class CitizenDAO
    {
        public bool validateCitizen(string citizenID)
        {
            SqlConnection con = DBUtil.GetDBConnection();
            con.Open();

            string query = "SELECT COUNT(*) FROM Citizen_TBL WHERE Citizen_ID = @id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", citizenID);

            int count = (int)cmd.ExecuteScalar();
            con.Close();

            return count > 0;
        }

        public CitizenBean viewCitizen(string citizenID)
        {
            SqlConnection con = DBUtil.GetDBConnection();
            con.Open();

            string query = "SELECT * FROM Citizen_TBL WHERE Citizen_ID = @id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", citizenID);

            SqlDataReader dr = cmd.ExecuteReader();
            CitizenBean bean = null;

            if (dr.Read())
            {
                bean = new CitizenBean();
                bean.CitizenID = dr["Citizen_ID"].ToString();
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

            string query =
                "INSERT INTO Citizen_TBL VALUES (@id,@name,@age,@city,@status)";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@id", citizenBean.CitizenID);
            cmd.Parameters.AddWithValue("@name", citizenBean.CitizenName);
            cmd.Parameters.AddWithValue("@age", citizenBean.Age);
            cmd.Parameters.AddWithValue("@city", citizenBean.City);
            cmd.Parameters.AddWithValue("@status", citizenBean.Status);

            int rows = cmd.ExecuteNonQuery();
            con.Close();

            return rows > 0;
        }
    }
}
