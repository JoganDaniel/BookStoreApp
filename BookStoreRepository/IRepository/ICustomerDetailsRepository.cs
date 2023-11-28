using BookStoreCommon.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepository.IRepository
{
    public interface ICustomerDetailsRepository
    {
        public bool AddToCustomerDetails(CustomerDetails cDetails);
        public IEnumerable<CustomerDetails> GetCustomerDetails(int userid);
        public CustomerDetails EditAddress(int userId, CustomerDetails details);
        public bool DeleteAddress(int userId, int customerid);

    }
}
