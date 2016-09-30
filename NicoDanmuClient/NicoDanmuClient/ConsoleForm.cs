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
    public partial class ConsoleForm : Form
    {
        Form1 f;

        public ConsoleForm(Form1 f)
        {
            InitializeComponent();
            this.f = f;
            button2.Enabled = false;
        }

        private void ConsoleForm_Load(object sender, EventArgs e)
        {

        }

        public void appendText(String s)
        {
            DateTime d = DateTime.Now;
            textBox4.Text += d.ToString("HH:mm:ss") + " : "+s+"\r\n";
            textBox4.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            appendText("[System]Connecting...");
            if ( DanmuManager.getInstance().connect(textBox1.Text.ToString()) )
            {
                appendText("[System]Connect success");
                button1.Enabled = false;
                button2.Enabled = true;
            }
            else
            {
                appendText("[System]Connect fail");
                button1.Enabled = true;
                button2.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DanmuManager.getInstance().closeSocket();
            appendText("[System]Disconnect");
            button1.Enabled = true;
            button2.Enabled = false;
        }
    }
}
