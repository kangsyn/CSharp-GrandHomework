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
    public partial class FormShelf : Form
    {
        bool isNormal = true;//决定是否为常规的管理方式
        Client currentClient { get; set; }
        public FormShelf(Client client)
        {
            InitializeComponent();
            shelfBindingSource.DataSource = BookShelfService.GetAllShelfs();
            currentClient = client;
        }

        private void addShelfButton_Click(object sender, EventArgs e)//添加书架，直接添加一个新的空书架
        {
            int k = Convert.ToInt32(BookShelfService.GetAllShelfs().Max(i => i.BookShelfId))+1;
            BookShelfService.AddBookShelf(new BookShelf() { BookShelfId = k+ "" });
            shelfBindingSource.DataSource = BookShelfService.GetAllShelfs();
        }

        private void deleteShelfButton_Click(object sender, EventArgs e)//删除书架
        {
            BookShelf shelf = shelfBindingSource.Current as BookShelf;
            BookShelfService.RemoveBookShelf(shelf.BookShelfId);
            shelfBindingSource.DataSource = BookShelfService.GetAllShelfs();
        }

        private void addButton_Click(object sender, EventArgs e)//添加图书，必须选取一个书架才能添加
        {
            BookShelf shelf = shelfBindingSource.Current as BookShelf;
            FormBookDetail formBookDetail = new FormBookDetail(shelf);
            if (formBookDetail.ShowDialog() == DialogResult.OK)
            {
                shelfBindingSource.DataSource = BookShelfService.GetAllShelfs();
                bookBindingSource.DataSource = shelfBindingSource;
                bookBindingSource.DataMember = "Books";
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)//删除图书，同上
        {
            Book book = bookBindingSource.Current as Book;
            if (book == null)
            {
                MessageBox.Show("请选择一本书进行操作！");
                return;
            }
            BookShelfService.RemoveBooks(book.BookId);
            shelfBindingSource.DataSource = BookShelfService.GetAllShelfs();
        }

        private void searchButton_Click(object sender, EventArgs e)//查询图书
        {
            if (searchTextBox.Text == null)
            {
                MessageBox.Show("没有输入用于查询的关键词！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                String key = searchTextBox.Text;
                List<Book> searchBooks = new List<Book>();
                switch (searchComboBox.Text)
                {
                    case "书号":
                        if (!isNormal)
                        {
                            foreach (var book in BookShelfService.AllBooks().Where(o => o.BookId.Contains(key) == true))
                            {
                                searchBooks.Add(book);
                            }
                        }
                        else
                        {
                            BookShelf shelf = shelfBindingSource.Current as BookShelf;
                            foreach (var book in BookShelfService.AllBooks().Where(o => o.BookId.Contains(key) == true).Where(i => i.BookShelfId == shelf.BookShelfId))
                            {
                                searchBooks.Add(book);
                            }
                        }
                        break;
                    case "书名":
                        if (!isNormal)
                        {
                            foreach (var book in BookShelfService.AllBooks().Where(o => o.Name.Contains(key) == true))
                            {
                                searchBooks.Add(book);
                            }
                        }
                        else
                        {
                            BookShelf shelf = shelfBindingSource.Current as BookShelf;
                            foreach (var book in BookShelfService.AllBooks().Where(o => o.Name.Contains(key) == true).Where(i => i.BookShelfId == shelf.BookShelfId))
                            {
                                searchBooks.Add(book);
                            }
                        }
                        break;
                    case "作者":
                        if (!isNormal)
                        {
                            foreach (var book in BookShelfService.AllBooks().Where(o => o.Author.Contains(key) == true))
                            {
                                searchBooks.Add(book);
                            }
                        }
                        else
                        {
                            BookShelf shelf = shelfBindingSource.Current as BookShelf;
                            foreach (var book in BookShelfService.AllBooks().Where(o => o.Author.Contains(key) == true).Where(i=>i.BookShelfId == shelf.BookShelfId))
                            {
                                searchBooks.Add(book);
                            }
                        }
                        break;
                    case "分类":
                        if (!isNormal)
                        {
                            foreach (var book in BookShelfService.AllBooks().Where(o => o.Sort.Contains(key) == true))
                            {
                                searchBooks.Add(book);
                            }
                        }
                        else
                        {
                            BookShelf shelf = shelfBindingSource.Current as BookShelf;
                            foreach (var book in BookShelfService.AllBooks().Where(o => o.Sort.Contains(key) == true).Where(i => i.BookShelfId == shelf.BookShelfId))
                            {
                                searchBooks.Add(book);
                            }
                        }
                        break;
                    default:
                        MessageBox.Show("没有选择正确的查询方式！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
                bookBindingSource.DataMember = null;
                bookBindingSource.DataSource = searchBooks;
            }
        }

        private void changeDetailButton_Click(object sender, EventArgs e)//修改图书细节
        {
            BookShelf shelf = shelfBindingSource.Current as BookShelf;
            Book book = bookBindingSource.Current as Book;
            FormBookDetail formBookDetail = new FormBookDetail(book,currentClient,2);
            if (formBookDetail.ShowDialog() == DialogResult.OK)
            {
                shelfBindingSource.DataSource = BookShelfService.GetAllShelfs();
                bookBindingSource.DataSource = shelfBindingSource;
                bookBindingSource.DataMember = "Books";
            }
        }

        private void managebySortButton_Click(object sender, EventArgs e)//改为按分类管理图书
        {
                    isNormal = false;
            shelfBindingSource.DataSource = BookShelfService.GetAllShelfs();
            bookBindingSource.DataMember = null;
            bookBindingSource.DataSource = BookShelfService.AllBooks();
        }

        private void refreshButton_Click(object sender, EventArgs e)//将管理方式改为按书架
        {
                    isNormal = true;
            shelfBindingSource.DataSource = BookShelfService.GetAllShelfs();
            bookBindingSource.DataSource = shelfBindingSource;
            bookBindingSource.DataMember = "Books";
        }
    }
}
