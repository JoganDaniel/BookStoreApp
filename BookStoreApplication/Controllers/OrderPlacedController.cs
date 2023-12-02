using BookStoreBussiness.IBussiness;
using BookStoreCommon.Model;
using BookStoreRepository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderPlacedController : ControllerBase
    {
        public readonly IOrderBusiness orderBusiness;

        public OrderPlacedController(IOrderBusiness orderBusiness)
        {
            this.orderBusiness = orderBusiness;
        }
        [HttpPost]
        [Route("PlaceOrder")]
        public Task<ActionResult> AddFeedback(int customerid,int cartid)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.orderBusiness.PlaceOrder(customerid, cartid,userId);
                if (result != 0)
                {
                    return Task.FromResult<ActionResult>(this.Ok(new { Status = true, Message = "Order placed successfully", Data = "success" }));
                }
                return Task.FromResult<ActionResult>(this.BadRequest(new { Status = false, Message = "order Unsuccessful" }));
            }
            catch (Exception ex)
            {
                return Task.FromResult<ActionResult>(this.NotFound(new { Status = false, Message = ex.Message }));
            }
        }
    }
}
