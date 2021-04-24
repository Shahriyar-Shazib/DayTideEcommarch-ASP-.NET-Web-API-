using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Models
{
    [Table("Application",Schema ="dbo")]
    public class Application
    {
        public User user { get; set; }
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required,StringLength(50)]
        public string ApplicationType { get; set; }
        [Required, StringLength(100)]
        public string Massage { get; set; }
        [Required,Column("SentBy"),ForeignKey("user")]
        public string SentBy { get; set; }
        [Required]
        public string Status { get; set; }
        public User usr { get; set; }
        [ForeignKey("usr")]
        public string Accepted_RejectedBy { get; set; }
    }
}