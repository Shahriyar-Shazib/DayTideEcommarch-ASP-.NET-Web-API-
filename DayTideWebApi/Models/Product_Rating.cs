using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Models
{
    public class Product_Rating
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Product product { get; set; }
        [Required,ForeignKey("product")]
        public int ProductId { get; set; }
        [Required]
        public int Rating { get; set; }
        public string Comments { get; set; }
        public Customer customer { get; set; }
        [Required, StringLength(50), ForeignKey("customer")]
        public string CustomerId { get; set; }
    }
}