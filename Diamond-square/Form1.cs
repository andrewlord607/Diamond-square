using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diamond_square
{
    public partial class Form1 : Form
    {
        Random rnd;

        Bitmap DrawArea;

        public Form1()
        {
            rnd = new Random();
            InitializeComponent();
            DrawArea = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            pictureBox1.Image = DrawArea;

        }



        private double rand(double min, double max)
        {
            return min + Math.Floor(rnd.NextDouble() * (max - min));
        }

        private void midpoint(double[] vector, int left, int right, int len, double r)
        {
            if (right - left < 2)
                return;
            var hl = vector[left];
            var hr = vector[right];
            var h = (hl + hr) / 2 + rand(-r * len, +r * len);
            var index = (int)Math.Floor(left + (right - left) / 2.0);
            vector[index] = h;

            midpoint(vector, left, index, len / 2, r);
            midpoint(vector, index, right, len / 2, r);
        }

        private void draw(double[] vector, int len, int size, int left)
        {
            Graphics g = Graphics.FromImage(DrawArea);
            g.FillRectangle(new SolidBrush(Color.LightGreen), new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height / 2));
            g.FillRectangle(new SolidBrush(Color.LightPink), new Rectangle(0, pictureBox1.Height / 2, pictureBox1.Width, pictureBox1.Height / 2));

            var step = len / (size - 1);
            Point prev = new Point(0, pictureBox1.Height / 2 - left);
            for (int i = 0, l = vector.Length; i < l; i++)
            {
                Point cur = new Point((step * i), -(int)vector[i] + pictureBox1.Height/2);
                g.DrawLine(new Pen(Color.Black, 3), prev, cur);
                prev = cur;
            }
            pictureBox1.Image = DrawArea;
        }
        
        private void generate()
        {
            try
            {
                var left = int.Parse(textBox1.Text);
                var right = int.Parse(textBox2.Text);
                var len = int.Parse(textBox3.Text);
                var size = int.Parse(textBox4.Text);
                var roughness = double.Parse(textBox5.Text);



                double[] vector = new double[size];
                for (var i = 0; i < size; i++)
                {
                    vector[i] = 0;
                }

                vector[0] = left;
                vector[vector.Length - 1] = right;
                //console.log(vector.length);
                midpoint(vector, 0, vector.Length - 1, len, roughness);

                draw(vector, len, size, left);
            }
            catch
            {
                MessageBox.Show("Некоретные данные");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            generate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
