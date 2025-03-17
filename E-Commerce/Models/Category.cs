using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_comm.Models
{
    public class Category
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(30, ErrorMessage = "Product name must be less than 30 characters")]
        public string CategoryName { get; set; }

       // public ICollection<Product> Products { get; set; }
    }

}
