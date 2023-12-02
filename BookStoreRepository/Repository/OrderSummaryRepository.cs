using BookStoreCommon.Model;
using BookStoreRepository.IRepository;
using Microsoft.Extensions.Configuration;
using NLogImplementation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class OrderSummaryRepository : IOrderSummaryRepository
    {
        private readonly IConfiguration configuration;
        SqlConnection con;
        Nlog nlog = new Nlog();
        public OrderSummaryRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        private void connection()
        {
            string connectionStr = configuration.GetConnectionString("UserDbConnection");
            con = new SqlConnection(connectionStr);
        }
        public IEnumerable<OrderSummary> GetOrderSummary(int userid)
        {
            try
            {
                connection();
                List<OrderSummary> summaryOrder = new List<OrderSummary>();
                SqlCommand com = new SqlCommand("spOrderSummary", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserId", userid);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    summaryOrder.Add(
                        new OrderSummary()
                        {
                            SummaryId = Convert.ToInt32(dr["summaryid"]),
                            OrderId = Convert.ToInt32(dr["orderid"]),
                            OrderPlaced = new OrderPlaced()
                            {
                                OrderId = Convert.ToInt32(dr["orderid"]),
                                CustomerId = Convert.ToInt32(dr["customerid"]),
                                CartId = Convert.ToInt32(dr["cartid"]),
                                UserId = Convert.ToInt32(dr["userid"]),
                                Cart = new Cart()
                                {
                                    BookCount = Convert.ToInt32(dr["bookcount"]),
                                    Book = new Book()
                                    {
                                        BookId = Convert.ToInt32(dr["bookid"]),
                                        Bookname = Convert.ToString(dr["bookname"]),
                                        BookDescription = Convert.ToString(dr["bookdecription"]),
                                        BookAuthor = Convert.ToString(dr["bookauthor"]),
                                        Image = Convert.ToString(dr["image"]),
                                        BookCount = Convert.ToInt32(dr["bookcount"]),
                                        BookPrice = Convert.ToInt32(dr["bookprice"]),
                                        Rating = Convert.ToInt32(dr["rating"])

                                    },

                                }
                            }
                        }
                        );
                }
                nlog.LogDebug("Got Order Summary");
                return summaryOrder;
            }
            catch (Exception ex)
            {
                nlog.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
