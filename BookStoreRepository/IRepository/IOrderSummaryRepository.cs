using BookStoreCommon.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepository.IRepository
{
    public interface IOrderSummaryRepository
    {
        public IEnumerable<OrderSummary> GetOrderSummary(int userid);
    }
}
