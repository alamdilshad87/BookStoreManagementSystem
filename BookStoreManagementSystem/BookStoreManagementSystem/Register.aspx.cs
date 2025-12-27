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
            byte[] hash, salt;
            PasswordHelper.CreateHash(txtPassword.Text, out hash, out salt);

            using (var db = new BookStoreContext())
            {
                if (db.Users.Any(u => u.Email == txtEmail.Text))
                {
                    lblMessage.Text = "Email already registered";
                    return;
                }

                db.Users.Add(new User
                {
                    FullName = txtName.Text,
                    Email = txtEmail.Text,
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    CreatedAt = DateTime.Now
                });

                db.SaveChanges();
            }

            Response.Redirect("Login.aspx");
        }
    }
}
