using BookStoreBussiness.IBussiness;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;
using BookStoreCommon.Model;

namespace BookStoreApplication.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        public readonly ICustomerDetailsBusiness customerBusiness;

        public CustomerController(ICustomerDetailsBusiness customerBusiness)
        {
            this.customerBusiness = customerBusiness;
        }
        [HttpPost]
        [Route("AddCustomer")]
        public Task<ActionResult> AddCustomer(CustomerDetails cDetails)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                cDetails.UserId=userId;
                var result = this.customerBusiness.AddToCustomerDetails(cDetails);
                if (result == true)
                {
                    return Task.FromResult<ActionResult>(this.Ok(new { Status = true, Message = "Customer added successfully", Data = "success" }));
                }
                return Task.FromResult<ActionResult>(this.BadRequest(new { Status = false, Message = "cart adding Unsuccessful" }));
            }
            catch (Exception ex)
            {
                return Task.FromResult<ActionResult>(this.NotFound(new { Status = false, Message = ex.Message }));
            }
        }
        [HttpGet]
        [Route("GetCustomerDetails")]
        public Task<ActionResult> GetcustomerDetails()
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.customerBusiness.GetCustomerDetails(userId);
                if (result != null)
                {
                    return Task.FromResult<ActionResult>(this.Ok(new { Status = true, Message = "Customer details retrieved", data = result }));
                }
                return Task.FromResult<ActionResult>(this.BadRequest(new { Status = false, Message = "No customers Found" }));
            }
            catch (Exception ex)
            {
                return Task.FromResult<ActionResult>(this.NotFound(new { Status = false, Message = ex.Message }));
            }
        }
        [HttpPut]
        [Route("DeleteCustomer")]
        public ActionResult DeleteCustomer(int customerid)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.customerBusiness.DeleteAddress(userId, customerid);
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
        [Route("EditAddress")]
        public ActionResult EditAddress(CustomerDetails details)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                //book.Image = this.ImageUrl;
                var result = this.customerBusiness.EditAddress(userId, details);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Edit Task Successful", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Notes found empty" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
