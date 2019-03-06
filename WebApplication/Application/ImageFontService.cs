using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Repositories;
using Models;

namespace WebApplication.Application
{
    public class ImageFontService
    {
        private readonly Contexto contexto;

        public ImageFontService()
        {
            contexto = new Contexto();
        }

        public List<ImageFont> GetAll()
        {
            List<ImageFont> list = new List<ImageFont>();
            ImageFont imageFont = null;
            string sql = "SELECT * FROM imagefont;";
            var rows = contexto.ExecuteCommandSQL(sql, null);
            foreach (var row in rows)
            {
                imageFont = new ImageFont();
                imageFont.id = int.Parse(row["id"].ToString());
                imageFont.name = row["Name"].ToString();
                imageFont.url = row["URL"].ToString();
                imageFont.ifSystem = Convert.ToBoolean(int.Parse(row["IfSystem"].ToString()));
                list.Add(imageFont);
            }
            return list;
        }

        public ImageFont GetById(int id)
        {
            ImageFont imageFont = null;
            string sql = "SELECT * FROM imagefont where id = @id limit 1;";
            var parameters = new Dictionary<string, object>
            {
                { "id", id}
            }; 
            var rows = contexto.ExecuteCommandSQL(sql, parameters);
            foreach (var row in rows)
            {
                imageFont = new ImageFont();
                imageFont.id = int.Parse(row["id"].ToString());
                imageFont.name = row["Name"].ToString();
                imageFont.url = row["URL"].ToString();
                imageFont.ifSystem = Convert.ToBoolean(int.Parse(row["IfSystem"].ToString()));
            }
            return imageFont;
        }

        public ImageFont GetByName(string name)
        {
            ImageFont imageFont = null;
            string sql = "SELECT * FROM imagefont where name = @name limit 1;";
            var parameters = new Dictionary<string, object>
            {
                { "name", name}
            };
            var rows = contexto.ExecuteCommandSQL(sql, parameters);
            foreach (var row in rows)
            {
                imageFont = new ImageFont();
                imageFont.id = int.Parse(row["id"].ToString());
                imageFont.name = row["Name"].ToString();
                imageFont.url = row["URL"].ToString();
                imageFont.ifSystem = Convert.ToBoolean(int.Parse(row["IfSystem"].ToString()));
            }
            return imageFont;
        }

        public int Add(ImageFont imageFont)
        {
            const string commandSQL = "INSERT INTO imagefont (NAME, URL, IfSystem) VALUES (@Name, @URL, @IfSystem)";
            var parameters = new Dictionary<string, object>
            {
                { "Name", imageFont.name},
                { "URL", imageFont.url },
                { "IfSystem", imageFont.ifSystem}
            };
            return contexto.ExecuteCommand(commandSQL, parameters);
        }

        public int Delete(int id)
        {
            const string commandSQL = "delete from imagefont where id = @id";
            var parameters = new Dictionary<string, object>
            {
                { "id", id}
            };
            return contexto.ExecuteCommand(commandSQL, parameters);
        }
    }
}