using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace BookStoreManagementSystem.Helpers
{
    public static class PasswordHelper
    {
        public static void CreateHash(string password,
            out byte[] hash, out byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, 16, 10000))
            {
                salt = pbkdf2.Salt;
                hash = pbkdf2.GetBytes(32);
            }
        }

        public static bool Verify(string password,
            byte[] storedHash, byte[] storedSalt)
        {
            using (var pbkdf2 =
                new Rfc2898DeriveBytes(password, storedSalt, 10000))
            {
                var hash = pbkdf2.GetBytes(32);
                return hash.SequenceEqual(storedHash);
            }
        }
    }
}