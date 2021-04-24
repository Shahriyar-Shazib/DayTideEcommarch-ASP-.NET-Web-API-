using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Models
{
    public class CartBackup
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required,StringLength(50)]
        public string CustomerId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantiry { get; set; }
        [Required]
        public double Price { get; set; }
       
        public Nullable<int> OrderId { get; set; }
    }
}