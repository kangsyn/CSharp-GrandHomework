using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{//数据库相关内容
    public class BookShelfContext:DbContext
    {
        public BookShelfContext() : base("LibraryDataBase")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BookShelfContext>());
        }
        public DbSet<BookShelf> BookShelves { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
