using System;
using System.Linq;
using BookStoreManagementSystem.Data;
using BookStoreManagementSystem.Helpers;
using BookStoreManagementSystem.Models;

namespace BookStoreManagementSystem
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Register_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            // 🔐 Basic validation
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                lblMessage.Text = "All fields are required.";
                return;
            }

            if (password.Length < 6)
            {
                lblMessage.Text =
                    "Password must be at least 6 characters.";
                return;
            }

            byte[] hash, salt;
            PasswordHelper.CreateHash(password, out hash, out salt);

            using (var db = new BookStoreContext())
            {
                if (db.Users.Any(u => u.Email == email))
                {
                    lblMessage.Text = "Email already registered.";
                    return;
                }

                db.Users.Add(new User
                {
                    FullName = name,
                    Email = email,
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    CreatedAt = DateTime.Now,
                    IsAdmin = false
                });

                db.SaveChanges();
            }

            Response.Redirect("Login.aspx");
        }
    }
}
