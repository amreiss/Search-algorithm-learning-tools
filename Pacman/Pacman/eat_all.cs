using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pacman
{
    public partial class eat_all : Form
    {
        public eat_all()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            G.count = 0;
            var small = new int[9, 9] { { 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 2, 0, 0, 1, 2, 2, 0, 1 }, { 1, 0, 1, 0, 1, 1, 1, 0, 1 }, { 1, 0, 1, 0, 2, 0, 0, 2, 1 }, { 1, 0, 1, 1, 3, 1, 1, 0, 1 }, { 1, 2, 0, 2, 2, 0, 0, 2, 1 }, { 1, 0, 1, 0, 1, 1, 1, 1, 1 }, { 1, 2, 0, 2, 0, 0, 2, 2, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1 } };
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (small[i, j] == 1)
                    {
                        int length = j * 20;
                        int bredth = i * 20;


                        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                        System.Drawing.Graphics formGraphics = this.CreateGraphics();
                        formGraphics.FillRectangle(myBrush, new Rectangle(length, bredth, 20, 20));
                        myBrush.Dispose();
                        formGraphics.Dispose();
                    }
                    if (small[i, j] == 2)
                    {
                        int length = j * 20;
                        int bredth = i * 20;
                        G.count++;

                        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Gray);
                        System.Drawing.Graphics formGraphics = this.CreateGraphics();
                        formGraphics.FillRectangle(myBrush, new Rectangle(length, bredth, 20, 20));
                        myBrush.Dispose();
                        formGraphics.Dispose();
                    }
                }
            }
        }
       
    }
}
