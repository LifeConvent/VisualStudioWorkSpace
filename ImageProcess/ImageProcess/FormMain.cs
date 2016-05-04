using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ImageProcess
{
    //定义部分类PictureProcessing
    public partial class PictureProcessing : Form
    {
        //文件名
        private string curFileName;

        //位图对象
        private System.Drawing.Bitmap objBitmap;

        //当前文件//原始文件//原始位图
        private System.Drawing.Bitmap sourceBitmap;

        //新建矩形框体
        private Rectangle cursize = new Rectangle(0, 0, 0, 0);

        //声明窗体的宽高
        int width, height;

        //创建GDI+绘画类对象
        Graphics graphic;



        //获取图片显示尺寸-构造方法
        public PictureProcessing()
        {
            //初始化控件等
            InitializeComponent();
            width = this.pictureBox1.Width;
            height = this.pictureBox1.Height;
            graphic = this.pictureBox1.CreateGraphics();

        }

        /// <summary>
        /// 获取PictureBox在Zoom下显示的位置和大小
        /// <param name="p_PictureBox">Picture 如果没有图形或则非ZOOM模式 返回的是PictureBox的大小</param>
        /// <returns>如果p_PictureBox为null 返回 Rectangle(0, 0, 0, 0)</returns>
        public Rectangle GetPictureBoxZoomSize(PictureBox p_PictureBox)
        {
            if (p_PictureBox != null)
            {
                PropertyInfo _ImageRectanglePropert = p_PictureBox.GetType().GetProperty("ImageRectangle", BindingFlags.Instance | BindingFlags.NonPublic);

                return (Rectangle)_ImageRectanglePropert.GetValue(p_PictureBox, null);
            }
            return new Rectangle(0, 0, 0, 0);
        }

        //一堆功能函数
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog opnDig = new OpenFileDialog();
            //选择一个筛选器，默认选择所有的图片文件格式
            opnDig.Filter = "所有图像文件|*.bmp;*.img;*.png;*.jpg;*.jif";
            //设置窗口标题
            opnDig.Title = "打开图像文件";
            //如果为”打开“选定文件
            if (opnDig.ShowDialog() == DialogResult.OK)
            {
                //读取当前文件名
                curFileName = opnDig.FileName;
                //使用Image.FromFile创建图像对象
                try
                {
                    objBitmap = (Bitmap)Image.FromFile(curFileName);
                    sourceBitmap = (Bitmap)Image.FromFile(curFileName);

                    this.pictureBox1.Image = objBitmap;
                    this.pictureBox2.Image = sourceBitmap;
                    cursize = GetPictureBoxZoomSize(pictureBox2);

                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
            //对窗体进行重新绘制
            Invalidate();
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //如果没有创建图像则退出
            if (sourceBitmap == null)
            {
                return;
            }
            //调用SaveFileDialog
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Title = "另存为";
            //改写已存在文件提示
            saveDlg.OverwritePrompt = true;
            saveDlg.Filter = "BMP文件(*.bmp)|*.bmp|JPG文件(*.jpg)|*.jpg|BMP文件(*.gif)|*.gif|PNG文件(*.png)|*.png";
            saveDlg.ShowHelp = true;

            //保存图像
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveDlg.FileName;
                //获取扩展名
                string strFilExtn = fileName.Remove(0, fileName.Length - 3);

                switch (strFilExtn)
                {
                    case "bmp":
                        sourceBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "jpg":
                        sourceBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "png":
                        sourceBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case "gif":
                        sourceBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    default:
                        break;
                }
            }
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //如果没有创建图像则退出
            if (sourceBitmap == null)
            {
                return;
            }
            //调用SaveFileDialog
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Title = "另存为";
            //改写已存在文件提示
            saveDlg.OverwritePrompt = true;
            saveDlg.Filter = "BMP文件(*.bmp)|*.bmp|JPG文件(*.jpg)|*.jpg|BMP文件(*.gif)|*.gif|PNG文件(*.png)|*.png";
            saveDlg.ShowHelp = true;

            //保存图像
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveDlg.FileName;
                //获取扩展名
                string strFilExtn = fileName.Remove(0, fileName.Length - 3);

                switch (strFilExtn)
                {
                    case "bmp":
                        sourceBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "jpg":
                        sourceBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "png":
                        sourceBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case "gif":
                        sourceBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    default:
                        break;
                }
            }
        }

        private void 仅水平缩放ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sourceBitmap == null) { return; }
            graphic = this.pictureBox2.CreateGraphics();
            height = cursize.Height;
            width = cursize.Width;
            graphic.Clear(this.BackColor);
            for (int x = 0; x <= width; x += 1)
            {
                graphic.DrawImage(sourceBitmap, cursize.X, cursize.Y, x, height);
            }
            graphic.Dispose();
        }

        private void 上下ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graphic = this.pictureBox2.CreateGraphics();
            height = cursize.Height;
            width = cursize.Width;
            graphic.Clear(this.BackColor);
            for (int x = 0; x <= height; x += 1)
            {
                graphic.DrawImage(sourceBitmap, cursize.X, cursize.Y, width, x);
            }
            graphic.Dispose();
        }

        private void 全部ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graphic = this.pictureBox2.CreateGraphics();
            graphic.Clear(this.BackColor);
            int tempheight = 10;
            int theight = cursize.Height;
            width = cursize.Width;
            for (int x = 0; x <= width; x += 1)
            {
                tempheight = theight * x / width;
                graphic.DrawImage(sourceBitmap, cursize.X, cursize.Y, x, tempheight);
            }
            graphic.Dispose();
        }

        private void 上下ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            graphic = this.pictureBox2.CreateGraphics();
            graphic.Clear(this.BackColor);
            height = cursize.Height;
            width = cursize.Width;
            for (int x = -height / 2; x <= height / 2; x++)
            {
                Rectangle DestRect = new Rectangle(cursize.X, cursize.Y + height / 2 - x, width, 2 * x);
                Rectangle SrcRect = new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height);
                graphic.DrawImage(sourceBitmap, DestRect, SrcRect, GraphicsUnit.Pixel);
            }
            graphic.Dispose();
        }

        private void 左右ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            graphic = this.pictureBox2.CreateGraphics();
            graphic.Clear(this.BackColor);
            height = cursize.Height;
            width = cursize.Width;
            for (int x = -width / 2; x <= width / 2; x++)
            {
                Rectangle DestRect = new Rectangle(width / 2 - x, cursize.Y, 2 * x, height);
                Rectangle SrcRect = new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height);
                graphic.DrawImage(sourceBitmap, DestRect, SrcRect, GraphicsUnit.Pixel);
            }
            graphic.Dispose();
        }

        private void 平移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graphic = pictureBox2.CreateGraphics();
            graphic.Clear(pictureBox2.BackColor);
            for (int i = 0; i < 10; i++)
            {
                graphic.TranslateTransform(i, i);
                graphic.Clear(pictureBox2.BackColor);
                graphic.DrawImage(sourceBitmap, this.pictureBox2.ClientRectangle, 0, 0, cursize.Width, cursize.Height, GraphicsUnit.Pixel);
            }
        }

        private void 灰度变换ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            height = sourceBitmap.Height;
            width = sourceBitmap.Width;
            Color color;
            int r, g, b, Result = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    color = sourceBitmap.GetPixel(i, j);
                    r = color.R;
                    g = color.G;
                    b = color.B;
                    Result = ((int)(0.11 * r) + (int)(0.59 * g) + (int)(0.3 * b));
                    sourceBitmap.SetPixel(i, j, Color.FromArgb(Result, Result, Result));
                }
                this.pictureBox2.Image = sourceBitmap;
            }

        }

        private void 目标提取ToolStripMenuItem_Click(object sender, EventArgs e)
        {//罗伯特方法
            //定义四个颜色结构体
            Color c1 = new Color();
            Color c2 = new Color();
            Color c3 = new Color();
            Color c4 = new Color();
            int r1, r2, r3, r4, rr, gg, bb, rx, ry;
            int g1, g2, g3, g4, gx, gy;
            int b1, b2, b3, b4, bx, by;
            for (int i = 0; i < sourceBitmap.Width - 2; i++)
            {
                for (int j = 0; j < sourceBitmap.Height - 2; j++)
                {
                    //获取相邻四个像素颜色值
                    c1 = sourceBitmap.GetPixel(i, j);
                    c2 = sourceBitmap.GetPixel(i + 1, j + 1);
                    c3 = sourceBitmap.GetPixel(i, j + 1);
                    c4 = sourceBitmap.GetPixel(i + 1, j);
                    //红色分量
                    r1 = c1.R;
                    r2 = c2.R;
                    r3 = c3.R;
                    r4 = c4.R;
                    rx = r1 - r2;
                    ry = r3 - r4;
                    rr = Math.Abs(rx) + Math.Abs(ry) + 128;
                    //绿色分量
                    g1 = c1.G;
                    g2 = c2.G;
                    g3 = c3.G;
                    g4 = c4.G;
                    gx = g1 - g2;
                    gy = g3 - g4;
                    gg = Math.Abs(gx) + Math.Abs(gy) + 128;
                    //蓝色分量
                    b1 = c1.B;
                    b2 = c2.B;
                    b3 = c3.B;
                    b4 = c4.B;
                    bx = b1 - b2;
                    by = b3 - b4;
                    bb = Math.Abs(bx) + Math.Abs(by) + 128;

                    //处理溢出问题
                    rr = rr >= 255 ? 255 : rr;
                    gg = gg >= 255 ? 255 : gg;
                    bb = bb >= 255 ? 255 : bb;
                    rr = rr <= 0 ? 0 : rr;
                    gg = gg <= 0 ? 0 : gg;
                    bb = bb <= 0 ? 0 : bb;
                    Color cc = Color.FromArgb(rr, gg, bb);
                    sourceBitmap.SetPixel(i, j, cc);
                }
                pictureBox2.Image = sourceBitmap;
            }
        }

        private void 二值化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color color = new Color();
            int r, g, b;
            for (int i = 0; i < sourceBitmap.Width; i++)
            {
                for (int j = 0; j < sourceBitmap.Height; j++)
                {
                    color = sourceBitmap.GetPixel(i, j);
                    if (color.R > 128) r = 255; else r = 0;
                    if (color.G > 128) g = 255; else g = 0;
                    if (color.B > 128) b = 255; else b = 0;
                    Color cc = Color.FromArgb(r, g, b);
                    sourceBitmap.SetPixel(i, j, cc);
                }
            }
            pictureBox2.Image = sourceBitmap;
        }

        unsafe private void 八位灰度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bit = new Bitmap(sourceBitmap.Width, sourceBitmap.Height, PixelFormat.Format8bppIndexed);
            BitmapData data = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            byte* bp = (byte*)data.Scan0.ToPointer();
            BitmapData data2 = bit.LockBits(new Rectangle(0, 0, bit.Width, bit.Height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            byte* bp2 = (byte*)data2.Scan0.ToPointer();
            for (int i = 0; i != data.Height; i++)
            {
                for (int j = 0; j != data.Width; j++)
                {
                    //0.3R+0.59G+0.11B
                    float value = 0.11F * bp[i * data.Stride + j * 3]
                    + 0.59F * bp[i * data.Stride + j * 3 + 1]
                    + 0.3F * bp[i * data.Stride + j * 3 + 2];
                    bp2[i * data2.Stride + j] = (byte)value;
                }
            }
            sourceBitmap.UnlockBits(data);
            bit.UnlockBits(data2);
            ColorPalette palette = bit.Palette;
            for (int i = 0; i < palette.Entries.Length; i++)
            {
                palette.Entries[i] = Color.FromArgb(i, i, i);
            }
            bit.Palette = palette;
            pictureBox2.Image = bit;
            sourceBitmap = bit;
        }

        private void 重置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sourceBitmap = (Bitmap)Image.FromFile(curFileName);
            pictureBox2.Image = sourceBitmap;
            Invalidate();

        }

        private void 垂直翻转ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sourceBitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
            pictureBox2.Image = sourceBitmap;
        }

        private void 水平翻转ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sourceBitmap.RotateFlip(RotateFlipType.Rotate180FlipY);
            pictureBox2.Image = sourceBitmap;
        }

        private void 度水平翻转ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sourceBitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
            pictureBox2.Image = sourceBitmap;
        }

        private void 度水平翻转ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            sourceBitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
            pictureBox2.Image = sourceBitmap;
        }

        private void 锐化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            height = sourceBitmap.Height;
            width = sourceBitmap.Width;
            Color pixel;
            //拉普拉斯模版
            int[] Laplacian = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    int r = 0, g = 0, b = 0;
                    int Index = 0;
                    for (int col = -1; col <= 1; col++)
                    {
                        for (int row = -1; row <= 1; row++)
                        {
                            pixel = sourceBitmap.GetPixel(x + row, y + col);
                            r += pixel.R * Laplacian[Index];
                            g += pixel.G * Laplacian[Index];
                            b += pixel.B * Laplacian[Index];
                            Index++;
                        }
                    }

                    //处理溢出问题
                    r = r >= 255 ? 255 : r;
                    g = g >= 255 ? 255 : g;
                    b = b >= 255 ? 255 : b;
                    r = r <= 0 ? 0 : r;
                    g = g <= 0 ? 0 : g;
                    b = b <= 0 ? 0 : b;
                    sourceBitmap.SetPixel(x - 1, y - 1, Color.FromArgb(r, g, b));
                }
                this.pictureBox2.Image = sourceBitmap;
            }
        }

        private void 裁剪ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(new Point(0, 0), new Size(0, 0));
            FormCut formcut = new FormCut(objBitmap);
            formcut.ShowDialog();
            if (formcut.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                rect = formcut.retRect();
                sourceBitmap = sourceBitmap.Clone(rect, PixelFormat.DontCare);
            }
            formcut.Close();
            pictureBox2.Image = sourceBitmap;

        }

        private void 随机噪声ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int r, g, b;
            Random ran = new Random();
            for (int i = 0; i < sourceBitmap.Width; i++)
            {
                for (int j = 0; j < sourceBitmap.Height; j++)
                {
                    r = ran.Next(0, 255);
                    g = ran.Next(0, 255);
                    b = ran.Next(0, 255);
                    if (r % 3 == 0)
                    {
                        Color cc = Color.FromArgb(r, g, b);
                        sourceBitmap.SetPixel(i, j, cc);
                    }
                }
            }
            pictureBox2.Image = sourceBitmap;
        }

        private void 椒盐噪声ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int r, g, b;
            Random ran = new Random();
            for (int i = 0; i < sourceBitmap.Width; i++)
            {
                for (int j = 0; j < sourceBitmap.Height; j++)
                {
                    r = ran.Next(0, 255);
                    g = ran.Next(0, 255);
                    b = ran.Next(0, 255);
                    if (r % 5 == 0)
                    {
                        Color cc = Color.FromArgb(255, 255, 255);
                        sourceBitmap.SetPixel(i, j, cc);
                    }
                }
            }
            pictureBox2.Image = sourceBitmap;
        }
        /// <summary>
        /// 中值滤波
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nN中值滤波ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            height = sourceBitmap.Height;
            width = sourceBitmap.Width;
            Color[] pixel = new Color[9];//暂时建立一个3*3模版
            int[] red = new int[9];
            int[] green = new int[9];
            int[] blue = new int[9];
            int temp1 = 0, temp2 = 0, temp3 = 0;
            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    pixel[0] = sourceBitmap.GetPixel(i - 1, j - 1);
                    pixel[1] = sourceBitmap.GetPixel(i - 1, j);
                    pixel[2] = sourceBitmap.GetPixel(i - 1, j + 1);
                    pixel[3] = sourceBitmap.GetPixel(i, j - 1);
                    pixel[4] = sourceBitmap.GetPixel(i, j);
                    pixel[5] = sourceBitmap.GetPixel(i, j + 1);
                    pixel[6] = sourceBitmap.GetPixel(i + 1, j - 1);
                    pixel[7] = sourceBitmap.GetPixel(i + 1, j);
                    pixel[8] = sourceBitmap.GetPixel(i + 1, j + 1);
                    //取中值

                    for (int s = 0; s < 9; s++)
                    {
                        red[s] = pixel[s].R;
                        green[s] = pixel[s].R;
                        blue[s] = pixel[s].R;
                    }
                    //起泡排序
                    for (int x = 0; x < 8; x++)
                    {
                        for (int y = 0; y < 8 - x; y++)
                        {
                            if (red[y] < red[y + 1])
                            {
                                temp1 = red[y];
                                red[y] = red[y + 1];
                                red[y + 1] = temp1;
                            }
                            if (green[y] < green[y + 1])
                            {
                                temp2 = green[y];
                                green[y] = green[y + 1];
                                green[y + 1] = temp2;
                            }
                            if (blue[y] < blue[y + 1])
                            {
                                temp3 = blue[y];
                                blue[y] = blue[y + 1];
                                blue[y + 1] = temp3;
                            }
                        }
                    }
                    Color cc = Color.FromArgb(red[4], green[4], blue[4]);
                    sourceBitmap.SetPixel(i, j, cc);
                }
            }
            this.pictureBox2.Image = sourceBitmap;
        }

        private void 马赛克ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color c = new Color();//定义颜色
            int r1, g1, b1, rr, gg, bb, rx, gx, bx, k1, k2;
            //设定一个5*5的马赛克
            for (int i = 0; i < sourceBitmap.Width - 5; i += 5)
            {
                for (int j = 0; j < sourceBitmap.Height - 5; j += 5)
                {
                    rx = 0; gx = 0; bx = 0;
                    //获取颜色块分量
                    for (k1 = 0; k1 <= 5; k1++)
                    {
                        for (k2 = 0; k2 <= 5; k2++)
                        {
                            c = sourceBitmap.GetPixel(i + k1, j + k2);
                            r1 = c.R;
                            g1 = c.G;
                            b1 = c.B;
                            rx = rx + r1;
                            gx = gx + g1;
                            bx = bx + b1;
                        }
                    }
                    rr = (int)rx / 25;
                    gg = (int)gx / 25;
                    bb = (int)bx / 25;

                    rr = rr >= 255 ? 255 : rr;
                    gg = gg >= 255 ? 255 : gg;
                    bb = bb >= 255 ? 255 : bb;
                    rr = rr <= 0 ? 0 : rr;
                    gg = gg <= 0 ? 0 : gg;
                    bb = bb <= 0 ? 0 : bb;

                    for (k1 = 0; k1 <= 5; k1++)
                    {
                        for (k2 = 0; k2 <= 5; k2++)
                        {
                            Color cc = Color.FromArgb(rr, gg, bb);
                            sourceBitmap.SetPixel(i + k1, j + k2, cc);
                        }
                    }
                }
            }
            pictureBox2.Image = sourceBitmap;
        }

        private void 反色处理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color cc = new Color();
            for (int i = 0; i < sourceBitmap.Width; i++)
            {
                for (int j = 0; j < sourceBitmap.Height; j++)
                {
                    cc = sourceBitmap.GetPixel(i, j);
                    int r = 255 - cc.R;
                    int g = 255 - cc.G;
                    int b = 255 - cc.B;
                    Color newcolor = Color.FromArgb(r, g, b);
                    sourceBitmap.SetPixel(i, j, newcolor);
                }
            }
            pictureBox2.Image = sourceBitmap;
        }

        private void 浮雕效果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < sourceBitmap.Width - 1; i++)
            {
                for (int j = 0; j < sourceBitmap.Height - 1; j++)
                {
                    Color c1 = sourceBitmap.GetPixel(i, j);
                    Color c2 = sourceBitmap.GetPixel(i + 1, j + 1);
                    int r = Math.Max(67, Math.Min(255, Math.Abs(c1.R - c2.R + 128)));
                    int g = Math.Max(67, Math.Min(255, Math.Abs(c1.G - c2.G + 128)));
                    int b = Math.Max(67, Math.Min(255, Math.Abs(c1.B - c2.B + 128)));
                    Color result = Color.FromArgb(255, r, g, b);
                    sourceBitmap.SetPixel(i, j, result);
                }
            }
            pictureBox2.Image = sourceBitmap;
        }

        private void 百叶窗ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int width = sourceBitmap.Width;
            int height = sourceBitmap.Height / 20;
            Graphics myGraphics = this.CreateGraphics();
            myGraphics.Clear(Color.WhiteSmoke);
            Point[] mypoint = new Point[30];
            for (int i = 0; i < 30; i++)
            {
                mypoint[i].X = 0;
                mypoint[i].Y = i * height;
            }
            Bitmap bitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);
            for (int i = 0; i < height; i++)
            {
                for (int n = 0; n < 20; n++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        bitmap.SetPixel(mypoint[n].X + j, mypoint[n].Y + i, sourceBitmap.GetPixel(mypoint[n].X + j, mypoint[n].Y + i));
                    }
                }
                this.Refresh();
                pictureBox2.Image = bitmap;
                System.Threading.Thread.Sleep(300);
            }
        }

        private void 十字星中值滤波ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            height = sourceBitmap.Height;
            width = sourceBitmap.Width;
            Color[] pixel = new Color[5];//暂时建立一个3*3模版
            int[] red = new int[5];
            int[] green = new int[5];
            int[] blue = new int[5];
            int temp1 = 0, temp2 = 0, temp3 = 0;
            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    pixel[0] = sourceBitmap.GetPixel(i - 1, j);
                    pixel[1] = sourceBitmap.GetPixel(i, j - 1);
                    pixel[2] = sourceBitmap.GetPixel(i, j);
                    pixel[3] = sourceBitmap.GetPixel(i, j + 1);
                    pixel[4] = sourceBitmap.GetPixel(i + 1, j);
                    //取中值

                    for (int s = 0; s < 5; s++)
                    {
                        red[s] = pixel[s].R;
                        green[s] = pixel[s].R;
                        blue[s] = pixel[s].R;
                    }
                    //起泡排序
                    for (int x = 0; x < 4; x++)
                    {
                        for (int y = 0; y < 4 - x; y++)
                        {
                            if (red[y] < red[y + 1])
                            {
                                temp1 = red[y];
                                red[y] = red[y + 1];
                                red[y + 1] = temp1;
                            }
                            if (green[y] < green[y + 1])
                            {
                                temp2 = green[y];
                                green[y] = green[y + 1];
                                green[y + 1] = temp2;
                            }
                            if (blue[y] < blue[y + 1])
                            {
                                temp3 = blue[y];
                                blue[y] = blue[y + 1];
                                blue[y + 1] = temp3;
                            }
                        }
                    }
                    Color cc = Color.FromArgb(red[2], green[2], blue[2]);
                    sourceBitmap.SetPixel(i, j, cc);
                }
            }
            this.pictureBox2.Image = sourceBitmap;
        }

        private void nN最大值滤波ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            height = sourceBitmap.Height;
            width = sourceBitmap.Width;
            Color[] pixel = new Color[9];//暂时建立一个3*3模版
            int[] red = new int[9];
            int[] green = new int[9];
            int[] blue = new int[9];
            int temp1 = 0, temp2 = 0, temp3 = 0;
            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    pixel[0] = sourceBitmap.GetPixel(i - 1, j - 1);
                    pixel[1] = sourceBitmap.GetPixel(i - 1, j);
                    pixel[2] = sourceBitmap.GetPixel(i - 1, j + 1);
                    pixel[3] = sourceBitmap.GetPixel(i, j - 1);
                    pixel[4] = sourceBitmap.GetPixel(i, j);
                    pixel[5] = sourceBitmap.GetPixel(i, j + 1);
                    pixel[6] = sourceBitmap.GetPixel(i + 1, j - 1);
                    pixel[7] = sourceBitmap.GetPixel(i + 1, j);
                    pixel[8] = sourceBitmap.GetPixel(i + 1, j + 1);
                    //取最大值
                    for (int s = 0; s < 9; s++)
                    {
                        red[s] = pixel[s].R;
                        green[s] = pixel[s].R;
                        blue[s] = pixel[s].R;
                        if (temp1 < red[s]) temp1 = red[s];
                        if (temp2 < green[s]) temp2 = green[s];
                        if (temp3 < blue[s]) temp3 = blue[s];
                    }
                    Color cc = Color.FromArgb(temp1, temp2, temp3);
                    sourceBitmap.SetPixel(i, j, cc);
                    temp1 = 0; temp2 = 0; temp3 = 0;
                }
            }
            this.pictureBox2.Image = sourceBitmap;
        }

        private void 临域平均ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            height = sourceBitmap.Height;
            width = sourceBitmap.Width;
            Color[] pixel = new Color[8];//暂时建立一个3*3模版
            int[] red = new int[8];
            int[] green = new int[8];
            int[] blue = new int[8];
            int temp1 = 0, temp2 = 0, temp3 = 0;
            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    pixel[0] = sourceBitmap.GetPixel(i - 1, j - 1);
                    pixel[1] = sourceBitmap.GetPixel(i - 1, j);
                    pixel[2] = sourceBitmap.GetPixel(i - 1, j + 1);
                    pixel[3] = sourceBitmap.GetPixel(i, j - 1);
                    pixel[4] = sourceBitmap.GetPixel(i, j + 1);
                    pixel[5] = sourceBitmap.GetPixel(i + 1, j - 1);
                    pixel[6] = sourceBitmap.GetPixel(i + 1, j);
                    pixel[7] = sourceBitmap.GetPixel(i + 1, j + 1);
                    //取最大值
                    for (int s = 0; s < 8; s++)
                    {
                        red[s] = pixel[s].R;
                        green[s] = pixel[s].R;
                        blue[s] = pixel[s].R;
                        temp1 += red[s];
                        temp2 += green[s];
                        temp3 += blue[s];
                    }
                    Color cc = Color.FromArgb(temp1 / 8, temp2 / 8, temp3 / 8);
                    sourceBitmap.SetPixel(i, j, cc);
                    temp1 = 0; temp2 = 0; temp3 = 0;
                }
            }
            this.pictureBox2.Image = sourceBitmap;
        }

        private void gaussianFilter()
        {
            Color c1 = new Color();
            Color c2 = new Color();
            Color c3 = new Color();
            Color c4 = new Color();
            Color c5 = new Color();
            Color c6 = new Color();
            Color c7 = new Color();
            Color c8 = new Color();
            Color c9 = new Color();
            int rr, r1, r2, r3, r4, r5, r6, r7, r8, r9, i, j, fxr;
            Bitmap box1 = new Bitmap(pictureBox1.Image);
            for (i = 1; i <= pictureBox1.Image.Width - 2; i++)
            {
                for (j = 1; j <= pictureBox1.Image.Height - 2; j++)
                {
                    c1 = box1.GetPixel(i, j - 1);
                    c2 = box1.GetPixel(i - 1, j);
                    c3 = box1.GetPixel(i, j);
                    c4 = box1.GetPixel(i + 1, j);
                    c5 = box1.GetPixel(i, j + 1);
                    c6 = box1.GetPixel(i - 1, j - 1);
                    c7 = box1.GetPixel(i - 1, j + 1);
                    c8 = box1.GetPixel(i + 1, j - 1);
                    c9 = box1.GetPixel(i + 1, j + 1);
                    r1 = c1.R;
                    r2 = c2.R;
                    r3 = c3.R;
                    r4 = c4.R;
                    r5 = c5.R;
                    r6 = c6.R;
                    r7 = c7.R;
                    r8 = c8.R;
                    r9 = c9.R;
                    fxr = (r6 + r7 + r8 + r9 + 2 * r1 + 2 * r2 + 2 * r4 + 2 * r5 + 4 * r3) / 16;
                    rr = fxr;
                    if (rr < 0) rr = 0;
                    if (rr > 255) rr = 255;
                    Color cc = Color.FromArgb(rr, rr, rr);
                    box1.SetPixel(i, j, cc);
                }
            }
            this.pictureBox2.Image = box1;
        }

        private void gamma曲线调整ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GammaForm gfrom = new GammaForm(sourceBitmap);
            gfrom.ShowDialog();
            if (gfrom.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                sourceBitmap = gfrom.retBitmap();
            }
            gfrom.Close();
            pictureBox2.Image = sourceBitmap;
        }

        private void PictureProcessing_Load(object sender, EventArgs e)
        {

        }

        private void 功能组合ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 水平镜像转置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sourceBitmap == null) { return; }
            graphic = this.pictureBox2.CreateGraphics();
            height = cursize.Height;
            width = cursize.Width;
            graphic.Clear(this.BackColor);
            for (int x = 0; x <= width; x += 1)
            {
                graphic.DrawImage(sourceBitmap, cursize.X, cursize.Y, x, height);
            }
            sourceBitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
            pictureBox2.Image = sourceBitmap;
            //释放资源
            graphic.Dispose();
        }

        private void 垂直镜像转置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graphic = this.pictureBox2.CreateGraphics();
            graphic.Clear(this.BackColor);
            height = cursize.Height;
            width = cursize.Width;
            for (int x = -width / 2; x <= width / 2; x++)
            {
                Rectangle DestRect = new Rectangle(width / 2 - x, cursize.Y, 2 * x, height);
                Rectangle SrcRect = new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height);
                graphic.DrawImage(sourceBitmap, DestRect, SrcRect, GraphicsUnit.Pixel);
            }
            sourceBitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
            pictureBox2.Image = sourceBitmap;
            //释放资源
            graphic.Dispose();
        }

        private void 基本效果ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 灰度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            height = sourceBitmap.Height;
            width = sourceBitmap.Width;
            Color color;
            int r, g, b, Result = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    color = sourceBitmap.GetPixel(i, j);
                    r = color.R;
                    g = color.G;
                    b = color.B;
                    Result = ((int)(0.11 * r) + (int)(0.59 * g) + (int)(0.3 * b));
                    sourceBitmap.SetPixel(i, j, Color.FromArgb(Result, Result, Result));
                }
                this.pictureBox2.Image = sourceBitmap;
            }
        }

        private void 灰度直方图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graphic = this.pictureBox2.CreateGraphics();
            graphic.Clear(this.BackColor);
            try
            {
                //显示图片
                pictureBox1.Image = sourceBitmap;
                //图像高度
                int height = sourceBitmap.Height;
                //图像宽度
                int width = sourceBitmap.Width;
                int[] values = new int[256];
                for (int i = 0; i < 256; i++) values[i] = 0;

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Color pixelColor = sourceBitmap.GetPixel(x, y);
                        //颜色的 red 分量值
                        byte red = pixelColor.R;
                        //颜色的 green 分量值
                        byte green = pixelColor.G;
                        //颜色的 blue 分量值
                        byte blue = pixelColor.B;
                        int value = (red + green + blue) / 3;
                        values[value]++;
                    }
                }
                //找出要画图像里数据的最高值
                int max = 0;
                foreach (int i in values) max = max > i ? max : i;
                //图像高度为最大值加10；
                Image myimage = null;
                //画图
                draw(ref values, max, out myimage);
                pictureBox2.Image = myimage;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            //释放资源
            graphic.Dispose();
        }

        //画直方图
        private bool draw(ref int[] datas, int height, out Image myimage)
        {
            myimage = new Bitmap(256, height + 10);
            Graphics g = Graphics.FromImage(myimage);
            Pen pen = new Pen(Color.Blue);
            for (int i = 0; i <= 255; i++)
            {
                g.DrawLine(pen, i, height - datas[i] + 10, i, height + 10);
            }
            pictureBox2.Image = myimage;
            return true;
        }

        private void 灰度变换ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Bitmap myimage = null;
            Balance(sourceBitmap, out myimage);
            pictureBox2.Image = myimage;
        }

        public static bool Balance(Bitmap srcBmp, out Bitmap dstBmp)
        {
            if (srcBmp == null)
            {
                dstBmp = null;
                return false;
            }
            int[] histogramArrayR = new int[256];//各个灰度级的像素数R  
            int[] histogramArrayG = new int[256];//各个灰度级的像素数G  
            int[] histogramArrayB = new int[256];//各个灰度级的像素数B  
            int[] tempArrayR = new int[256];
            int[] tempArrayG = new int[256];
            int[] tempArrayB = new int[256];
            byte[] pixelMapR = new byte[256];
            byte[] pixelMapG = new byte[256];
            byte[] pixelMapB = new byte[256];
            dstBmp = new Bitmap(srcBmp);
            Rectangle rt = new Rectangle(0, 0, srcBmp.Width, srcBmp.Height);
            BitmapData bmpData = dstBmp.LockBits(rt, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                //统计各个灰度级的像素个数  
                for (int i = 0; i < bmpData.Height; i++)
                {
                    byte* ptr = (byte*)bmpData.Scan0 + i * bmpData.Stride;
                    for (int j = 0; j < bmpData.Width; j++)
                    {
                        histogramArrayB[*(ptr + j * 3)]++;
                        histogramArrayG[*(ptr + j * 3 + 1)]++;
                        histogramArrayR[*(ptr + j * 3 + 2)]++;
                    }
                }
                //计算各个灰度级的累计分布函数  
                for (int i = 0; i < 256; i++)
                {
                    if (i != 0)
                    {
                        tempArrayB[i] = tempArrayB[i - 1] + histogramArrayB[i];
                        tempArrayG[i] = tempArrayG[i - 1] + histogramArrayG[i];
                        tempArrayR[i] = tempArrayR[i - 1] + histogramArrayR[i];
                    }
                    else
                    {
                        tempArrayB[0] = histogramArrayB[0];
                        tempArrayG[0] = histogramArrayG[0];
                        tempArrayR[0] = histogramArrayR[0];
                    }
                    //计算累计概率函数，并将值放缩至0~255范围内  
                    pixelMapB[i] = (byte)(255.0 * tempArrayB[i] / (bmpData.Width * bmpData.Height) + 0.5);//加0.5为了四舍五入取整  
                    pixelMapG[i] = (byte)(255.0 * tempArrayG[i] / (bmpData.Width * bmpData.Height) + 0.5);
                    pixelMapR[i] = (byte)(255.0 * tempArrayR[i] / (bmpData.Width * bmpData.Height) + 0.5);
                }
                //映射转换  
                for (int i = 0; i < bmpData.Height; i++)
                {
                    byte* ptr = (byte*)bmpData.Scan0 + i * bmpData.Stride;
                    for (int j = 0; j < bmpData.Width; j++)
                    {
                        *(ptr + j * 3) = pixelMapB[*(ptr + j * 3)];
                        *(ptr + j * 3 + 1) = pixelMapG[*(ptr + j * 3 + 1)];
                        *(ptr + j * 3 + 2) = pixelMapR[*(ptr + j * 3 + 2)];
                    }
                }
            }
            dstBmp.UnlockBits(bmpData);
            return true;
        }

        private void 对数变换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            height = sourceBitmap.Height;
            width = sourceBitmap.Width;
            Color color;
            int[] c = new int[256];
            Bitmap bitmap = new Bitmap(width, height);
            for (int i = 0; i < 256; i++)
            {
                c[i] = (int)(Math.Log((double)i + 1.0) / (double)(25 * 0.001) + 0);
                if (c[i] < 0)
                {
                    c[i] = 0;
                }
                else if (c[i] > 255)
                {
                    c[i] = 255;
                }
            }
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    color = sourceBitmap.GetPixel(x, y);
                    bitmap.SetPixel(x, y, Color.FromArgb(c[color.R], c[color.G], c[color.B]));
                }
            }
            this.pictureBox2.Image = bitmap;
        }

        private void 幂次变换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            height = sourceBitmap.Height;
            width = sourceBitmap.Width;
            Color color;
            int[] c = new int[256];
            Bitmap bitmap = new Bitmap(width, height);
            for (int i = 0; i < 256; i++)
            {
                c[i] = (int)(10 * 0.1 * Math.Pow(i / 255.0, 20 * 0.01) * 255 + 20);
                if (c[i] < 0)
                {
                    c[i] = 0;
                }
                else if (c[i] > 255)
                {
                    c[i] = 255;
                }
            }
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    color = sourceBitmap.GetPixel(x, y);
                    bitmap.SetPixel(x, y, Color.FromArgb(c[color.R], c[color.G], c[color.B]));
                }
            }
            this.pictureBox2.Image = bitmap;
        }

        private void 指数变换ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            height = sourceBitmap.Height;
            width = sourceBitmap.Width;
            Color color;
            int[] c = new int[256];
            Bitmap bitmap = new Bitmap(width, height);
            for (int i = 0; i < 256; i++)
            {
                c[i] = (int)(Math.Pow(15 * 0.1, 50 * 0.001 * (i - 0)) - 1);
                if (c[i] < 0)
                {
                    c[i] = 0;
                }
                else if (c[i] > 255)
                {
                    c[i] = 255;
                }
            }
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    color = sourceBitmap.GetPixel(x, y);
                    bitmap.SetPixel(x, y, Color.FromArgb(c[color.R], c[color.G], c[color.B]));
                }
            }
            this.pictureBox2.Image = bitmap;
        }

        private void 阈值变换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            height = sourceBitmap.Height;
            width = sourceBitmap.Width;
            Color color;
            int r, g, b, Result = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    color = sourceBitmap.GetPixel(i, j);
                    r = color.R;
                    g = color.G;
                    b = color.B;
                    Result = ((int)(0.11 * r) + (int)(0.59 * g) + (int)(0.3 * b));
                    sourceBitmap.SetPixel(i, j, Color.FromArgb(Result, Result, Result));
                }
                this.pictureBox2.Image = sourceBitmap;
            }
        }

        private void 拉伸变换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            height = sourceBitmap.Height;
            width = sourceBitmap.Width;
            Color color;
            int r, g, b, Result = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    color = sourceBitmap.GetPixel(i, j);
                    r = color.R;
                    g = color.G;
                    b = color.B;
                    Result = ((int)(0.11 * r) + (int)(0.59 * g) + (int)(0.3 * b));
                    sourceBitmap.SetPixel(i, j, Color.FromArgb(Result, Result, Result));
                }
                this.pictureBox2.Image = sourceBitmap;
            }
        }

        private void 缩放ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 中间向两边拉伸ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sourceBitmap == null) { return; }
            graphic = this.pictureBox2.CreateGraphics();
            height = cursize.Height;
            width = cursize.Width;
            graphic.Clear(this.BackColor);
            for (int y = 0; y < width / 2; y++)
            {
                Rectangle DestRect = new Rectangle(width / 2 - y, 0, 2 * y, height);
                Rectangle SrcRect = new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height);
                graphic.DrawImage(sourceBitmap, DestRect, SrcRect, GraphicsUnit.Pixel);
            }
            graphic.Dispose();
        }

        private void 灰度拉伸ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap myimage = null;
            Stretch(sourceBitmap, out myimage);
            pictureBox2.Image = myimage;
        }

        public static bool Stretch(Bitmap srcBmp, out Bitmap dstBmp)
        {
            if (srcBmp == null)
            {
                dstBmp = null;
                return false;
            }
            double pR = 0.0;//斜率  
            double pG = 0.0;//斜率  
            double pB = 0.0;//斜率  
            byte minGrayDegree = 255;
            byte maxGrayDegree = 0;
            byte minGrayDegreeR = 255;
            byte maxGrayDegreeR = 0;
            byte minGrayDegreeG = 255;
            byte maxGrayDegreeG = 0;
            byte minGrayDegreeB = 255;
            byte maxGrayDegreeB = 0;
            dstBmp = new Bitmap(srcBmp);
            Rectangle rt = new Rectangle(0, 0, dstBmp.Width, dstBmp.Height);
            BitmapData bmpData = dstBmp.LockBits(rt, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                for (int i = 0; i < bmpData.Height; i++)
                {
                    byte* ptr = (byte*)bmpData.Scan0 + i * bmpData.Stride;
                    for (int j = 0; j < bmpData.Width; j++)
                    {
                        if (minGrayDegreeR > *(ptr + j * 3 + 2))
                            minGrayDegreeR = *(ptr + j * 3 + 2);
                        if (maxGrayDegreeR < *(ptr + j * 3 + 2))
                            maxGrayDegreeR = *(ptr + j * 3 + 2);
                        if (minGrayDegreeG > *(ptr + j * 3 + 1))
                            minGrayDegreeG = *(ptr + j * 3 + 1);
                        if (maxGrayDegreeG < *(ptr + j * 3 + 1))
                            maxGrayDegreeG = *(ptr + j * 3 + 1);
                        if (minGrayDegreeB > *(ptr + j * 3))
                            minGrayDegreeB = *(ptr + j * 3);
                        if (maxGrayDegreeB < *(ptr + j * 3))
                            maxGrayDegreeB = *(ptr + j * 3);
                    }
                }
                pR = 255.0 / (maxGrayDegreeR - minGrayDegreeR);
                pG = 255.0 / (maxGrayDegreeG - minGrayDegreeG);
                pB = 255.0 / (maxGrayDegreeB - minGrayDegreeB);
                for (int i = 0; i < bmpData.Height; i++)
                {
                    byte* ptr1 = (byte*)bmpData.Scan0 + i * bmpData.Stride;
                    for (int j = 0; j < bmpData.Width; j++)
                    {
                        *(ptr1 + j * 3) = (byte)((*(ptr1 + j * 3) - minGrayDegreeB) * pB + 0.5);
                        *(ptr1 + j * 3 + 1) = (byte)((*(ptr1 + j * 3 + 1) - minGrayDegreeG) * pG + 0.5);
                        *(ptr1 + j * 3 + 2) = (byte)((*(ptr1 + j * 3 + 2) - minGrayDegreeR) * pR + 0.5);
                    }
                }
            }
            dstBmp.UnlockBits(bmpData);
            return true;
        }

        private void 三种效果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //灰度变换部分
            height = sourceBitmap.Height;
            width = sourceBitmap.Width;
            Color color;
            int r, g, b, Result = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height / 3; j++)
                {
                    color = sourceBitmap.GetPixel(i, j);
                    r = color.R;
                    g = color.G;
                    b = color.B;
                    Result = ((int)(0.11 * r) + (int)(0.59 * g) + (int)(0.3 * b));
                    sourceBitmap.SetPixel(i, j, Color.FromArgb(Result, Result, Result));
                }
                this.pictureBox2.Image = sourceBitmap;
            }
            //反色处理部分
            Color cc = new Color();
            for (int i = 0; i < sourceBitmap.Width; i++)
            {
                for (int j = sourceBitmap.Height / 3; j < 2 * sourceBitmap.Height / 3; j++)
                {
                    cc = sourceBitmap.GetPixel(i, j);
                    int rr1 = 255 - cc.R;
                    int gg1 = 255 - cc.G;
                    int bb1 = 255 - cc.B;
                    Color newcolor = Color.FromArgb(rr1, gg1, bb1);
                    sourceBitmap.SetPixel(i, j, newcolor);
                }
            }
            pictureBox2.Image = sourceBitmap;

            //马赛克部分
            Color c = new Color();//定义颜色
            int r1, g1, b1, rr, gg, bb, rx, gx, bx, k1, k2;
            //设定一个5*5的马赛克
            for (int i = 0; i < sourceBitmap.Width - 5; i += 5)
            {
                for (int j = 2 * sourceBitmap.Height / 3; j < sourceBitmap.Height - 5; j += 5)
                {
                    rx = 0; gx = 0; bx = 0;
                    //获取颜色块分量
                    for (k1 = 0; k1 <= 5; k1++)
                    {
                        for (k2 = 0; k2 <= 5; k2++)
                        {
                            c = sourceBitmap.GetPixel(i + k1, j + k2);
                            r1 = c.R;
                            g1 = c.G;
                            b1 = c.B;
                            rx = rx + r1;
                            gx = gx + g1;
                            bx = bx + b1;
                        }
                    }
                    rr = (int)rx / 25;
                    gg = (int)gx / 25;
                    bb = (int)bx / 25;

                    rr = rr >= 255 ? 255 : rr;
                    gg = gg >= 255 ? 255 : gg;
                    bb = bb >= 255 ? 255 : bb;
                    rr = rr <= 0 ? 0 : rr;
                    gg = gg <= 0 ? 0 : gg;
                    bb = bb <= 0 ? 0 : bb;

                    for (k1 = 0; k1 <= 5; k1++)
                    {
                        for (k2 = 0; k2 <= 5; k2++)
                        {
                            Color cc1 = Color.FromArgb(rr, gg, bb);
                            sourceBitmap.SetPixel(i + k1, j + k2, cc1);
                        }
                    }
                }
            }
            pictureBox2.Image = sourceBitmap;
        }

        private void 边缘检测ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void roberts算子ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap myimage = null;
            Edge(sourceBitmap, "Roberts", out myimage);
            pictureBox2.Image = myimage;
        }

        private void prewitt算子ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap myimage = null;
            Edge(sourceBitmap, "Prewitt", out myimage);
            pictureBox2.Image = myimage;
        }

        private void sobel算子ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap myimage = null;
            Edge(sourceBitmap, "Sobel", out myimage);
            pictureBox2.Image = myimage;
        }

        private void 高斯平滑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color c1 = new Color();
            Color c2 = new Color();
            Color c3 = new Color();
            Color c4 = new Color();
            Color c5 = new Color();
            Color c6 = new Color();
            Color c7 = new Color();
            Color c8 = new Color();
            Color c9 = new Color();
            int rr, r1, r2, r3, r4, r5, r6, r7, r8, r9, i, j, fxr;
            Bitmap box1 = new Bitmap(pictureBox1.Image);
            for (i = 1; i <= pictureBox1.Image.Width - 2; i++)
            {
                for (j = 1; j <= pictureBox1.Image.Height - 2; j++)
                {
                    c1 = box1.GetPixel(i, j - 1);
                    c2 = box1.GetPixel(i - 1, j);
                    c3 = box1.GetPixel(i, j);
                    c4 = box1.GetPixel(i + 1, j);
                    c5 = box1.GetPixel(i, j + 1);
                    c6 = box1.GetPixel(i - 1, j - 1);
                    c7 = box1.GetPixel(i - 1, j + 1);
                    c8 = box1.GetPixel(i + 1, j - 1);
                    c9 = box1.GetPixel(i + 1, j + 1);
                    r1 = c1.R;
                    r2 = c2.R;
                    r3 = c3.R;
                    r4 = c4.R;
                    r5 = c5.R;
                    r6 = c6.R;
                    r7 = c7.R;
                    r8 = c8.R;
                    r9 = c9.R;
                    fxr = (r6 + r7 + r8 + r9 + 2 * r1 + 2 * r2 + 2 * r4 + 2 * r5 + 4 * r3) / 16;
                    rr = fxr;
                    if (rr < 0) rr = 0;
                    if (rr > 255) rr = 255;
                    Color cc = Color.FromArgb(rr, rr, rr);
                    box1.SetPixel(i, j, cc);
                }
            }
            this.pictureBox2.Image = box1;
        }

        /// <summary>  
        /// 边缘检测  
        /// </summary>  
        /// <param name="srcBmp">原始图像</param>  
        /// <param name="edgeDetectors">边缘检测算子</param>  
        /// <param name="dstBmp">目标图像</param>  
        /// <param name="mask">模板</param>  
        /// <param name="T">阈值，当算子为拉普拉斯时有用</param>  
        /// <returns>处理成功 true 失败 false</returns>  
        public static bool Edge(Bitmap srcBmp, String edgeDetectors, out Bitmap dstBmp, int[] mask = null, int T = 0)
        {
            if (srcBmp == null)
            {
                dstBmp = null;
                return false;
            }
            int[] template = new int[25];
            if (mask != null) { template = mask; }
            Bitmap tempSrcBmp = new Bitmap(srcBmp);//为形态学边缘检测所用  
            dstBmp = new Bitmap(srcBmp);
            BitmapData bmpDataSrc = srcBmp.LockBits(new Rectangle(0, 0, srcBmp.Width, srcBmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmpDataDst = dstBmp.LockBits(new Rectangle(0, 0, dstBmp.Width, dstBmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            //double[] laplacianArray = new double[bmpDataSrc.Stride * bmpDataSrc.Height];//存储拉普拉斯算子中间结果   
            unsafe
            {
                byte* ptrSrc = (byte*)bmpDataSrc.Scan0;
                byte* ptrDst = (byte*)bmpDataDst.Scan0;
                double gradX, gradY, grad;
                switch (edgeDetectors)
                {
                    case "Roberts"://Roberts算子  
                        for (int i = 0; i < srcBmp.Height; i++)
                        {
                            for (int j = 0; j < srcBmp.Width; j++)
                            {
                                gradX = ptrSrc[i * bmpDataSrc.Stride + j * 3] - ptrSrc[(i + 1) % srcBmp.Height * bmpDataSrc.Stride + (j + 1) % srcBmp.Width * 3];
                                gradY = ptrSrc[i * bmpDataSrc.Stride + (j + 1) % srcBmp.Width * 3] - ptrSrc[(i + 1) % srcBmp.Height * bmpDataSrc.Stride + j * 3];
                                grad = Math.Sqrt(gradX * gradX + gradY * gradY);
                                grad = grad > 255 ? 255 : grad;
                                ptrDst[i * bmpDataDst.Stride + j * 3] = ptrDst[i * bmpDataDst.Stride + j * 3 + 1] = ptrDst[i * bmpDataDst.Stride + j * 3 + 2] = (byte)grad;
                            }
                        }
                        break;
                    case "Prewitt"://prewitt算子  
                        for (int i = 0; i < srcBmp.Height; i++)
                        {
                            for (int j = 0; j < srcBmp.Width; j++)
                            {
                                gradX = ptrSrc[Math.Abs(i - 1) % srcBmp.Height * bmpDataSrc.Stride + (j + 1) % srcBmp.Width * 3] +
                                        ptrSrc[i * bmpDataSrc.Stride + (j + 1) % srcBmp.Width * 3] +
                                        ptrSrc[(i + 1) % srcBmp.Height * bmpDataSrc.Stride + (j + 1) % srcBmp.Width * 3] -
                                        ptrSrc[Math.Abs(i - 1) % srcBmp.Height * bmpDataSrc.Stride + Math.Abs(j - 1) % srcBmp.Width * 3] -
                                        ptrSrc[i * bmpDataSrc.Stride + Math.Abs(j - 1) % srcBmp.Width * 3] -
                                        ptrSrc[(i + 1) % srcBmp.Height * bmpDataSrc.Stride + Math.Abs(j - 1) % srcBmp.Width * 3];
                                gradY = ptrSrc[Math.Abs(i - 1) % srcBmp.Height * bmpDataSrc.Stride + Math.Abs(j - 1) % srcBmp.Width * 3] +
                                        ptrSrc[Math.Abs(i - 1) % srcBmp.Height * bmpDataSrc.Stride + j * 3] +
                                        ptrSrc[Math.Abs(i - 1) % srcBmp.Height * bmpDataSrc.Stride + (j + 1) % srcBmp.Width * 3] -
                                        ptrSrc[(i + 1) % srcBmp.Height * bmpDataSrc.Stride + Math.Abs(j - 1) % srcBmp.Width * 3] -
                                        ptrSrc[(i + 1) % srcBmp.Height * bmpDataSrc.Stride + j * 3] -
                                        ptrSrc[(i + 1) % srcBmp.Height * bmpDataSrc.Stride + (j + 1) % srcBmp.Width * 3];
                                grad = Math.Sqrt(gradX * gradX + gradY * gradY);
                                grad = grad > 255 ? 255 : grad;
                                ptrDst[i * bmpDataDst.Stride + j * 3] = ptrDst[i * bmpDataDst.Stride + j * 3 + 1] = ptrDst[i * bmpDataDst.Stride + j * 3 + 2] = (byte)grad;
                            }
                        }
                        break;
                    case "Sobel"://solbel算子  
                        for (int i = 0; i < srcBmp.Height; i++)
                        {
                            for (int j = 0; j < srcBmp.Width; j++)
                            {
                                gradX = ptrSrc[Math.Abs(i - 1) % srcBmp.Height * bmpDataSrc.Stride + (j + 1) % srcBmp.Width * 3] +
                                        2 * ptrSrc[i * bmpDataSrc.Stride + (j + 1) % srcBmp.Width * 3] +
                                        ptrSrc[(i + 1) % srcBmp.Height * bmpDataSrc.Stride + (j + 1) % srcBmp.Width * 3] -
                                        ptrSrc[Math.Abs(i - 1) % srcBmp.Height * bmpDataSrc.Stride + Math.Abs(j - 1) % srcBmp.Width * 3] -
                                        2 * ptrSrc[i * bmpDataSrc.Stride + Math.Abs(j - 1) % srcBmp.Width * 3] -
                                        ptrSrc[(i + 1) % srcBmp.Height * bmpDataSrc.Stride + Math.Abs(j - 1) % srcBmp.Width * 3];
                                gradY = ptrSrc[Math.Abs(i - 1) % srcBmp.Height * bmpDataSrc.Stride + Math.Abs(j - 1) % srcBmp.Width * 3] +
                                        2 * ptrSrc[Math.Abs(i - 1) % srcBmp.Height * bmpDataSrc.Stride + j * 3] +
                                        ptrSrc[Math.Abs(i - 1) % srcBmp.Height * bmpDataSrc.Stride + (j + 1) % srcBmp.Width * 3] -
                                        ptrSrc[(i + 1) % srcBmp.Height * bmpDataSrc.Stride + Math.Abs(j - 1) % srcBmp.Width * 3] -
                                        2 * ptrSrc[(i + 1) % srcBmp.Height * bmpDataSrc.Stride + j * 3] -
                                        ptrSrc[(i + 1) % srcBmp.Height * bmpDataSrc.Stride + (j + 1) % srcBmp.Width * 3];
                                grad = Math.Sqrt(gradX * gradX + gradY * gradY);
                                grad = grad > 255 ? 255 : grad;
                                ptrDst[i * bmpDataDst.Stride + j * 3] = ptrDst[i * bmpDataDst.Stride + j * 3 + 1] = ptrDst[i * bmpDataDst.Stride + j * 3 + 2] = (byte)grad;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            srcBmp.UnlockBits(bmpDataSrc);
            dstBmp.UnlockBits(bmpDataDst);

            return true;
        }

        /// <summary>  
        /// 腐蚀运算  
        /// </summary>  
        /// <param name="srcBmp">原始图像</param>  
        /// <param name="dstBmp">目标图像</param>  
        /// <returns>处理成功 true 失败 false</returns>  
        public static bool Erode(Bitmap srcBmp, out Bitmap dstBmp)
        {
            if (srcBmp == null)
            {
                dstBmp = null;
                return false;
            }
            Bitmap grayBmp = null;
            dstBmp = new Bitmap(srcBmp);
            BitmapData bmpDataGray = null;
            ToGray(srcBmp, out grayBmp);
            bmpDataGray = grayBmp.LockBits(new Rectangle(0, 0, grayBmp.Width, grayBmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData bmpDataSrc = srcBmp.LockBits(new Rectangle(0, 0, srcBmp.Width, srcBmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmpDataDst = dstBmp.LockBits(new Rectangle(0, 0, dstBmp.Width, dstBmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                int[] grayValueArray = new int[25];
                long[] index = new long[25];
                for (int i = 0; i < 25; i++)
                {
                    grayValueArray[i] = 255;
                }
                byte* ptrSrc = (byte*)bmpDataSrc.Scan0;
                byte* ptrDst = (byte*)bmpDataDst.Scan0;
                byte* ptrGray = null;
                ptrGray = (byte*)bmpDataGray.Scan0;
                for (int i = 0; i < bmpDataSrc.Height; i++)
                {//循环延拓，解决图像四周不能处理的问题  
                    for (int j = 0; j < bmpDataSrc.Width; j++)
                    {
                        grayValueArray[0] = ptrGray[Math.Abs(i - 1) % bmpDataSrc.Height * bmpDataSrc.Stride + Math.Abs(j - 1) % bmpDataSrc.Width * 3];
                        grayValueArray[1] = ptrGray[Math.Abs(i - 1) % bmpDataSrc.Height * bmpDataSrc.Stride + j * 3];
                        grayValueArray[2] = ptrGray[Math.Abs(i - 1) % bmpDataSrc.Height * bmpDataSrc.Stride + (j + 1) % bmpDataSrc.Width * 3];
                        grayValueArray[3] = ptrGray[i * bmpDataSrc.Stride + Math.Abs(j - 1) % bmpDataSrc.Width * 3];
                        grayValueArray[4] = ptrGray[i * bmpDataSrc.Stride + j * 3];
                        grayValueArray[5] = ptrGray[i * bmpDataSrc.Stride + (j + 1) % bmpDataSrc.Width * 3];
                        grayValueArray[6] = ptrGray[(i + 1) % bmpDataSrc.Height * bmpDataSrc.Stride + Math.Abs(j - 1) % bmpDataSrc.Width * 3];
                        grayValueArray[7] = ptrGray[(i + 1) % bmpDataSrc.Height * bmpDataSrc.Stride + j * 3];
                        grayValueArray[8] = ptrGray[(i + 1) % bmpDataSrc.Height * bmpDataSrc.Stride + (j + 1) % bmpDataSrc.Width * 3];
                        index[0] = Math.Abs(i - 1) % bmpDataSrc.Height * bmpDataSrc.Stride + Math.Abs(j - 1) % bmpDataSrc.Width * 3;
                        index[1] = Math.Abs(i - 1) % bmpDataSrc.Height * bmpDataSrc.Stride + j * 3;
                        index[2] = Math.Abs(i - 1) % bmpDataSrc.Height * bmpDataSrc.Stride + (j + 1) % bmpDataSrc.Width * 3;
                        index[3] = i * bmpDataSrc.Stride + Math.Abs(j - 1) % bmpDataSrc.Width * 3;
                        index[4] = i * bmpDataSrc.Stride + j * 3;
                        index[5] = i * bmpDataSrc.Stride + (j + 1) % bmpDataSrc.Width * 3;
                        index[6] = (i + 1) % bmpDataSrc.Height * bmpDataSrc.Stride + Math.Abs(j - 1) % bmpDataSrc.Width * 3;
                        index[7] = (i + 1) % bmpDataSrc.Height * bmpDataSrc.Stride + j * 3;
                        index[8] = (i + 1) % bmpDataSrc.Height * bmpDataSrc.Stride + (j + 1) % bmpDataSrc.Width * 3;
                        Array.Sort(grayValueArray, index);
                        ptrDst[i * bmpDataDst.Stride + j * 3] = ptrSrc[index[0]];
                        ptrDst[i * bmpDataDst.Stride + j * 3 + 1] = ptrSrc[index[0] + 1];
                        ptrDst[i * bmpDataDst.Stride + j * 3 + 2] = ptrSrc[index[0] + 2];
                    }
                }
                srcBmp.UnlockBits(bmpDataSrc);
                dstBmp.UnlockBits(bmpDataDst);
            }
            return true;
        }

        /// <summary>  
        /// 膨胀运算  
        /// </summary>  
        /// <param name="srcBmp">原始图像</param>  
        /// <param name="dstBmp">目标图像</param>  
        /// <returns>处理成功 true 失败 false</returns>  
        public static bool enErode(Bitmap srcBmp, out Bitmap dstBmp)
        {
            if (srcBmp == null)
            {
                dstBmp = null;
                return false;
            }
            Bitmap grayBmp = null;
            dstBmp = new Bitmap(srcBmp);
            BitmapData bmpDataGray = null;
            ToGray(srcBmp, out grayBmp);
            bmpDataGray = grayBmp.LockBits(new Rectangle(0, 0, grayBmp.Width, grayBmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData bmpDataSrc = srcBmp.LockBits(new Rectangle(0, 0, srcBmp.Width, srcBmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmpDataDst = dstBmp.LockBits(new Rectangle(0, 0, dstBmp.Width, dstBmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                int[] grayValueArray = new int[25];
                long[] index = new long[25];
                for (int i = 0; i < 25; i++)
                {
                    grayValueArray[i] = 255;
                }
                byte* ptrSrc = (byte*)bmpDataSrc.Scan0;
                byte* ptrDst = (byte*)bmpDataDst.Scan0;
                byte* ptrGray = null;
                ptrGray = (byte*)bmpDataGray.Scan0;
                for (int i = 0; i < bmpDataSrc.Height; i++)
                {
                    for (int j = 0; j < bmpDataSrc.Width; j++)
                    {//循环延拓，解决图像四周不能处理的问题  
                        grayValueArray[0] = ptrGray[Math.Abs(i - 1) % bmpDataSrc.Height * bmpDataSrc.Stride + Math.Abs(j - 1) % bmpDataSrc.Width * 3];
                        grayValueArray[1] = ptrGray[Math.Abs(i - 1) % bmpDataSrc.Height * bmpDataSrc.Stride + j * 3];
                        grayValueArray[2] = ptrGray[Math.Abs(i - 1) % bmpDataSrc.Height * bmpDataSrc.Stride + (j + 1) % bmpDataSrc.Width * 3];
                        grayValueArray[3] = ptrGray[i * bmpDataSrc.Stride + Math.Abs(j - 1) % bmpDataSrc.Width * 3];
                        grayValueArray[4] = ptrGray[i * bmpDataSrc.Stride + j * 3];
                        grayValueArray[5] = ptrGray[i * bmpDataSrc.Stride + (j + 1) % bmpDataSrc.Width * 3];
                        grayValueArray[6] = ptrGray[(i + 1) % bmpDataSrc.Height * bmpDataSrc.Stride + Math.Abs(j - 1) % bmpDataSrc.Width * 3];
                        grayValueArray[7] = ptrGray[(i + 1) % bmpDataSrc.Height * bmpDataSrc.Stride + j * 3];
                        grayValueArray[8] = ptrGray[(i + 1) % bmpDataSrc.Height * bmpDataSrc.Stride + (j + 1) % bmpDataSrc.Width * 3];
                        index[0] = Math.Abs(i - 1) % bmpDataSrc.Height * bmpDataSrc.Stride + Math.Abs(j - 1) % bmpDataSrc.Width * 3;
                        index[1] = Math.Abs(i - 1) % bmpDataSrc.Height * bmpDataSrc.Stride + j * 3;
                        index[2] = Math.Abs(i - 1) % bmpDataSrc.Height * bmpDataSrc.Stride + (j + 1) % bmpDataSrc.Width * 3;
                        index[3] = i * bmpDataSrc.Stride + Math.Abs(j - 1) % bmpDataSrc.Width * 3;
                        index[4] = i * bmpDataSrc.Stride + j * 3;
                        index[5] = i * bmpDataSrc.Stride + (j + 1) % bmpDataSrc.Width * 3;
                        index[6] = (i + 1) % bmpDataSrc.Height * bmpDataSrc.Stride + Math.Abs(j - 1) % bmpDataSrc.Width * 3;
                        index[7] = (i + 1) % bmpDataSrc.Height * bmpDataSrc.Stride + j * 3;
                        index[8] = (i + 1) % bmpDataSrc.Height * bmpDataSrc.Stride + (j + 1) % bmpDataSrc.Width * 3;
                        Array.Sort(grayValueArray, index);
                        ptrDst[i * bmpDataDst.Stride + j * 3] = ptrSrc[index[8]];
                        ptrDst[i * bmpDataDst.Stride + j * 3 + 1] = ptrSrc[index[8] + 1];
                        ptrDst[i * bmpDataDst.Stride + j * 3 + 2] = ptrSrc[index[8] + 2];
                    }
                }
                srcBmp.UnlockBits(bmpDataSrc);
                dstBmp.UnlockBits(bmpDataDst);
            }
            return true;
        }

        public static bool ToGray(Bitmap srcImg, out Bitmap dstImg)
        {
            if (srcImg == null)
            {
                dstImg = null;
                return false;
            }
            dstImg = new Bitmap(srcImg);
            Rectangle ret = new Rectangle(0, 0, dstImg.Width, dstImg.Height);//划定要处理的像素区域              
            BitmapData dstbmpData = dstImg.LockBits(ret, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);//获取像素数据    
            unsafe
            {
                byte temp = 0;
                byte* dsttempPtr = (byte*)dstbmpData.Scan0;
                for (int i = 0; i < dstbmpData.Height; i++)
                {
                    dsttempPtr = (byte*)dstbmpData.Scan0 + i * dstbmpData.Stride;
                    for (int j = 0; j < dstbmpData.Width; j++)
                    {//在内存中的数据顺序是BGR  
                        temp = (byte)(0.299 * dsttempPtr[j * 3 + 2] + 0.587 * dsttempPtr[j * 3 + 1] + 0.114 * dsttempPtr[j * 3]);
                        dsttempPtr[j * 3 + 2] = dsttempPtr[j * 3 + 1] = dsttempPtr[j * 3] = temp;
                    }
                }
            }
            dstImg.UnlockBits(dstbmpData);//解锁  
            return true;
        }

        private void 拉普拉斯锐化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           pictureBox2.Image = SharpenImage(sourceBitmap);
        }

        public Bitmap SharpenImage(Bitmap bmp)
        {
            int height = bmp.Height;
            int width = bmp.Width;
            Bitmap newbmp = new Bitmap(width, height);

            LockBitmap lbmp = new LockBitmap(bmp);
            LockBitmap newlbmp = new LockBitmap(newbmp);
            lbmp.LockBits();
            newlbmp.LockBits();

            Color pixel;
            //拉普拉斯模板
            int[] Laplacian = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    int r = 0, g = 0, b = 0;
                    int Index = 0;
                    for (int col = -1; col <= 1; col++)
                    {
                        for (int row = -1; row <= 1; row++)
                        {
                            pixel = lbmp.GetPixel(x + row, y + col); r += pixel.R * Laplacian[Index];
                            g += pixel.G * Laplacian[Index];
                            b += pixel.B * Laplacian[Index];
                            Index++;
                        }
                    }
                    //处理颜色值溢出
                    r = r > 255 ? 255 : r;
                    r = r < 0 ? 0 : r;
                    g = g > 255 ? 255 : g;
                    g = g < 0 ? 0 : g;
                    b = b > 255 ? 255 : b;
                    b = b < 0 ? 0 : b;
                    newlbmp.SetPixel(x - 1, y - 1, Color.FromArgb(r, g, b));
                }
            }
            lbmp.UnlockBits();
            newlbmp.UnlockBits();
            return newbmp;
        }

        //雾化
        public Bitmap AtomizationImage(Bitmap bmp)
        {
            int height = bmp.Height;
            int width = bmp.Width;
            Bitmap newbmp = new Bitmap(width, height);

            LockBitmap lbmp = new LockBitmap(bmp);
            LockBitmap newlbmp = new LockBitmap(newbmp);
            lbmp.LockBits();
            newlbmp.LockBits();

            System.Random MyRandom = new Random();
            Color pixel;
            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    int k = MyRandom.Next(123456);
                    //像素块大小
                    int dx = x + k % 19;
                    int dy = y + k % 19;
                    if (dx >= width)
                        dx = width - 1;
                    if (dy >= height)
                        dy = height - 1;
                    pixel = lbmp.GetPixel(dx, dy);
                    newlbmp.SetPixel(x, y, pixel);
                }
            }
            lbmp.UnlockBits();
            newlbmp.UnlockBits();
            return newbmp;
        }

        //拉普拉斯算子（四邻域）
        //  g(i,j) = abs(4f(i,j) - f(i,j-1) - f(i,j+1) - f(i-1,j) - f(i+1,j))
        private Bitmap Laplace4(Bitmap bmp)
        {
            Bitmap newbmp = new Bitmap(bmp.Width, bmp.Height);
            LockBitmap lbmp = new LockBitmap(bmp);
            LockBitmap newlbmp = new LockBitmap(newbmp);
            lbmp.LockBits();
            newlbmp.LockBits();

            for (int i = 1; i < bmp.Width - 1; i++)
            {
                for (int j = 1; j < bmp.Height - 1; j++)
                {
                    Color c2 = lbmp.GetPixel(i, j - 1);
                    Color c4 = lbmp.GetPixel(i - 1, j);
                    Color c5 = lbmp.GetPixel(i, j);
                    Color c6 = lbmp.GetPixel(i + 1, j);
                    Color c8 = lbmp.GetPixel(i, j + 1);

                    int r = Math.Abs(4 * c5.R - c2.R - c4.R - c6.R - c8.R);
                    int g = Math.Abs(4 * c5.G - c2.G - c4.G - c6.G - c8.G);
                    int b = Math.Abs(4 * c5.B - c2.B - c4.B - c6.B - c8.B);

                    if (r > 255) r = 255;
                    if (r < 0) r = 0;
                    if (g > 255) g = 255;
                    if (g < 0) g = 0;
                    if (b > 255) b = 255;
                    if (b < 0) b = 0;

                    newlbmp.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }

            lbmp.UnlockBits();
            newlbmp.UnlockBits();

            return newbmp;
        }
        private Bitmap Laplace8(Bitmap bmp)
        {
            Bitmap newbmp = new Bitmap(bmp.Width, bmp.Height);
            LockBitmap lbmp = new LockBitmap(bmp);
            LockBitmap newlbmp = new LockBitmap(newbmp);
            lbmp.LockBits();
            newlbmp.LockBits();

            for (int i = 1; i < bmp.Width - 1; i++)
            {
                for (int j = 1; j < bmp.Height - 1; j++)
                {
                    Color c1 = lbmp.GetPixel(i - 1, j - 1);
                    Color c2 = lbmp.GetPixel(i, j - 1);
                    Color c3 = lbmp.GetPixel(i + 1, j - 1);
                    Color c4 = lbmp.GetPixel(i - 1, j);
                    Color c5 = lbmp.GetPixel(i, j);
                    Color c6 = lbmp.GetPixel(i + 1, j);
                    Color c7 = lbmp.GetPixel(i - 1, j + 1);
                    Color c8 = lbmp.GetPixel(i, j + 1);
                    Color c9 = lbmp.GetPixel(i + 1, j + 1);

                    int r = Math.Abs(8 * c5.R - c1.R - c2.R - c3.R - c4.R - c6.R - c7.R - c8.R - c9.R);
                    int g = Math.Abs(8 * c5.G - c1.G - c2.G - c3.G - c4.G - c6.G - c7.G - c8.G - c9.G);
                    int b = Math.Abs(8 * c5.B - c1.B - c2.B - c3.B - c4.B - c6.B - c7.B - c8.B - c9.B);

                    if (r > 255) r = 255;
                    if (r < 0) r = 0;
                    if (g > 255) g = 255;
                    if (g < 0) g = 0;
                    if (b > 255) b = 255;
                    if (b < 0) b = 0;

                    newlbmp.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }

            lbmp.UnlockBits();
            newlbmp.UnlockBits();

            return newbmp;
        }

        private void 四邻域ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = Laplace4(sourceBitmap);
        }

        private void 八邻域ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = Laplace8(sourceBitmap);
        }

        private void 右下ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = RightBottomEdge(sourceBitmap);
        }

        //右下边缘抽出
        //  g(i,j) = abs(2f(i+1,j) + 2f(i,j+1) - 2f(i,j-1) - 2f(i-1,j));
        private Bitmap RightBottomEdge(Bitmap bmp)
        {
            Bitmap newbmp = new Bitmap(bmp.Width, bmp.Height);
            LockBitmap lbmp = new LockBitmap(bmp);
            LockBitmap newlbmp = new LockBitmap(newbmp);
            lbmp.LockBits();
            newlbmp.LockBits();

            for (int i = 1; i < bmp.Width - 1; i++)
            {
                for (int j = 1; j < bmp.Height - 1; j++)
                {
                    Color c2 = lbmp.GetPixel(i, j - 1);
                    Color c4 = lbmp.GetPixel(i - 1, j);
                    Color c6 = lbmp.GetPixel(i + 1, j);
                    Color c8 = lbmp.GetPixel(i, j + 1);

                    int r = 2 * Math.Abs(c6.R + c8.R - c2.R - c4.R);
                    int g = 2 * Math.Abs(c6.G + c8.G - c2.G - c4.G);
                    int b = 2 * Math.Abs(c6.B + c8.B - c2.B - c4.B);

                    if (r > 255) r = 255;
                    if (r < 0) r = 0;
                    if (g > 255) g = 255;
                    if (g < 0) g = 0;
                    if (b > 255) b = 255;
                    if (b < 0) b = 0;

                    newlbmp.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }

            lbmp.UnlockBits();
            newlbmp.UnlockBits();

            return newbmp;
        }
    }

    public class LockBitmap
    {
        Bitmap source = null;
        IntPtr Iptr = IntPtr.Zero;
        BitmapData bitmapData = null;

        public byte[] Pixels { get; set; }
        public int Depth { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public LockBitmap(Bitmap source)
        {
            this.source = source;
        }

        /// <summary>
        /// Lock bitmap data
        /// </summary>
        public void LockBits()
        {
            try
            {
                // Get width and height of bitmap
                Width = source.Width;
                Height = source.Height;

                // get total locked pixels count
                int PixelCount = Width * Height;

                // Create rectangle to lock
                Rectangle rect = new Rectangle(0, 0, Width, Height);

                // get source bitmap pixel format size
                Depth = System.Drawing.Bitmap.GetPixelFormatSize(source.PixelFormat);

                // Check if bpp (Bits Per Pixel) is 8, 24, or 32
                if (Depth != 8 && Depth != 24 && Depth != 32)
                {
                    throw new ArgumentException("Only 8, 24 and 32 bpp images are supported.");
                }

                // Lock bitmap and return bitmap data
                bitmapData = source.LockBits(rect, ImageLockMode.ReadWrite,
                                             source.PixelFormat);

                // create byte array to copy pixel values
                int step = Depth / 8;
                Pixels = new byte[PixelCount * step];
                Iptr = bitmapData.Scan0;

                // Copy data from pointer to array
                Marshal.Copy(Iptr, Pixels, 0, Pixels.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Unlock bitmap data
        /// </summary>
        public void UnlockBits()
        {
            try
            {
                // Copy data from byte array to pointer
                Marshal.Copy(Pixels, 0, Iptr, Pixels.Length);

                // Unlock bitmap data
                source.UnlockBits(bitmapData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Color GetPixel(int x, int y)
        {
            Color clr = Color.Empty;

            // Get color components count
            int cCount = Depth / 8;

            // Get start index of the specified pixel
            int i = ((y * Width) + x) * cCount;

            if (i > Pixels.Length - cCount)
                throw new IndexOutOfRangeException();

            if (Depth == 32) // For 32 bpp get Red, Green, Blue and Alpha
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                byte a = Pixels[i + 3]; // a
                clr = Color.FromArgb(a, r, g, b);
            }
            if (Depth == 24) // For 24 bpp get Red, Green and Blue
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                clr = Color.FromArgb(r, g, b);
            }
            if (Depth == 8)
            // For 8 bpp get color value (Red, Green and Blue values are the same)
            {
                byte c = Pixels[i];
                clr = Color.FromArgb(c, c, c);
            }
            return clr;
        }

        /// <summary>
        /// Set the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public void SetPixel(int x, int y, Color color)
        {
            // Get color components count
            int cCount = Depth / 8;

            // Get start index of the specified pixel
            int i = ((y * Width) + x) * cCount;

            if (Depth == 32) // For 32 bpp set Red, Green, Blue and Alpha
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
                Pixels[i + 3] = color.A;
            }
            if (Depth == 24) // For 24 bpp set Red, Green and Blue
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
            }
            if (Depth == 8)
            // For 8 bpp set color value (Red, Green and Blue values are the same)
            {
                Pixels[i] = color.B;
            }
        }
    }
}
