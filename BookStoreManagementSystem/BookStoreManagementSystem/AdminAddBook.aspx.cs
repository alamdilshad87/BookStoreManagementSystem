using System;
using BookStoreManagementSystem.Data;
using BookStoreManagementSystem.Models;

namespace BookStoreManagementSystem
{
    public partial class AdminAddBook : System.Web.UI.Page
    {
        protected void AddBook_Click(object sender, EventArgs e)
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
        }
    }
}
