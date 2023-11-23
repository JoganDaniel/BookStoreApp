using BookStoreCommon.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBussiness.IBussiness
{
    public interface IUserBussiness
    {
        public bool RegisterUser(User user);
        public string LoginUser(string email, string password);
        public string ForgetPassword(string Email);

        public User ResetPassword(string email, string newpassword, string confirmpassword);
    }
}
