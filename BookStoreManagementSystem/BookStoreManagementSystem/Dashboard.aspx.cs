using System;
using BookStoreManagementSystem.Data;

namespace BookStoreManagementSystem
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("Login.aspx");

            lblUser.Text = Session["UserName"].ToString();

            using (var db = new BookStoreContext())
            {
                int userId = (int)Session["UserId"];
                var user = db.Users.Find(userId);

                if (user != null && user.IsAdmin)
                {
                    pnlAdmin.Visible = true;
                }
            }
        }
    }
}
