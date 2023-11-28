using BookStoreCommon.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBussiness.IBussiness
{
    public interface ICustomerDetailsBusiness
    {
        public bool AddToCustomerDetails(CustomerDetails cDetails);
        public IEnumerable<CustomerDetails> GetCustomerDetails(int userId);
        public CustomerDetails EditAddress(int customerid, CustomerDetails details);
        public bool DeleteAddress(int userId, int customerid);
    }
}
