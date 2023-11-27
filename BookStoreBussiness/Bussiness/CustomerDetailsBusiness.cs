using BookStoreBussiness.IBussiness;
using BookStoreCommon.Model;
using BookStoreRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBussiness.Bussiness
{
    public class CustomerDetailsBusiness : ICustomerDetailsBusiness
    {
        public readonly ICustomerDetailsRepository customerRepository;
        public CustomerDetailsBusiness(ICustomerDetailsRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public bool AddToCustomerDetails(CustomerDetails cDetails)
        {
            return this.customerRepository.AddToCustomerDetails(cDetails);
        }

        public List<CustomerDetails> GetCustomerDetails(int userId)
        {
            return this.GetCustomerDetails(userId);
        }
    }
}
