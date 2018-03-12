namespace ParallelMandelbrot
{
    partial class Form_Mandelbrot
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox_Result = new System.Windows.Forms.PictureBox();
            this.label_calcTime = new System.Windows.Forms.Label();
            this.btn_CalcNewMandelbrot = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_realMin = new System.Windows.Forms.TextBox();
            this.textBox_imagMin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_realMax = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_imagMax = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_Iterations = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_height = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_width = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_parallel = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Result)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_Result
            // 
            this.pictureBox_Result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_Result.Location = new System.Drawing.Point(200, 12);
            this.pictureBox_Result.Name = "pictureBox_Result";
            this.pictureBox_Result.Size = new System.Drawing.Size(724, 415);
            this.pictureBox_Result.TabIndex = 0;
            this.pictureBox_Result.TabStop = false;
            // 
            // label_calcTime
            // 
            this.label_calcTime.AutoSize = true;
            this.label_calcTime.Location = new System.Drawing.Point(13, 13);
            this.label_calcTime.Name = "label_calcTime";
            this.label_calcTime.Size = new System.Drawing.Size(57, 13);
            this.label_calcTime.TabIndex = 1;
            this.label_calcTime.Text = "CalcTime: ";
            // 
            // btn_CalcNewMandelbrot
            // 
            this.btn_CalcNewMandelbrot.Location = new System.Drawing.Point(12, 42);
            this.btn_CalcNewMandelbrot.Name = "btn_CalcNewMandelbrot";
            this.btn_CalcNewMandelbrot.Size = new System.Drawing.Size(182, 42);
            this.btn_CalcNewMandelbrot.TabIndex = 2;
            this.btn_CalcNewMandelbrot.Text = "Calc";
            this.btn_CalcNewMandelbrot.UseVisualStyleBackColor = true;
            this.btn_CalcNewMandelbrot.Click += new System.EventHandler(this.btn_CalcNewMandelbrot_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Real_Min";
            // 
            // textBox_realMin
            // 
            this.textBox_realMin.Location = new System.Drawing.Point(12, 183);
            this.textBox_realMin.Name = "textBox_realMin";
            this.textBox_realMin.Size = new System.Drawing.Size(87, 20);
            this.textBox_realMin.TabIndex = 4;
            // 
            // textBox_imagMin
            // 
            this.textBox_imagMin.Location = new System.Drawing.Point(107, 183);
            this.textBox_imagMin.Name = "textBox_imagMin";
            this.textBox_imagMin.Size = new System.Drawing.Size(87, 20);
            this.textBox_imagMin.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(107, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Imag_Min";
            // 
            // textBox_realMax
            // 
            this.textBox_realMax.Location = new System.Drawing.Point(12, 233);
            this.textBox_realMax.Name = "textBox_realMax";
            this.textBox_realMax.Size = new System.Drawing.Size(87, 20);
            this.textBox_realMax.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 217);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Real_Max";
            // 
            // textBox_imagMax
            // 
            this.textBox_imagMax.Location = new System.Drawing.Point(107, 233);
            this.textBox_imagMax.Name = "textBox_imagMax";
            this.textBox_imagMax.Size = new System.Drawing.Size(87, 20);
            this.textBox_imagMax.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(104, 217);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Imag_Max";
            // 
            // textBox_Iterations
            // 
            this.textBox_Iterations.Location = new System.Drawing.Point(12, 286);
            this.textBox_Iterations.Name = "textBox_Iterations";
            this.textBox_Iterations.Size = new System.Drawing.Size(182, 20);
            this.textBox_Iterations.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 270);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Iterations";
            // 
            // textBox_height
            // 
            this.textBox_height.Location = new System.Drawing.Point(107, 134);
            this.textBox_height.Name = "textBox_height";
            this.textBox_height.Size = new System.Drawing.Size(87, 20);
            this.textBox_height.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(107, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Height";
            // 
            // textBox_width
            // 
            this.textBox_width.Location = new System.Drawing.Point(12, 134);
            this.textBox_width.Name = "textBox_width";
            this.textBox_width.Size = new System.Drawing.Size(87, 20);
            this.textBox_width.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 117);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Width";
            // 
            // cb_parallel
            // 
            this.cb_parallel.AutoSize = true;
            this.cb_parallel.Location = new System.Drawing.Point(12, 91);
            this.cb_parallel.Name = "cb_parallel";
            this.cb_parallel.Size = new System.Drawing.Size(81, 17);
            this.cb_parallel.TabIndex = 17;
            this.cb_parallel.Text = "Use parallel";
            this.cb_parallel.UseVisualStyleBackColor = true;
            // 
            // Form_Mandelbrot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 439);
            this.Controls.Add(this.cb_parallel);
            this.Controls.Add(this.textBox_height);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox_width);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox_Iterations);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_imagMax);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_realMax);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_imagMin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_realMin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_CalcNewMandelbrot);
            this.Controls.Add(this.label_calcTime);
            this.Controls.Add(this.pictureBox_Result);
            this.Name = "Form_Mandelbrot";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Result)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Result;
        private System.Windows.Forms.Label label_calcTime;
        private System.Windows.Forms.Button btn_CalcNewMandelbrot;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_realMin;
        private System.Windows.Forms.TextBox textBox_imagMin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_realMax;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_imagMax;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_Iterations;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_height;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_width;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cb_parallel;
    }
}

