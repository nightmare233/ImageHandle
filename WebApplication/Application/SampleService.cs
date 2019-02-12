using System;
 using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Repositories;
using Models;

namespace WebApplication.Application
{
    public class SampleService
    {
        private readonly Contexto contexto;

        public SampleService()
        {
            contexto = new Contexto();
        }

        public List<Sample> ListAll(EnumImageType? enumImageType, EnumImageStyle? enumImageStyle, bool ifGetTexts)
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
            var rows = contexto.ExecuteCommandSQL(sb.ToString(), null);
            foreach (var row in rows)
            {
                Sample tempSample = new Sample();
                tempSample.Id = int.Parse(row["Id"].ToString());
                tempSample.IfHasSmallText = Convert.ToBoolean(int.Parse(row["IfHasSmallText"].ToString()));
                tempSample.MainTextNumber = int.Parse(row["MainTextNumber"].ToString());
                tempSample.ImageSizeX = int.Parse(row["ImageSizeX"].ToString());
                tempSample.ImageSizeY = int.Parse(row["ImageSizeY"].ToString());
                tempSample.ImageType = (EnumImageType)int.Parse(row["ImageType"].ToString());
                tempSample.Style = (EnumImageStyle)int.Parse(row["Style"].ToString());
                tempSample.ImageUrl = row["ImageURL"].ToString();
                if (ifGetTexts)
                {
                    tempSample = GetMainTexts(tempSample.Id, tempSample);
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
                tempSample.IfHasSmallText = Convert.ToBoolean(int.Parse(rows[0]["IfHasSmallText"].ToString()));
                tempSample.MainTextNumber = int.Parse(rows[0]["MainTextNumber"].ToString());
                tempSample.ImageSizeX = int.Parse(rows[0]["ImageSizeX"].ToString());
                tempSample.ImageSizeY = int.Parse(rows[0]["ImageSizeY"].ToString());
                tempSample.ImageType = (EnumImageType)int.Parse(rows[0]["ImageType"].ToString());
                tempSample.Style = (EnumImageStyle)int.Parse(rows[0]["Style"].ToString());
                tempSample.ImageUrl = rows[0]["ImageURL"].ToString();
                if (ifGetTexts)
                {
                    tempSample = GetMainTexts(tempSample.Id, tempSample);
                }
            }
            
            return tempSample;
        }


        public Sample GetMainTexts(int sampleId, Sample sample)
        {
            List<ImageText> mainTexts = new List<ImageText>();
            string strQuery = $"SELECT * FROM imageText WHERE sampleId = {sampleId}";

            var rows = contexto.ExecuteCommandSQL(strQuery, null);
            foreach (var row in rows)
            {
                ImageText imageText = new ImageText(); 
                imageText.PositionX = int.Parse(row["PositionX"].ToString());
                imageText.PositionY = int.Parse(row["PositionY"].ToString());
                imageText.Type = int.Parse(row["Type"].ToString());
                imageText.FontSize = int.Parse(row["FontSize"].ToString());
                imageText.Font = row["Font"].ToString();
                imageText.Text = row["Text"].ToString();
                if (row["Order"] != null)
                {
                    imageText.Order = Convert.ToBoolean(int.Parse(row["Order"].ToString()));
                }
                

                if (imageText.Type == 1)
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

         
    }
}