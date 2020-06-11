using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    class ClientService
    {
        public static bool Register(Client client)//注册
        {
            List<Client> clients = AllClients();
            var query = from c in clients where (c.Name == client.Name) select c;
            if (query.Count() == 0)
            {
                client.Id = clients.Count + 1 + "";
                AddClient(client);
                return true;
            }
            else
            {
                MessageBox.Show("错误！已有该账号！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }
        public static bool Sighin(Client client)//登录
        {
            List<Client> clients = AllClients();
            var query = from c in clients where (c.Name == client.Name) select c;
            if (query.Count() == 1)
            {
                Client shouldClient = query.First();
                if(MatchClient(client, shouldClient))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                MessageBox.Show("错误！没有有该账号！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }
        public static List<Client> AllClients()//取得所有的用户
        {
            using (var db = new BookShelfContext())
            {
                return db.Clients.ToList();
            }
        }
        public static bool AddClient(Client client)//添加新的用户
        {
            try
            {
                using (var db = new BookShelfContext())
                {
                    db.Clients.Add(client);
                    db.SaveChanges();
                    MessageBox.Show("已成功注册账号！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("注册账号错误！" + e.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw new ApplicationException($"添加书架错误: {e.Message}");
            }
        }
        public static void AddAdministrator(Client admin)//添加管理员
        {
            try
            {
                using (var db = new BookShelfContext())
                {
                    db.Clients.Add(admin);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("注册账号错误！" + e.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw new ApplicationException($"添加书架错误: {e.Message}");
            }
        }
        public static bool MatchClient(Client currentClient, Client shouldClient)
        {
            if(currentClient.Name == shouldClient.Name)
            {
                if(currentClient.Password == shouldClient.Password)
                {
                    MessageBox.Show("已成功登录！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show("密码错误！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("账号错误！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }
    }
}
