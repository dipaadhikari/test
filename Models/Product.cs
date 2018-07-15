using System;
using System.ComponentModel.DataAnnotations;

namespace ProductMgmt.Models
{
    public class Product
    {
        public int Id{get; set;}
       
        [Required]
        [MinLength(3,ErrorMessage="The {0} should be greater than or equals to {1} character in length")]
        [Display(Name="Product Name")]
         public string ProductName{get;set;}
        
        [Required]
        [Display(Name="Quantity")]
        public string Quantity{get; set;}
        
        [Required]
        [Display(Name="Price")]
        public decimal Price{get; set;}

        public DateTime CreatedAt{get; set;}

    }
}