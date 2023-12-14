using BookStoreBussiness.IBussiness;
using BookStoreCommon.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Routing;
using NLogImplementation;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FeedbackController : ControllerBase
    {
        Nlog nLog = new Nlog();
        public readonly IFeedbackBusiness feedbackBusiness;

        public FeedbackController(IFeedbackBusiness feedbackBusiness)
        {
            this.feedbackBusiness = feedbackBusiness;
        }
        [HttpPost]
        [Route("AddToFeedback")]
        public Task<ActionResult> AddFeedback(CustomerFeedback feedback)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                feedback.UserId = userId;
                var result = this.feedbackBusiness.AddToCustomerFeedback(feedback);
                if (result !=null)
                {
                    return Task.FromResult<ActionResult>(this.Ok(new { Status = true, Message = "Customer feedback added successfully", Data = "success" }));
                }
                return Task.FromResult<ActionResult>(this.BadRequest(new { Status = false, Message = "feedback adding Unsuccessful" }));
            }
            catch (Exception ex)
            {
                return Task.FromResult<ActionResult>(this.NotFound(new { Status = false, Message = ex.Message }));
            }
        }
        [HttpGet]
        [Route("GetFeedback")]
        public Task<ActionResult> GetFeedback(int bookid)
        {
            try
            {
                //var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.feedbackBusiness.GetAllFeedback(bookid);
                if (result != null)
                {
                    nLog.LogInfo("Feedback got successfully");
                    return Task.FromResult<ActionResult>(this.Ok(new { Status = true, Message = "feedback details retrieved", data = result }));
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
