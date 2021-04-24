using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Models
{
    public class Notice
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required,StringLength(500)]
        public string Massage { get; set; }
        public User user { get; set; }
        [StringLength(50) ,ForeignKey("user")]
        public string Send_For { get; set; }
        public User user1 { get; set; }
        [ StringLength(50), ForeignKey("user1")]
        public string Send_by { get; set; }
        [Required,StringLength(50)]
        public string Status { get; set; }
    }
}