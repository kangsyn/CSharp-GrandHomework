using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Client
    {
        //预设管理员账号为管理员，密码为admin
        public string Id { get; set; }//用户编号
        public string Name { get; set; }//用户名
        public string Password { get; set; }//密码
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
