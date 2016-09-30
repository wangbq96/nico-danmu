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

namespace NicoDanmuClient
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        Queue<Label> labelQueue = new Queue<Label>();

        System.Threading.Timer shoot_Data_Timer;
        System.Drawing.Color wordColor;
        int wordSize;
        int speed;

        ConsoleForm console ;

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
            Control.CheckForIllegalCrossThreadCalls = false;//解决"线程间操作无效: 从不是创建控件"Form1"的线程访问它."

            wordSize = 20;
            wordColor = Color.Black;
            speed = 8;

            console = new ConsoleForm(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //界面设置
            Left = Top = 0;
            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.TransparencyKey = this.BackColor;//透明穿透
            FormBorderStyle = FormBorderStyle.None;//去掉边框
            this.DoubleBuffered = true;//设置双缓冲

            //弹幕发射
            shoot_Data_Timer = new System.Threading.Timer(launch, null, 0, 2000);
            //弹幕移动
            new Thread(moveThread).Start();
            //显示控制台
            console.ShowDialog();
        }

        private void launch(object o)
        {
            while (labelQueue.Count() != 0) 
            {
                try
                {
                    Invoke((MethodInvoker)delegate//某个线程上创建的控件不能成为在另一个线程上创建的控件的父级
                    {
                        this.Controls.Add(labelQueue.Dequeue());
                    });
                }
                catch
                {
                    Console.WriteLine("Danmu launch fail");
                }
            }
        }

        private void moveThread()
        {
            while(true)
            {
                move();
                Thread.Sleep(100);
            }
        }

        private void move()
        {
            //Console.WriteLine("移动线程执行");
            foreach (System.Windows.Forms.Control item in this.Controls)
            {
                if (item is System.Windows.Forms.Label)
                {
                    //move_Timer.SynchronizingObject = item;
                    try
                    {
                        //item.Left = item.Left - (int)item.Tag;
                        item.Left = item.Left - speed;
                    }
                    catch
                    {
                        Console.WriteLine("item.Left = item.Left - (int)item.Tag;");
                    }
                    if (item.Left + item.Width < 0)
                    {
                        this.Controls.Remove(item);
                    }
                }
            }
            //Console.WriteLine("移动线程执行完毕");
        }

        public void loadDanmu(String s)
        {
            Label currentLabel = new Label();
            currentLabel.AutoSize = true;
            currentLabel.Top = random.Next(0, this.Height);
            currentLabel.Left = this.Width;
            currentLabel.BackColor = Color.Transparent;

            currentLabel.ForeColor = wordColor;
            currentLabel.Font = new Font("微软雅黑", wordSize);
            currentLabel.Text = s;
            //currentLabel.Tag = random.Next(3, 8);//设置移动速度

            labelQueue.Enqueue(currentLabel);
            console.appendText(s);
        }

        public void setSpeed(int s)
        {
            this.speed = s;
        }

        public void setWordSize(int s)
        {
            this.wordSize = s;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            DanmuManager.getInstance().closeSocket();
            System.Environment.Exit(0);
        }

        private void 显示控制台ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.ShowDialog();
        }
    }
}
