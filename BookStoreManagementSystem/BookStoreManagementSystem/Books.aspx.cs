using BookStoreManagementSystem.Data;
using BookStoreManagementSystem.Helpers;
using BookStoreManagementSystem.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web.UI.WebControls;

namespace BookStoreManagementSystem
{
    public partial class Books : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                using (var db = new BookStoreContext())
                {
                    gvBooks.DataSource = db.Books.ToList();
                    gvBooks.DataBind();
                }
            }
        }
        private int GetUserId()
        {
            var identity = (ClaimsIdentity)Context.User.Identity;
            return int.Parse(
                identity.FindFirst(ClaimTypes.NameIdentifier).Value
            );
        }
        protected void gvBooks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int bookId = Convert.ToInt32(gvBooks.DataKeys[index].Value);
            int userId = GetUserId();

            using (var db = new BookStoreContext())
            {
                if (e.CommandName == "Cart")
                {
                    var existingItem = db.Carts
                        .FirstOrDefault(c =>
                            c.UserId == userId &&
                            c.BookId == bookId);

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
                }
                else if (e.CommandName == "Wish")
                {
                    var wishItem = db.Wishlists
                        .FirstOrDefault(w =>
                            w.UserId == userId &&
                            w.BookId == bookId);

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
                }

                db.SaveChanges();
            }
        }
    }
}
