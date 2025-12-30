using BookStoreManagementSystem.Helpers;
using System;
using System.Security.Claims;

namespace BookStoreManagementSystem
{
    public partial class Dashboard : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var identity = (ClaimsIdentity)Context.User.Identity;
            lblUser.Text = identity.FindFirst(ClaimTypes.Email)?.Value;

            if (User.IsInRole("Admin"))
            {
                pnlAdmin.Visible = true;
            }
        }
    }
}
