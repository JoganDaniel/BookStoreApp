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
        public bool AddToCart(int bookId, int userId)
        {
            return this.cartrepository.AddToCart(bookId, userId);
        }

        public bool DeleteCart(int cartid)
        {
            return this.cartrepository.DeleteCart(cartid);
        }

        public List<Cart> GetCart(int userId)
        {
            return this.cartrepository.GetCart(userId);
        }
    }
}
