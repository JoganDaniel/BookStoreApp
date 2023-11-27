using BookStoreCommon.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBussiness.IBussiness
{
    public interface ICustomerDetailsBusiness
    {
        public bool AddToCustomerDetails(CustomerDetails cDetails);
        public List<CustomerDetails> GetCustomerDetails(int userId);
    }
}
