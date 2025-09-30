using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using WebCamLib;

namespace ImageProcessingActivity
{
    public partial class Form1 : Form
    {
        Bitmap imageA, imageB;
        Device[] devices;   
        Device activeDevice;
        private bool isCapturing = false;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
            webcamTimer.Interval = 100; 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            devices = DeviceManager.GetAllDevices();
            foreach (Device d in devices)
            {
                listBox1.Items.Add(d.Name);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                activeDevice = devices[listBox1.SelectedIndex];
            }
        }

        private void btnCaptureWebcam_Click(object sender, EventArgs e)
        {
            if (!isCapturing)
            {
                StartWebcamCapture();
            }
            else
            {
                StopWebcamCapture();
            }
        }

        private void StartWebcamCapture()
        {
            try
            {
                if (activeDevice != null)
                    activeDevice.Stop();

                activeDevice.ShowWindow(orig);

                isCapturing = true;
                btnCaptureWebcam.Text = "Stop Webcam";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting webcam: " + ex.Message);
            }
        }

        private void StopWebcamCapture()
        {
            webcamTimer.Stop();
            if (activeDevice != null)
                activeDevice.Stop();
            isCapturing = false;
            btnCaptureWebcam.Text = "Capture Webcam";
            orig.Image = null;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopWebcamCapture();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (activeDevice == null || !isCapturing)
                return;

            try
            {
                CaptureFrame();
            }
            catch (Exception ex)
            {
            }
        }

        private void CaptureFrame()
        {
            IDataObject data;
            Image bmap;

            // Use the exact pattern you specified
            Device d = DeviceManager.GetDevice(listBox1.SelectedIndex);
            d.Sendmessage();
            data = Clipboard.GetDataObject();

            if (data != null && data.GetDataPresent("System.Drawing.Bitmap", true))
            {
                bmap = (Image)(data.GetData("System.Drawing.Bitmap", true));

                if (bmap != null)
                {
                    // Dispose of previous image to prevent memory leaks
                    if (orig.Image != null)
                    {
                        orig.Image.Dispose();
                        orig.Image = null;
                    }

                    // Create new bitmap using your specified pattern
                    Bitmap b = new Bitmap(bmap);
                    imageA = b;
                    orig.Image = imageA;

                    // Clean up
                    bmap.Dispose();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StopWebcamCapture();
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

                case "Smooth":
                    Bitmap bp = (Bitmap)orig.Image.Clone();
                    Smooth(bp);
                    processed.Image = bp;
                    break;
                case "Gaussian Blur":
                    Bitmap bp2 = (Bitmap)orig.Image.Clone();
                    GaussianBlur(bp2);
                    processed.Image = bp2;
                    break;
                case "Sharpen":
                    Bitmap bp3 = (Bitmap)orig.Image.Clone();
                    Sharpen(bp3);
                    processed.Image = bp3;
                    break;
                case "Mean Removal":
                    Bitmap bp4 = (Bitmap)orig.Image.Clone();
                    MeanRemoval(bp4);
                    processed.Image = bp4;
                    break;
                case "Emboss Laplascian":
                    Bitmap bp5 = (Bitmap)orig.Image.Clone();
                    EmbossLaplascian(bp5);
                    processed.Image = bp5;
                    break;
                case "Emboss (Horizontal/Vertical)":
                    Bitmap bp6 = (Bitmap)orig.Image.Clone();
                    HorizontalOrVertical(bp6);
                    processed.Image = bp6;
                    break;
                case "Emboss (All Directions)":
                    Bitmap bp7 = (Bitmap)orig.Image.Clone();
                    AllDirections(bp7);
                    processed.Image = bp7;
                    break;
                case "Emboss (Horizontal)":
                    Bitmap bp8 = (Bitmap)orig.Image.Clone();
                    Horizontal(bp8);
                    processed.Image = bp8;
                    break;
                case "Emboss (Vertical)":
                    Bitmap bp9 = (Bitmap)orig.Image.Clone();
                    Vertical(bp9);
                    processed.Image = bp9;
                    break;
                case "Emboss (Lossy)":
                    Bitmap bp10 = (Bitmap)orig.Image.Clone();
                    EmbossLossy(bp10);
                    processed.Image = bp10;
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

        private void cbImageProcess_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            //subtract
            StopWebcamCapture();
            processed.Image = null;
            imageA = (Bitmap)orig.Image;
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

        public static bool Conv3x3(Bitmap b, ConvMatrix m)
        {
            if (0 == m.Factor)
                return false; Bitmap

            bSrc = (Bitmap)b.Clone();
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height),
                                ImageLockMode.ReadWrite,
                                PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height),
                               ImageLockMode.ReadWrite,
                               PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            int stride2 = stride * 2;

            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;
                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width - 2;
                int nHeight = b.Height - 2;
                int nPixel;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nPixel = ((((pSrc[2] * m.TopLeft) +
                            (pSrc[5] * m.TopMid) +
                            (pSrc[8] * m.TopRight) +
                            (pSrc[2 + stride] * m.MidLeft) +
                            (pSrc[5 + stride] * m.Pixel) +
                            (pSrc[8 + stride] * m.MidRight) +
                            (pSrc[2 + stride2] * m.BottomLeft) +
                            (pSrc[5 + stride2] * m.BottomMid) +
                            (pSrc[8 + stride2] * m.BottomRight))
                            / m.Factor) + m.Offset);
                        
                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;
                        p[5 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[1] * m.TopLeft) +
                            (pSrc[4] * m.TopMid) +
                            (pSrc[7] * m.TopRight) +
                            (pSrc[1 + stride] * m.MidLeft) +
                            (pSrc[4 + stride] * m.Pixel) +
                            (pSrc[7 + stride] * m.MidRight) +
                            (pSrc[1 + stride2] * m.BottomLeft) +
                            (pSrc[4 + stride2] * m.BottomMid) +
                            (pSrc[7 + stride2] * m.BottomRight))
                            / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;
                        p[4 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[0] * m.TopLeft) +
                                       (pSrc[3] * m.TopMid) +
                                       (pSrc[6] * m.TopRight) +
                                       (pSrc[0 + stride] * m.MidLeft) +
                                       (pSrc[3 + stride] * m.Pixel) +
                                       (pSrc[6 + stride] * m.MidRight) +
                                       (pSrc[0 + stride2] * m.BottomLeft) +
                                       (pSrc[3 + stride2] * m.BottomMid) +
                                       (pSrc[6 + stride2] * m.BottomRight))
                            / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;
                        p[3 + stride] = (byte)nPixel;

