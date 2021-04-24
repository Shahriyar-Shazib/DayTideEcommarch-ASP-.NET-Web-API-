using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Required,StringLength(100)]
        public string ProductName { get; set; }
        [Required, StringLength(500)]
        public string Description { get; set; }

        public Category category { get; set; }
        [Required,ForeignKey("category")]
        public int CategoryId { get; set; }
        [Required]
        public double Buying_Price { get; set; }
        [Required]
        public double Selling_Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Picture { get; set; }

}}