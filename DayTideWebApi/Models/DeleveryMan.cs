using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Models
{
    public class DeleveryMan
    {
        public User user { get; set; }
        [Key, StringLength(50), ForeignKey("user")]
        public string DelManId { get; set; }
        [Required, StringLength(150)]
        public string Name { get; set; }
        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }
        [Required, StringLength(11)]
        public string Phone { get; set; }
        [Required, StringLength(500)]
        public string Address { get; set; }
        [Required]
        public double Salary { get; set; }
        public string Picture { get; set; }
        [Required]
        public int Complete_Task { get; set; }
    }
}