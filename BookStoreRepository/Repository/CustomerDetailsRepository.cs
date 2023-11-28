using BookStoreCommon.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using BookStoreRepository.IRepository;
using System.Net;
using System.Numerics;

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
        
        public IEnumerable<CustomerDetails> GetCustomerDetails(int userid)
        {
            try
            {
                connection();
                //nlog.LogDebug("Attempting to get all Personal details ");
                  List<CustomerDetails> details = new List<CustomerDetails>();
                SqlCommand com = new SqlCommand("spGetCustomerDetails", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                com.Parameters.AddWithValue("@userid", userid);
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    details.Add(
                       new CustomerDetails

                       {
                           CustomerId = Convert.ToInt32(dr["id"]),
                           CustomerName = Convert.ToString(dr["customername"]),
                           Phone = Convert.ToString(dr["phone"]),
                           Address = Convert.ToString(dr["address"]),
                           City = Convert.ToString(dr["city"]),
                           State = Convert.ToString(dr["state"]),
                           TypeId = Convert.ToInt32(dr["typeid"]),
                           UserId = Convert.ToInt32(dr["userid"]),
                           Type = new AddressType()
                           {
                               TypeName = Convert.ToString(dr["typename"]),
                               TypeId = Convert.ToInt32(dr["typeid"])
                           }
                       }
                       );
                }
                return details;
            }
            catch (Exception ex)
            {
                //nlog.LogError(ex.Message);

                throw new Exception(ex.Message);
            }
        }
        public CustomerDetails EditAddress(int userId,CustomerDetails details)
        {
            var cDetails = GetCustomerDetails(userId);
            if (cDetails != null)
            {
                connection();
                con.Open();
                SqlCommand com = new SqlCommand("spEditCustomer", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@userid", userId);
                com.Parameters.AddWithValue("@customerid", details.CustomerId);
                com.Parameters.AddWithValue("@address", details.Address);
                com.Parameters.AddWithValue("@customername", details.CustomerName);
                com.Parameters.AddWithValue("@phone", details.Phone);
                com.Parameters.AddWithValue("@city", details.City);
                com.Parameters.AddWithValue("@state", details.State);
                com.Parameters.AddWithValue("@typeid", details.TypeId);
                int i = com.ExecuteNonQuery();

                if (i != 0)
                {
                    return details;
                }
                else
                {
                    return null;
                }
            }
            con.Close();
            return null;
        }
        public bool DeleteAddress(int userId,int customerid)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("spDeleteCustomer", con);
                com.Parameters.AddWithValue("@userid", userId);
                com.Parameters.AddWithValue("@customerid", customerid);
                com.CommandType = CommandType.StoredProcedure;
                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
