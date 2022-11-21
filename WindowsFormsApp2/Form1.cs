using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        Bitmap image;
        Bitmap secondimage;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image file | *.png; *.jpg; *.bmp | All files (*.*)| *.* ";
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
               
            }
        }

        

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (pictureBox1.Image != null)
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Title = "save file as...";
                saveFile.OverwritePrompt = true;
              
                saveFile.CheckPathExists = true;
                saveFile.Filter = "Image file | *.png; *.jpg; *.bmp;*.jpeg | All files (*.*)| *.* ";
                if(saveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox1.Image.Save(saveFile.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }




        }

        private void оттенкиСерогоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new Filter();
            Bitmap resultImage = filter.Execute(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }


        private void медианныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new Filter();
            Bitmap resultImage = filter.Execute12(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }

        private void autocontrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new Filter();
            Bitmap resultImage = filter.Execute8(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }
   
        private void оттенкиСерогоToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Filter filter = new Filter();
            Bitmap resultImage = filter.ottenki_serogo(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }

        private void thresholdToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Filter filter = new Filter();
            Bitmap resultImage = filter.Threshold(image, 150);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }

        private void ниблэкToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new Filter();
            Bitmap resultImage = filter.Niblack(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }

        private void гистограммаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new Filter();
            Bitmap resultImage = filter.HistogramExe(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }

        private void гаммаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new Filter();
            Bitmap resultImage = filter.ExecuteHamma(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }

        private void равномерныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new Filter();
            Bitmap resultImage = filter.ExecuteUniform(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }

        private void билатериальныйШутерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new Filter();
            Bitmap resultImage = filter.Gaussexecute(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }

        private void билетариальныйФильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new Filter();
            Bitmap resultImage = filter.BilaterialExecute(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }

        private void sSIMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new Filter();
            MessageBox.Show(filter.ExecuteSSIM((Bitmap)pictureBox1.Image, image).ToString());
            //pictureBox1.Image = resultImage;
            //pictureBox1.Refresh();
        }

        private void pSNRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new Filter();
            MessageBox.Show(filter.ExecuteSSIM((Bitmap)pictureBox1.Image, image).ToString());
        }

        private void гистограммаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (image == null) return;
            Filter filter = new Filter();
            Hist frame = new Hist(CalculateHistogram((Bitmap)pictureBox1.Image));
            frame.Show();
        }
        public int[] CalculateHistogram(Bitmap source)
        {
            int[] histogram = new int[256];
            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    Color color = source.GetPixel(x, y);
                    histogram[color.R]++;
                }
            }


            return histogram;
        }
    }
}

//namespace WindowsFormsApp2
//{
//    public partial class Form1 : Form
//    {
//        Bitmap image;
//        public Form1()
//        {
//            InitializeComponent();
//        }

//        private void Form1_Load(object sender, EventArgs e)
//        {

//        }

//        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            OpenFileDialog dialog = new OpenFileDialog();
//            dialog.Filter = "Image file | *.png; *.jpg; *.bmp | All files (*.*)| *.* ";
//            if (dialog.ShowDialog() == DialogResult.OK)
//            {
//                image = new Bitmap(dialog.FileName);
//                pictureBox1.Image = image;
//                pictureBox1.Refresh();

//            }
//        }

//        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            Filter filter = new Filter();
//            Bitmap resultImage = filter.Execute(image);
//            pictureBox1.Image = resultImage;
//            pictureBox1.Refresh();
//        }

//        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
//        {

//        }

//        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
//        {

//            if (pictureBox1.Image != null)
//            {
//                SaveFileDialog saveFile = new SaveFileDialog();
//                saveFile.Title = "save file as...";
//                saveFile.OverwritePrompt = true;

//                saveFile.CheckPathExists = true;
//                saveFile.Filter = "Image file | *.png; *.jpg; *.bmp | All files (*.*)| *.* ";
//                if (saveFile.ShowDialog() == DialogResult.OK)
//                {
//                    try
//                    {
//                        pictureBox1.Image.Save(saveFile.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
//                    }
//                    catch
//                    {
//                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
//            MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                }
//            }




//        }

//        private void оттенкиСерогоToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            Filter filter = new Filter();
//            Bitmap resultImage = filter.Execute(image);
//            pictureBox1.Image = resultImage;
//            pictureBox1.Refresh();
//        }

//        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            Filter filter = new Filter();
//            Bitmap resultImage = filter.Execute2(image);
//            pictureBox1.Image = resultImage;
//            pictureBox1.Refresh();
//        }

//        private void увеличениеЯркостиToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            Filter filter = new Filter();
//            Bitmap resultImage = filter.Execute3(image);
//            pictureBox1.Image = resultImage;
//            pictureBox1.Refresh();
//        }

//        private void сдвигВправоНа50PixToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            Filter filter = new Filter();
//            Bitmap resultImage = filter.Execute4(image);
//            pictureBox1.Image = resultImage;
//            pictureBox1.Refresh();
//        }

//        private void тиснениеToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            Filter filter = new Filter();

//            Bitmap resultImage = filter.Execute5(image);
//            resultImage = filter.Execute(resultImage);
//            resultImage = filter.Execute3(resultImage);

//            pictureBox1.Image = resultImage;
//            pictureBox1.Refresh();
//        }

//        private void motionblurToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            Filter filter = new Filter();
//            Bitmap resultImage = filter.Execute6(image);
//            pictureBox1.Image = resultImage;
//            pictureBox1.Refresh();
//        }

//        private void серыйМирToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            Filter filter = new Filter();
//            Bitmap resultImage = filter.Execute7(image);
//            pictureBox1.Image = resultImage;
//            pictureBox1.Refresh();
//        }

//        private void растяжениеToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            Filter filter = new Filter();
//            Bitmap resultImage = filter.Execute8(image);
//            pictureBox1.Image = resultImage;
//            pictureBox1.Refresh();
//        }

//        private void иДЕАЛЬНЫЙОТРАЖАТЕЛЬToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            Filter filter = new Filter();
//            Bitmap resultImage = filter.Execute9(image);
//            pictureBox1.Image = resultImage;
//            pictureBox1.Refresh();
//        }

//        private void расширениеToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            Filter filter = new Filter();
//            Bitmap resultImage = filter.Execute10(image);
//            pictureBox1.Image = resultImage;
//            pictureBox1.Refresh();
//        }

//        private void сужениеToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            Filter filter = new Filter();
//            Bitmap resultImage = filter.Execute11(image);
//            pictureBox1.Image = resultImage;
//            pictureBox1.Refresh();
//        }

//        private void медианныйToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            Filter filter = new Filter();
//            Bitmap resultImage = filter.Execute12(image);
//            pictureBox1.Image = resultImage;
//            pictureBox1.Refresh();
//        }

//        private void собеляToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            Filter filter = new Filter();
//            Bitmap resultImage = filter.Execute13(image);
//            pictureBox1.Image = resultImage;
//            pictureBox1.Refresh();
//        }

//        private void щарраToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            Filter filter = new Filter();
//            Bitmap resultImage = filter.Execute15(image);
//            pictureBox1.Image = resultImage;
//            pictureBox1.Refresh();
//        }
//    }
//}