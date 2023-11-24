using BookStoreCommon.Model;
using BookStoreRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace BookStoreRepository.Repository
{
    public class BookRepository : IBookRepository                                                       
    {
        private readonly IConfiguration configuration;
        SqlConnection con;
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
        
    }
}
