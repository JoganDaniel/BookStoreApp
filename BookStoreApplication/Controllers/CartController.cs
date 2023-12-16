using BookStoreBussiness.IBussiness;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        public readonly ICartBusiness cartBusiness;

        public CartController(ICartBusiness cartBusiness)
        {
            this.cartBusiness = cartBusiness;
        }
        [HttpPost]
        [Route("AddCart")]
        public Task<ActionResult> AddCart(int bookId,int bookcount)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.cartBusiness.AddToCart(bookId, userId, bookcount);
                if (result == true)
                {
                    return Task.FromResult<ActionResult>(this.Ok(new { Status = true, Message = "Cart added successfully", Data = "success" }));
                }
                return Task.FromResult<ActionResult>(this.BadRequest(new { Status = false, Message = "cart adding Unsuccessful" }));
            }
            catch (Exception ex)
            {
                return Task.FromResult<ActionResult>(this.NotFound(new { Status = false, Message = ex.Message }));
            }
        }
        [HttpGet]
        [Route("GetCart")]
        public async Task<ActionResult> GetCart()
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.cartBusiness.GetCart(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Cart retrieved", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No cart Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("DeleteCart")]
        public ActionResult DeleteCart(int cartid)
        {
            try
            {
                var result = this.cartBusiness.DeleteCart(cartid);
                if (result != false)
                {
                    return this.Ok(new { Status = true, Message = "Deleted cart successfully" });
                }
                return this.BadRequest(new { Status = false, Message = "Data empty" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("UpdateCart")]
        public ActionResult UpdateCart(int cartid,int bookcount)
        {
            try
            {
                var userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.cartBusiness.UpdateCart(userid, cartid, bookcount);
                if (result !=0)
                {
                    return this.Ok(new { Status = true, Message = "Updated cart successfully" });
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
