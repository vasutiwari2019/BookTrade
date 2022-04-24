using BookTrade.Data;
using BookTrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookTrade.BookTradeData
{
    public class UserData : IUserData
    {
        private readonly BookTradeContext _bookTradeContext;

        public UserData(BookTradeContext bookTradeContext)
        {
            _bookTradeContext = bookTradeContext;
        }

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

        public void DeleteUser(User user)
        {
            _bookTradeContext.Remove(user);
            _bookTradeContext.SaveChanges();
        }

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

        public User GetUsers(Guid id)
        {
            var user = _bookTradeContext.Users.Find(id);

            return user;
        }

        public List<User> GetUsers()
        {
            return _bookTradeContext.Users.ToList();
        }

        public User LoginUser(Login user)
        {
            var userFound = _bookTradeContext.Users.Where(x=> x.Email == user.Email && x.Password == user.Password).FirstOrDefault();

            return userFound != null ? userFound : null;
        }

        public Login UpdateUserPassword(Login user)
        {
            var existingUser = _bookTradeContext.Users.Where(x => x.Email == user.Email).FirstOrDefault();

            if (existingUser != null)
            {
                if (existingUser.Password != user.Password)
                {
                    existingUser.Password = user.Password;
                    _bookTradeContext.Users.Update(existingUser);
                    _bookTradeContext.SaveChanges();

                    return user;
                }

                user.Password = null;
            }

            return null;
        }
    }
}
