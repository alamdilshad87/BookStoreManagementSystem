using System;

namespace BookStoreManagementSystem
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Clear all session data
            Session.Clear();
            Session.Abandon();

            // Optional: prevent back button access
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            // Redirect to Login
            Response.Redirect("Login.aspx");
        }
    }
}
