using BookStoreCommon.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBussiness.IBussiness
{
    public interface IFeedbackBusiness
    {
        public CustomerFeedback AddToCustomerFeedback(CustomerFeedback feedback);
        public List<CustomerFeedback> GetAllFeedback(int bookid);
    }
}
