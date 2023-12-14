using BookStoreCommon.Model;
using BookStoreRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Linq;
using NLogImplementation;

namespace BookStoreRepository.Repository
{
    public class BookRepository : IBookRepository                                                       
    {
        private readonly IConfiguration configuration;
        SqlConnection con;
        Nlog nlog = new Nlog();
        public readonly IDistributedCache distributedCache;
        public BookRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        private void connection()
        {
            string connectionStr = configuration.GetConnectionString("UserDbConnection");
            con = new SqlConnection(connectionStr);
        }

        public bool AddBook(Book book)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("spAddBook", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@bookname", book.Bookname);
                com.Parameters.AddWithValue("@bookdecription", book.BookDescription);
                com.Parameters.AddWithValue("@bookauthor", book.BookAuthor);
                com.Parameters.AddWithValue("@image", book.Image);
                com.Parameters.AddWithValue("@bookcount", book.BookCount);
                com.Parameters.AddWithValue("@bookprice", book.BookPrice);
                com.Parameters.AddWithValue("@rating", book.Rating);
                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }
        public List<Book> GetAllBooks()
        {
            List<Book> ListBook = new List<Book>();
            try
            {
                connection();
                SqlCommand com = new SqlCommand("spGetAllBooks", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                //var cacheResult = GetListFromCache("bookList");
                //if (cacheResult!=null)
                //{
                //    nlog.LogDebug("cache list got");
                //    return cacheResult;
                //}
                foreach (DataRow dr in dt.Rows)
                {
                    ListBook.Add(
                   new Book
                   {
                       BookId = Convert.ToInt32(dr["bookid"]),
                       Bookname = Convert.ToString(dr["bookname"]),
                       BookDescription = Convert.ToString(dr["bookdecription"]),
                       BookAuthor = Convert.ToString(dr["bookauthor"]),
                       Image = Convert.ToString(dr["image"]),
                       BookCount = Convert.ToInt32(dr["bookcount"]),
                       BookPrice = Convert.ToDouble(dr["bookprice"]),
                       Rating = Convert.ToDouble(dr["rating"]),
                   });
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            if (ListBook.Count > 0)
            {
                //PutListToCache(ListBook);
                return ListBook;
            }
            else
            {
                return null;
            }
        }
        public Book EditBook(Book book)
        {
            var book1 = GetBook(book.BookId);
           
                if (book1 != null)
                {
                connection();
                con.Open();
                SqlCommand com = new SqlCommand("EditBook", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@bookid", book.BookId);
                com.Parameters.AddWithValue("@bookname", book.Bookname);
                com.Parameters.AddWithValue("@bookdecription", book.BookDescription);
                com.Parameters.AddWithValue("@bookauthor", book.BookAuthor);
                com.Parameters.AddWithValue("@image", book.Image);
                com.Parameters.AddWithValue("@bookcount", book.BookCount);
                com.Parameters.AddWithValue("@bookprice", book.BookPrice);
                com.Parameters.AddWithValue("@rating", book.Rating);
                
                    int i = com.ExecuteNonQuery();
                    
                    if (i != 0)
                    {
                        return book;
                    }
                    else
                    {
                        return null;
                    }
            }
            con.Close();
            return null;
        }
        public Book GetBook(int bookId)
        {
            var book = new Book();
            connection();
            con.Open();
            SqlCommand com = new SqlCommand("spGetBook", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@bookid", bookId);
            //SqlDataAdapter da = new SqlDataAdapter(com);
            SqlDataReader reader = com.ExecuteReader();

            if (reader.Read())
            {
                book = new Book
                {
                    BookId = (int)reader["bookid"],
                    Bookname =(string)reader["bookname"],
                    BookDescription = (string)reader["bookdecription"],
                    BookAuthor = (string)reader["bookauthor"],
                    Image = (string)reader["image"],
                    BookCount = (int)reader["bookcount"],
                    BookPrice = (double)reader["bookprice"],
                    Rating = (double)reader["rating"]
                };
            }
            con.Close();
            return book;
        }
        public string UploadImage(IFormFile file, int bookId)
        {
            try
            {
                if (file == null)
                {
                    return null;
                }
                var stream = file.OpenReadStream();
                var name = file.FileName;
                Account account = new Account("din6haoa4", "776115924597624", "rn41eF0fTqTN_7IMKefa2NycM7I");
                Cloudinary cloudinary = new Cloudinary(account);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(name, stream)
                };
                ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
                cloudinary.Api.UrlImgUp.BuildUrl(String.Format("{0}.{1}", uploadResult.PublicId, uploadResult.Format));
                var cloudnaryfilelink = uploadResult.Uri.ToString();
                UpdateImageUrl(cloudnaryfilelink, bookId);
                return cloudnaryfilelink;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateImageUrl(string url,int bookId)
        {
            Book book5 = GetBook(bookId);
            book5.Image = url;
            EditBook(book5);
        }
        public bool DeleteBook(int bookId)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("spDeleteBook", con);
                com.Parameters.AddWithValue("@bookid", bookId);
                com.CommandType = CommandType.StoredProcedure;
                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);   
            }
        }
        public void PutListToCache(List<Book> books)
        {
            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));
            var jsonString = JsonConvert.SerializeObject(books);
            distributedCache.SetString("bookList", jsonString, options);
        }
        public List<Book> GetListFromCache(string key)
        {
            var cacheString = this.distributedCache.GetString(key);
            return JsonConvert.DeserializeObject<IEnumerable<Book>>(cacheString).ToList();
        }
    }
}
