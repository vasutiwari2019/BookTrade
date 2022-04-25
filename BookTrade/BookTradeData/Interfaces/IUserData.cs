using BookTrade.Models;
using System;
using System.Collections.Generic;

namespace BookTrade.BookTradeData
{
    // UserData Interface
    public interface IUserData
    {
        #region Methods
        List<User> GetUsers();

        User GetUsers(Guid id);

        User AddUser(User user);

        void DeleteUser(User user);

        User EditUser(User user);

        User LoginUser(Login login);

        Login UpdateUserPassword(Login login);
        #endregion
    }
}
