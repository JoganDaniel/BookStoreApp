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
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfiguration configuration;
        SqlConnection con;
        public OrderRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        private void connection()
        {
            string connectionStr = configuration.GetConnectionString("UserDbConnection");
            con = new SqlConnection(connectionStr);
        }
        public int PlaceOrder(int cartid,int customerid,int userid)
        {
            try
            {
                connection();
                con.Open();
                SqlCommand com = new SqlCommand("spPlaceOrder", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@customerid", customerid);
                com.Parameters.AddWithValue("@cartid", cartid);
                  //con.Open();
                int i = com.ExecuteNonQuery();
                //con.Close();
                return i;
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
