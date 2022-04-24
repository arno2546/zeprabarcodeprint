using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;


namespace posdesktop.Services
{
    public static class ConnectionString
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
        private static string PosTestApiIP = ConfigurationManager.AppSettings["PosTestApiIP"];
        private static string TwelveApiIP = ConfigurationManager.AppSettings["TwelveApiIP"];
        public static string GetPosApiTestCS
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }
        public static string GetTwelveApiIP { get { return TwelveApiIP; } }
        public static string GetPosTestApiIP
        {

            get
            {
                return PosTestApiIP;
            }
            set
            {
                PosTestApiIP = value;
            }
        }
    }
    public static class CSTWelveERP
    {
        private static string connectionString = ConfigurationManager.AppSettings["PosTestApi"];

        public static string GetConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }
    }

}
