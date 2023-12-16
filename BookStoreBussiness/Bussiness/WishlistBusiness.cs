using BookStoreBussiness.IBussiness;
using BookStoreCommon.Model;
using BookStoreRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBussiness.Bussiness
{

    public class WishlistBusiness : IWishlistBusiness
    {
        public readonly IWishlistRepository wishrepository;
        public WishlistBusiness(IWishlistRepository wishrepository)
        {
            this.wishrepository = wishrepository;
        }
        public bool AddToWishlist(int bookId, int userId)
        {
            return this.wishrepository.AddToWishlist(bookId,userId);
        }

        public bool DeleteWishlist(int wishlistid)
        {
            return this.wishrepository.DeleteWishlist(wishlistid);
        }

        public List<Wishlist> GetWishList(int userId)
        {
            return this.wishrepository.GetWishList(userId);
        }

        public int MoveToCart(Wishlist wishlist)
        {
            return this.wishrepository.MoveToCart(wishlist);
        }
    }
}
