using System;
using System.Linq;
using BookStoreManagementSystem.Data;

namespace BookStoreManagementSystem
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("Login.aspx");

            using (var db = new BookStoreContext())
            {
                gvCart.DataSource = db.Carts
                    .Where(c => c.UserId == (int)Session["UserId"])
                    .ToList();
                gvCart.DataBind();
            }
        }
    }
}
