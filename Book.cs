using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Book { 
        //[Key]
        public string BookId { get; set; }//书号，自动识别为主键
        public string Name { get; set; }//名字
        public string Author { get; set; }//作者
        public string State { get; set; }//状态
        public string AppointedTime { get; set; }//预约的时间，也可表示到期的时间
        public string Description { get; set; }//描述
        public string ClientName { get; set; }//使用人
        public int Recommend { get; set; }//推荐数，选择前4位的数推荐
        //[ForeignKey("BookShelfId")]
        public string BookShelfId { get; set; }//所属书架号，自动识别为外键
        public string Sort { get; set; }//分类
        public List<String> Appointers { get; set; }//预约人的名字
        public string Appoint { get; set; }//预约情况，显示名字和序号
        //public BookShelf BookShelf { get; set; }//多对一关联

        public Book()
        {
            //BookId = Guid.NewGuid().ToString();
            Name = "";
            Author = "";
            State = "";
            AppointedTime = "";
            Description = "";
            ClientName = "";
            Recommend = 0;
            Sort = "";
            Appointers = new List<String>();
        }
        public Book(string name, string author,string description) : this()
        {
            this.Name = name;
            this.Author = author;
            //this.State = state;
            //this.AppointedTime = appointedTime;
            this.Description = description;
            //this.ClientName = clientName;
            //this.Recommend = recommend;
        }
        public override int GetHashCode()
        {
            var hashCode = -2127770830;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(BookId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Author);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(State);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(AppointedTime);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ClientName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(BookShelfId);
            hashCode = hashCode * -1521134295 + Recommend.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            var book = obj as Book;
            return book != null && 
                Name == book.Name && 
                Author == book.Author && 
                Description == book.Description && 
                Recommend == book.Recommend && 
                BookShelfId == book.BookShelfId;

        }
        public void setAppoint()//设置预约情况，显示名字和序号（也就是从Appointers列表里，挨个取元素，格式为“序号：用户名”+“ ”+“序号：用户名”。。。）
        {
            foreach(var c in Appointers)
            {
                Appoint = Appoint + Appointers.IndexOf(c) + " " + c + " ";
            }
        }
    }
}
