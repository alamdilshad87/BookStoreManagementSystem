using System;
using System.Linq;
using BookStoreManagementSystem.Data;
using BookStoreManagementSystem.Models;

namespace BookStoreManagementSystem.Admin
{
    public partial class Books : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 🔒 ADMIN CHECK
            if (Session["UserId"] == null || Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
                Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                LoadBooks();
            }
        }

        private void LoadBooks()
        {
            using (var db = new BookStoreContext())
            {
                gvBooks.DataSource = db.Books.ToList();
                gvBooks.DataBind();
            }
        }

        // ➕ ADD BOOK
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (var db = new BookStoreContext())
            {
                db.Books.Add(new Book
                {
                    Title = txtTitle.Text,
                    Author = txtAuthor.Text,
                    Price = Convert.ToDecimal(txtPrice.Text)
                });
                db.SaveChanges();
            }

            txtTitle.Text = txtAuthor.Text = txtPrice.Text = "";
            LoadBooks();
        }

        // ✏️ EDIT
        protected void gvBooks_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvBooks.EditIndex = e.NewEditIndex;
            LoadBooks();
        }

        protected void gvBooks_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvBooks.EditIndex = -1;
            LoadBooks();
        }

        protected void gvBooks_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            int id = (int)gvBooks.DataKeys[e.RowIndex].Value;
            var row = gvBooks.Rows[e.RowIndex];

            string title = ((System.Web.UI.WebControls.TextBox)row.Cells[1].Controls[0]).Text;
            string author = ((System.Web.UI.WebControls.TextBox)row.Cells[2].Controls[0]).Text;
            decimal price = Convert.ToDecimal(((System.Web.UI.WebControls.TextBox)row.Cells[3].Controls[0]).Text);

            using (var db = new BookStoreContext())
            {
                var book = db.Books.Find(id);
                book.Title = title;
                book.Author = author;
                book.Price = price;
                db.SaveChanges();
            }

            gvBooks.EditIndex = -1;
            LoadBooks();
        }

        // ❌ DELETE
        protected void gvBooks_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = (int)gvBooks.DataKeys[e.RowIndex].Value;

            using (var db = new BookStoreContext())
            {
                var book = db.Books.Find(id);
                db.Books.Remove(book);
                db.SaveChanges();
            }

            LoadBooks();
        }
    }
}
