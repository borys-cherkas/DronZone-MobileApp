using System;

namespace DronZone_UWP.Data.Api
{
    public class ApiException : Exception
    {
        public ApiException()
        {
        }

        public ApiException(string message) : base(message)
        {
        }
    }
}
