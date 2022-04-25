using BookTrade.Data;
using BookTrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookTrade.BookTradeData
{
    // UserData class for handling User related operation
    public class UserData : IUserData
    {
        #region Global variables
        private readonly BookTradeContext _bookTradeContext;
        #endregion

        #region Constructor
        public UserData(BookTradeContext bookTradeContext)
        {
            _bookTradeContext = bookTradeContext;
        }
        #endregion

        #region Public Methods

        // Method for adding a new User
        public User AddUser(User user)
        {
            var userfound = _bookTradeContext.Users.Where(x=>x.Email == user.Email).FirstOrDefault();
            if (userfound == null)
            {
                user.UserId = Guid.NewGuid();
                _bookTradeContext.Users.Add(user);
                _bookTradeContext.SaveChanges();
                return user;
            }

            return null;
        }

        // Method for deleting specified user
        public void DeleteUser(User user)
        {
            _bookTradeContext.Remove(user);
            _bookTradeContext.SaveChanges();
        }

        // Method for editing specified user
        public User EditUser(User user)
        {
            var existingUser = _bookTradeContext.Users.Find(user.UserId);

            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.Address = user.Address;
                _bookTradeContext.Users.Update(existingUser);
                _bookTradeContext.SaveChanges();
            }

            return user;
        }

        // Method for getting specified user
        public User GetUsers(Guid id)
        {
            return _bookTradeContext.Users.Find(id);
        }

        // Method for getting all users
        public List<User> GetUsers()
        {
            return _bookTradeContext.Users.ToList();
        }

        // Method for checking user login details
        public User LoginUser(Login login)
        {
            var emailFound = _bookTradeContext.Users.FirstOrDefault(x => x.Email == login.Email);
            if (emailFound != null)
            {
                var userFound =  _bookTradeContext.Users.Where(x => x.Email == login.Email && x.Password == login.Password).FirstOrDefault();

                return userFound ?? new User();
            }
            else
            {
                return null;
            }
        }

        // Method for updating user password
        public Login UpdateUserPassword(Login login)
        {
            var existingUser = _bookTradeContext.Users.Where(x => x.Email == login.Email).FirstOrDefault();

            if (existingUser != null)
            {
                if (existingUser.Password != login.Password)
                {
                    existingUser.Password = login.Password;
                    _bookTradeContext.Users.Update(existingUser);
                    _bookTradeContext.SaveChanges();

                    return login;
                }

                login.Password = null;
            }

            return null;
        }
        #endregion
    }
}
