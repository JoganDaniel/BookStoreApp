using BookStoreCommon.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepository.IRepository
{
    public interface ICustomerDetailsRepository
    {
        public bool AddToCustomerDetails(CustomerDetails cDetails);
        public List<CustomerDetails> GetCustomerDetails(int userId);

    }
}
