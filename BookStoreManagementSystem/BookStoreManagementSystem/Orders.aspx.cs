using System;
using System.Linq;
using System.Text;
using BookStoreManagementSystem.Data;
using System.Data.Entity;

namespace BookStoreManagementSystem
{
    public partial class Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("Login.aspx");

            if (!IsPostBack)
                LoadOrders();
        }

        private void LoadOrders()
        {
            int userId = (int)Session["UserId"];

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
            using (var db = new BookStoreContext())
            {
                var items = db.OrderItems
                    .Include(i => i.Book)
                    .Where(i => i.OrderId == orderId)
                    .ToList();

                var sb = new StringBuilder();
                foreach (var i in items)
                {
                    sb.Append($"<li>{i.Book.Title} × {i.Quantity}</li>");
                }
                return sb.ToString();
            }
        }
    }
}
