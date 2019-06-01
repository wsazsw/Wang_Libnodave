using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {

        public libnodave.daveOSserialType fds; //连接类型说明
        public libnodave.daveInterface di;     //连接接口声明
        public libnodave.daveConnection dc;    //连接声明
        public int res;                       //声明函数返回值
        public byte plcValue;                 //从PLC读取的数值
        public int memoryres;
        public int arera;
        public byte[] memoryBuffer = new byte[200];
        public byte[] byteBuffer = new byte[0];
        public byte[] intBuffer = new byte[1];
        public byte[] floatBuffer = new byte[3];


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fds.rfd = libnodave.openSocket(102, textBox2.Text);
           
                
            fds.wfd = fds.rfd;

            di = new libnodave.daveInterface(fds, "IF1", 0, libnodave.daveProtoISOTCP, libnodave.daveSpeed187k);
            di.setTimeout(1000);
            res = di.initAdapter();
            try
            {
                dc = new libnodave.daveConnection(di, 0, 0, 1);
                res = dc.connectPLC();
                MessageBox.Show("连接成功");
            }
            catch (Exception)
            {

                MessageBox.Show("连接失败");
            }


       
            
        }
        //byte
        private void button2_Click(object sender, EventArgs e)
        {

            if (Int32.Parse(textBox4.Text) != 0)
            {
                arera = libnodave.daveDB;
            }
            if (Int32.Parse(textBox4.Text) == 0)
            {
                arera = libnodave.daveFlags;
            }

            memoryres = 0;
                memoryres = dc.readBytes(arera, Int32.Parse(textBox4.Text), Int32.Parse(textBox3.Text), 1, memoryBuffer);
               
            if (memoryres == 0)
                {

                
                    textBox5.Text = Convert.ToString(memoryBuffer[0]);
                
                
                }
                else
                {
                MessageBox.Show("读取失败");
                }



            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(textBox4.Text) != 0)
            {
                arera = libnodave.daveDB;
                arera = libnodave.daveDB;
                arera = libnodave.daveDB;
                arera = libnodave.daveDB;
            }
            if (Int32.Parse(textBox4.Text) == 0)
            {
                arera = libnodave.daveFlags;
            }
            memoryres = 0;
            byteBuffer = BitConverter.GetBytes(Int32.Parse(textBox6.Text));
            memoryres = dc.writeBytes(arera, Int32.Parse(textBox4.Text), Int32.Parse(textBox7.Text),1, byteBuffer);
            if (memoryres!=0)
            {
                MessageBox.Show("Error");
            }

        }

        //PLC:  float
        private void button4_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(textBox4.Text) != 0)
            {
                arera = libnodave.daveDB;
            }
            if (Int32.Parse(textBox4.Text) == 0)
            {
                arera = libnodave.daveFlags;
            }
            memoryres = 0;
            memoryres = dc.readBytes(arera, Int32.Parse(textBox4.Text), Int32.Parse(textBox9.Text), 4, memoryBuffer);

            if (memoryres == 0)
            {

               
                textBox8.Text = Convert.ToString(dc.getFloatAt(0));
            }
            else
            {
                MessageBox.Show("读取失败"); 
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(textBox4.Text) != 0)
            {
                arera = libnodave.daveDB;
            }
            if (Int32.Parse(textBox4.Text) == 0)
            {
                arera = libnodave.daveFlags;
            }
            floatBuffer = BitConverter.GetBytes(Convert.ToSingle(textBox10.Text));
            Array.Reverse(floatBuffer, 0, 4);
            memoryres = 0;
            memoryres = dc.writeBytes(arera, Int32.Parse(textBox4.Text), Int32.Parse(textBox11.Text), 4, floatBuffer);
            if (memoryres != 0)
            {
                MessageBox.Show("Error");
            }

        }


        //INT
        private void button7_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(textBox4.Text) != 0)
            {
                arera = libnodave.daveDB;
            }
            if (Int32.Parse(textBox4.Text) == 0)
            {
                arera = libnodave.daveFlags;
            }
            memoryres = 0;
            memoryres = dc.readBytes(arera, Int32.Parse(textBox4.Text), Int32.Parse(textBox15.Text), 2, memoryBuffer);

            if (memoryres == 0)
            {

                
                textBox14.Text = Convert.ToString(dc.getS16At(0));
            }
            else
            {
                MessageBox.Show("读取失败"); 
            }



        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(textBox4.Text) != 0)
            {
                arera = libnodave.daveDB;
            }
            if (Int32.Parse(textBox4.Text) == 0)
            {
                arera = libnodave.daveFlags;
            }
            intBuffer = BitConverter.GetBytes(Convert.ToInt16(textBox12.Text));
            Array.Reverse(intBuffer,0,2);
            memoryres = 0;
            memoryres = dc.writeBytes(arera, Int32.Parse(textBox4.Text), Int32.Parse(textBox13.Text), 2, intBuffer);
            if (memoryres != 0)
            {
                MessageBox.Show("Error");
            }

        }


        //bool
        private void button8_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(textBox4.Text) != 0)
            {
                arera = libnodave.daveDB;
            }
            if (Int32.Parse(textBox4.Text) == 0)
            {
                arera = libnodave.daveFlags;
            }

            memoryres = 0;
            memoryres = dc.readBits(arera, Int16.Parse(textBox4.Text), Int16.Parse(textBox17.Text)*8+ Int16.Parse(textBox16.Text), 0, memoryBuffer);
            if (memoryres == 0)
            {
                if (memoryBuffer[0]==1)
                {
                    MessageBox.Show("True");
                }

                if (memoryBuffer[0] == 0)
                {
                    MessageBox.Show("False");
                }
                
            }
            else
            { MessageBox.Show("读取失败"); }
                
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(textBox4.Text) != 0)
            {
                arera = libnodave.daveDB;
            }
            if (Int32.Parse(textBox4.Text) == 0)
            {
                arera = libnodave.daveFlags;
            }


            memoryres = 0;
            memoryBuffer[0] = 1;
            memoryres = dc.writeBits(arera, Int16.Parse(textBox4.Text), Int16.Parse(textBox19.Text) * 8 + Int16.Parse(textBox18.Text), 1, memoryBuffer);

            if (memoryres == 0)
            {
               MessageBox.Show("设置成功");
              


            }
            else
            { MessageBox.Show("设置失败"); }







        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(textBox4.Text) != 0)
            {
                arera = libnodave.daveDB;
            }
            if (Int32.Parse(textBox4.Text) == 0)
            {
                arera = libnodave.daveFlags;
            }


            memoryres = 0;
            memoryBuffer[0] = 0;
            memoryres = dc.writeBits(arera, Int16.Parse(textBox4.Text), Int16.Parse(textBox19.Text) * 8 + Int16.Parse(textBox18.Text), 1, memoryBuffer);

            if (memoryres == 0)
            {
                MessageBox.Show("设置成功");



            }
            else
            { MessageBox.Show("设置失败"); }


        }
    }
}
