using BookStoreBussiness.IBussiness;
using BookStoreCommon.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WishListController : ControllerBase
    {
        public readonly IWishlistBusiness wishlistBusiness;

        public WishListController(IWishlistBusiness wishlistBusiness)
        {
            this.wishlistBusiness = wishlistBusiness;
        }
        [HttpPost]
        [Route("AddwishList")]
        public Task<ActionResult> AddWishList(int bookId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.wishlistBusiness.AddToWishlist(bookId, userId);
                if (result == true)
                {
                    return Task.FromResult<ActionResult>(this.Ok(new { Status = true, Message = "wishlist added successfully", Data = "success" }));
                }
                return Task.FromResult<ActionResult>(this.BadRequest(new { Status = false, Message = "wishlist adding Unsuccessful" }));
            }
            catch (Exception ex)
            {
                return Task.FromResult<ActionResult>(this.NotFound(new { Status = false, Message = ex.Message }));
            }
        }
        [HttpGet]
        [Route("GetWishlist")]
        public async Task<ActionResult> GetWishlist()
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.wishlistBusiness.GetWishList(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Wishlist retrieved", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No wishlist Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("DeleteWishList")]
        public ActionResult DeleteWishList(int wishlistId)
        {
            try
            {
                var result = this.wishlistBusiness.DeleteWishlist(wishlistId);
                if (result != false)
                {
                    return this.Ok(new { Status = true, Message = "Deleted wishlist" });
                }
                return this.BadRequest(new { Status = false, Message = "Data empty" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("MoveWishListToCart")]
        public ActionResult MoveToCart(Wishlist wishlist)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                wishlist.UserId = userId;
                var result = this.wishlistBusiness.MoveToCart(wishlist);
                if (result >0)
                {
                    return this.Ok(new { Status = true, Message = "Moved to cart" });
                }
                return this.BadRequest(new { Status = false, Message = "Data empty" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
