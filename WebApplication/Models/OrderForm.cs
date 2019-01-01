using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
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
                new ImageType {Name = "全部", Id = 1 },
                new ImageType {Name = "圆形章", Id = 2},
                new ImageType {Name = "扁章", Id = 3},
                new ImageType {Name = "闲章", Id = 4},
                new ImageType {Name = "方形章", Id = 5},
                new ImageType {Name = "儿童印章", Id = 6},
                new ImageType {Name = "个性签名章", Id = 7}
            };
        }
    }

    public class ImageTypeModel
    {
        [Display(Name = "选择印章类型")]
        public int[] ImageType { get; set; }
    }
}
