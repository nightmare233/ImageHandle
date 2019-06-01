using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication.Repositories
{
    public class DapperHelper
    {
        public static string connStr = ConfigurationManager.ConnectionStrings["MysqlDB"].ConnectionString;
         
    }
}