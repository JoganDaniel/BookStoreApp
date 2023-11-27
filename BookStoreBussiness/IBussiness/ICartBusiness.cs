using BookStoreCommon.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBussiness.IBussiness
{
    public interface ICartBusiness
    {
        public bool AddToCart(int bookId, int userId);
        public List<Cart> GetCart(int userId);
        public bool DeleteCart(int cartid);
    }
}
