using BookStoreCommon.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepository.IRepository
{
    public interface IFeedbackRepository
    {
        public CustomerFeedback AddToCustomerFeedback(CustomerFeedback feedback);
        public List<CustomerFeedback> GetAllFeedback(int bookid);
    }
}
