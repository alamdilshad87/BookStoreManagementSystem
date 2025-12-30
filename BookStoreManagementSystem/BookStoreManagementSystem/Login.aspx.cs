using BookStoreManagementSystem.Data;
using BookStoreManagementSystem.Helpers;
using System;
using System.Linq;
using System.Web;

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

                if (user == null ||
                    !PasswordHelper.Verify(
                        txtPassword.Text,
                        user.PasswordHash,
                        user.PasswordSalt))
                {
                    lblMessage.Text = "Invalid email or password";
                    return;
                }

                string token = JwtHelper.GenerateToken(
                    user.UserId,
                    user.Email,
                    user.IsAdmin);

                HttpCookie jwtCookie =
                    new HttpCookie("JWT_TOKEN", token)
                    {
                        HttpOnly = true,
                        Expires = DateTime.Now.AddHours(1)
                    };

                Response.Cookies.Add(jwtCookie);
                Response.Redirect("Dashboard.aspx");
            }
        }
    }
}
