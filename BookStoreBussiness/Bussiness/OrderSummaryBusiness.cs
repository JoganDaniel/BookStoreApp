using BookStoreBussiness.IBussiness;
using BookStoreCommon.Model;
using BookStoreRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBussiness.Bussiness
{
    public class OrderSummaryBusiness : IOrderSummaryBusiness
    {
        public readonly IOrderSummaryRepository summaryRepository;
        public OrderSummaryBusiness(IOrderSummaryRepository summaryRepository)
        {
            this.summaryRepository = summaryRepository;
        }

        public IEnumerable<OrderSummary> GetOrderSummary(int userid)
        {
            return this.summaryRepository.GetOrderSummary(userid);
        }
    }
}
