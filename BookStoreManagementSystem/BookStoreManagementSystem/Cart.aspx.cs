using BookStoreManagementSystem.Data;
using BookStoreManagementSystem.Helpers;
using BookStoreManagementSystem.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;

namespace BookStoreManagementSystem
{
    public partial class CartPage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadCart();
        }

        private int GetUserIdFromJwt()
        {
            var identity = (ClaimsIdentity)Context.User.Identity;
            return int.Parse(
                identity.FindFirst(ClaimTypes.NameIdentifier).Value
            );
        }

        private void LoadCart()
        {
            int userId = GetUserIdFromJwt();

            using (var db = new BookStoreContext())
            {
                var cartItems = db.Carts
                    .Include(c => c.Book)
                    .Where(c => c.UserId == userId)
                    .ToList();

                gvCart.DataSource = cartItems;
                gvCart.DataBind();

                decimal grandTotal = cartItems
                    .Sum(c => c.Book.Price * c.Quantity);

                lblGrandTotal.Text = grandTotal.ToString("N2");
            }
        }

        protected void gvCart_RowDeleting(
            object sender,
            System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int cartId = Convert.ToInt32(
                gvCart.DataKeys[e.RowIndex].Value);

            using (var db = new BookStoreContext())
            {
                try
                {
                    var item = db.Carts.Find(cartId);
                    if (item != null)
                    {
                        db.Carts.Remove(item);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            LoadCart();
        }

        protected void PlaceOrder_Click(object sender, EventArgs e)
        {
            int userId = GetUserIdFromJwt();

            using (var db = new BookStoreContext())
            using (var tx = db.Database.BeginTransaction())
            {
                try
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
                        TotalAmount =
                            cartItems.Sum(
                                c => c.Book.Price * c.Quantity)
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

                    tx.Commit();
                    Response.Redirect("OrderSuccess.aspx");
                }
                catch (Exception)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }
    }
}
