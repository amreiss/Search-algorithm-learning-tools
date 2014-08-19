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
    public partial class small_maze : Form
    {
        
        public small_maze()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {

            var small = new int[10, 22] { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1 }, { 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1 }, { 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 1, 0, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1 }, { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };

            G.stack = new int[100000];
            for (G.i = 0; G.i < 100000; G.i++)
                G.stack[G.i] = 0;

            G.i = 3; G.j = 11;
            int z = 0;
            int value = position(G.i, G.j);

            G.stack[z] = value;
            G.top = 0; 
            int length, bredth;


            while (G.stack[G.top] != G.i)
            {
                int current = G.stack[G.top];



                G.i = current / 10000;
                G.j = current % 10000;
                {
                    newnode(small, G.stack, G.i, G.j, G.z);
                    G.top++;
                }


                length = G.j * 20;
                bredth = G.i * 20;

                System.Threading.Thread.Sleep(100);
                pictureBox1.Location = new Point(length, bredth);
                if (current == 80001)
                {
                    MessageBox.Show("i have reached");
                    break;
                }

            }
            int i = 8, j = 1;
            for (int k = 0; k < 10000; k++)
            {
                if (small[i, j] == 0)
                {
                    break;
                }

                int val = small[i, j];
                i = val / 10000;
                j = val % 10000;
                int x = i * 20;
                int y = j * 20;

                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(y, x, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();




            }

            
           
        }

        public int position(int x, int y)
        {
            int result = 10000 * x + y;
            return result;
        }
       
        public void newnode( int[,] small,int[] stack,int i, int j,int z)
        {

            G.z = z;
          
            if (G.i != 0 && G.j != 0)
            {
                if (small[i, j - 1] == 0)
                {

                    int sign = 1;
                    int d = position(i, j - 1);
                    for (int h = 0; h < z; h++)
                    {
                        if (G.stack[h] == d)
                        {
                            sign = 0;
                        }


                    }
                    if (sign == 1)
                    {
                        stack[G.z++] = d;
                        small[i, j - 1] = position(i, j);
                    }
                    
                    



                }
                if (small[i, j + 1] == 0)
                {
                    int sign = 1;
                    int d = position(i, j + 1);
                    for (int h = 0; h < z; h++)
                    {
                        if (G.stack[h] == d)
                        {
                            sign = 0;
                        }


                    }
                    if (sign == 1)
                    {
                        stack[G.z++] = d;
                        small[i, j + 1] = position(i, j);
                    }
                }
                if (small[i - 1, j] == 0)
                {
                    int sign = 1;

                    int d = position(i - 1, j);
                    for (int h = 0; h < z; h++)
                    {
                        if (G.stack[h] == d)
                        {
                            sign = 0;
                        }


                    }
                    if (sign == 1)
                    {
                        stack[G.z++] = d;
                        small[i - 1, j] = position(i, j);
                    }



                }

                if (small[i + 1, j] == 0)
                {
                    int sign = 1;
                    int d = position(i + 1, j);
                    for (int h = 0; h < z; h++)
                    {

                        if (G.stack[h] == d)
                        {
                            sign = 0;
                        }


                    }
                    if (sign == 1)
                    {
                        stack[G.z++] = d;
                        small[i + 1, j] = position(i, j);
                    }

                }
            }
            


        }

        private void rectangleShape11_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        }

        private void rectangleShape19_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var small = new int[10, 22] { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1 }, { 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1 }, { 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 1, 0, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1 }, { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };

            G.stack = new int[100000];
            for (G.i = 0; G.i < 100000; G.i++)
                G.stack[G.i] = 0;
            

           


            G.i = 3; G.j = 11;
            int z = 0;
            int value = position(G.i, G.j);

            G.stack[z] = value;
            G.top = 0; 
            int length, bredth;
            


            while (G.stack[G.top] != G.i)
            {
                int current = G.stack[G.z];
                small[3, 11] = value;


                G.i = current / 10000;
                G.j = current % 10000;
                {
                    int flag =newnode1(small, G.stack, G.i, G.j, G.z);
                    G.z--;
                }


                length = G.j * 20;
                bredth = G.i * 20;

                System.Threading.Thread.Sleep(100);
                pictureBox1.Location = new Point(length, bredth);
                if (current == 80001)
                {
                    MessageBox.Show("i have reached");
                    break;
                }

            }
            int i = 8, j = 1;
            for (int k = 0; k < 10000; k++)
            {
                if (small[i, j] == 0)
                {
                    break;
                }

                int val = small[i, j];
                i = val / 10000;
                j = val % 10000;
                int x = i * 20;
                int y = j * 20;

                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(y, x, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();




            }
        }

        public int newnode1(int[,] small, int[] stack, int i, int j, int z)
        {

            G.z = z;
            int flag = 0;

            if (G.i != 0 && G.j != 0)
            {
                if (small[i, j - 1] == 0)
                {
                    

                    int sign = 1;
                    int d = position(i, j - 1);
                    for (int h = 0; h < z; h++)
                    {
                        if (G.stack[h] == d)
                        {
                            sign = 0;
                        }


                    }
                    if (sign == 1)
                    {
                        flag = 1;
                        stack[G.z++] = d;
                        small[i, j - 1] = position(i, j);
                    }





                }
                if (small[i, j + 1] == 0)
                {
                    int sign = 1;
                    int d = position(i, j + 1);
                    for (int h = 0; h < z; h++)
                    {
                        if (G.stack[h] == d)
                        {
                            sign = 0;
                        }


                    }
                    if (sign == 1)
                    {
                        flag = 1;
                        stack[G.z++] = d;

                        small[i, j + 1] = position(i, j);
                    }
                }
                if (small[i - 1, j] == 0)
                {
                    int sign = 1;

                    int d = position(i - 1, j);
                    for (int h = 0; h < z; h++)
                    {
                        if (G.stack[h] == d)
                        {
                            sign = 0;
                        }


                    }
                    if (sign == 1)
                    {
                        flag = 1;
                        stack[G.z++] = d;
                        small[i - 1, j] = position(i, j);
                    }



                }

                if (small[i + 1, j] == 0)
                {
                    int sign = 1;
                    int d = position(i + 1, j);
                    for (int h = 0; h < z; h++)
                    {
                        if (G.stack[h] == d)
                        {
                            sign = 0;
                        }


                    }
                    if (sign == 1)
                    {
                        flag = 1;
                        stack[G.z++] = d;
                        small[i + 1, j] = position(i, j);
                    }

                }
                
            }



            return flag;
        }

        private void rectangleShape12_Click(object sender, EventArgs e)
        {

        }
    }

}