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
        string imgName;
        string resultFilePath;
        string textResult;
        public Form1()
        {
            InitializeComponent();
            string DIRECTORY_NAME = ConfigurationManager.AppSettings.Get("models");
            string LRN_DIRECTORY_NAME = ConfigurationManager.AppSettings.Get("learning");
            resultFilePath = ConfigurationManager.AppSettings.Get("results");
           // listBox1.Items.AddRange(Directory.GetFiles(DIRECTORY_NAME));
            //listBox1.SelectedIndex = 0;
            dataGridView1.ColumnCount = 1;
            lrnItems = Directory.GetFiles(LRN_DIRECTORY_NAME);
            dataGridView1.RowCount = lrnItems.Length;
            CheckBox[]checkBoxes = new CheckBox[lrnItems.Length];
            DataGridViewCheckBoxColumn dgvCmb = new DataGridViewCheckBoxColumn();
            dgvCmb.ValueType = typeof(bool);
            dgvCmb.Name = "Chk";
            dgvCmb.HeaderText = @"Проверка";
            dataGridView1.Columns.Add(dgvCmb);

            for (var i = 0; i < lrnItems.Length; i++)
            {
                dataGridView1[0, i].Value = lrnItems[i];
                dataGridView1[1, i].Value = CheckState.Checked;
            }
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            //listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
           // pictureBox1.Load(listBox1.SelectedItem.ToString());
           // imgName = listBox1.SelectedItem.ToString();
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var sel = dataGridView1.SelectedCells;
            pictureBox1.Load(dataGridView1[0, sel[0].RowIndex].Value.ToString());
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
                if (network.GetResult(bitmap) != tildaY)
                {
                    network.Layers[0].IncW();
                    i = -1;
                }
            }

            //for (int i = 0; i < lrnItems.Count(); i++)
            //{
            //    pictureBox1.Load(dataGridView1[0, i].Value.ToString());
            //    Bitmap bitmap = pictureBox1.Image as Bitmap;
            //    bool tildaY = true;
            //    if (dataGridView1[1, i].Value.Equals(CheckState.Checked)) tildaY = false;
            //    if (network.GetResult(bitmap) != tildaY)
            //    {
            //        network.Layers[0].IncW();
            //        i = -1;
            //    }
            //}
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

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    int[,] f;


        //    openFileDialog1.Title = "Укажите вайл весов";
        //    openFileDialog1.ShowDialog();
        //    string fileName = openFileDialog1.FileName;
        //    StreamReader stream = File.OpenText(fileName);
        //    string line;
        //    string[] s1;
        //    int k = 0;
        //    while ((line = stream.ReadLine()) != null)
        //    {
        //        s1 = line.Split(' ');
        //        for(int i = 0; i < s1.Length; i++)
        //        {
        //            listBox2.Items.Add("");
        //            if (k < 16)
        //            {
        //                //network.doIt(s1[i]);
        //            }
        //        }
        //    }
        //}
    }
}
