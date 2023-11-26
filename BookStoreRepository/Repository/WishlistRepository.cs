using BookStoreCommon.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class WishlistRepository
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
        public bool AddToWishlist(Wishlist wlist)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("spAddWishlist", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@bookid", wlist.BookId);
                com.Parameters.AddWithValue("@userid", wlist.UserId);
                com.Parameters.AddWithValue("@bookcount", wlist.BookCount);
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
        public List<Wishlist> GetWishList(int userId)
        {
            List<Wishlist> WishlistList = new List<Wishlist>();
            try
            {
                connection();
                SqlCommand com = new SqlCommand("spGetWishList", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    WishlistList.Add(
                   new Wishlist
                   {
                       BookId = Convert.ToInt32(dr["bookid"]),
                       WishlistId = Convert.ToInt32(dr["wishlistid"]),
                       UserId = Convert.ToInt32(dr["userid"]),
                       BookCount = Convert.ToInt32(dr["bookcount"]),
                       
                   });
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            if (WishlistList.Count > 0)
            {
                return WishlistList;
            }
            else
            {
                return null;
            }
        }
    }
}
