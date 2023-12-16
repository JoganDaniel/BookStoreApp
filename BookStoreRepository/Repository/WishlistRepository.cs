using BookStoreCommon.Model;
using BookStoreRepository.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class WishlistRepository : IWishlistRepository
    {
        private SqlConnection con;
        public readonly IConfiguration configuration;
        private void connection()
        {
            string connectionstr = configuration.GetConnectionString("UserDbConnection");
            con = new SqlConnection(connectionstr);
        }
        public WishlistRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public bool AddToWishlist(int bookId,int userId)
        {
            var a = GetWishListByBook(userId, bookId);
            if (a == null)
            {
                try
                {
                    connection();
                    con.Open();
                    SqlCommand com = new SqlCommand("spAddWishlist", con);
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
            else { return false; }

        }
        public List<Wishlist> GetWishList(int userId)
        {
            List<Wishlist> WishlistList = new List<Wishlist>();
            try
            {
                connection();
                SqlCommand com = new SqlCommand("spGetWishList", con);
                com.Parameters.AddWithValue("@userid", userId);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    WishlistList.Add(
                   new Wishlist
                   {
                       BookId = Convert.ToInt32(dr["bookid"]),
                       WishlistId = Convert.ToInt32(dr["wishlistid"]),
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
                }) ;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            if (WishlistList.Count > 0)
            {
                con.Close();
                return WishlistList;
            }
            else
            {
                con.Close ();
                return null;
            }
        }

        public List<Wishlist> GetWishListByBook(int userId,int bookid)
        {
            List<Wishlist> WishlistList = new List<Wishlist>();
            try
            {
                connection();
                SqlCommand com = new SqlCommand("spGetWishListbyBook", con);
                com.Parameters.AddWithValue("@userid", userId);
                com.Parameters.AddWithValue("@bookid", bookid);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    WishlistList.Add(
                   new Wishlist
                   {
                       BookId = Convert.ToInt32(dr["bookid"]),
                       WishlistId = Convert.ToInt32(dr["wishlistid"]),
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
            if (WishlistList.Count > 0)
            {
                con.Close();
                return WishlistList;
            }
            else
            {
                con.Close();
                return null;
            }
        }
        public int MoveToCart(Wishlist wishlist)
        {
            try
            {
                CartRepository cartRepo = new CartRepository(configuration);
                var a=cartRepo.AddToCart(wishlist.BookId, wishlist.UserId, 1);
                var b=DeleteWishlist(wishlist.WishlistId);
                if (a && b)
                {
                    return 7;
                }
                return 0;
            }
            catch(Exception e) {
                return -1;
            }
            }
        public bool DeleteWishlist(int wishlistid)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("spDeleteWishList", con);
                com.Parameters.AddWithValue("@wishlistid", wishlistid);
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