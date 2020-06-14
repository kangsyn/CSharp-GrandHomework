using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Library
{
    public class BookShelfService
    {
        public BookShelfService()
        {

        }

       public static IQueryable<BookShelf> AllShelves(BookShelfContext db)
        {
            return db.BookShelves.Include(o => o.Books)
                //.Include("Books")
                ;
        }

        public static List<BookShelf> GetAllShelfs()//取得数据库内全部的书架
        {
            using (var db = new BookShelfContext())
            {
                var query = AllShelves(db).ToList();
                foreach(var shelf in query)
                {
                    shelf.SetNum();
                }
                return query;
            }
        }
        
        public static BookShelf GetBookShelfById(string id)//按Id取得特定的书架
        {
            using (var db = new BookShelfContext())
            {
                return AllShelves(db).FirstOrDefault(o => o.BookShelfId == id);
            }
        }

        /*public static BookShelf GetBookShelfBySort(string sort)//按分类取得特定的书架
        {
            using (var db = new BookShelfContext())
            {
                return AllShelves(db).FirstOrDefault(o => o.Sort == sort);
            }
        }*/
        
        public static BookShelf AddBookShelf(BookShelf shelf)//添加新的书架
        {
            try
            {
                using (var db = new BookShelfContext())
                {
                    //MessageBox.Show(shelf.Sort + shelf.Books[0].Name, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //MessageBox.Show(shelf.Sort + shelf.Books[1].Name, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    db.BookShelves.Add(shelf);
                    //MessageBox.Show(shelf.Sort + shelf.Books[2].Name, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    db.SaveChanges();
                    MessageBox.Show("已成功添加书架！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return shelf;
            }
            catch (Exception e)
            {
                MessageBox.Show("添加书架错误！"+e.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw new ApplicationException($"添加书架错误: {e.Message}");
            }
        }

        public static void DeleteBookShelf(string id)
        {
            try
            {
                using (var db = new BookShelfContext())
                {
                    var shelf = db.BookShelves.Include("Books").Where(o => o.BookShelfId == id).FirstOrDefault();
                    db.BookShelves.Remove(shelf);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                //TODO 需要更加错误类型返回不同错误信息
                MessageBox.Show("删除书架错误！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw new ApplicationException($"删除书架错误!");
            }
        }

       /* public static void AddBook(Book book)//增加新书，现已改为直接在书架中进行
        {
            try
            {
                using (var db = new BookShelfContext())
                {
                    db.Books.Add(book);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("添加新书错误！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw new ApplicationException($"添加新书错误!");
            }
        }*/

        public static List<Book> AllBooks()//取得所有的书
        {
            using (var db = new BookShelfContext())
            {
                return db.Books.ToList();
            }
        }
        

        private static void RemoveBooksFromShelf(string bookShelfId)//删除书籍(对整个书架进行操作)，可能无用
        {
            using (var db = new BookShelfContext())
            {
                var oldBooks = db.Books.Where(book => book.BookShelfId == bookShelfId);
                db.Books.RemoveRange(oldBooks);
                MessageBox.Show(BookShelfService.AllBooks().Count + "" + "2");
                db.SaveChanges();
            }
        }

        private static void RemoveBooks(string bookId)//删除书籍(对单本书进行操作)
        {
            using (var db = new BookShelfContext())
            {
                var oldBooks = db.Books.Where(book => book.BookId == bookId);
                db.Books.RemoveRange(oldBooks);
                MessageBox.Show(BookShelfService.AllBooks().Count + "" + "2");
                db.SaveChanges();
            }
        }

        public static void UpdateShelf(BookShelf newBookShelf)//更新数据库内的书架,有问题
        {
            RemoveBooksFromShelf(newBookShelf.BookShelfId);
            using (var db = new BookShelfContext())
            {
                db.Entry(newBookShelf).State = EntityState.Modified;
                db.Books.AddRange(newBookShelf.Books);
                db.SaveChanges();
            }
        }

        public static void UpdateBook(Book newBook)//更新数据库内的书籍
        {
            RemoveBooks(newBook.BookId);
            MessageBox.Show(BookShelfService.AllBooks().Count + "" + "3");
            using (var db = new BookShelfContext())
            {
                db.Entry(newBook).State = EntityState.Modified;
                db.Books.Add(newBook);
                db.SaveChanges();
            }
        }

        public static void SetBookState(Book book, string state, Client client)//设置书籍的当前状态，包括“可正常使用”、“已被借阅”、“已被预约”
        {
            if (state == "可正常使用")
            {
                book.ClientName = "";
            }
            else
            {
                book.ClientName = client.Name;
            }
            book.State = state;
            MessageBox.Show(BookShelfService.AllBooks().Count + "" + "1");
            using (var db = new BookShelfContext())
            {
                BookShelf bookShelf = AllShelves(db).FirstOrDefault(o => o.BookShelfId == book.BookShelfId);
                UpdateBook(book);
            }
            MessageBox.Show(BookShelfService.AllBooks().Count + "" + "4");
        }

        public static List<Book> GetAllLentBooks(Client client)//取得所有已借阅的书
        {
            using (var db = new BookShelfContext())
            {
                return db.Books.Where(o => o.State == "已被借阅").Where(p => p.ClientName == client.Name).ToList();
            }
        }

        public static List<Book> GetAllAppointedBooks(Client client)//取得所有预约的书
        {
            using (var db = new BookShelfContext())
            {
                return db.Books.Where(o => o.State == "已被预约").Where(p => p.ClientName == client.Name).ToList();
            }
        }

        public static void ReNewLending(Book book, string newDate)//续借新书
        {
            book.AppointedTime = time;
            //book.Appointers.Add(client);
            SetBookState(book, "已被借阅", client);
        }
        
        public static void ReturnBooks(Book book, Client client)//还书
        {
            SetBookState(book, "可正常使用", client);
        }

        public static void CancellAppoint(Book book, Client client)//取消预约
        {
            book.AppointedTime = null;
            SetBookState(book, "可正常使用", client);
        }

        public static void AppointBook(Book book, Client client, string time)//预约
        {
            book.AppointedTime = time;
            //book.Appointers.Add(client);
            SetBookState(book, "已被预约", client);
        }
        
        public static void LendBook(Book book, Client client)//借阅
        {
            SetBookState(book, "可正常使用", client);
        }
    }
}
