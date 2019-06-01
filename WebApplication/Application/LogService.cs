using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Repositories;
using Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using MySql.Data.MySqlClient;

namespace WebApplication.Application
{
    public class LogService
    { 
        private log4net.ILog log = log4net.LogManager.GetLogger("ImageFontController");
        public LogService()
        { 
        }

        public List<Logs> ListAll(DateTime beginDate, DateTime endDate)
        {
            string sql = $"SELECT * FROM Logs where time BETWEEN '{beginDate}' AND '{endDate}';";
            using (IDbConnection conn = new MySqlConnection(DapperHelper.connStr))
            {
                
                return conn.Query<Logs>(sql).ToList();
            } 
        }

        public int Insert(Logs logs)
        {
            try
            {
                using (IDbConnection conn = new MySqlConnection(DapperHelper.connStr))
                {
                    string insetSql = "INSERT logs(time, action, userid, detail)VALUES(@time, @action, @userid, @detail)";
                    return conn.Execute(insetSql, logs);
                }

            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return 0;
        }

        private int update(User User)
        {

            return 0;
        }
    }
}