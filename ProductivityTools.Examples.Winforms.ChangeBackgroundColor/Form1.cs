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

namespace ProductivityTools.Examples.Winforms.ChangeBackgroundColor
{
    delegate void ChangeColorDelegate(Color c);
    public partial class Form1 : Form
    {
        Thread thread;
        Boolean b;
        Random Random;


        public Form1()
        {
            InitializeComponent();
            thread = new Thread(ChangeColorBackground);
            thread.Start();
            Random = new Random();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            b = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            b = false;
        }

        private void ChangeColorBackground()
        {
            while (true)
            {
                if (b == true)
                {

                    int r = Random.Next(0, 254);
                    int n = Random.Next(0, 254);
                    int d = Random.Next(0, 254);
                    Color c = Color.FromArgb(r, n, d);
                    ChangeColorThreadSafe(c);
                }
                System.Threading.Thread.Sleep(100);
            }
        }

        private void ChangeColorThreadSafe(Color c)
        {
            if (button1.InvokeRequired)
            {
                ChangeColorDelegate delgate = new ChangeColorDelegate(ChangeColorThreadSafe);
                button1.Invoke(delgate, new object[] { c });
            }
            else
            {
                this.BackColor = c;
                //   Application.DoEvents();
            }
        }
    }
}
