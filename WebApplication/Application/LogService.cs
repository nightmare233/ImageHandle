﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Repositories;
using Models;

namespace WebApplication.Application
{
    public class LogService
    {
        private readonly Contexto contexto;
        public LogService()
        {
            contexto = new Contexto();
        }

        public List<Logs> ListAll()
        {
            var logs = new List<Logs>();
            const string strQuery = "SELECT * FROM Logs";

            var rows = contexto.ExecuteCommandSQL(strQuery);
            foreach (var row in rows)
            {
                var log = new Logs
                {
                    Id = int.Parse(row["Id"].ToString()),
                    Action = (EnumAction)int.Parse(row["Action"].ToString()),
                    UserId = int.Parse(row["UserId"].ToString()),
                    Detail = row["Detail"].ToString(),
                    Time = DateTime.Parse(row["Password"].ToString())
                };
                logs.Add(log);
            }
            return logs;
        }

        public int Insert(User User)
        {
            //const string commandSQL = "INSERT INTO Users (Name, LoginName, Role, Password) VALUES (@Name, @LoginName, @Role, @Password)";
            //var parameters = new Dictionary<string, object>
            //{
            //    { "Name", User.Name},
            //    { "LoginName", User.LoginName },
            //    { "Role", User.Role},
            //    { "Password", User.Password }
            //};
            //return contexto.ExecuteCommand(commandSQL, parameters);
            return 0;
        }

        private int update(User User)
        {
            //var commandSQL = "UPDATE Users SET Name = @Name, LoginName = @LoginName, Role = @Role, Password = @Password WHERE Id = @Id";
            //var parameters = new Dictionary<string, object>
            //{
            //    { "Id", User.Id },
            //    { "Name", User.Name },
            //    { "LoginName", User.LoginName },
            //    { "Role", User.Role },
            //    { "Password", User.Password }
            //};
            //return contexto.ExecuteCommand(commandSQL, parameters);
            return 0;
        }

        //public void Save(User User)
        //{
        //    if (User.Id > 0)
        //        update(User);
        //    else
        //        Insert(User);
        //}

        //public int Delete(int Id)
        //{
        //    const string commandSQL = "DELETE FROM Users WHERE Id=@Id";
        //    var parameters = new Dictionary<string, object>
        //    {
        //        {"Id", Id}
        //    };
        //    return contexto.ExecuteCommand(commandSQL, parameters);
        //}

        //public User GetUserForId(int Id)
        //{
        //    var Users = new List<User>();
        //    const string commandSQL = "SELECT * FROM Users WHERE Id=@Id";
        //    var parameters = new Dictionary<string, object>
        //    {
        //        {"Id", Id}
        //    };

        //    var rows = contexto.ExecuteCommandSQL(commandSQL, parameters);

        //    foreach (var row in rows)
        //    {
        //        var tempUser = new User()
        //        {
        //            Id = int.Parse(row["Id"].ToString()),
        //            Name = row["Name"].ToString(),
        //            LoginName = row["LoginName"].ToString(),
        //            Role = row["Role"].ToString(),
        //            Password = row["Password"].ToString()
        //        };
        //        Users.Add(tempUser);
        //    }
        //    return Users.FirstOrDefault();
        //}

        //public User GetUserForLoginName(string LoginName)
        //{
        //    var Users = new List<User>();
        //    const string commandSQL = "SELECT * FROM Users WHERE LoginName = @LoginName";
        //    var parameters = new Dictionary<string, object>
        //    {
        //        {"LoginName", LoginName}
        //    };

        //    var rows = contexto.ExecuteCommandSQL(commandSQL, parameters);

        //    foreach (var row in rows)
        //    {
        //        var tempUser = new User()
        //        {
        //            Id = int.Parse(row["Id"].ToString()),
        //            Name = row["Name"].ToString(),
        //            LoginName = row["LoginName"].ToString(),
        //            Role = row["Role"].ToString(),
        //            Password = row["Password"].ToString()
        //        };
        //        Users.Add(tempUser);
        //    }
        //    return Users.FirstOrDefault();

        //}
    }
}