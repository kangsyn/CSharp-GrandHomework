using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public class Book { 
        public string BookId { get; set; }//书号，自动识别为主键
        public string Name { get; set; }//名字
        public string Author { get; set; }//作者
        public string State { get; set; }//状态
        public string LendTime { get; set; }//到期的时间
        public string Description { get; set; }//描述
        public string ClientName { get; set; }//使用人
        public int Recommend { get; set; }//推荐数，选择前4位的数推荐
        public string BookShelfId { get; set; }//所属书架号，自动识别为外键
        public string Sort { get; set; }//分类
        public String Appointers { get; set; }//预约人
        public int reNewNum { get; set; }//续借的次数
        public string imagePath { get; set; }//图像路径

        public Book()
        {
            Name = "";
            Author = "";
            State = "";
            LendTime = "";
            Description = "";
            ClientName = "";
            Recommend = 0;
            Sort = "";
        }
        public Book(string bookID,string name, string shelfID, string author,string description,string lend, string client, string sort,string appointers)
        {
            BookId = bookID;
            Name = name;
            Author = author;
            BookShelfId = shelfID;
            LendTime = lend;
            ClientName = client;
            Sort = sort;
            Appointers = appointers;
            Description = description;
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
    }
}
