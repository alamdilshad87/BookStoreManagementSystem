using System;
using System.Linq;
using BookStoreManagementSystem.Data;
using BookStoreManagementSystem.Models;

namespace BookStoreManagementSystem
{
    public partial class Books : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("Login.aspx");

            if (!IsPostBack)
            {
                using (var db = new BookStoreContext())
                {
                    gvBooks.DataSource = db.Books.ToList();
                    gvBooks.DataBind();
                }
            }
        }

        protected void gvBooks_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int bookId = Convert.ToInt32(gvBooks.DataKeys[index].Value);
            int userId = (int)Session["UserId"];

            using (var db = new BookStoreContext())
            {
                if (e.CommandName == "Cart")
                {
                    db.Carts.Add(new Cart { BookId = bookId, UserId = userId, Quantity = 1 });
                }
                else if (e.CommandName == "Wish")
                {
                    if (!db.Wishlists.Any(w => w.BookId == bookId && w.UserId == userId))
                        db.Wishlists.Add(new Wishlist { BookId = bookId, UserId = userId });
                }
                db.SaveChanges();
            }
        }
    }
}
