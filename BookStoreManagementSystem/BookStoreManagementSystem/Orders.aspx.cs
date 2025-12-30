using BookStoreManagementSystem.Data;
using BookStoreManagementSystem.Helpers;
using System;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Claims;
using System.Text;

namespace BookStoreManagementSystem
{
    public partial class Orders : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
                LoadOrders();
        }

        private int GetUserIdFromJwt()
        {
            var identity = (ClaimsIdentity)Context.User.Identity;
            return int.Parse(
                identity.FindFirst(ClaimTypes.NameIdentifier).Value
            );
        }

        private void LoadOrders()
        {
            int userId = GetUserIdFromJwt();

            using (var db = new BookStoreContext())
            {
                rptOrders.DataSource = db.Orders
                    .Where(o => o.UserId == userId)
                    .OrderByDescending(o => o.OrderDate)
                    .ToList();

                rptOrders.DataBind();
            }
        }

        public string GetOrderItems(int orderId)
        {
            int userId = GetUserIdFromJwt();

            using (var db = new BookStoreContext())
            {
                var orderExists = db.Orders
                    .Any(o => o.OrderId == orderId &&
                              o.UserId == userId);

                if (!orderExists)
                    return "";

                var items = db.OrderItems
                    .Include(i => i.Book)
                    .Where(i => i.OrderId == orderId)
                    .ToList();

                var sb = new StringBuilder();
                foreach (var i in items)
                {
                    sb.Append(
                        $"<li>{i.Book.Title} × {i.Quantity}</li>"
                    );
                }

                return sb.ToString();
            }
        }
    }
}
