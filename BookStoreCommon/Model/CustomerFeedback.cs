using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookStoreCommon.Model
{
    public class CustomerFeedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedbackId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [ForeignKey("BookId")]
        public int BookId { get; set; }
        
        public string Description { get; set; }
        public double Rating { get; set; }
        public Book Book { get; set; }
    }
}
//1.FeedbackId
//2.UserId(take it from token)
//3.BookId
//4.CustomerDescription
//5.Rating