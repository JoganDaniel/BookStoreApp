using BookStoreBussiness.IBussiness;
using BookStoreCommon.Model;
using BookStoreRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBussiness.Bussiness
{
    public class CartBusiness : ICartBusiness
    {
        public readonly ICartRepository cartrepository;
        public CartBusiness(ICartRepository cartrepository)
        {
            this.cartrepository = cartrepository;
        }
        public bool AddToCart(int bookId, int userId, int bookcount)
        {
            return this.cartrepository.AddToCart(bookId, userId,bookcount);
        }

        public bool DeleteCart(int cartid)
        {
            return this.cartrepository.DeleteCart(cartid);
        }

        public List<Cart> GetCart(int userId)
        {
            return this.cartrepository.GetCart(userId);
        }

        public List<Cart> GetCartByBook(int userId, int bookid)
        {
           return this.cartrepository.GetCartByBook(userId,bookid);
        }

        public int UpdateCart(int userId, int cartid, int count)
        {
            return this.cartrepository.UpdateCart(userId, cartid, count);
        }
    }
}
