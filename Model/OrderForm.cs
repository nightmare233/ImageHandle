using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication.Common;

namespace Model
{
    public class OrderForm
    {
        public ImageTypeModel ImageTypes { get; set; }
        public string URL { get; set; }
        public DateTime ExpireTime { get; set; }
        public Guid formGuid { get; set; }
    }

    public class ImageType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static List<ImageType> GetAll()
        {
            return new List<ImageType>
            {
                new ImageType { Name = EnumImageType.全部.ToString(), Id = (int)EnumImageType.全部 },
                new ImageType { Name = EnumImageType.圆形章.ToString(), Id = (int)EnumImageType.圆形章},
                new ImageType { Name = EnumImageType.扁章.ToString(), Id = (int)EnumImageType.扁章},
                new ImageType { Name = EnumImageType.闲章.ToString(), Id = (int)EnumImageType.闲章},
                new ImageType { Name = EnumImageType.方形章.ToString(), Id = (int)EnumImageType.方形章},
                new ImageType { Name = EnumImageType.儿童印章.ToString(), Id = (int)EnumImageType.儿童印章},
                new ImageType { Name = EnumImageType.个性签名章.ToString(), Id = (int)EnumImageType.个性签名章}
            };
        }
    }

    public class ImageTypeModel
    {
        [Display(Name = "选择印章类型")]
        public int[] ImageType { get; set; }
    }
}
