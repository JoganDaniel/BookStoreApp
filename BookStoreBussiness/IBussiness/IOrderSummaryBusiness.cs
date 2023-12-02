using BookStoreCommon.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBussiness.IBussiness
{
    public interface IOrderSummaryBusiness
    {
        public IEnumerable<OrderSummary> GetOrderSummary(int userid);
    }
}
