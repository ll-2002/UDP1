using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //��ʾ��Ϣ
            Console.WriteLine("�������ⰴ����ʼ����...");
            Console.ReadKey();
            
            int m;

            //��������׼��
            UdpClient client = new UdpClient();  //ʵ��һ���˿�
            IPAddress remoteIP = IPAddress.Parse("127.0.0.1");  //���跢�͸����IP
            int remotePort = 11000;  //���ö˿ں�
            IPEndPoint remotePoint = new IPEndPoint(remoteIP, remotePort);  //ʵ����һ��Զ�̶˵� 

            for(int i = 0; i < 50; i++)
            {
                //Ҫ���͵����ݣ���n�У�hello cqjtu���ؽ�����2018��
                string sendString = null;
                sendString += "��";
                m = i+1;
                sendString += m.ToString();
                sendString += "�У�hello cqjtu��cj����2019��";

                //���巢�͵��ֽ�����
                //���ַ���ת��Ϊ�ֽڲ��洢���ֽ�������
                byte[] sendData = null;
                sendData = Encoding.Default.GetBytes(sendString);

                client.Send(sendData, sendData.Length, remotePoint);//�����ݷ��͵�Զ�̶˵� 
            }
            client.Close();//�ر�����

            //��ʾ��Ϣ
            Console.WriteLine("");
            Console.WriteLine("���ݷ��ͳɹ�����������˳�...");
            System.Console.ReadKey();
        }
    }
}
