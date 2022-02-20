using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public static class SD
    {
        public static string APIBaseUrl = "https://localhost:44313/";
        public static string AccountAPIPath = APIBaseUrl + "api/account/";
        public static string RegisterAPIPath = APIBaseUrl + "api/register/";
        public static string LoginAPIPath = APIBaseUrl + "api/login/";
        public const string SuperAdminRole = "SuperAdmin";
    }
}
