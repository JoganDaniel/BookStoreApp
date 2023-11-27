using BookStoreCommon.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepository.IRepository
{
    public interface ICartRepository
    {
        public bool AddToCart(int bookId, int userId);
        public List<Cart> GetCart(int userId);
        public bool DeleteCart(int cartid);
    }
}
