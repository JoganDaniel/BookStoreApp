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
        public List<CustomerFeedback> GetAllFeedback(int bookid)
        {
            List<CustomerFeedback> feedbackList = new List<CustomerFeedback>();
            try
            {
                connection();
                SqlCommand com = new SqlCommand("spGetFeedback", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@bookid", bookid);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
        
                foreach (DataRow dr in dt.Rows)
                {
                    feedbackList.Add(  
                   new CustomerFeedback
                   {
                       FeedbackId = Convert.ToInt32(dr["feedbackid"]),
                       UserId = Convert.ToInt32(dr["userid"]),
                       BookId = Convert.ToInt32(dr["bookid"]),
                       Description = Convert.ToString(dr["description"]),
                       Rating = Convert.ToDouble(dr["rating"]),
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
                       User = new User()
                       {
                           Name = Convert.ToString(dr["name"])
                       }
                   }
                   );
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            if (feedbackList.Count > 0)
            {
                return feedbackList;
            }
            else
            {
                return null;
            }
        }
    }
}
