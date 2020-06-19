using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace Library
{
    public class BookShelf
    {
        [Key]
        public string BookShelfId { get; set; }//书架号，自动识别为主键
        public int Num { get; set; }//数量
        public List<Book> Books { get; set; }//一对多关联

        public BookShelf()
        {
            Books = new List<Book>();
            Num = 0;
        }
        public BookShelf(List<Book> books, String sort):this()
        {
            if(books != null)
            {
                foreach(var book in books)
                {
                    book.BookShelfId = BookShelfId;
                }
                Books = books;
            }
        }
        public void SetNum()//取得书架内的藏书数量
        {
            this.Num = Books.Count();
        }
        public void AddBook(Book book)//添加书籍
        {
            if (Books.Contains(book))
                throw new ApplicationException($"添加错误：书籍已存在!");
            Books.Add(book);
        }

        public override bool Equals(object obj)
        {
            var bookshelf = obj as BookShelf;
            return bookshelf != null &&
                   BookShelfId == bookshelf.BookShelfId;
        }

        public override int GetHashCode()
        {
            var hashCode = -531220479;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(BookShelfId);
            hashCode = hashCode * -1521134295 + Num.GetHashCode();
            return hashCode;
        }

        public int CompareTo(BookShelf other)
        {
            if (other == null) return 1;
            return this.BookShelfId.CompareTo(other.BookShelfId);
        }
    }

}
