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
    public partial class Pac : Form
    {
        public Pac()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            small_maze f = new small_maze();
            f.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            eat_all r = new eat_all();
            r.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Chess ef = new Chess();
            ef.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            medium q = new medium();
            q.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Large f = new Large();
            f.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            open o = new open();
            o.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            cost1 p = new cost1();
            p.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Multismall d = new Multismall();
            d.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Multitricky n = new Multitricky();
            n.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            bug b = new bug();
            b.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            submedium o = new submedium();
            o.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            sublarge m = new sublarge();
            m.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            bonous kj = new bonous();
            kj.ShowDialog();
        }

    
    }
}
