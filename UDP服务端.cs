using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            int result;
            string str = "��50�У�hello cqjtu��cj����2019��";
            UdpClient client = new UdpClient(11000);
            string receiveString = null;
            byte[] receiveData = null;
            //ʵ����һ��Զ�̶˵㣬IP�Ͷ˿ڿ�������ָ�����ȵ���client.Receive(ref remotePoint)ʱ�Ὣ�ö˵�ĳ��������Ͷ˶˵� 
            IPEndPoint remotePoint = new IPEndPoint(IPAddress.Any, 0);
            Console.WriteLine("����׼����������...");
            while (true)
            {
                receiveData = client.Receive(ref remotePoint);//�������� 
                receiveString = Encoding.Default.GetString(receiveData);
                Console.WriteLine(receiveString);
                result = String.Compare(receiveString, str);
                if (result == 0)
                {
                    break;
                }
            }
            client.Close();//�ر�����
            Console.WriteLine("");
            Console.WriteLine("���ݽ�����ϣ���������˳�...");
            System.Console.ReadKey();
        }
    }
}