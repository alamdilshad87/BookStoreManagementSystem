using System;
using System.Linq;
using BookStoreManagementSystem.Data;
using BookStoreManagementSystem.Helpers;

namespace BookStoreManagementSystem
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Login_Click(object sender, EventArgs e)
        {
            using (var db = new BookStoreContext())
            {
                var user = db.Users
                    .FirstOrDefault(u => u.Email == txtEmail.Text);

                if (user == null || !PasswordHelper.Verify(txtPassword.Text,user.PasswordHash,user.PasswordSalt))
                {
                    lblMessage.Text = "Invalid email or password";
                    return;
                }

                Session["UserId"] = user.UserId;
                Session["UserName"] = user.FullName;
                Session["IsAdmin"] = user.IsAdmin;

                Response.Redirect("Dashboard.aspx");
            }
        }
    }
}
