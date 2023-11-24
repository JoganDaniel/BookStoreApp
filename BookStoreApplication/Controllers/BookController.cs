using BookStoreBussiness.IBussiness;
using BookStoreCommon.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System;
using System.Threading.Tasks;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        public readonly IBookBussiness bookBussiness;
         public BookController(IBookBussiness bookBussiness)
        {
            this.bookBussiness = bookBussiness; 
        }
        [HttpPost]
        [Route("AddBook")]
        public Task<ActionResult> AddBook(Book book)
        {
            try
            {
                var result = this.bookBussiness.AddBook(book);
                if (result == true)
                {
                    return Task.FromResult<ActionResult>(this.Ok(new { Status = true, Message = "book added successfully", Data = book }));
                }
                return Task.FromResult<ActionResult>(this.BadRequest(new { Status = false, Message = "book adding Unsuccessful" }));
            }
            catch (Exception ex)
            {
                return Task.FromResult<ActionResult>(this.NotFound(new { Status = false, Message = ex.Message }));
            }
        }
        //[HttpGet]
        //[Route("GetAllBooks")]
        //public async Task<ActionResult> GetAllBooks()
        //{
        //    try
        //    {
        //        var result = this.bookBussiness.GetAllBooks();
        //        if (result != null)
        //        {
        //            return this.Ok(new { Status = true, Message = "Books retrieved", data = result });
        //        }
        //        return this.BadRequest(new { Status = false, Message = "No books Found" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.NotFound(new { Status = false, Message = ex.Message });
        //    }
        //}
    }
}
