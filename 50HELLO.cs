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
            string s = "hello cqjtu���ؽ�����2019��";
            //��Ϊ����Ҫʹ��StringBuilder��Append����
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 50; i++)
            {
                sb.Append(s);
            }
            //��StringBuilderת��Ϊstring����д��
            Console.WriteLine(sb.ToString());
            //����̨��ʾ
            Console.ReadLine();
        }
    }
}