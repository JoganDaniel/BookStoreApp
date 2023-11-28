using BookStoreCommon.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class OrderSummaryRepository
    {
        private readonly IConfiguration configuration;
        SqlConnection con;
        public OrderSummaryRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        private void connection()
        {
            string connectionStr = configuration.GetConnectionString("UserDbConnection");
            con = new SqlConnection(connectionStr);
        }
        public IEnumerable<OrderSummary> GetOrderSummary()
        {
            return null;
        }
    }
}
