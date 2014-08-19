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
    public partial class sublarge : Form
    {
        public sublarge()
        {
            InitializeComponent();
        }
        public int position(int x, int y)
        {
            int result = 10000 * x + y;
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            G.count = 0;
            G.maxf = 0;
            G.path = 0;                                                                                                 //function which loads the maze in to the interface
            var small = new int[8, 31] { {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},{1,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,1},{1,1,1,2,1,2,2,2,1,1,1,2,2,2,2,2,2,2,2,2,1,2,1,2,2,2,1,2,1,1,1},{1,2,2,2,1,1,1,2,1,2,1,1,1,1,2,1,2,1,1,1,1,1,1,2,1,1,1,2,2,2,1},{1,2,1,2,2,2,2,2,1,2,2,2,2,2,2,1,2,2,2,2,2,2,1,2,2,2,2,2,1,2,1},{1,2,1,1,1,2,1,1,1,1,1,2,1,1,1,1,1,1,1,2,1,1,1,2,1,2,1,1,1,1,1},{1,2,2,2,2,2,1,2,2,2,2,2,2,2,2,3,2,2,2,2,1,2,2,2,1,2,2,2,2,2,1},{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}};
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 31; j++)
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

                        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);
                        System.Drawing.Graphics formGraphics = this.CreateGraphics();
                        formGraphics.FillRectangle(myBrush, new Rectangle(length, bredth, 20, 20));
                        myBrush.Dispose();
                        formGraphics.Dispose();
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            G.d = 0;
            var small = new int[8, 31] { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 }, { 1, 1, 1, 2, 1, 2, 2, 2, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 2, 1, 2, 2, 2, 1, 2, 1, 1, 1 }, { 1, 2, 2, 2, 1, 1, 1, 2, 1, 2, 1, 1, 1, 1, 2, 1, 2, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 2, 2, 2, 1 }, { 1, 2, 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1, 2, 1 }, { 1, 2, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 2, 1, 2, 1, 1, 1, 1, 1 }, { 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 1, 2, 2, 2, 1, 2, 2, 2, 2, 2, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };
           
            G.stack = new int[100000];                                                                      //function which does the search to eat all the food items in the maze
            for (G.i = 0; G.i < 100000; G.i++)
                G.stack[G.i] = 0;

            G.i = G.stx; G.j = G.sty;
            int z = 0;
            G.path = 157;
            int value = position(G.i, G.j);


            G.stack[z] = value;
            G.top = 0;
            int length, bredth;
            G.z = 1;

            int flag = 1;
            int current = value;
            
            string p = "Path";
            for (int y = 0; y < G.count  ; y++)
            {
                p = p + "(" + G.i.ToString() + "," + G.j.ToString() + ")-->";
                G.i = current / 10000;
                G.j = current % 10000;
                G.z = 1;
                G.top = 0;
                for (int i = 0; i < 100000; i++)
                    G.stack[i] = 0;
                G.stack[0] = current;
                length = G.j * 20;
                bredth = G.i * 20;
                System.Threading.Thread.Sleep(20);
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue); //function to display how the nodes are being eaten
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(length, bredth, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();




                for (; ; )
                {


                    int i = G.i;
                    int j = G.j;
                    G.d++;
                    flag = 0;                                                                                                   //function which searches all the surrounding nodes if they are filled with food ot emty path
                    if (G.i != 0 && G.j != 0)
                    {
                        if (small[i, j - 1] == 2)
                        {

                            int sign = 1;
                            int d = position(i, j - 1);
                            for (int h = 0; h < G.z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;

                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i, j - 1] = 0;
                                flag++;
                                current = d;
                                small[i, j - 1] = 0;
                                break;

                            }
                        }
                        if (small[i, j - 1] == 0)
                        {
                            int sign = 1;
                            int d = position(i, j - 1);
                            for (int h = 0; h < G.z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;

                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;

                                flag++;
                                current = d;




                            }
                        }
                        if (small[i, j + 1] == 2)
                        {
                            int sign = 1;
                            int d = position(i, j + 1);
                            for (int h = 0; h < G.z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i, j + 1] = position(i, j);
                                flag++;
                                current = d;
                                small[i, j + 1] = 0;
                                break;
                            }
                        }
                        if (small[i, j + 1] == 0)
                        {
                            int sign = 1;
                            int d = position(i, j + 1);
                            for (int h = 0; h < G.z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;

                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;

                                flag++;
                                current = d;



                            }
                        }
                        if (small[i - 1, j] == 2)
                        {
                            int sign = 1;

                            int d = position(i - 1, j);
                            for (int h = 0; h < G.z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;

                                flag++;
                                current = d;
                                small[i - 1, j] = 0;
                                break;

                            }




                        }
                        if (small[i - 1, j] == 0)
                        {
                            int sign = 1;
                            int d = position(i - 1, j);
                            for (int h = 0; h < G.z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;

                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;

                                flag++;
                                current = d;



                            }
                        }

                        if (small[i + 1, j] == 2)
                        {
                            int sign = 1;
                            int d = position(i + 1, j);
                            for (int h = 0; h < G.z; h++)
                            {

                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;

                                flag++;
                                current = d;
                                small[i + 1, j] = 0;
                                break;
                            }

                        }
                        if (small[i + 1, j] == 0)
                        {
                            int sign = 1;
                            int d = position(i + 1, j);
                            for (int h = 0; h < G.z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;

                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;

                                flag++;
                                current = d;



                            }
                        }
                        G.top++;
                        current = G.stack[G.top];

                        G.i = current / 10000;
                        G.j = current % 10000;
                    }
                }
                
            }
            MessageBox.Show(p + Environment.NewLine + "Nodes Expanded = " + G.d.ToString() + Environment.NewLine + "Path cost = " + G.path);//display the different items
        }
    }
}
