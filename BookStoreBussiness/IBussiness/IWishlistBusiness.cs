using BookStoreCommon.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBussiness.IBussiness
{
    public interface IWishlistBusiness
    {
        public bool AddToWishlist(int bookId,int userId);
        public List<Wishlist> GetWishList(int userId);
        public bool DeleteWishlist(int wishlistid);
        public int MoveToCart(Wishlist wishlist);
    }
}
