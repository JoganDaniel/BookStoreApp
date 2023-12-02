using BookStoreBussiness.IBussiness;
using BookStoreCommon.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using NLogImplementation;
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
        public string ImageUrl = null;
        Nlog nlog = new Nlog();
         public BookController(IBookBussiness bookBussiness)
        {
            this.bookBussiness = bookBussiness; 
        }
        [HttpPost]
        [Route("AddBook")]
        [Authorize(Roles ="Admin")]
        public Task<ActionResult> AddBook(Book book)
        {
            try
            {
               // book.Image = this.ImageUrl;
                var result = this.bookBussiness.AddBook(book);
                if (result == true)
                {
                    nlog.LogInfo("book added Successfully");
                    return Task.FromResult<ActionResult>(this.Ok(new { Status = true, Message = "book added successfully", Data = book }));
                }
                return Task.FromResult<ActionResult>(this.BadRequest(new { Status = false, Message = "book adding Unsuccessful" }));
            }
            catch (Exception ex)
            {
                return Task.FromResult<ActionResult>(this.NotFound(new { Status = false, Message = ex.Message }));
            }
        }
        [HttpGet]
        [Route("GetAllBooks")]
        public async Task<ActionResult> GetAllBooks()
        {
            try
            {
                var result = this.bookBussiness.GetAllBooks();
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Books retrieved", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No books Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("EditBook")]
        [Authorize(Roles = "Admin")]

        public ActionResult EditBook(Book book)
        {
            try
            {
                //book.Image = this.ImageUrl;
                var result = this.bookBussiness.EditBook(book);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Edit Task Successful", data = book });
                }
                return this.BadRequest(new { Status = false, Message = "Notes found empty" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("UploadImage")]
        public ActionResult AddBook(IFormFile file,int bookId)
        {
            try
            {
                var result = this.bookBussiness.UploadImage(file, bookId);
                if (result != null)
                {
                    //this.ImageUrl = result.ToString();
                    return this.Ok(new { Status = true, Message = "image added", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("DeleteBook")]
        [Authorize(Roles = "Admin")]

        public ActionResult DeleteBook(int bookId)
        {
            try
            {
                var result = this.bookBussiness.DeleteBook(bookId);
                if (result != false)
                {
                    return this.Ok(new { Status = true, Message = "Deleted book" });
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