                        p += 3;
                        pSrc += 3;
                    }
                    p += nOffset;
                    pSrc += nOffset;
                }
            }
            b.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);

            return true;
        }
        public static bool Smooth(Bitmap b, int nWeight = 1)
        {
            ConvMatrix m = new ConvMatrix();
            m.SetAll(1);
            m.Pixel = nWeight;
            m.Factor = nWeight + 8;
            return Conv3x3(b, m);
        }

        public static bool GaussianBlur(Bitmap b)
        {
            ConvMatrix m = new ConvMatrix();
            m.TopLeft = m.TopRight = m.BottomLeft = m.BottomRight = 1;
            m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = 2;
            m.Pixel = 4;
            m.Factor = 16;
            return Conv3x3(b, m);
        }

        public static bool Sharpen(Bitmap b)
        {
            ConvMatrix m = new ConvMatrix();
            m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = -2;
            m.TopLeft = m.TopRight = m.BottomLeft = m.BottomRight = 0;
            m.Pixel = 11;
            m.Factor = 3;
            return Conv3x3(b, m);
        }

        public static bool MeanRemoval(Bitmap b)
        {
            ConvMatrix m = new ConvMatrix();
            m.SetAll(-1);
            m.Pixel = 9;
            m.Factor = 1;
            return Conv3x3(b, m);
        }

        public static bool EmbossLaplascian(Bitmap b)
        {
            ConvMatrix m = new ConvMatrix();
            m.TopLeft = -1; m.TopRight = -1;
            m.MidLeft = 0; m.Pixel = 4; m.MidRight = 0;
            m.BottomLeft = -1; m.BottomRight = -1;
            m.TopMid = 0; m.BottomMid = 0;
            m.Factor = 1;
            m.Offset = 127;
            return Conv3x3(b, m);
        }

        public static bool HorizontalOrVertical(Bitmap b)
        {
            ConvMatrix m = new ConvMatrix();
            m.TopLeft = m.TopRight = m.BottomLeft = m.BottomRight = 0;
            m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = -1;
            m.Pixel = 4;
            m.Factor = 1;
            m.Offset = 127;
            return Conv3x3(b, m);
        }

        public static bool AllDirections(Bitmap b)
        {
            ConvMatrix m = new ConvMatrix();
            m.SetAll(-1);
            m.Pixel = 8;
            m.Factor = 1;
            m.Offset = 127;
            return Conv3x3(b, m);
        }

        public static bool Horizontal(Bitmap b)
        {
            ConvMatrix m = new ConvMatrix();
            m.SetAll(0);
            m.MidRight = m.MidLeft = -1;
            m.Pixel = 2;
            m.Factor = 1;
            m.Offset = 127;
            return Conv3x3(b, m);
        }

        public static bool Vertical(Bitmap b)
        {
            ConvMatrix m = new ConvMatrix();
            m.SetAll(0);
            m.TopMid = -1; m.BottomMid = 1;
            m.Factor = 1;
            m.Offset = 127;
            return Conv3x3(b, m);
        }

        public static bool EmbossLossy(Bitmap b)
        {
            ConvMatrix m = new ConvMatrix();
            m.Pixel = 4;
            m.TopRight = 1;
            m.TopLeft = 1;
            m.BottomMid = 1;
            m.TopMid = m.MidLeft = m.MidRight = m.BottomLeft = m.BottomRight = -2;
            m.Factor = 1;
            m.Offset = 127;
            return Conv3x3(b, m);
        }
    }
}
