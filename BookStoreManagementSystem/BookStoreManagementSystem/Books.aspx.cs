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
                var existingItem = db.Carts
                    .FirstOrDefault(c => c.UserId == userId && c.BookId == bookId);

                if (existingItem != null)
                {
                    existingItem.Quantity += 1;
                }
                else
                {
                    db.Carts.Add(new Cart
                    {
                        UserId = userId,
                        BookId = bookId,
                        Quantity = 1
                    });
                }

                db.SaveChanges();
            }
            using (var db = new BookStoreContext())
            {
                if (e.CommandName == "Wish")
                {
                    var wishItem = db.Wishlists
                        .FirstOrDefault(w => w.UserId == userId && w.BookId == bookId);

                    if (wishItem != null)
                    {
                        db.Wishlists.Remove(wishItem);
                    }
                    else
                    {
                        db.Wishlists.Add(new Wishlist
                        {
                            UserId = userId,
                            BookId = bookId
                        });
                    }
                    db.SaveChanges();
                }

            }
        }
    }
}
