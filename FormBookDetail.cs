using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class FormBookDetail : Form
    {
        public BookShelf currentShelf { get; set; }
        public Client currentClient { get; set; }//现在正在使用的用户
        public Book currentBook { get; set; }//现在正在查看的图书
        public string path { get; set; }
        public static string resPath { get; set; }
        public int Flag = 0;//指示该界面由主界面还是图书管理界面进入
        public FormBookDetail()//无参数，表示是由添加新书按钮进入，所有文本框均为空并可用，但借阅、预约、推荐相关组件不可使用完成
        {
            InitializeComponent();
            currentShelf = shelf;
            this.lendButton.Enabled = false;
            this.lendButton.Visible = false;
            this.recommendButton.Enabled = false;
            this.recommendButton.Visible = false;
            this.appointButton.Enabled = false;
            this.appointButton.Visible = false;
            this.shelfTextBox.Text = shelf.BookShelfId;
            this.shelfTextBox.ReadOnly = true;

            //与预约相关的控件
            this.textBox1.Enabled = false;
            this.textBox1.Visible = false;
            this.textBox2.Enabled = false;
            this.textBox2.Visible = false;
            this.textBox3.Enabled = false;
            this.textBox3.Visible = false;
            this.yearComboBox.Enabled = false;
            this.yearComboBox.Visible = false;
            this.monthComboBox.Enabled = false;
            this.monthComboBox.Visible = false;
            this.dayComboBox.Enabled = false;
            this.dayComboBox.Visible = false;
            findPath();
        }

        public FormBookDetail(Book book,Client client, int flag)//表示是由查看书籍详情进入的,flag的值取1、2（分别表示从主界面进入和从图书管理界面进入，前者只读），完成
        {
            InitializeComponent();
            this.nameTextBox.Text = book.Name;
            this.authorTextBox.Text = book.Author;
            this.sortTextBox.Text = book.Sort;
            this.shelfTextBox.Text = book.BookShelfId;
            this.introductionTextBox.Text = book.Description;
            this.currentClient = client;
            this.currentBook = book;
            if(book.imagePath != null)
            {
                if (File.Exists(book.imagePath))
                {
                    this.discoverPictureBox.Image = Image.FromFile(book.imagePath);
                }
            }
            if (flag == 1)//从主界面进入则不能更改内容，加入书架按钮也被隐藏
            {
                Flag = 1;
                this.nameTextBox.ReadOnly = true;
                this.authorTextBox.ReadOnly = true;
                this.sortTextBox.ReadOnly = true;
                this.shelfTextBox.ReadOnly = true;
                this.introductionTextBox.ReadOnly = true;
                this.addButton.Enabled = false;
                this.addButton.Visible = false;
            }
            else//从图书管理界面进入则借阅、推荐、预约按钮被隐藏
            {
                Flag = 2;
                this.shelfTextBox.ReadOnly = true;
                this.lendButton.Enabled = false;
                this.lendButton.Visible = false;
                this.recommendButton.Enabled = false;
                this.recommendButton.Visible = false;
                this.appointButton.Enabled = false;
                this.appointButton.Visible = false;
                //与预约相关的控件
                this.textBox1.Enabled = false;
                this.textBox1.Visible = false;
                this.textBox2.Enabled = false;
                this.textBox2.Visible = false;
                this.textBox3.Enabled = false;
                this.textBox3.Visible = false;
                this.yearComboBox.Enabled = false;
                this.yearComboBox.Visible = false;
                this.monthComboBox.Enabled = false;
                this.monthComboBox.Visible = false;
                this.dayComboBox.Enabled = false;
                this.dayComboBox.Visible = false;
                this.addButton.Enabled = false;
                this.addButton.Visible = false;
            }
            findPath();
        }

        private void lendButton_Click(object sender, EventArgs e)//借阅,当图书可用时将图书的状态改为借阅
        {
            if (yearComboBox.Text != "" && monthComboBox.Text != "" && dayComboBox.Text != "")
            {
                string year = yearComboBox.Text;
                string month = monthComboBox.Text;
                string day = dayComboBox.Text;
                BookShelfService.LendBook(currentBook, currentClient, year, month, day);
            }
            else
            {
                MessageBox.Show("没有输入正确的日期！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void recommendButton_Click(object sender, EventArgs e)//推荐，推荐数+1
        {
            currentBook.Recommend = currentBook.Recommend + 1;
            BookShelfService.UpdateBook(currentBook);
            this.recommendButton.Enabled = false;
            this.recommendButton.Visible = false;
        }

        private void appointButton_Click(object sender, EventArgs e)//预约
        {
            BookShelfService.AppointBook(currentBook, currentClient);
            this.appointButton.Enabled = false;
            this.appointButton.Visible = false;
        }

        private void addButton_Click(object sender, EventArgs e)//增加图书
        {
            if (nameTextBox.Text != null && authorTextBox.Text != null && sortTextBox.Text != null && introductionTextBox.Text != null)
            {
                int k = Convert.ToInt32(BookShelfService.AllBooks().Max(i => i.BookId)) + 1;
                string bookId = k + "";
                string name = this.nameTextBox.Text;
                string author = this.authorTextBox.Text;
                string bookShelfId = currentShelf.BookShelfId;
                string lendTime = null;
                string clientName = null;
                string sort = this.sortTextBox.Text;
                string appointers = "";
                string description = this.introductionTextBox.Text;
                Book newBook = new Book(bookId, name, bookShelfId, author, description, lendTime, clientName, sort, appointers) { State = "可正常使用"};
                if(path != null)
                {
                    File.Copy(path, resPath + @"\" + newBook.Name + ".jpg");
                    newBook.imagePath = path;
                }
                currentShelf.AddBook(newBook);
                BookShelfService.UpdateShelf(currentShelf);
                MessageBox.Show("已添加新书！");
                this.addButton.Enabled = false;
                this.addButton.Visible = false;
            }
            else
            {
                MessageBox.Show("没有输入完整的信息！");
            }
        }

       private void finishButton_Click(object sender, EventArgs e)//完成
        {
            if(Flag == 2)
            {
                currentBook.Name = this.nameTextBox.Text;
                currentBook.Author = this.authorTextBox.Text;
                currentBook.Sort = this.sortTextBox.Text;
                currentBook.Description = this.introductionTextBox.Text;
                if(path != null)
                {
                    if (File.Exists(resPath + @"\" + currentBook.Name + ".jpg")) //判断文件是否存在
                    {
                        File.Delete(resPath + @"\" + currentBook.Name + ".jpg");
                    }
                    File.Copy(path, resPath + @"\" + currentBook.Name + ".jpg");
                    currentBook.imagePath = path;
                }
                BookShelfService.UpdateBook(currentBook);
            }
            this.Close();
        }

        public void findPath()
        {
            string basePath = Directory.GetCurrentDirectory();
            resPath = basePath.Replace(@"\bin\Debug", @"\Resources");
        }
        private void chooseImageButton_Click(object sender, EventArgs e)//设置图像
        {
                this.openFileDialog.ShowDialog();
                path = openFileDialog.FileName;
            if(path.Contains(".jpg") || path.Contains(".png"))
            {
                discoverPictureBox.Image = Image.FromFile(path);
            }
            else
            {
                MessageBox.Show("请选择图片文件！");
            }
        }
    }
}
