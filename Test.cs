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
        static String user1 = ClientService.AllClients().FirstOrDefault(o => o.Id == "2").Name;
        static String user2 = ClientService.AllClients().FirstOrDefault(o => o.Id == "3").Name;
        static String user3 = ClientService.AllClients().FirstOrDefault(o => o.Id == "4").Name;
        public static void Test1()
        {
            //MessageBox.Show(administrator1);
            //MessageBox.Show(administrator2);
            BookShelf test1 = new BookShelf();
            Book book1 = new Book("1","毛泽东选集","1","毛泽东", "毛泽东所写的一本书。", null,null,"政治","")
            {
                State = "可正常使用",
                Recommend = 10,
                imagePath = @"C:\Users\tjw20\source\repos\Library\Resources\毛泽东选集.jpg"
            };
            Book book2 = new Book("2", "论持久战", "1", "毛泽东", "毛泽东所写的一本书。", "2020年8月1日", "涂珈玮", "政治","")
            { 
                State = "已被借阅",
                Recommend = 15,
                imagePath = @"C:\Users\tjw20\source\repos\Library\Resources\论持久战.jpg"
            };
            //book2.Appointer = ClientService.transferIntoAppointer(administrator2, book2);
            //book2.Appointers.Add(administrator2);           
            Book book3 = new Book("3", "钢铁是怎样炼成的", "1", "奥斯特洛夫斯基", "奥斯特洛夫斯基所写的一本书。", "2020年8月1日", "涂珈玮", "文学",user2+" ") 
            { 
                State = "已被借阅", 
                Recommend = 15,
                imagePath = @"C:\Users\tjw20\source\repos\Library\Resources\钢铁是怎样炼成的.jpg"
            };
            //book3.Appointer = ClientService.transferIntoAppointer(administrator2, book3);
            //book3.Appointers.Add(administrator2);
            Book book4 = new Book("4", "西游记", "1", "吴承恩", "吴承恩所写的一本书。", "2020年8月1日", "康盛尧", "文学",user3+" ") 
            { 
                State = "已被借阅",
                Recommend = 13,
                imagePath = @"C:\Users\tjw20\source\repos\Library\Resources\西游记.jpg"
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
            Book book1 = new Book("5", "C#程序设计语言", "2", "Peter Golde", "C#编程指南。", null, null, "编程","")
            {
                State = "可正常使用",
                Recommend = 14,
                imagePath = @"C:\Users\tjw20\source\repos\Library\Resources\C#程序设计语言.jpg"
            };

            Book book2 = new Book("6", "JAVA语言程序设计", "2", "佚名", "JAVA编程指南。", "2020年5月1日", "李梦凡", "编程",user1+" ")
            {
                State = "已被借阅",
                Recommend = 12,
                imagePath = @"C:\Users\tjw20\source\repos\Library\Resources\JAVA语言程序设计.jpg"
            };
            //book2.Appointer = ClientService.transferIntoAppointer(administrator1, book2);
            //book2.Appointers.Add(administrator2);           
            Book book3 = new Book("7", "三国演义", "2", "罗贯中", "罗贯中所写的一本书。", "2020年9月1日", "康盛尧", "文学",user1+" "+user3+" ")
            {
                State = "已被借阅",
                Recommend = 13,
                imagePath = @"C:\Users\tjw20\source\repos\Library\Resources\三国演义.jpg"
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
