using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Client
    {
        //预设管理员账号为Admin，密码为123
        public string Id { get; set; }//用户编号
        public string Name { get; set; }//用户名
        public string Password { get; set; }//密码
        public List<Book> LendBooks { get; set; }//被借阅的书籍
        public List<Book> AppointedBooks { get; set; }//被预约的书籍
        public Client()
        {
            Id = Guid.NewGuid().ToString();
        }
        public Client(string name, string password)
        {
            Name = name; Password = password;
        }
    }
}
