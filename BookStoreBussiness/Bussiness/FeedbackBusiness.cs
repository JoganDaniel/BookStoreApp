using BookStoreBussiness.IBussiness;
using BookStoreCommon.Model;
using BookStoreRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBussiness.Bussiness
{
    public class FeedbackBusiness : IFeedbackBusiness
    {
        public readonly IFeedbackRepository feedbackRepository;
        public FeedbackBusiness(IFeedbackRepository feedbackRepository) { 
            this.feedbackRepository = feedbackRepository;
        }
        public CustomerFeedback AddToCustomerFeedback(CustomerFeedback feedback)
        {
           return this.feedbackRepository.AddToCustomerFeedback(feedback);
        }

        public List<CustomerFeedback> GetAllFeedback(int bookid)
        {
            return this.feedbackRepository.GetAllFeedback(bookid);
        }
    }
}
