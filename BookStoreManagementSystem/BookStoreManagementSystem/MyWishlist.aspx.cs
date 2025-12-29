using BookStoreManagementSystem.Data;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.UI.WebControls;

namespace BookStoreManagementSystem
{
    public partial class MyWishlist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("Login.aspx");

            if (!IsPostBack)
                LoadWishlist();
        }
        protected void gvWishlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int wishlistId = Convert.ToInt32(gvWishlist.DataKeys[index].Value);

                using (var db = new BookStoreContext())
                {
                    var item = db.Wishlists.Find(wishlistId);
                    if (item != null)
                    {
                        db.Wishlists.Remove(item);
                        db.SaveChanges();
                    }
                }

                LoadWishlist();
            }
        }

        private void LoadWishlist()
        {
            int userId = (int)Session["UserId"];

            using (var db = new BookStoreContext())
            {
                var wishlistItems = db.Wishlists
                    .Include(w => w.Book)
                    .Where(w => w.UserId == userId)
                    .ToList();

                gvWishlist.DataSource = wishlistItems;
                gvWishlist.DataBind();
            }
        }
    }
}
