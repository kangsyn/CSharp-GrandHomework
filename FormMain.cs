using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class FormMain : Form
    {
        public Client currentClient { get; set; }
        public Client administrator = new Client("管理员", "admin");
        public List<Book> Books { get; set; }
        public List<Book> appointBooks { get; set; }
        public List<Book> lendBooks { get; set; }
        public FormMain()
        {
            InitializeComponent();
            administrator.Id = "1";
            if(currentClient == null)
            {
                this.signOutButton.Visible = false;
                this.signOutButton.Enabled = false;
            }
            if (ClientService.AllClients().Count == 0)
            {
                ClientService.AddAdministrator(administrator);
            }
            this.manageButton.Visible = false;
            this.manageButton.Enabled = false;
            if(BookShelfService.AllBooks().Count == 0)
            {
                BookShelf test1 = new BookShelf();
                List<Book> Books1 = new List<Book>() {
                    new Book() { BookId = "1", Name = "毛泽东选集", BookShelfId = "1",
                    State = "可正常使用", Description = "毛泽东所写的一本书。", Recommend = 10, Author = "毛泽东", AppointedTime = null, ClientName = null, Sort = "文学" },
                    new Book() { BookId = "2", Name = "毛泽东选集二", BookShelfId = "1", State = "已被借阅", Description = "毛泽东所写的一本书。", Recommend = 15, Author = "毛泽东", AppointedTime = null, ClientName = "管理员", Sort = "文学" },
                    new Book() { BookId = "3", Name = "毛泽东选集三", BookShelfId = "1", State = "已被借阅", Description = "毛泽东所写的一本书。", Recommend = 15, Author = "毛泽东", AppointedTime = null, ClientName = "管理员", Sort = "文学" },
                    new Book() { BookId = "4", Name = "毛泽东选集四", BookShelfId = "1", State = "已被预约", Description = "毛泽东所写的一本书。", Recommend = 13, Author = "毛泽东", AppointedTime = "2020 8 1", ClientName = "管理员", Sort = "文学" }
                };
                foreach (var book in Books1)
                {
                    test1.AddBook(book);
                }
                test1.BookShelfId = "1";
                BookShelfService.AddBookShelf(test1);
                BookShelf test2 = new BookShelf();
                List<Book> Books2 = new List<Book>() {
                    new Book() { BookId = "5", Name = "C#编程", BookShelfId = "2",
                    State = "可正常使用", Description = "C#编程指南。", Recommend = 14, Author = "佚名", AppointedTime = null, ClientName = null, Sort = "C#" },
                    new Book() { BookId = "6", Name = "JAVA编程指南", BookShelfId = "2", State = "已被借阅", Description = "JAVA编程指南。", Recommend = 12, Author = "佚名", AppointedTime = null, ClientName = "Admin", Sort = "JAVA" },
                    new Book() { BookId = "7", Name = "C++编程指南", BookShelfId = "2", State = "已被预约", Description = "C++编程指南。", Recommend = 13, Author = "佚名", AppointedTime = "2020 9 1", ClientName = "Admin" , Sort = "C++"}
                };
                foreach (var book in Books2)
                {
                    test2.AddBook(book);
                }
                test2.BookShelfId = "2";
                BookShelfService.AddBookShelf(test2);
            }

            
            Books = BookShelfService.AllBooks();
            booksBindingSource.DataSource = Books;
            Query(1);
        }

        private void returnButton_Click(object sender, EventArgs e)//还书，完成
        {
            Book book = lendBindingSource.Current as Book;
            if (book == null)
            {
                MessageBox.Show("请选择一本书进行操作！");
                return;
            }
            BookShelfService.ReturnBooks(book, currentClient);//问题
            lendBooks = new List<Book>();
            lendBindingSource.DataSource = lendBooks;
            lendBooks = BookShelfService.GetAllLentBooks(currentClient);
            lendBindingSource.ResetBindings(false);
            lendBindingSource.DataSource = lendBooks;
            Books = new List<Book>();
            booksBindingSource.DataSource = Books;
            Books = BookShelfService.AllBooks();
            booksBindingSource.ResetBindings(false);
            booksBindingSource.DataSource = Books;
        }

        private void signInButton_Click(object sender, EventArgs e)//登录，完成
        {
            FormRegisterandSignin formRegisterandSignin = new FormRegisterandSignin(1);
            if(formRegisterandSignin.ShowDialog() == DialogResult.OK)
            {
                currentClient = formRegisterandSignin.currentClient;
                if(currentClient != null)
                {
                    nameTextBox.ReadOnly = false;
                    nameTextBox.Text = currentClient.Name;
                    nameTextBox.ReadOnly = true;
                    this.signOutButton.Visible = true;
                    this.signOutButton.Enabled = true;
                    this.signInButton.Visible = false;
                    this.signInButton.Enabled = false;
                    this.registerButton.Visible = false;
                    this.registerButton.Enabled = false;
                    if (currentClient.Name == "管理员")
                    {
                        this.manageButton.Visible = true;
                        this.manageButton.Enabled = true;
                    }

                    appointBooks = BookShelfService.GetAllAppointedBooks(currentClient);
                    lendBooks = BookShelfService.GetAllLentBooks(currentClient);
                    appointBindingSource.DataSource = appointBooks;
                    lendBindingSource.DataSource = lendBooks;
                }
            
            }
        }

        private void registeButton_Click(object sender, EventArgs e)//注册，完成
        {
            FormRegisterandSignin formRegisterandSignin = new FormRegisterandSignin(2);
            formRegisterandSignin.ShowDialog();
        }

        private void cancellButton_Click(object sender, EventArgs e)//取消预约，完成
        {
            Book book = appointBindingSource.Current as Book;
            if (book == null)
            {
                MessageBox.Show("请选择一本书进行操作！");
                return;
            }
            BookShelfService.CancellAppoint(book, currentClient);
            appointBooks = new List<Book>();
            appointBindingSource.DataSource = appointBooks;
            appointBooks = BookShelfService.GetAllAppointedBooks(currentClient);
            appointBindingSource.ResetBindings(false);
            appointBindingSource.DataSource = appointBooks;
            Books = new List<Book>();
            booksBindingSource.DataSource = Books;
            Books = BookShelfService.AllBooks();
            booksBindingSource.ResetBindings(false);
            booksBindingSource.DataSource = Books;
        }

        private void renewButton_Click(object sender, EventArgs e)//续借，未完成
        {

        }

        private void searchButton_Click(object sender, EventArgs e)//查询，完成
        {
            if(searchTextBox.Text == null)
            {
                MessageBox.Show("没有输入用于查询的关键词！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                switch (searchComboBox.Text)
                {
                    case "书号":
                        Search(1, searchTextBox.Text);
                        break;
                    case "书名":
                        Search(2, searchTextBox.Text);
                        break;
                    case "作者":
                        Search(3, searchTextBox.Text);
                        break;
                    case "分类":
                        Search(4, searchTextBox.Text);
                        break;
                    case "书架号":
                        Search(5, searchTextBox.Text);
                        break;
                    default:
                        MessageBox.Show("没有选择正确的查询方式！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }
        }

        private void queryButton_Click(object sender, EventArgs e)//排序，完成
        {
            switch (queryComboBox.Text)
            {
                case "推荐数":
                    Query(1);
                    break;
                case "书号":
                    Query(2);
                    break;
                case "作者":
                    Query(3);
                    break;
                case "分类":
                    Query(4);
                    break;
                case "书架号":
                    Query(5);
                    break;
                default:
                    MessageBox.Show("没有选择正确的排序方式！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        private void intoDetailButton_Click(object sender, EventArgs e)//查看细节,完成
        {
            Book book = booksBindingSource.Current as Book;
            FormBookDetail formBookDetail = new FormBookDetail(book, currentClient, 1);
            if (formBookDetail.ShowDialog() == DialogResult.OK)
            {
                Query(1);
            }
        }

        private void manageButton_Click(object sender, EventArgs e)//图书管理，完成
        {
            FormShelf formShelf = new FormShelf();
            if (formShelf.ShowDialog() == DialogResult.OK)
            {
                Query(1);
            }
        }

        private void exitButton_Click(object sender, EventArgs e)//退出，完成
        {
            this.Close();
        }

        private void signoutButton_Click(object sender, EventArgs e)//退出登录，改变各种按钮的可用性、可视性，完成
        {
            currentClient = null;
            nameTextBox.ReadOnly = false;
            nameTextBox.Text = null;
            nameTextBox.ReadOnly = true;
            this.signOutButton.Visible = false;
            this.signOutButton.Enabled = false;
            this.signInButton.Visible = true;
            this.signInButton.Enabled = true;
            this.registerButton.Visible = true;
            this.registerButton.Enabled = true;
            this.manageButton.Visible = false;
            this.manageButton.Enabled = false;
        }

        private void Query(int i)//排序方法，i表示具体的排序规则，完成
        {
            List<Book> queryBooks = new List<Book>();
            using (var db = new BookShelfContext())
            {               
                switch (i)
                {
                    case 1://默认的按推荐数排序
                        var query1 = BookShelfService.AllBooks();
                        var list1 = from t in query1 orderby t.Recommend descending select t;
                        queryBooks = list1.ToList();
                        break;
                    case 2://书号
                        var query2 = BookShelfService.AllBooks();
                        var list2 = from t in query2 orderby t.BookId ascending select t;
                        queryBooks = list2.ToList();
                        break;
                    case 3://作者
                        var query3 = BookShelfService.AllBooks();
                        var list3 = from t in query3 orderby t.Author ascending select t;
                        queryBooks = list3.ToList();
                        break;
                    case 4://分类
                        var query4 = BookShelfService.AllBooks();
                        var list4 = from t in query4 orderby t.BookId ascending select t;
                        queryBooks = list4.ToList();
                        break;
                    case 5://书架号
                        var query5 = BookShelfService.AllBooks();
                        var list5 = from t in query5 orderby t.BookId ascending select t;
                        queryBooks = list5.ToList();
                        break;
                }
                booksBindingSource.DataSource = new List<Book>();
                booksBindingSource.ResetBindings(false);
                booksBindingSource.DataSource = queryBooks;
            }
        }

        private void Search(int i, string key)//查询方法，i表示具体的排序规则，完成
        {
            List<Book> searchBooks = new List<Book>();
            using (var db = new BookShelfContext())
            {
                switch (i)
                {
                    case 1://按书号查询
                        searchBooks.Add(BookShelfService.AllBooks().FirstOrDefault(o => o.BookId == key));
                        break;
                    case 2://书名
                        searchBooks.Add(BookShelfService.AllBooks().FirstOrDefault(o => o.Name == key));
                        break;
                    case 3://作者
                        foreach(var book in BookShelfService.AllBooks().Where(o => o.Author == key))
                        {
                            searchBooks.Add(book);
                        }
                        break;
                    case 4://分类
                        foreach (var book in BookShelfService.AllBooks().Where(o => o.Sort == key))
                        {
                            searchBooks.Add(book);
                        }
                        break;
                    case 5://书架号
                        foreach (var book in BookShelfService.AllBooks().Where(o => o.BookShelfId == key))
                        {
                            searchBooks.Add(book);
                        }
                        break;
                }
                booksBindingSource.DataSource = new List<Book>();
                booksBindingSource.ResetBindings(false);
                booksBindingSource.DataSource = searchBooks;
            }
        }

        /*private void resetBind()//重设数据绑定，基本完成，不确定是否有用
        {
            Books = BookShelfService.AllBooks();
            appointBooks = BookShelfService.GetAllAppointedBooks(currentClient);
            lendBooks = BookShelfService.GetAllLentBooks(currentClient);
            appointBindingSource.DataSource = appointBooks;
            appointBindingSource.ResetBindings(false);
            lendBindingSource.DataSource = lendBooks;
            lendBindingSource.ResetBindings(false);
            booksBindingSource.DataSource = Books;
            booksBindingSource.ResetBindings(false);
        }*/
    }
}
