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
    public partial class FormRegisterandSignin : Form
    {
        public int Flag = 0;
        public Client currentClient { get; set; }
        public Client testClient { get; set; }
        public FormRegisterandSignin(int flag)
        {
            InitializeComponent();
            this.Flag = flag;
            if(Flag == 1)
            {
                this.Text = "登录";
                this.finishButton.Text = "登录";
            }
            else
            {
                this.Text = "注册";
                this.finishButton.Text = "注册";
            }
        }

        private void finishButton_Click(object sender, EventArgs e)
        {
            if(nameTextBox.Text == null)
            {
                MessageBox.Show("没有输入账号！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(passwordTextBox.Text == null)
            {
                MessageBox.Show("没有输入账号！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            testClient = new Client(nameTextBox.Text, passwordTextBox.Text);
            if (Flag == 1 && ClientService.Sighin(testClient) != true)
            {
                MessageBox.Show("输入的数据不正确！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(Flag == 2 && ClientService.Register(testClient)!= true)
            {
                MessageBox.Show("没有输入账号！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            currentClient = testClient;
            this.Close();
        }
    }
}
