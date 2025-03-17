using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_comm.Models;

namespace e_comm.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReviewId { get; set; }


        [Required]
        public int ProductId {  get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }



        [Range(1,5,ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating {  get; set; }

        public string ReviewText { get; set; }

        public DateOnly PostedDate { get; set; }
        //include userid
    }
}
