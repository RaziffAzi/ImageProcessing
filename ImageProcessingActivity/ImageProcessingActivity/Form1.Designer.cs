namespace ImageProcessingActivity
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.cbImageProcess = new System.Windows.Forms.ComboBox();
            this.orig = new System.Windows.Forms.PictureBox();
            this.processed = new System.Windows.Forms.PictureBox();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.btnProcessImage = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.picture2 = new System.Windows.Forms.PictureBox();
            this.btnSubtract = new System.Windows.Forms.Button();
            this.btnSelectImage2 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnCaptureWebcam = new System.Windows.Forms.Button();
            this.webcamTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.orig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.processed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture2)).BeginInit();
            this.SuspendLayout();
            // 
            // cbImageProcess
            // 
            this.cbImageProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImageProcess.FormattingEnabled = true;
            this.cbImageProcess.Items.AddRange(new object[] {
            "Copy",
            "Greyscale",
            "Inversion",
            "Histogram",
            "Sepia"});
            this.cbImageProcess.Location = new System.Drawing.Point(21, 18);
            this.cbImageProcess.Name = "cbImageProcess";
            this.cbImageProcess.Size = new System.Drawing.Size(121, 28);
            this.cbImageProcess.TabIndex = 0;
            // 
            // orig
            // 
            this.orig.Location = new System.Drawing.Point(39, 72);
            this.orig.Name = "orig";
            this.orig.Size = new System.Drawing.Size(500, 500);
            this.orig.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.orig.TabIndex = 1;
            this.orig.TabStop = false;
            // 
            // processed
            // 
            this.processed.Location = new System.Drawing.Point(594, 72);
            this.processed.Name = "processed";
            this.processed.Size = new System.Drawing.Size(500, 500);
            this.processed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.processed.TabIndex = 2;
            this.processed.TabStop = false;
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.Location = new System.Drawing.Point(81, 585);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(194, 39);
            this.btnSelectImage.TabIndex = 3;
            this.btnSelectImage.Text = "Select Image";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnProcessImage
            // 
            this.btnProcessImage.Location = new System.Drawing.Point(644, 18);
            this.btnProcessImage.Name = "btnProcessImage";
            this.btnProcessImage.Size = new System.Drawing.Size(194, 39);
            this.btnProcessImage.TabIndex = 4;
            this.btnProcessImage.Text = "Process Image";
            this.btnProcessImage.UseVisualStyleBackColor = true;
            this.btnProcessImage.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // picture2
            // 
            this.picture2.Location = new System.Drawing.Point(1128, 72);
            this.picture2.Name = "picture2";
            this.picture2.Size = new System.Drawing.Size(500, 500);
            this.picture2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture2.TabIndex = 5;
            this.picture2.TabStop = false;
            // 
            // btnSubtract
            // 
            this.btnSubtract.Location = new System.Drawing.Point(864, 18);
            this.btnSubtract.Name = "btnSubtract";
            this.btnSubtract.Size = new System.Drawing.Size(194, 39);
            this.btnSubtract.TabIndex = 6;
            this.btnSubtract.Text = "Subtract";
            this.btnSubtract.UseVisualStyleBackColor = true;
            this.btnSubtract.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // btnSelectImage2
            // 
            this.btnSelectImage2.Location = new System.Drawing.Point(1311, 590);
            this.btnSelectImage2.Name = "btnSelectImage2";
            this.btnSelectImage2.Size = new System.Drawing.Size(194, 39);
            this.btnSelectImage2.TabIndex = 7;
            this.btnSelectImage2.Text = "Select Image";
            this.btnSelectImage2.UseVisualStyleBackColor = true;
            this.btnSelectImage2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(181, 13);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(208, 44);
            this.listBox1.TabIndex = 8;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // btnCaptureWebcam
            // 
            this.btnCaptureWebcam.Location = new System.Drawing.Point(299, 585);
            this.btnCaptureWebcam.Name = "btnCaptureWebcam";
            this.btnCaptureWebcam.Size = new System.Drawing.Size(194, 39);
            this.btnCaptureWebcam.TabIndex = 9;
            this.btnCaptureWebcam.Text = "Capture Webcam";
            this.btnCaptureWebcam.UseVisualStyleBackColor = true;
            this.btnCaptureWebcam.Click += new System.EventHandler(this.btnCaptureWebcam_Click);
            // 
            // webcamTimer
            // 
            this.webcamTimer.Interval = 33;
            this.webcamTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1651, 636);
            this.Controls.Add(this.btnCaptureWebcam);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnSelectImage2);
            this.Controls.Add(this.btnSubtract);
            this.Controls.Add(this.picture2);
            this.Controls.Add(this.btnProcessImage);
            this.Controls.Add(this.btnSelectImage);
            this.Controls.Add(this.processed);
            this.Controls.Add(this.orig);
            this.Controls.Add(this.cbImageProcess);
            this.Name = "Form1";
            this.Text = " ";
            ((System.ComponentModel.ISupportInitialize)(this.orig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.processed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbImageProcess;
        private System.Windows.Forms.PictureBox orig;
        private System.Windows.Forms.PictureBox processed;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.Button btnProcessImage;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox picture2;
        private System.Windows.Forms.Button btnSubtract;
        private System.Windows.Forms.Button btnSelectImage2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnCaptureWebcam;
        private System.Windows.Forms.Timer webcamTimer;
    }
}

