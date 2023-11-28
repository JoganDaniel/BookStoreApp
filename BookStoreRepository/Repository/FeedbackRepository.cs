using BookStoreCommon.Model;
using BookStoreRepository.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private SqlConnection con;
        public readonly IConfiguration configuration;
        private void connection()
        {
            string connectionstr = configuration.GetConnectionString("UserDbConnection");
            con = new SqlConnection(connectionstr);
        }
        public FeedbackRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public CustomerFeedback AddToCustomerFeedback(CustomerFeedback feedback)
        {
            try
            {
                connection();
                con.Open();
                SqlCommand com = new SqlCommand("spAddCustomerFeedback", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@bookid", feedback.BookId);
                com.Parameters.AddWithValue("@userid", feedback.UserId);
                com.Parameters.AddWithValue("@description", feedback.Description);
                com.Parameters.AddWithValue("@rating",feedback.Rating);
  
                //con.Open();
                int i = com.ExecuteNonQuery();
                //con.Close();
                if (i != 0)
                {
                    return feedback;
                }
                else
                {
                    return null;
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
