using System;
using Com.Wipro.Citizen.Bean;
using Com.Wipro.Citizen.Dao;
using Com.Wipro.Citizen.Util;

namespace Com.Wipro.Citizen.Service
{
    public class CitizenMain
    {
        CitizenDAO dao = new CitizenDAO();

        // View Citizen
        public string viewCitizen(string citizenID)
        {
            if (!dao.validateCitizen(citizenID))
                return "CITIZEN NOT FOUND";

            return "CITIZEN DETAILS FOUND";
        }

        // Register Citizen
        public string register(CitizenBean citizenBean)
        {
            if (citizenBean == null)
                return "INVALID";

            try
            {
                // Validate ID uniqueness and age
                if (dao.validateCitizen(citizenBean.CitizenID) || citizenBean.Age < 18)
                    throw new InvalidCitizenException();

                // Insert into DB
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

            // Test Case 1
            Console.WriteLine("Test Case 1: View Citizen");
            Console.WriteLine(citizenMain.viewCitizen("C10001"));
            Console.WriteLine();

            // Test Case 2 (VALID – NEW ID)
            Console.WriteLine("Test Case 2: Register Citizen (Valid)");
            CitizenBean c1 = new CitizenBean();
            c1.CitizenID = "C20001";   // ✅ NEW ID
            c1.CitizenName = "Arun";
            c1.Age = 29;
            c1.City = "Madurai";
            c1.Status = "Active";
            Console.WriteLine(citizenMain.register(c1));
            Console.WriteLine();

            // Test Case 3 (Invalid – Duplicate ID)
            Console.WriteLine("Test Case 3: Invalid Citizen Details");
            CitizenBean c2 = new CitizenBean();
            c2.CitizenID = "C20001";   // ❌ Duplicate ID
            c2.CitizenName = "Prakash";
            c2.Age = 22;
            c2.City = "Dindigul";
            c2.Status = "Active";
            Console.WriteLine(citizenMain.register(c2));
            Console.WriteLine();

            // Test Case 4
            Console.WriteLine("Test Case 4: Invalid Citizen ID");
            Console.WriteLine(citizenMain.viewCitizen("C99999"));
            Console.WriteLine();

            // Test Case 5
            Console.WriteLine("Test Case 5: Null Citizen");
            Console.WriteLine(citizenMain.register(null));
        }
    }
}
