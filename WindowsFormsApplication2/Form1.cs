using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        Bitmap c;
        //grayscaling
        private void button1_Click(object sender, EventArgs e)
        {
            c = new Bitmap(pictureBox1.Image);
            Bitmap d = new Bitmap(c.Width, c.Height);

            for (int i = 0; i < c.Width; i++)
            {
                for (int x = 0; x < c.Height; x++)
                {
                    Color oc = c.GetPixel(i, x);
                    int grayScale = (int)((oc.R * 0.3) + (oc.G * 0.59) + (oc.B * 0.11));

                    Color nc = Color.FromArgb(oc.A, grayScale, grayScale, grayScale);
                    d.SetPixel(i, x, nc);
                }
            }

            pictureBox1.Image = d;

        }

        //inverting
        private void button2_Click(object sender, EventArgs e)
        {
            c = new Bitmap(pictureBox1.Image);
            Bitmap d = new Bitmap(c.Width, c.Height);

            for (int i = 0; i < c.Width; i++)
            {
                for (int x = 0; x < c.Height; x++)
                {
                    Color oc = c.GetPixel(i, x);
                    int invR = (int)(255 - oc.R);
                    int invG = (int)(255 - oc.G);
                    int invB = (int)(255 - oc.B);

                    Color nc = Color.FromArgb(oc.A, invR, invG, invB);
                    d.SetPixel(i, x, nc);
                }
            }

            pictureBox1.Image = d;
        }
        
        //brightness
        private void button3_Click(object sender, EventArgs e)
        {
            c = new Bitmap(pictureBox1.Image);

            int k = 55;//brightness coefficient

            Bitmap d = new Bitmap(c.Width, c.Height);
   

            for (int i = 0; i < c.Width; i++)
            {
                for (int x = 0; x < c.Height; x++)
                {
                    Color oc = c.GetPixel(i, x);

                    int brR = (int)(oc.R + k);
                    int brG = (int)(oc.G + k);
                    int brB = (int)(oc.B + k);

                    int bR = (int)brR;
                    bR = bR > 255 ? 255 : bR;
                    bR = bR < 0 ? 0 : bR;
                    int bG = (int)brG;
                    bG = bG > 255 ? 255 : bG;
                    bG = bG < 0 ? 0 : bG;
                    int bB = (int)brB;
                    bB = bB > 255 ? 255 : bB;
                    bB = bB < 0 ? 0 : bB;

                    Color nc;
                    nc = Color.FromArgb(oc.A, bR, bG, bB);


                    d.SetPixel(i, x, nc);
                }
            }

            pictureBox1.Image = d;
        }

        //exit button
        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //reseting picturebox image
        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("cat.jpg");
        }

        //binarization 1 - global thresholding
        private void button6_Click(object sender, EventArgs e)
        {
            c = new Bitmap(pictureBox1.Image);
            Bitmap d = new Bitmap(c.Width, c.Height);

            for (int i = 0; i < c.Width; i++)
            {
                for (int x = 0; x < c.Height; x++)
                {
                    Color oc = c.GetPixel(i, x);

                    //grayscale
                    int ret = (int)(oc.R * 0.299 + oc.G * 0.578 + oc.B*0.114);
                    if (ret > 120)
                    {
                        ret = 255;
                    }
                    else
                    {
                        ret = 0;
                    }

                    Color nc = Color.FromArgb(oc.A, ret, ret, ret);
                    d.SetPixel(i, x, nc);
                }
            }

            pictureBox1.Image = d;     }

        //binarization 2 - half thresholding
        private void button7_Click(object sender, EventArgs e)
        {
            c = new Bitmap(pictureBox1.Image);
            Bitmap d = new Bitmap(c.Width, c.Height);

            for (int i = 0; i < c.Width; i++)
            {
                for (int x = 0; x < c.Height; x++)
                {
                    Color oc = c.GetPixel(i, x);

                    //grayscale
                    int ret = (int)(oc.R * 0.299 + oc.G * 0.578 + oc.B * 0.114);
                    if (ret > 120)
                    {
                        ret = ret;
                    }
                    else
                    {
                        ret = 0;
                    }

                    Color nc = Color.FromArgb(oc.A, ret, ret, ret);
                    d.SetPixel(i, x, nc);
                }
            }

            pictureBox1.Image = d;
        }

        //binarization 3 - bernsen
        private void button8_Click(object sender, EventArgs e)
        {
            c = new Bitmap(pictureBox1.Image);
            Bitmap d = new Bitmap(c.Width, c.Height);
            int thr = 120; //threshold value
            int top=0,bottom=0,right=0,left=0;

            for (int x = 1; x < c.Width-1; x++)
            {
                for (int y = 1; y < c.Height-1; y++)
                {
                    Color oc = c.GetPixel(x, y);
                    int ocR = (int)(oc.R);
                    int ocG = (int)(oc.G);
                    int ocB = (int)(oc.B);
                    int ret = (int)(oc.R * 0.299 + oc.G * 0.578 + oc.B * 0.114);

                    Color prevX = c.GetPixel(x - 1, y);
                    int prevXR = (int)(prevX.R);
                    int prevXG = (int)(prevX.G);
                    int prevXB = (int)(prevX.B);
                    int ret1 = (int)(prevX.R * 0.299 + prevX.G * 0.578 + prevX.B * 0.114);

                    Color nextX = c.GetPixel(x + 1, y);
                    int bextXR = (int)(nextX.R);
                    int nextXG = (int)(nextX.G);
                    int nextXB = (int)(nextX.B);
                    int ret2 = (int)(nextX.R * 0.299 + nextX.G * 0.578 + nextX.B * 0.114);

                    Color prevY = c.GetPixel(x, y - 1);
                    int prevYR = (int)(prevY.R);
                    int prevYG = (int)(prevY.G);
                    int prevYB = (int)(prevY.B);
                    int ret3 = (int)(prevY.R * 0.299 + prevY.G * 0.578 + prevY.B * 0.114);

                    Color nextY = c.GetPixel(x, y + 1);
                    int bextYR = (int)(nextY.R);
                    int nextYG = (int)(nextY.G);
                    int nextYB = (int)(nextY.B);
                    int ret4 = (int)(nextY.R * 0.299 + nextY.G * 0.578 + nextY.B * 0.114);

                    int min1 = Math.Min(ret, ret1);
                    int min2 = Math.Min(ret2, ret3);
                    int min3 = Math.Min(min1, min2);
                    int min4 = Math.Min(min3, ret4);

                    int max1 = Math.Max(ret, ret1);
                    int max2 = Math.Max(ret2, ret3);
                    int max3 = Math.Max(max1, max2);
                    int max4 = Math.Max(max3, ret4);

                    int new_thr = ((max4 + min4) / 2);
                    
                    if (ret > new_thr)
                    {
                        ret = ret;
                    }
                    else
                    {
                        ret = 0;
                    }

                    Color nc = Color.FromArgb(oc.A, ret, ret, ret);
                    d.SetPixel(x, y, nc);
                }
            }

            pictureBox1.Image = d;
        }

        //contract adjustment
        private void button12_Click(object sender, EventArgs e)
        {
            c = new Bitmap(pictureBox1.Image);
            Bitmap d = new Bitmap(c.Width, c.Height);

            int k = 100;//coefficient
            int f = (int)((259 * (k + 255)) / (255 * (259 - k)));
            for (int i = 0; i < c.Width; i++)
            {
                for (int x = 0; x < c.Height; x++)
                {
                    Color oc = c.GetPixel(i, x);

                    int cR = (int)(f * (oc.R - 128) + 128);
                    int cG = (int)(f * (oc.G - 128) + 128);
                    int cB = (int)(f * (oc.B - 128) + 128);

                    int ccR = (int)cR;
                    ccR = ccR > 255 ? 255 : ccR;
                    ccR = ccR < 0 ? 0 : ccR;
                    int ccG = (int)cG;
                    ccG = ccG > 255 ? 255 : ccG;
                    ccG = ccG < 0 ? 0 : ccG;
                    int ccB = (int)cB;
                    ccB = ccB > 255 ? 255 : ccB;
                    ccB = ccB < 0 ? 0 : ccB;

                    Color nc = Color.FromArgb(oc.A, ccR, ccG, ccB);
                    d.SetPixel(i, x, nc);
                }
            }

            pictureBox1.Image = d;
        }

        //NOT gaussian blur, but another approach to bluring picture
        private void button9_Click(object sender, EventArgs e)
        {
            c = new Bitmap(pictureBox1.Image);
            Bitmap d = new Bitmap(c.Width, c.Height);
            //here the blurring coefficient is 5, 
            //so that the effect is more noticable
            for (int x=5;x< d.Width-5;x++)
            {
                for (int y = 5; y < d.Height-5; y++)
                {
                    
                    try
                    { 
                        Color prevX = c.GetPixel(x -5, y);
                        Color nextX = c.GetPixel(x + 5, y);
                        Color prevY = c.GetPixel(x, y - 5);
                        Color nextY = c.GetPixel(x, y + 5);
                        Color curPix = c.GetPixel(x, y);

                        int avgR = (int)((prevX.R + nextX.R + prevY.R + nextY.R + curPix.R) / 5);
                        int avgG = (int)((prevX.G + nextX.G + prevY.G + nextY.G + curPix.G) / 5);
                        int avgB = (int)((prevX.B + nextX.B + prevY.B + nextY.B + curPix.B) / 5);

                        d.SetPixel(x, y, Color.FromArgb(avgR,avgG,avgB));
                    }
                    catch (Exception) { }
                }
            }
            pictureBox1.Image = d;
        }

        //sobel filter
        private void button11_Click(object sender, EventArgs e)
        {
            c = new Bitmap(pictureBox1.Image);
            Bitmap d = new Bitmap(c.Width, c.Height);
            int width = d.Width, height = d.Height, limit = 100 * 100;
            int[,] gx = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] gy = new int[,] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
            int[,] allPixR = new int[width, height];
            int[,] allPixG = new int[width, height];
            int[,] allPixB = new int[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    allPixR[i, j] = c.GetPixel(i, j).R;
                    allPixG[i, j] = c.GetPixel(i, j).G;
                    allPixB[i, j] = c.GetPixel(i, j).B;
                }
            }
            int new_rx = 0, new_ry = 0;
            int new_gx = 0, new_gy = 0;
            int new_bx = 0, new_by = 0;
            int rc, gc, bc;
            for (int i = 1; i < d.Width - 1; i++)
            {
                for (int j = 1; j < d.Height - 1; j++)
                {

                    new_rx = 0; new_ry = 0;
                    new_gx = 0; new_gy = 0;
                    new_bx = 0; new_by = 0;
                    rc = 0; gc = 0; bc = 0;
                    for (int wi = -1; wi < 2; wi++)
                    {
                        for (int hw = -1; hw < 2; hw++)
                        {
                            rc = allPixR[i + hw, j + wi];
                            new_rx += gx[wi + 1, hw + 1] * rc;
                            new_ry += gy[wi + 1, hw + 1] * rc;

                            gc = allPixG[i + hw, j + wi];
                            new_gx += gx[wi + 1, hw + 1] * gc;
                            new_gy += gy[wi + 1, hw + 1] * gc;

                            bc = allPixB[i + hw, j + wi];
                            new_bx += gx[wi + 1, hw + 1] * bc;
                            new_by += gy[wi + 1, hw + 1] * bc;
                        }
                    }
                    if (new_rx * new_rx + new_ry * new_ry > limit || new_gx * new_gx + new_gy * new_gy > limit || new_bx * new_bx + new_by * new_by > limit)
                      d.SetPixel(i, j, Color.Black);
                    else
                      d.SetPixel(i, j, Color.Transparent);
                }
                pictureBox1.Image = d;
            }
        }
        //sharpening filter
        private void button10_Click(object sender, EventArgs e)
        {
            c = new Bitmap(pictureBox1.Image);
            Bitmap d = new Bitmap(c.Width, c.Height);
            int filterWidth = 3, filterHeight = 3, width = c.Width, height = c.Height;
            double[,] filter = new double[filterWidth, filterHeight];
            filter[0, 0] = filter[0, 1] = filter[0, 2] = filter[1, 0] = filter[1, 2] = filter[2, 0] = filter[2, 1] = filter[2, 2] = -1;
            filter[1, 1] = 9;
            double factor = 1.0, bias = 0.0;
            Color[,] result = new Color[c.Width, c.Height];
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    double red = 0.0, green = 0.0, blue = 0.0;

                    for (int filterX = 0; filterX < filterWidth; filterX++)
                    {
                        for (int filterY = 0; filterY < filterHeight; filterY++)
                        {
                            int imageX = (x - filterWidth / 2 + filterX + width) % width;
                            int imageY = (y - filterHeight / 2 + filterY + height) % height;

                            Color imageColor = c.GetPixel(imageX,imageY);

                            red += imageColor.R * filter[filterX, filterY];
                            green += imageColor.G * filter[filterX, filterY];
                            blue += imageColor.B * filter[filterX, filterY];
                        }
                        int r = Math.Min(Math.Max((int)(factor * red + bias), 0), 255);
                        int g = Math.Min(Math.Max((int)(factor * green + bias), 0), 255);
                        int b = Math.Min(Math.Max((int)(factor * blue + bias), 0), 255);

                        result[x, y] = Color.FromArgb(r, g, b);
                    }
                }
            }

            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    d.SetPixel(i, j, result[i, j]);
                }
            }
            pictureBox1.Image = d;
        }

        //roberts cross
        private void button13_Click(object sender, EventArgs e)
        {
            c = new Bitmap(pictureBox1.Image);
            Bitmap d = new Bitmap(c.Width, c.Height);
            int width = d.Width, height = d.Height, limit = 10 * 10;
            int[,] g1 = new int[,] { { 1,0},{ 0,-1} };
            int[,] g2 = new int[,] { { 0,1}, {-1,0 } };
            int[,] allPixR = new int[width, height];
            int[,] allPixG = new int[width, height];
            int[,] allPixB = new int[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    allPixR[i, j] = c.GetPixel(i, j).R;
                    allPixG[i, j] = c.GetPixel(i, j).G;
                    allPixB[i, j] = c.GetPixel(i, j).B;
                }
            }
            int new_r1 = 0, new_r2 = 0;
            int new_g1 = 0, new_g2 = 0;
            int new_b1 = 0, new_b2 = 0;
            int rc, gc, bc;
            for (int i = 1; i < d.Width - 1; i++)
            {
                for (int j = 1; j < d.Height - 1; j++)
                {

                    new_r1 = 0; new_r2 = 0;
                    new_g1 = 0; new_g2 = 0;
                    new_b1 = 0; new_b2 = 0;
                    rc = 0; gc = 0; bc = 0;

                    for (int wi = -1; wi < 1; wi++)
                    {
                        for (int hw = -1; hw < 1; hw++)
                        {
                            rc = allPixR[i + hw, j + wi];
                            new_r1 += g1[wi + 1, hw + 1] * rc;
                            new_r2 += g2[wi + 1, hw + 1] * rc;

                            gc = allPixG[i + hw, j + wi];
                            new_g1 += g1[wi + 1, hw + 1] * gc;
                            new_g2 += g2[wi + 1, hw + 1] * gc;

                            bc = allPixB[i + hw, j + wi];
                            new_b1 += g1[wi + 1, hw + 1] * bc;
                            new_b2 += g2[wi + 1, hw + 1] * bc;
                        }
                    }
                    if (new_r1 * new_r1 + new_r2 * new_r2 > limit || new_g1 * new_g1 + new_g2 * new_g2 > limit || new_b1 * new_b1 + new_b2 * new_b2 > limit)
                        d.SetPixel(i, j, Color.Black);
                    else
                        d.SetPixel(i, j, Color.Transparent);
                }
                pictureBox1.Image = d;
            }
        }

        //horizontal projection
        private void button14_Click(object sender, EventArgs e)
        {
            c = new Bitmap(pictureBox1.Image);

            int[] histogram = new int[256];
            float max = 0;

            for (int i = 0; i < c.Width; i++)
            {
                for (int j = 0; j < c.Height; j++)
                {
                    int redValue = c.GetPixel(i, j).R;
                    histogram[redValue]++;
                    if (max < histogram[redValue])
                        max = histogram[redValue];
                }
            }

            int histHeight = 150;
            Bitmap hist = new Bitmap(256, histHeight + 10);
            using (Graphics g = Graphics.FromImage(hist))
            {
                for (int i = 0; i < histogram.Length; i++)
                {
                    float pct = histogram[i] / max;
                    g.DrawLine(Pens.Black, new Point(i, hist.Height - 5), new Point(i, hist.Height - 5 - (int)(pct * histHeight)));
                }
            }
            pictureBox2.Image = hist;
        }

        //needed for histogram equalization
        static double Clamp(double val, double min, double max)
        {
            return Math.Min(Math.Max(val, min), max);
        }
        //histogram equalization
        private void button15_Click(object sender, EventArgs e)
        {
            c = new Bitmap(pictureBox1.Image);

            double blackPointPercent = 0.01, whitePointPercent = 0.03;
            BitmapData srcData = c.LockBits(new Rectangle(0, 0, c.Width, c.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Bitmap destImage = new Bitmap(c.Width, c.Height);
            BitmapData destData = destImage.LockBits(new Rectangle(0, 0, destImage.Width, destImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            int stride = srcData.Stride;
            IntPtr srcScan0 = srcData.Scan0;
            IntPtr destScan0 = destData.Scan0;
            var freq = new int[256];
            unsafe
            {
                byte* src = (byte*)srcScan0;
                for (int y = 0; y < c.Height; y++)
                {
                    for (int x = 0; x < c.Width; x++)
                    {
                       freq[src[y * stride + x * 4]]++;
                    }
                }

                int numPixels = c.Width * c.Height;
                int minI = 0, accum = 0; ;
                var blackPixels = numPixels * blackPointPercent;
                while (minI < 255)
                {
                    accum += freq[minI];
                    if (accum > blackPixels) break;
                    minI++;
                }
                int maxI = 255;
                var whitePixels = numPixels * whitePointPercent;
                accum = 0;
                while (maxI > 0)
                {
                    accum += freq[maxI];
                    if (accum > whitePixels) break;
                    maxI--;
                }
                double spread = 255d / (maxI - minI);
                byte* dst = (byte*)destScan0;
                for (int y = 0; y < c.Height; y++)
                {
                    for (int x = 0; x < c.Width; x++)
                    {
                        int i = y * stride + x * 4;

                        byte val = (byte)Clamp(Math.Round((src[i] - minI) * spread), 0, 255);
                        dst[i] = val;
                        dst[i + 1] = val;
                        dst[i + 2] = val;
                        dst[i + 3] = 255;
                    }
                }
                c.UnlockBits(srcData);
                destImage.UnlockBits(destData);
                Bitmap new_b = new Bitmap(destImage);
                int[] histogram = new int[256];
                float max = 0;
                for (int i = 0; i < new_b.Width; i++)
                {
                    for (int j = 0; j < new_b.Height; j++)
                    {
                        int redValue = new_b.GetPixel(i, j).R;
                        histogram[redValue]++;
                        if (max < histogram[redValue])
                            max = histogram[redValue];
                    }
                }
                int histHeight = 150;
                Bitmap hist = new Bitmap(256, histHeight + 10);
                using (Graphics g = Graphics.FromImage(hist))
                {
                    for (int i = 0; i < histogram.Length; i++)
                    {
                        float pct = histogram[i] / max;
                        g.DrawLine(Pens.Black, new Point(i, hist.Height - 5), new Point(i, hist.Height - 5 - (int)(pct * histHeight)));
                    }
                }
                pictureBox3.Image = hist;
            }


        }
    }
}
