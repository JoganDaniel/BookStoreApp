using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookStoreCommon.Model
{
    public class CustomerDetails
    {
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [ForeignKey("TypeId")]
        public int TypeId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public Type Type { get; set; }
    }
}
//1.FullName
//2.MobileNum
//3.Address
//4.CityOrTown
//5.State
//6.TypeId(FK)
//7.UserId(FK)