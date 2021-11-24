@[toc]

# 一、 UDP介绍：

 用户数据报协议，属于传输层的协议，无连接，不保证传输的可靠性。对于来自应用层的数据包，直接加上UDP报头然后传送给IP。UDP头部中有一个校验和字段，可用于差错的检测，但是UDP是不提供差错纠正的。此外IPV4不强制这个校验和字段必须使用，但IPV6是强制要求使用的。

# 二、C#实现hello world

>  用C#、Java或python编写一个命令行/控制台的简单hello world程序，实现如下功能：在屏幕上连续输出50行“hello cqjtu！重交物联2019级”；同时打开一个网络UDP 套接字，向另一台室友电脑发送这50行消息。

## 1、创建项目

 - 打开vs2022，选择创建控制台应用

![在这里插入图片描述](https://img-blog.csdnimg.cn/c8f615c8d47049b18116fcd7ff0cbb6c.png?x-oss-process=image/watermark,type_ZHJvaWRzYW5zZmFsbGJhY2s,shadow_50,text_Q1NETiBA6I-c5b6Q5Z2kMDAx,size_20,color_FFFFFF,t_70,g_se,x_16)

## 2、代码

```csharp
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

```

 - 运行结果：

![在这里插入图片描述](https://img-blog.csdnimg.cn/254d9b1514ea4d1997415843935b89ec.png?x-oss-process=image/watermark,type_ZHJvaWRzYW5zZmFsbGJhY2s,shadow_50,text_Q1NETiBA6I-c5b6Q5Z2kMDAx,size_20,color_FFFFFF,t_70,g_se,x_16)

## 3、主机间使用UDP通信

 - 客户端代码

```csharp
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
            //提示信息
            Console.WriteLine("按下任意按键开始发送...");
            Console.ReadKey();
            
            int m;

            //做好链接准备
            UdpClient client = new UdpClient();  //实例一个端口
            IPAddress remoteIP = IPAddress.Parse("127.0.0.1");  //假设发送给这个IP
            int remotePort = 11000;  //设置端口号
            IPEndPoint remotePoint = new IPEndPoint(remoteIP, remotePort);  //实例化一个远程端点 

            for(int i = 0; i < 50; i++)
            {
                //要发送的数据：第n行：hello cqjtu！重交物联2018级
                string sendString = null;
                sendString += "第";
                m = i+1;
                sendString += m.ToString();
                sendString += "行：hello cqjtu！cj物联2019级";

                //定义发送的字节数组
                //将字符串转化为字节并存储到字节数组中
                byte[] sendData = null;
                sendData = Encoding.Default.GetBytes(sendString);

                client.Send(sendData, sendData.Length, remotePoint);//将数据发送到远程端点 
            }
            client.Close();//关闭连接

            //提示信息
            Console.WriteLine("");
            Console.WriteLine("数据发送成功，按任意键退出...");
            System.Console.ReadKey();
        }
    }
}

```

 - 服务端代码：

```csharp
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
            string str = "第50行：hello cqjtu！cj物联2019级";
            UdpClient client = new UdpClient(11000);
            string receiveString = null;
            byte[] receiveData = null;
            //实例化一个远程端点，IP和端口可以随意指定，等调用client.Receive(ref remotePoint)时会将该端点改成真正发送端端点 
            IPEndPoint remotePoint = new IPEndPoint(IPAddress.Any, 0);
            Console.WriteLine("正在准备接收数据...");
            while (true)
            {
                receiveData = client.Receive(ref remotePoint);//接收数据 
                receiveString = Encoding.Default.GetString(receiveData);
                Console.WriteLine(receiveString);
                result = String.Compare(receiveString, str);
                if (result == 0)
                {
                    break;
                }
            }
            client.Close();//关闭连接
            Console.WriteLine("");
            Console.WriteLine("数据接收完毕，按任意键退出...");
            System.Console.ReadKey();
        }
    }
}

```

 - 客户端运行结果：
 ![在这里插入图片描述](https://img-blog.csdnimg.cn/859d9937b201493fb6d8930e02491fa0.png?x-oss-process=image/watermark,type_ZHJvaWRzYW5zZmFsbGJhY2s,shadow_50,text_Q1NETiBA6I-c5b6Q5Z2kMDAx,size_20,color_FFFFFF,t_70,g_se,x_16)
 - 服务端运行结果:
![在这里插入图片描述](https://img-blog.csdnimg.cn/9d774fdeb8ba4a1481e7599c83f331b1.png?x-oss-process=image/watermark,type_ZHJvaWRzYW5zZmFsbGJhY2s,shadow_50,text_Q1NETiBA6I-c5b6Q5Z2kMDAx,size_20,color_FFFFFF,t_70,g_se,x_16)

## 4. 使用Wireshark进行抓包
![在这里插入图片描述](https://img-blog.csdnimg.cn/e21b910104b94fc5a3e7407f318c9489.png?x-oss-process=image/watermark,type_ZHJvaWRzYW5zZmFsbGJhY2s,shadow_50,text_Q1NETiBA6I-c5b6Q5Z2kMDAx,size_20,color_FFFFFF,t_70,g_se,x_16)

# 三、Form窗口程序使用 UDP 通信

>  用VS2017/2019 的C#编写一个简单的Form窗口程序，有一个文本框 textEdit和一个发送按钮button，运行程序后，可以在文本框里输入文字，如“hello cqjtu！重交物联2019级”，点击button，将这些文字发送给室友电脑，采用UDP套接字；
>  

## 1、新建项目

 - 选择Windows窗口应用

![在这里插入图片描述](https://img-blog.csdnimg.cn/e05cdac291544f4d8aea886c0804d14c.png?x-oss-process=image/watermark,type_ZHJvaWRzYW5zZmFsbGJhY2s,shadow_50,text_Q1NETiBA6I-c5b6Q5Z2kMDAx,size_20,color_FFFFFF,t_70,g_se,x_16)

 - 如图：
 ![在这里插入图片描述](https://img-blog.csdnimg.cn/f8d56744e5e646c9a02be0cce53fc8a1.png?x-oss-process=image/watermark,type_ZHJvaWRzYW5zZmFsbGJhY2s,shadow_50,text_Q1NETiBA6I-c5b6Q5Z2kMDAx,size_20,color_FFFFFF,t_70,g_se,x_16)

## 2、界面设计

 - 从工具箱内拖 2 个TextBox 和 1 个Button 控件
 ![在这里插入图片描述](https://img-blog.csdnimg.cn/8172a8915fd84dc780462e65d9e96e1c.png?x-oss-process=image/watermark,type_ZHJvaWRzYW5zZmFsbGJhY2s,shadow_50,text_Q1NETiBA6I-c5b6Q5Z2kMDAx,size_20,color_FFFFFF,t_70,g_se,x_16)
 - 设置消息显示框属性
 ![在这里插入图片描述](https://img-blog.csdnimg.cn/e788be34f645427998e17f2325eb7765.png?x-oss-process=image/watermark,type_ZHJvaWRzYW5zZmFsbGJhY2s,shadow_50,text_Q1NETiBA6I-c5b6Q5Z2kMDAx,size_16,color_FFFFFF,t_70,g_se,x_16)

设置按钮框属性：
![在这里插入图片描述](https://img-blog.csdnimg.cn/801b718bd4a24fc6b223eef3c9b4f1bb.png?x-oss-process=image/watermark,type_ZHJvaWRzYW5zZmFsbGJhY2s,shadow_50,text_Q1NETiBA6I-c5b6Q5Z2kMDAx,size_15,color_FFFFFF,t_70,g_se,x_16)

 - 运行结果：

客户端：
![在这里插入图片描述](https://img-blog.csdnimg.cn/3b40605f4d78411a9bee0b5584bc7713.png?x-oss-process=image/watermark,type_ZHJvaWRzYW5zZmFsbGJhY2s,shadow_50,text_Q1NETiBA6I-c5b6Q5Z2kMDAx,size_20,color_FFFFFF,t_70,g_se,x_16)

服务端：
![在这里插入图片描述](https://img-blog.csdnimg.cn/f5ac79e1fef547f88367c897b83ac158.png?x-oss-process=image/watermark,type_ZHJvaWRzYW5zZmFsbGJhY2s,shadow_50,text_Q1NETiBA6I-c5b6Q5Z2kMDAx,size_20,color_FFFFFF,t_70,g_se,x_16)

## 4. 使用Wireshark进行抓包
![在这里插入图片描述](https://img-blog.csdnimg.cn/a52fb2b0da3c4a9cb3ca55d962a551ff.png?x-oss-process=image/watermark,type_ZHJvaWRzYW5zZmFsbGJhY2s,shadow_50,text_Q1NETiBA6I-c5b6Q5Z2kMDAx,size_20,color_FFFFFF,t_70,g_se,x_16)

# 四、总结
本次 实验总体来说内容还是比较多的，包含用C#写一个控制台程序，通过UDP发送给另一台电脑相关信息，以及通过可视化窗口的形式再次实现上述要求，并编写端口扫描程序，再进行抓包，总体来说还是有一定难度，在网上找了不少的参考资料，收获较多。


