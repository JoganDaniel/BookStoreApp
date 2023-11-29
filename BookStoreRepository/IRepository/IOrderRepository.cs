using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepository.IRepository
{
    public interface IOrderRepository
    {
        public int PlaceOrder(int cartid, int customerid);
    }
}
