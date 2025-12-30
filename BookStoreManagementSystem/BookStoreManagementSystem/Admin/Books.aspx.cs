using BookStoreManagementSystem.Data;
using BookStoreManagementSystem.Helpers;
using BookStoreManagementSystem.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web.UI.WebControls;

namespace BookStoreManagementSystem.Admin
{
    public partial class Books : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.IsInRole("Admin"))
            {
                Response.Redirect("~/Dashboard.aspx");
                return;
            }

            if (!IsPostBack)
                LoadBooks();
        }

        private void LoadBooks()
        {
            using (var db = new BookStoreContext())
            {
                gvBooks.DataSource = db.Books.ToList();
                gvBooks.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            decimal price;
            if (!decimal.TryParse(txtPrice.Text, out price))
            {
                lblMessage.Text = "Invalid price format.";
                return;
            }

            try
            {
                using (var db = new BookStoreContext())
                {
                    db.Books.Add(new Book
                    {
                        Title = txtTitle.Text.Trim(),
                        Author = txtAuthor.Text.Trim(),
                        Price = price
                    });

                    db.SaveChanges();
                }

                lblMessage.Text = "Book added successfully.";
                lblMessage.CssClass = "text-success";

                txtTitle.Text = txtAuthor.Text = txtPrice.Text = "";
                LoadBooks();
            }
            catch (Exception)
            {
                lblMessage.Text = "Error while adding book.";
            }
        }

        protected void gvBooks_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvBooks.EditIndex = e.NewEditIndex;
            LoadBooks();
        }

        protected void gvBooks_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvBooks.EditIndex = -1;
            LoadBooks();
        }

        protected void gvBooks_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = (int)gvBooks.DataKeys[e.RowIndex].Value;
            GridViewRow row = gvBooks.Rows[e.RowIndex];

            string title = ((TextBox)row.Cells[1].Controls[0]).Text.Trim();
            string author = ((TextBox)row.Cells[2].Controls[0]).Text.Trim();
            string priceText = ((TextBox)row.Cells[3].Controls[0]).Text;

            decimal price;
            if (!decimal.TryParse(priceText, out price))
            {
                lblMessage.Text = "Invalid price.";
                return;
            }

            try
            {
                using (var db = new BookStoreContext())
                {
                    var book = db.Books.Find(id);
                    if (book == null)
                    {
                        lblMessage.Text = "Book not found.";
                        return;
                    }

                    book.Title = title;
                    book.Author = author;
                    book.Price = price;

                    db.SaveChanges();
                }

                gvBooks.EditIndex = -1;
                LoadBooks();
            }
            catch (Exception)
            {
                lblMessage.Text = "Error while updating book.";
            }
        }

        protected void gvBooks_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = (int)gvBooks.DataKeys[e.RowIndex].Value;

            try
            {
                using (var db = new BookStoreContext())
                {
                    var book = db.Books.Find(id);
                    if (book != null)
                    {
                        db.Books.Remove(book);
                        db.SaveChanges();
                    }
                }

                LoadBooks();
            }
            catch (Exception)
            {
                lblMessage.Text = "Error while deleting book.";
            }
        }
    }
}
