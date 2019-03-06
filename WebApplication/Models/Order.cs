using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Order
    {
        public int Id { get; set; }
        public string TaobaoId { get; set; }
        public DateTime SubmitTime { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public DateTime AuditTime { get; set; }
        public int Auditor { get; set; }
        public string AuditorName { get; set; }
        public int Productor { get; set; }
        public string ProductorName { get; set; }
        public DateTime ProductTime { get; set; }
        public DateTime DeleteTime { get; set; }
        public int SampleId { get; set; }
        public Sample Sample { get; set; }
        public string MainText { get; set; }
        public string SmallText { get; set; }
        public string ImageUrl { get; set; } //生成的印章图片地址。
    } 
}