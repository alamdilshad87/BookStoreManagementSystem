using BookStoreManagementSystem.Data;
using BookStoreManagementSystem.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace BookStoreManagementSystem
{
    public partial class CartPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("Login.aspx");

            if (!IsPostBack)
                LoadCart();
        }

        private void LoadCart()
        {
            int userId = (int)Session["UserId"];

            using (var db = new BookStoreContext())
            {
                var cartItems = db.Carts
                    .Include(c => c.Book)
                    .Where(c => c.UserId == userId)
                    .ToList();

                gvCart.DataSource = cartItems;
                gvCart.DataBind();

                // 🔥 Grand Total
                decimal grandTotal = cartItems
                    .Sum(c => c.Book.Price * c.Quantity);

                lblGrandTotal.Text = grandTotal.ToString("N2");
            }
        }

        protected void gvCart_RowDeleting(object sender,
            System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int cartId = Convert.ToInt32(gvCart.DataKeys[e.RowIndex].Value);

            using (var db = new BookStoreContext())
            {
                var item = db.Carts.Find(cartId);
                if (item != null)
                {
                    db.Carts.Remove(item);
                    db.SaveChanges();
                }
            }

            LoadCart(); // refresh
        }

        protected void PlaceOrder_Click(object sender, EventArgs e)
        {
            int userId = (int)Session["UserId"];

            using (var db = new BookStoreContext())
            {
                var cartItems = db.Carts
                    .Include(c => c.Book)
                    .Where(c => c.UserId == userId)
                    .ToList();

                if (!cartItems.Any())
                    return;

                var order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.Now,
                    TotalAmount = cartItems.Sum(c => c.Book.Price * c.Quantity)
                };

                db.Orders.Add(order);
                db.SaveChanges();

                foreach (var item in cartItems)
                {
                    db.OrderItems.Add(new OrderItem
                    {
                        OrderId = order.OrderId,
                        BookId = item.BookId,
                        Quantity = item.Quantity,
                        Price = item.Book.Price
                    });
                }

                db.Carts.RemoveRange(cartItems);
                db.SaveChanges();
            }

            Response.Redirect("OrderSuccess.aspx");
        }
    }
}
