using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBussiness.IBussiness
{
    public interface IOrderBusiness
    {
        public int PlaceOrder(int cartid, int customerid);
    }
}
