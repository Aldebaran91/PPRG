using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParallelMandelbrot
{
    public partial class Form_Mandelbrot : Form
    {

        ParallelMandelbrotGenerator mandel = new ParallelMandelbrotGenerator();

        public Form_Mandelbrot()
        {
            InitializeComponent();

            this.textBox_width.Text = "1024";
            this.textBox_height.Text = "1024";

            this.textBox_realMin.Text = "-2";
            this.textBox_imagMin.Text = "1";
            this.textBox_realMax.Text = "1";
            this.textBox_imagMax.Text = "3";

            this.textBox_Iterations.Text = "1000";
        }

        private void btn_CalcNewMandelbrot_Click(object sender, EventArgs e)
        {
            int width = 0, height = 0;
            width = int.Parse(textBox_width.Text);
            height = int.Parse(textBox_height.Text);

            double realMin, imagMin, realMax, imagMax;

            realMin = double.Parse(textBox_realMin.Text);
            imagMin = double.Parse(textBox_imagMin.Text);
            realMax = double.Parse(textBox_realMax.Text);
            imagMax = double.Parse(textBox_imagMax.Text);

            int iterations = int.Parse(textBox_Iterations.Text);

            TimeSpan calcTime = new TimeSpan();
            Image result = mandel.Mandelbrot(cb_parallel.Checked, width, height, realMin, imagMin, realMax, imagMax, iterations, ref calcTime);

            label_calcTime.Text = String.Format("CalcTime: {0} ms", calcTime.TotalMilliseconds);
            this.pictureBox_Result.Image = result;
            this.pictureBox_Result.SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}
