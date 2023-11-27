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
    public class CustomerDetailsRepository: ICustomerDetailsRepository
    {
        private SqlConnection con;
        public readonly IConfiguration configuration;
        private void connection()
        {
            string connectionstr = configuration.GetConnectionString("UserDbConnection");
            con = new SqlConnection(connectionstr);
        }
        public CustomerDetailsRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public bool AddToCustomerDetails(CustomerDetails cDetails)
        {
            try
            {
                connection();
                con.Open();
                SqlCommand com = new SqlCommand("spAddCustomerDetails", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@customername", cDetails.CustomerName);
                com.Parameters.AddWithValue("@phone", cDetails.Phone);
                com.Parameters.AddWithValue("@address", cDetails.Address);
                com.Parameters.AddWithValue("@city", cDetails.City);
                com.Parameters.AddWithValue("@state", cDetails.State);
                com.Parameters.AddWithValue("@typeid", cDetails.TypeId);
                com.Parameters.AddWithValue("@userid", cDetails.UserId);
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
        public List<CustomerDetails> GetCustomerDetails(int userId)
        {
            List<CustomerDetails> CustomerList = new List<CustomerDetails>();
            try
            {
                connection();
                SqlCommand com = new SqlCommand("spGetCustomerDetails", con);
                com.Parameters.AddWithValue("@userid", userId);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    CustomerList.Add(
                   new CustomerDetails
                   {
                       CustomerName = Convert.ToString(dr["customername"]),
                       Phone = Convert.ToString(dr["phone"]),
                       Address = Convert.ToString(dr["address"]),
                       City = Convert.ToString(dr["city"]),
                       State = Convert.ToString(dr["state"]),
                       TypeId = Convert.ToInt32(dr["typeid"]),
                       UserId = Convert.ToInt32(dr["userid"]),
                       Type = new BookStoreCommon.Model.Type()
                       {
                           TypeId = Convert.ToInt32(dr["typeid"]),
                           TypeName = Convert.ToString(dr["typename"])
                       }
                   });
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            if (CustomerList.Count > 0)
            {
                con.Close();
                return CustomerList;
            }
            else
            {
                con.Close();
                return null;
            }
        }
    }
}
