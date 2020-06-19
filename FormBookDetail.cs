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
        public Client currentClient { get; set; }//现在正在使用的用户
        public Book currentBook { get; set; }//现在正在查看的图书
        public int Flag = 0;//指示该界面由主界面还是图书管理界面进入（可能无用）
        public FormBookDetail()//无参数，表示是由添加新书按钮进入，所有文本框均为空并可用，但借阅、预约、推荐相关组件不可使用，基本完成
        {
            InitializeComponent();
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
        }

        public FormBookDetail(Book book,Client client, int flag)//表示是由查看书籍详情进入的,flag的值取1、2（分别表示从主界面进入和从图书管理界面进入，前者只读），基本完成
        {
            InitializeComponent();
            this.nameTextBox.Text = book.Name;
            this.authorTextBox.Text = book.Author;
            this.sortTextBox.Text = book.Sort;
            this.shelfTextBox.Text = book.BookShelfId;
            this.introductionTextBox.Text = book.Description;
            if (flag == 1)//从主界面进入则不能更改内容，加入书架按钮也被隐藏
            {
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
            }
        }

        private void lendButton_Click(object sender, EventArgs e)//借阅,当图书可用时将图书的状态改为借阅
        {

        }

        private void recommendButton_Click(object sender, EventArgs e)//推荐，推荐数+1
        {

        }

        private void appointButton_Click(object sender, EventArgs e)//预约，预约时间必须后于当前时间（可能要涉及DateTime），并同时改动预约情况
        {

        }

        private void addButton_Click(object sender, EventArgs e)//增加图书
        {

        }

        private void finishButton_Click(object sender, EventArgs e)//完成
        {
            this.Close();
        }
    }
}
