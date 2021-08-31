using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace std
{
    public partial class Form1 : Form
    {
        string userName;
        const int port = 8888;
        int flag1 = 1;
        const string address = "127.0.0.1";
        Socket socket;


        public Form1()
        {
            InitializeComponent();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void Form1_Load_1(object sender, EventArgs e)
        {
                  try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);
            }
            catch { }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int flag;
            flag = 1;

            userName = textBox1.Text;
            textBox1.ReadOnly = true;

            try
            {

                if (flag1 == 1)
                {
                    string message = " Hello ," + userName +", you are ours friend now.";
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    socket.Send(data);
                    flag1 = 0;
                }
                while (flag == 1)
                {
                    Console.Write(userName + ": ");
                    // ввод сообщения
                    string message1 = richTextBox1.Text;
                    string message2 = userName + ": " + message1;
                    byte[] data1 = Encoding.Unicode.GetBytes(message2);

                    socket.Send(data1);
                    richTextBox1.Clear();
                    flag = 0;
                }

            }
            catch (Exception ex)
            {
                richTextBox1.Text = ex.Message;
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}

