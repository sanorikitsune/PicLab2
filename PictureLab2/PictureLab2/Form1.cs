using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureLab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public Image Img { get; set; }

        static byte[] Picture;

        void DrawGist()
        {
            int[] Gist = new int[256];

            int C;

            for (int i = 0; i<Picture.Length; i+=3)
            {
                C = (Picture[i] + Picture[i+1] + Picture[i+2]) / 3;
                Gist[C]++;
            }

            int[] GistM=new int[256];
            Gist.CopyTo(GistM,0);

            int maxValue = GistM.Max<int>();
            int height = 184;
            double k = Convert.ToDouble(height) / maxValue;


            var GistPic = new Bitmap(256, height);
           /* using (Graphics g = Graphics.FromImage(GistPic))
            {
                Pen blackPen = new Pen(Color.Black, 1);
                var whiteBrush = Brushes.White;
                g.FillRectangle(whiteBrush, 0, 0, 256, height);

                for (int i=0; i<256;i++)
                {
                    Point A = new Point(i, height-1);
                    Point B = new Point(i, height - 1 - Convert.ToInt32(Gist[i]*k));
                    g.DrawLine(blackPen, A, B);
                }
                GistPic.Save("1.jpg");
            }*/
            BitImg.writeImageBytes(GistPic, Picture);
            pictureBox2.Image = GistPic;
            


        }







        private void label1_Click(object sender, EventArgs e)
        {
            pictureBox1_Click(sender, e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "\\";
                openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    Img = Image.FromFile(openFileDialog.FileName);
                    pictureBox1.Image = Img;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                    Bitmap bmp = new Bitmap(Img);
                    
                    Picture = BitImg.getImgBytes(bmp);
                    label1.Visible = false;
                    DrawGist();


                }
            }
        }

     
    }
}
