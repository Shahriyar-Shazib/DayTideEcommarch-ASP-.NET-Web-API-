using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Models
{
    public class User
    {
        [Key]
        public string UserId { get; set; }
        [Required,StringLength(50)]
        public string Password { get; set; }
        [Required, StringLength(50)]
        public String Type { get; set; }
        [Required, StringLength(50)]
        public string Status { get; set; }
    }
}