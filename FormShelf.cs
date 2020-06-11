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
        public FormShelf()
        {
            InitializeComponent();
        }

        private void addShelfButton_Click(object sender, EventArgs e)//添加书架，直接添加一个新的空书架
        {

        }

        private void deleteShelfButton_Click(object sender, EventArgs e)//删除书架
        {

        }

        private void addButton_Click(object sender, EventArgs e)//添加图书，必须选取一个书架才能添加
        {

        }

        private void deleteButton_Click(object sender, EventArgs e)//删除图书，同上
        {

        }

        private void searchButton_Click(object sender, EventArgs e)//查询图书
        {

        }

        private void changeDetailButton_Click(object sender, EventArgs e)//修改图书细节
        {

        }

        private void managebySortButton_Click(object sender, EventArgs e)//改为按分类管理图书（预计是把bookBindingSource绑定的值换成数据库内所有书，暂不考虑）
        {

        }

        private void refreshButton_Click(object sender, EventArgs e)//将管理方式改为按书架（预计是把bookBindingSource绑定的值换成预设的值，暂不考虑）
        {

        }
    }
}
