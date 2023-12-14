using BookStoreBussiness.IBussiness;
using BookStoreCommon.Model;
using BookStoreRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBussiness.Bussiness
{
    public class UserBussiness : IUserBussiness
    {
        public readonly IUserRepository userRepository;
        public UserBussiness(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public bool RegisterUser(User user)
        {
            var result = this.userRepository.RegisterUser(user);
            return result;
        }
        public string LoginUser(string email, string password)
        {
            return this.userRepository.LoginUser(email, password);
        }
        public User ResetPassword(string email, string newpassword, string confirmpassword)
        {
            var result = this.userRepository.ResetPassword(email,newpassword,confirmpassword);
            return result;
        }
        public string ForgetPassword(string Email)
        {
                return this.userRepository.ForgetPassword(Email);
 
        }
    }
}
