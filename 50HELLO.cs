using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "hello cqjtu！重交物联2019级";
            //因为下面要使用StringBuilder的Append函数
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 50; i++)
            {
                sb.Append(s);
            }
            //将StringBuilder转换为string，并写入
            Console.WriteLine(sb.ToString());
            //控制台显示
            Console.ReadLine();
        }
    }
}