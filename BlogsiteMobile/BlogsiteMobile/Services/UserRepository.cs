using BlogsiteMobile.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BlogsiteMobile.Services
{
    public class UserRepository
    {
        private SQLiteConnection _connection;

        public UserRepository(string databasePath)
        {
            _connection = new SQLiteConnection(databasePath);
            _connection.CreateTable<User>();
        }

        public User FindUser(string userName, string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashedPasswordBytes = new SHA256Managed().ComputeHash(passwordBytes);
            string hashedPassword = Convert.ToBase64String(hashedPasswordBytes);
            User user = _connection.Table<User>().FirstOrDefault(u => u.Name == userName && u.Password == hashedPassword);
            if (user != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity("FormsAuthentication");
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                Thread.CurrentPrincipal = principal;
                App.Current.Properties["IsLoggedIn"] = true;
                App.Current.SavePropertiesAsync();
            }
            return user;
        }

        public int CreateUser(User user)
        {
            return _connection.Insert(user);
        }

        public bool getUserInfo(string userName, string email) 
        {
            User user = _connection.Table<User>().FirstOrDefault(u => u.Name == userName || u.Email == email);
            if (user != null)
            {
                return false;
            }
            return true;
        }

    }
}
