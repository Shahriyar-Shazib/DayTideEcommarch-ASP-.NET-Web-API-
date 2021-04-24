using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Models
{
    public class OrderRequest
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        [Required]
        public string Date { get; set; }
        [Required, StringLength(500)]
        public string Address { get; set; }
        [Required, StringLength(50)]
        public string District { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required, StringLength(50)]
        public string Payment_Type { get; set; }

        public Customer customer { get; set; }
        [Required, StringLength(50), ForeignKey("customer")]
        public string CustomerId { get; set; }
    }
}