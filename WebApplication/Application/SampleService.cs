using System;
 using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Repositories;
using Models;
using MySql.Data.MySqlClient;

namespace WebApplication.Application
{
    public class SampleService
    {
        private readonly Contexto contexto;

        public SampleService()
        {
            contexto = new Contexto();
        }

        public List<Sample> ListAll(EnumImageType? enumImageType, EnumImageStyle? enumImageStyle, bool? ifHasBgImage, bool ifGetTexts)
        {
            var samples = new List<Sample>();
            StringBuilder sb = new StringBuilder("SELECT * FROM sample where 1=1 ");
            if (enumImageType != null)
            {
                sb.Append($" and imageType = {(int)enumImageType}");
            }
            if (enumImageStyle != null)
            {
                sb.Append($" and style = {(int)enumImageStyle}");
            }
            if (ifHasBgImage.HasValue)
            {
                if (ifHasBgImage.Value)
                    sb.Append($" and BgImage <> ''");
                else
                    sb.Append($" and BgImage = ''");
            }
            var rows = contexto.ExecuteCommandSQL(sb.ToString(), null);
            foreach (var row in rows)
            {
                Sample tempSample = new Sample();
                tempSample.Id = int.Parse(row["Id"].ToString());
                tempSample.Name = row["Name"].ToString();
                tempSample.IfHasSmallText = Convert.ToBoolean(int.Parse(row["IfHasSmallText"].ToString()));
                tempSample.MainTextNumber = int.Parse(row["MainTextNumber"].ToString());
                tempSample.ImageSizeX = int.Parse(row["ImageSizeX"].ToString());
                tempSample.ImageSizeY = int.Parse(row["ImageSizeY"].ToString());
                tempSample.ImageType = (EnumImageType)int.Parse(row["ImageType"].ToString());
                tempSample.Style = (EnumImageStyle)int.Parse(row["Style"].ToString());
                tempSample.ImageUrl = row["ImageURL"].ToString();
                if (ifGetTexts)
                {
                    tempSample = GetTexts(tempSample.Id, tempSample);
                }
                samples.Add(tempSample);
            }
            return samples;
        }

        public Sample GetSample(int id, bool ifGetTexts)
        {
            Sample tempSample = new Sample();
            string strQuery = $"SELECT * FROM sample WHERE id = {id}";

            var rows = contexto.ExecuteCommandSQL(strQuery, null);
            if (rows.Count > 0)
            {
                tempSample.Id = int.Parse(rows[0]["Id"].ToString());
                tempSample.Name = rows[0]["Name"].ToString();
                tempSample.IfHasSmallText = Convert.ToBoolean(int.Parse(rows[0]["IfHasSmallText"].ToString()));
                tempSample.MainTextNumber = int.Parse(rows[0]["MainTextNumber"].ToString());
                tempSample.ImageSizeX = int.Parse(rows[0]["ImageSizeX"].ToString());
                tempSample.ImageSizeY = int.Parse(rows[0]["ImageSizeY"].ToString());
                tempSample.ImageType = (EnumImageType)int.Parse(rows[0]["ImageType"].ToString());
                tempSample.Style = (EnumImageStyle)int.Parse(rows[0]["Style"].ToString());
                tempSample.ImageUrl = rows[0]["ImageURL"].ToString();
                tempSample.BgImage = rows[0]["BgImage"].ToString();
                tempSample.IfHasBgImg = string.IsNullOrEmpty(tempSample.BgImage) ? false : true;
                if (ifGetTexts)
                {
                    tempSample = GetTexts(tempSample.Id, tempSample); 
                }
            }

            return tempSample;
        }

        public Sample GetTexts(int sampleId, Sample sample) //main text and small text
        {
            ImageFontService imageFontService = new ImageFontService();
            
            List<ImageText> mainTexts = new List<ImageText>();
            string strQuery = $"SELECT * FROM imageText WHERE sampleId = {sampleId} order by id";

            var rows = contexto.ExecuteCommandSQL(strQuery, null);
            foreach (var row in rows)
            {
                ImageText imageText = new ImageText();
                imageText.PositionX = int.Parse(row["PositionX"].ToString());
                imageText.PositionY = int.Parse(row["PositionY"].ToString());
                imageText.Type = int.Parse(row["Type"].ToString());
                imageText.FontSize = int.Parse(row["FontSize"].ToString());
                imageText.Font = row["Font"].ToString();
                imageText.imageFont = imageFontService.GetByName(imageText.Font);
                imageText.Text = row["Text"].ToString();
                if (row["FontOrder"] != null)
                {
                    imageText.Order = Convert.ToBoolean(int.Parse(row["FontOrder"].ToString()));
                }
                 
                if (imageText.Type == (int)EnumTextType.MainText)
                {
                    mainTexts.Add(imageText);
                }
                else
                {
                    sample.SmallText = imageText;
                }
            }
            if (mainTexts.Count > 0)
            {
                sample.MainText = mainTexts;
            }
            return sample;
        }

