using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using BookStoreCommon.Model;
using BookStoreRepository.IRepository;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace BookStoreRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration iconfiguration;
        private SqlConnection con;
        public string Key = "ankit@@sehrawat@@";
        public UserRepository(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        private void connection()
        {
            string connectionStr = iconfiguration.GetConnectionString("UserDbConnection");
            con = new SqlConnection(connectionStr);
        }

        public bool RegisterUser(User user)
        {
            var password = EncryptPassword(user.Password);
            user.Password = password;
            try
            {
                connection();
                SqlCommand com = new SqlCommand("AddUser", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@name", user.Name);
                com.Parameters.AddWithValue("@phone", user.Phone);
                com.Parameters.AddWithValue("@email", user.Email);
                com.Parameters.AddWithValue("@password", user.Password);
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

        public string LoginUser(string email, string password)
        {
            User user = GetTheUser(email);
            try
            {
                var decryptPassword = DecryptPassword(user.Password);
                if (user != null && decryptPassword.Equals(password))
                {
                    var token = GenerateSecurityToken(user.Email, user.Id);
                    return token;
                }
                return null;
            }
            catch (Exception)
            {
                //return null;
                throw;
            }
            finally { //con.Close();
                      }

        }
        public User GetTheUser(string email)
        {
            var objUser = new User();
            connection();
            SqlCommand com = new SqlCommand("GetUser", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@email", email);
            //SqlDataAdapter da = new SqlDataAdapter(com);
            SqlDataReader reader = com.ExecuteReader();

            if (reader.Read())
            {
                objUser = new User
                {
                    Id = (int)reader["id"],
                    Name = (string)reader["name"],
                    Email = (string)reader["email"],
                    Password = (string)reader["password"],
                    Phone = (string)reader["phone"]
                };
            }
            con.Close();
            return objUser;
            
        }
        public string ForgetPassword(string Email)
        {
            try
            {
                var emailcheck = GetTheUser(Email);
                if (emailcheck != null)
                {
                    var token = GenerateSecurityToken(emailcheck.Email, emailcheck.Id);
                    MSMQ msmq = new MSMQ();
                    msmq.sendData2Queue(token, Email);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public User ResetPassword(string email, string newpassword, string confirmpassword)
        {
            var user = GetTheUser(email);
            if (newpassword.Equals(confirmpassword))
            {
                var input = user;
                var password = EncryptPassword(newpassword);
                input.Password = password;
             }
                return null;
     
        }
        public string GenerateSecurityToken(string email, int userId)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.iconfiguration[("JWT:Key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("Id",userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            return tokenhandler.WriteToken(token);
        }
        public string EncryptPassword(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            //nlog.LogInfo("Password Encrypted");
            return strmsg;
        }
        public string DecryptPassword(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new string(decoded_char);
            //nlog.LogInfo("Password Decrypted");
            return decryptpwd;
        }

        
    }
}
