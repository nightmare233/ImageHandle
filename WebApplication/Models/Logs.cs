using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Logs
    {
        public int Id { get; set; }
        [Required]
        public EnumAction Action { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public int UserId { get; set; }
        public string Detail { get; set; }
    }
}