        public int Insert(Sample sample)
        {
            const string sql1 = @"INSERT into sample(ImageType, Name, ImageSizeX, ImageSizeY, Style, ImageURL, BgImage, MainTextNumber, IfHasSmallText)  
                                VALUES(@ImageType, @Name, @ImageSizeX, @ImageSizeY, @Style, @ImageURL, @BgImage, @MainTextNumber, @IfHasSmallText); 
                                SELECT LAST_INSERT_ID();";

            const string sql2 = @"INSERT into imagetext(SampleId, Type, Text, Font, PositionX, PositionY, FontSize, FontOrder)  
                                        VALUES(@SampleId, @Type, @Text, @Font, @PositionX, @PositionY, @FontSize, @FontOrder)";

            using (contexto.connection)
            {
                contexto.OpenConnection();
                MySqlTransaction transaction = contexto.connection.BeginTransaction();
                MySqlCommand cmd = contexto.connection.CreateCommand();
                cmd.Transaction = transaction;

                try
                {
                    cmd.CommandText = sql1;
                    var parameters = new Dictionary<string, object>
                            {
                                { "ImageType", sample.ImageType},
                                { "Name", sample.Name},
                                { "ImageSizeX", sample.ImageSizeX},
                                { "ImageSizeY", sample.ImageSizeY},
                                { "Style", sample.Style},
                                { "ImageURL", sample.ImageUrl},
                                { "BgImage", sample.BgImage},
                                { "MainTextNumber", sample.MainTextNumber},
                                { "IfHasSmallText", sample.IfHasSmallText}
                            };
                    contexto.AddParams(cmd, parameters);
                    int sampleId = int.Parse(cmd.ExecuteScalar().ToString());  //insert sample
                    cmd.Parameters.Clear();
                    cmd.CommandText = sql2;
                    foreach (var item in sample.MainText)  //insert maintext
                    {
                        Dictionary<string, object> param = new Dictionary<string, object>
                            {
                                { "SampleId", sampleId},
                                { "Type", item.Type},
                                { "Text", item.Text},
                                { "Font", item.Font},
                                { "PositionX", item.PositionX},
                                { "PositionY", item.PositionY},
                                { "FontSize", item.FontSize},
                                { "FontOrder", item.Order}
                            };
                        contexto.AddParams(cmd, param);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    if (sample.IfHasSmallText) //add small text
                    {
                        var param = new Dictionary<string, object>
                            {
                                { "SampleId", sampleId},
                                { "Type", sample.SmallText.Type},
                                { "Text", sample.SmallText.Text},
                                { "Font", sample.SmallText.Font},
                                { "PositionX", sample.SmallText.PositionX},
                                { "PositionY", sample.SmallText.PositionY},
                                { "FontSize", sample.SmallText.FontSize},
                                { "FontOrder", sample.SmallText.Order}
                            };
                        contexto.AddParams(cmd, param);
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    return sampleId;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    contexto.CloseConnection();
                }
            }
        }

        public void Delete(int sampleId)
        {
            const string sql1 = @"DELETE FROM imagetext WHERE SampleId = @sampleId";

            const string sql2 = @"DELETE FROM sample where id = @sampleId";

            using (contexto.connection)
            {
                contexto.OpenConnection();
                MySqlTransaction transaction = contexto.connection.BeginTransaction();
                MySqlCommand cmd = contexto.connection.CreateCommand();
                cmd.Transaction = transaction;

                try
                {
                    cmd.CommandText = sql1;
                    var parameters = new Dictionary<string, object>
                    {
                        { "sampleId", sampleId}
                    };
                    contexto.AddParams(cmd, parameters);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    cmd.CommandText = sql2;
                    contexto.AddParams(cmd, parameters);
                    cmd.ExecuteNonQuery();
                    transaction.Commit(); 
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    contexto.CloseConnection();
                }
            }
        }

        public Sample GetSampleByName(string name, bool ifGetTexts)
        {
            Sample tempSample = null;
            string strQuery = $"SELECT * FROM sample WHERE Name = '{name}'";

            var rows = contexto.ExecuteCommandSQL(strQuery, null);
            if (rows.Count > 0)
            {
                tempSample = new Sample();
                tempSample.Id = int.Parse(rows[0]["Id"].ToString());
                tempSample.Name = rows[0]["Name"].ToString();
                tempSample.IfHasSmallText = Convert.ToBoolean(int.Parse(rows[0]["IfHasSmallText"].ToString()));
                tempSample.MainTextNumber = int.Parse(rows[0]["MainTextNumber"].ToString());
                tempSample.ImageSizeX = int.Parse(rows[0]["ImageSizeX"].ToString());
                tempSample.ImageSizeY = int.Parse(rows[0]["ImageSizeY"].ToString());
                tempSample.ImageType = (EnumImageType)int.Parse(rows[0]["ImageType"].ToString());
                tempSample.Style = (EnumImageStyle)int.Parse(rows[0]["Style"].ToString());
                tempSample.ImageUrl = rows[0]["ImageURL"].ToString();
                tempSample.BgImage = rows[0]["BgImage"].ToString();
                tempSample.IfHasBgImg = string.IsNullOrEmpty(tempSample.BgImage) ? false : true;
                if (ifGetTexts)
                {
                    tempSample = GetTexts(tempSample.Id, tempSample);
                }
            }

            return tempSample;
        }
    }
}