using BookStoreManagementSystem.Data;
using BookStoreManagementSystem.Helpers;
using System;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Claims;
using System.Web.UI.WebControls;

namespace BookStoreManagementSystem
{
    public partial class MyWishlist : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
                LoadWishlist();
        }

        private int GetUserIdFromJwt()
        {
            var identity = (ClaimsIdentity)Context.User.Identity;
            return int.Parse(
                identity.FindFirst(ClaimTypes.NameIdentifier).Value
            );
        }
        protected void gvWishlist_RowCommand(
            object sender,
            GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int wishlistId =
                    Convert.ToInt32(
                        gvWishlist.DataKeys[index].Value);

                int userId = GetUserIdFromJwt();

                using (var db = new BookStoreContext())
                {
                    var item = db.Wishlists
                        .FirstOrDefault(w =>
                            w.WishlistId == wishlistId &&
                            w.UserId == userId);

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
            int userId = GetUserIdFromJwt();

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
