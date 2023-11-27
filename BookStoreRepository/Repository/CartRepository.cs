using BookStoreCommon.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using BookStoreRepository.IRepository;

namespace BookStoreRepository.Repository
{
    public class CartRepository : ICartRepository
    {
        private SqlConnection con;
        public readonly IConfiguration configuration;
        private void connection()
        {
            string connectionstr = configuration.GetConnectionString("UserDbConnection");
            con = new SqlConnection(connectionstr);
        }
        public CartRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public bool AddToCart(int bookId, int userId)
        {
            try
            {
                connection();
                con.Open();
                SqlCommand com = new SqlCommand("spAddCart", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@bookid", bookId);
                com.Parameters.AddWithValue("@userid", userId);
                //con.Open();
                int i = com.ExecuteNonQuery();
                //con.Close();
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
        public List<Cart> GetCart(int userId)
        {
            List<Cart> CartList = new List<Cart>();
            try
            {
                connection();
                SqlCommand com = new SqlCommand("spGetCart", con);
                com.Parameters.AddWithValue("@userid", userId);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    CartList.Add(
                   new Cart
                   {
                       BookId = Convert.ToInt32(dr["bookid"]),
                       CartId = Convert.ToInt32(dr["cartid"]),
                       UserId = Convert.ToInt32(dr["userid"]),
                       Book = new Book()
                       {
                           BookId = Convert.ToInt32(dr["bookid"]),
                           Bookname = Convert.ToString(dr["bookname"]),
                           BookDescription = Convert.ToString(dr["bookdecription"]),
                           BookAuthor = Convert.ToString(dr["bookauthor"]),
                           Image = Convert.ToString(dr["image"]),
                           BookCount = Convert.ToInt32(dr["bookcount"]),
                           BookPrice = Convert.ToDouble(dr["bookprice"]),
                           Rating = Convert.ToDouble(dr["rating"])
                       }
                   });
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            if (CartList.Count > 0)
            {
                con.Close();
                return CartList;
            }
            else
            {
                con.Close();
                return null;
            }
        }
        public bool DeleteCart(int cartid)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("spDeleteCart", con);
                com.Parameters.AddWithValue("@cartid", cartid);
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
    }
}
