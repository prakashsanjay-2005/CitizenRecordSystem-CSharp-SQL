using System;

namespace Com.Wipro.Citizen.Util
{
    public class InvalidCitizenException : Exception
    {
        public override string ToString()
        {
            return "INVALID CITIZEN DETAILS";
        }
    }
}

