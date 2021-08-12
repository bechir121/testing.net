using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApplication2.Config
{
    public class StoreConnexion
    {
        public static string GetConnection()
        {
            return ConfigurationManager.ConnectionStrings["ecs"].ConnectionString;
        }
    }
}