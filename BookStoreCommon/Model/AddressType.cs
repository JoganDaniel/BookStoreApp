using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreCommon.Model
{
    public class AddressType
    {
        [Key]
        [Required]
        public int TypeId { get; set; }
        [Required]
        public string TypeName { get; set; }
    }
}
