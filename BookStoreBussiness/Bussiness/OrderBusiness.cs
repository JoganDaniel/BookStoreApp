using BookStoreBussiness.IBussiness;
using BookStoreRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBussiness.Bussiness
{
    public class OrderBusiness: IOrderBusiness
    {
        public readonly IOrderRepository orderrepository;
        public OrderBusiness(IOrderRepository orderrepository)
        {
            this.orderrepository = orderrepository;
        }

        public int PlaceOrder(int cartid, int customerid)
        {
            return orderrepository.PlaceOrder(cartid, customerid);
        }
    }
}
