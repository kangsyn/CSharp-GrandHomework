using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    class Test
    {
        public static void Test1()
        {
            String administrator = ClientService.AllClients().FirstOrDefault(o => o.Id == "1").Name;
            //MessageBox.Show(administrator);
            BookShelf test1 = new BookShelf();
            Book book1 = new Book()
            {
                BookId = "1",
                Name = "毛泽东选集",
                BookShelfId = "1",
                State = "可正常使用",
                Description = "毛泽东所写的一本书。",
                Recommend = 10,
                Author = "毛泽东",
                LendTime = null,
                ClientName = null,
                Sort = "文学"
            };
            Book book2 = new Book() { BookId = "2", Name = "毛泽东选集二", BookShelfId = "1", State = "已被借阅", Description = "毛泽东所写的一本书。", Recommend = 15, Author = "毛泽东", LendTime = "2020年8月1日", ClientName = "管理员", Sort = "文学" };
            //book1.Appointers.Add(administrator);
            Book book3 = new Book() { BookId = "3", Name = "毛泽东选集三", BookShelfId = "1", State = "已被借阅", Description = "毛泽东所写的一本书。", Recommend = 15, Author = "毛泽东", LendTime = "2020年8月1日", ClientName = "管理员", Sort = "文学" };
            Book book4 = new Book() { BookId = "4", Name = "毛泽东选集四", BookShelfId = "1", State = "已被预约", Description = "毛泽东所写的一本书。", Recommend = 13, Author = "毛泽东", LendTime = "2020年8月1日", ClientName = "管理员", Sort = "文学" };
            List<Book> Books1 = new List<Book>()
            {
                book1,book2,book3,book4
            };
            foreach (var book in Books1)
            {
                //MessageBox.Show(book.Appointers.Count()+"");
                book.Appointers.Add(administrator);
                MessageBox.Show(book.Appointers.Count() + "");
                book.setAppoint();
                test1.AddBook(book);
            }
            test1.BookShelfId = "1";
            BookShelfService.AddBookShelf(test1);
        }

        public static void Test2()
        {
            BookShelf test2 = new BookShelf();
            List<Book> Books2 = new List<Book>() {
                    new Book() { BookId = "5", Name = "C#编程", BookShelfId = "2",
                    State = "可正常使用", Description = "C#编程指南。", Recommend = 14, Author = "佚名", LendTime = null, ClientName = null, Sort = "C#" },
                    new Book() { BookId = "6", Name = "JAVA编程指南", BookShelfId = "2", State = "已被借阅", Description = "JAVA编程指南。", Recommend = 12, Author = "佚名", LendTime = "2020年8月1日", ClientName = "Admin", Sort = "JAVA" },
                    new Book() { BookId = "7", Name = "C++编程指南", BookShelfId = "2", State = "已被预约", Description = "C++编程指南。", Recommend = 13, Author = "佚名", LendTime = "2020年9月1日", ClientName = "Admin" , Sort = "C++"}
                };
            foreach (var book in Books2)
            {
                test2.AddBook(book);
            }
            test2.BookShelfId = "2";
            BookShelfService.AddBookShelf(test2);
        }
    }
}
