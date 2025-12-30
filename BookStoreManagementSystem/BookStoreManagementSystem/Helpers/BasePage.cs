using System;
using System.Security.Claims;
using System.Web.UI;

namespace BookStoreManagementSystem.Helpers
{
    public class BasePage : Page
    {
        protected override void OnLoad(EventArgs e)
        {
            var cookie = Request.Cookies["JWT_TOKEN"];
            if (cookie == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            try
            {
                var principal =
                    JwtHelper.ValidateToken(cookie.Value);

                Context.User = principal;
            }
            catch
            {
                Response.Redirect("~/Login.aspx");
            }

            base.OnLoad(e);
        }
    }
}
