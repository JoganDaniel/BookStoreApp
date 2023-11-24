using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreCommon.Model
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [Required]
        public string Bookname { get; set; }
        [Required]
        public string BookDescription { get; set; }
        [Required]
        public string BookAuthor { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int BookCount { get; set; }
        [Required]
        public double BookPrice { get; set; }
        [Required]
        public double Rating { get; set; }
    }
}
//1.BookId 2.BookName 3.BookDescription 4.BookAuthor 5.BookImage 6.BookCount 7.BookPrice 8.Rating 