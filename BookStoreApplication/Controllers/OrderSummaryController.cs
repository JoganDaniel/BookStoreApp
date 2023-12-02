using BookStoreBussiness.IBussiness;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;
using NLogImplementation;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderSummaryController : ControllerBase
    {
        public readonly IOrderSummaryBusiness orderSummaryBusiness;
        Nlog nlog = new Nlog();
        public OrderSummaryController(IOrderSummaryBusiness orderSummaryBusiness)
        {
            this.orderSummaryBusiness = orderSummaryBusiness;
        }
        [HttpGet]
        [Route("OrderSummary")]
        public Task<ActionResult> GetOrderSummary()
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.orderSummaryBusiness.GetOrderSummary(userId);
                if (result != null)
                {
                    nlog.LogInfo("Order summary got successfully");
                    return Task.FromResult<ActionResult>(this.Ok(new { Status = true, Message = "summary details retrieved", data = result }));
                }
                return Task.FromResult<ActionResult>(this.BadRequest(new { Status = false, Message = "Not Found" }));
            }
            catch (Exception ex)
            {
                return Task.FromResult<ActionResult>(this.NotFound(new { Status = false, Message = ex.Message }));
            }
        }
    }
}
