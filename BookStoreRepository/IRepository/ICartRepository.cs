using BookStoreCommon.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepository.IRepository
{
    public interface ICartRepository
    {
        public bool AddToCart(int bookId, int userId, int bookcount);
        public List<Cart> GetCart(int userId);
        public bool DeleteCart(int cartid);
        public int UpdateCart(int userId, int cartid, int count);
        public List<Cart> GetCartByBook(int userId, int bookid);

    }
}