using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;
using System.Configuration;
using System.Collections.Specialized;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        
        NetworkNeuron network = new NetworkNeuron(400);
        string[] lrnItems;
        string resultFilePath;
        string textResult;
        public Form1()
        {
            InitializeComponent();
            string DIRECTORY_NAME = ConfigurationManager.AppSettings.Get("models");
            string LRN_DIRECTORY_NAME = ConfigurationManager.AppSettings.Get("learning");
            resultFilePath = ConfigurationManager.AppSettings.Get("results");
            lrnItems = Directory.GetFiles(LRN_DIRECTORY_NAME);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            pictureBox1.Load(filename);
            Bitmap bitmap = pictureBox1.Image as Bitmap;
            pictureBox1.Image = new Bitmap(bitmap, 20, 20);
            string result;
            if (network.GetResult(bitmap)) result = "Изображен человек ждать";//true
            else result = "Изображен человек идти";//false
            textResult += "" + filename + " : " + result + "\r\n";
            MessageBox.Show(result);
            using(StreamWriter stream = new StreamWriter(resultFilePath,false, System.Text.Encoding.Default))
            {
                stream.WriteLine(textResult);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lrnItems.Count(); i++)
            {
                Bitmap bitmap = new Bitmap(lrnItems[i]);
                bool tildaY = true;
                if (lrnItems[i].Contains("go")) tildaY = false;
                else if (lrnItems[i].Contains("wait")) tildaY = true;
                if (network.GetResult(bitmap) != tildaY)
                {
                    network.Layers[0].IncW();
                    i = -1;
                }
            }
            MessageBox.Show("Сеть успешно обучена");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            pictureBox1.Load(filename);
            Bitmap bitmap = pictureBox1.Image as Bitmap;
            pictureBox1.Image = new Bitmap(bitmap, 20, 20);
            string result;
            if (network.GetResult(bitmap)) result = "Изображен человек ждать";//true
            else result = "Изображен человек идти";//false
            textResult += "" + filename + " : " + result + "\r\n";
            MessageBox.Show(result);
            using (StreamWriter stream = new StreamWriter(resultFilePath, false, System.Text.Encoding.Default))
            {
                stream.WriteLine(textResult);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(ConfigurationManager.AppSettings.Get("results"));
        }
    }
}
