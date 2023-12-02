using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreCommon.Model
{
    public class Cart
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }
        [Required]
        [ForeignKey("BookId")]
        public int BookId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public int BookCount { get; set; }
        public Book Book { get; set; }
    }
}
