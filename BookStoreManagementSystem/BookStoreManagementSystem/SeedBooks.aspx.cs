using System;
using System.Linq;
using BookStoreManagementSystem.Data;
using BookStoreManagementSystem.Models;

namespace BookStoreManagementSystem
{
    public partial class SeedBooks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var db = new BookStoreContext())
            {
                if (!db.Books.Any())
                {
                    db.Books.AddRange(new[]
                    {
                        new Book{ Title="C# Basics", Author="MS", Price=499 },
                        new Book{ Title="ASP.NET Core", Author="Microsoft", Price=699 },
                        new Book{ Title="Clean Code", Author="Robert C Martin", Price=899 },
                        new Book{ Title="Design Patterns", Author="GoF", Price=999 }
                    });
                    db.SaveChanges();
                }
            }
        }
    }
}
