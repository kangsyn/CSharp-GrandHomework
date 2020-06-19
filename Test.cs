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
        static String administrator1 = ClientService.AllClients().FirstOrDefault(o => o.Id == "1").Name;
        static String administrator2 = ClientService.AllClients().FirstOrDefault(o => o.Id == "2").Name;
        static String administrator3 = ClientService.AllClients().FirstOrDefault(o => o.Id == "3").Name;
        public static void Test1()
        {
            //MessageBox.Show(administrator1);
            //MessageBox.Show(administrator2);
            BookShelf test1 = new BookShelf();
            Book book1 = new Book("1","毛泽东选集一","1","毛泽东", "毛泽东所写的一本书。", null,null,"文学","")
            {
                State = "可正常使用",
                Recommend = 10
            };
            Book book2 = new Book("2", "毛泽东选集二", "1", "毛泽东", "毛泽东所写的一本书。", "2020年8月1日", "管理员1", "文学",administrator2+" "+ administrator3+" ")
            { 
                State = "已被借阅",
                Recommend = 15
            };
            //book2.Appointer = ClientService.transferIntoAppointer(administrator2, book2);
            //book2.Appointers.Add(administrator2);           
            Book book3 = new Book("3", "毛泽东选集三", "1", "毛泽东", "毛泽东所写的一本书。", "2020年8月1日", "管理员1", "文学",administrator2+" ") 
            { 
                State = "已被借阅", 
                Recommend = 15
            };
            //book3.Appointer = ClientService.transferIntoAppointer(administrator2, book3);
            //book3.Appointers.Add(administrator2);
            Book book4 = new Book("4", "毛泽东选集四", "1", "毛泽东", "毛泽东所写的一本书。", "2020年8月1日", "管理员1", "文学",administrator2+" "+administrator3+" ") 
            { 
                State = "已被借阅",
                Recommend = 13
            };
            //book4.Appointer = ClientService.transferIntoAppointer(administrator2, book4);
            //book4.Appointers.Add(administrator2);
            List<Book> Books1 = new List<Book>()
            {
                book1,book2,book3,book4
            };
            foreach (var book in Books1)
            {
                //MessageBox.Show(book.Name+book.Appointer.Count()+"");
                //book.setAppoint();
                test1.AddBook(book);
            }
            test1.BookShelfId = "1";
            BookShelfService.AddBookShelf(test1);
        }

        public static void Test2()
        {
            BookShelf test2 = new BookShelf();
            Book book1 = new Book("5", "C#编程", "2", "佚名", "C#编程指南。", null, null, "C#","")
            {
                State = "可正常使用",
                Recommend = 14
            };

            Book book2 = new Book("6", "JAVA编程", "2", "佚名", "JAVA编程指南。", "2020年5月1日", "管理员2", "JAVA",administrator1+" ")
            {
                State = "已被借阅",
                Recommend = 12
            };
            //book2.Appointer = ClientService.transferIntoAppointer(administrator1, book2);
            //book2.Appointers.Add(administrator2);           
            Book book3 = new Book("7", "C++编程", "2", "佚名", "C++编程指南。", "2020年9月1日", "管理员2", "C++",administrator1+" "+administrator3+" ")
            {
                State = "已被借阅",
                Recommend = 13
            };
            //book3.Appointer = ClientService.transferIntoAppointer(administrator1, book3);
            List<Book> Books2 = new List<Book>()
            {
                book1,book2,book3
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
