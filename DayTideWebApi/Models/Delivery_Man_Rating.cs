using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Models
{
    public class Delivery_Man_Rating
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DeliveryMan delman { get; set; }

        [Required, StringLength(50), ForeignKey("delman")]
        public string DelManID { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required,StringLength(50)]
        public string Comments { get; set; }
        public Customer customer { get; set; }

        [Required,StringLength(50), ForeignKey("customer")]
        public string CustomerId { get; set; }
    }
}