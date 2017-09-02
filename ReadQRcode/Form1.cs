using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using BarcodeLib.BarcodeReader;
using System.Data.OleDb;
using System.IO.Ports;

namespace ReadQRcode
{
    public partial class Form1 : Form
    {
        FilterInfoCollection ball;
        VideoCaptureDevice fuenteVideo;
        public Form1()
        {
            
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ball = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo x in ball)
            {
                comboBox1.Items.Add(x.Name);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
            fuenteVideo = new VideoCaptureDevice(ball[comboBox1.SelectedIndex].MonikerString);
            videoSourcePlayer1.VideoSource = fuenteVideo;
            videoSourcePlayer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (fuenteVideo.IsRunning)
            {

                fuenteVideo.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (videoSourcePlayer1.GetCurrentVideoFrame() != null)
            {

                Bitmap img = new Bitmap(videoSourcePlayer1.GetCurrentVideoFrame());
                string[] resultados = BarcodeReader.read(img, BarcodeReader.QRCODE);
                img.Dispose();
                if (resultados != null)
                {

                    listBox1.Items.Add(resultados[0]);
                }
            }
        }

        private void videoSourcePlayer1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    //}
}
