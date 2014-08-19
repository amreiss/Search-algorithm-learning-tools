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
    public partial class Multitricky : Form
    {
        public Multitricky()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            G.count = 0;
            var small = new int[7, 20] { {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
{1,2,0,0,0,0,0,0,0,0,0,0,0,2,2,1,0,0,0,1},
{1,2,1,1,2,1,1,2,1,1,2,1,1,2,1,1,0,1,0,1},
{1,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,1,0,1},
{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1},
{1,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}

 };
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 20; j++)
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
                    if (small[i, j] == 3)
                    {
                        int length = j * 20;
                        int bredth = i * 20;

                        G.stx = i; G.sty = j;
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
