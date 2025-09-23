using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace ImageProcessingActivity
{
    public partial class Form1 : Form
    {
        Bitmap imageA, imageB;


        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCaptureWebcam_Click(object sender, EventArgs e)
        {
            //IDataObject data;
            //Image bmap;
            //Device d = DeviceManager.GetDevice(listBox1.SelectedIndex);
            //d.Sendmessage();
            //data = Clipboard.GetDataObject();
            //bmap = (Image)(data.GetData("System.Drawing.Bitmap", true));
            //Bitmap b = new Bitmap(bmap);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog1.Title = "Select an image";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    imageA = new Bitmap(openFileDialog1.FileName);
                    orig.Image = imageA; // show it in PictureBox (optional)
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid image file: " + ex.Message);
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (orig.Image == null)
            {
                MessageBox.Show("Please select an image first.");
                return;
            }
            if(cbImageProcess.SelectedItem == null)
            {
                MessageBox.Show("Please select an image processing method.");
                return;
            }
            switch (cbImageProcess.SelectedItem.ToString())
            {

                case "Copy":
                    processed.Image = (Image)orig.Image.Clone();
                    break;

                case "Greyscale":
                    Greyscale();
                    break;

                case "Inversion":
                    Inversion();
                    break;

                case "Histogram":
                    Histogram();
                    break;

                case "Sepia":
                    Sepia();
                    break;
            }
        }
        
        private void Greyscale()
        {
            Bitmap bp = new Bitmap(orig.Image.Width, orig.Image.Height);
            for (int i = 0; i < orig.Image.Width; i++)
            {
                for (int j = 0; j < orig.Image.Height; j++)
                {
                    Color c = ((Bitmap)orig.Image).GetPixel(i, j);
                    int gray = ((c.R + c.G + c.B) / 3);
                    bp.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            processed.Image = bp;
        }

        private void Inversion()
        {
            Bitmap bp = new Bitmap(orig.Image.Width, orig.Image.Height);
            for (int i = 0; i < orig.Image.Width; i++)
            {
                for (int j = 0; j < orig.Image.Height; j++)
                {
                    Color c = ((Bitmap)orig.Image).GetPixel(i, j);
                    bp.SetPixel(i, j, Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
                }
            }
            processed.Image = bp;
        }

        private void Sepia()
        {
            Bitmap bp = new Bitmap(orig.Image.Width, orig.Image.Height);
            for (int i = 0; i < orig.Image.Width; i++)
                    {
                        for (int j = 0; j < orig.Image.Height; j++)
                        {
                            Color c = ((Bitmap)orig.Image).GetPixel(i, j);
                            int tr = (int)(0.393 * c.R + 0.769 * c.G + 0.189 * c.B);
                            int tg = (int)(0.349 * c.R + 0.686 * c.G + 0.168 * c.B);
                            int tb = (int)(0.272 * c.R + 0.534 * c.G + 0.131 * c.B);

                            if(tr > 255)
                            {
                                tr = 255;
                            }
                            if(tg > 255)
                            {
                                tg = 255;
                            }
                            if (tb > 255)
                            {
                                tb = 255;
                            }
                            bp.SetPixel(i,j, Color.FromArgb(tr, tg, tb));
                        }
                    }
                    processed.Image = bp;
            processed.Image = bp;
        }

        public void Histogram()
        {
            int[] arr = new int[256];
            Bitmap bp = new Bitmap(orig.Image.Width, orig.Image.Height);
            for (int i = 0; i < orig.Image.Width; i++)
            {
                for (int j = 0; j < orig.Image.Height; j++)
                {
                    Color c = ((Bitmap)orig.Image).GetPixel(i, j);
                    int gray = ((c.R + c.G + c.B) / 3);
                    arr[gray]++;
                    //bp.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            int max = arr.Max();
            Graphics g = Graphics.FromImage(bp);
            for (int i = 0; i < 256; i++)
            {
                int scaledHeight = (arr[i] * bp.Height) / max;
                int x = (i * bp.Width) / 256;
                g.DrawLine(Pens.Black, x, bp.Height, x, bp.Height - scaledHeight);
            }
            processed.Image = bp;
        }

        private void processed_Click(object sender, EventArgs e)
        {

        }

        private void picture2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //load image2
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog1.Title = "Select an image";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    imageB = new Bitmap(openFileDialog1.FileName);
                    picture2.Image = imageB; // show it in PictureBox (optional)
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid image file: " + ex.Message);
                }
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            //subtract
            processed.Image = null;
            if (imageA.Width != imageB.Width || imageA.Height != imageB.Height)
            {
                imageB = new Bitmap(imageB, new Size(imageA.Width, imageA.Height));
            }
            Color mygreen = Color.FromArgb(0, 255, 0); 
            int threshold = 5; 
            int greygreen = (mygreen.R + mygreen.G + mygreen.B) / 3;
            Bitmap resultImage = new Bitmap(imageA.Width, imageA.Height);

            for (int x = 0; x < imageA.Width; x++)
            {
                for (int y = 0; y < imageA.Height; y++)
                {
                    Color pixel = imageA.GetPixel(x, y);
                    Color backpixel = imageB.GetPixel(x, y);

                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    
                    int subtractvalue = Math.Abs(grey-greygreen);

                    if (subtractvalue < threshold)
                        resultImage.SetPixel(x, y, backpixel);
                    else
                        resultImage.SetPixel(x, y, pixel);
                }
            }

            processed.Image = resultImage;
        }
    }
}
