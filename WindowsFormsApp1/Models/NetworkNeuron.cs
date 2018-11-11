using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    class NetworkNeuron
    {
        private List<Layer> layers;

        internal List<Layer> Layers { get => layers; set => layers = value; }

        public NetworkNeuron(int size)
        {
            layers = new List<Layer>();
            layers.Add(new Layer(size));
        }

        public void doIt(double[] x)
        {
            for (int i = 0; i < layers.Count; i++)
                for (int j = 0; j < layers[i].Neurons.Count; j++)
                    for (int k = 0; k < x.Length; k++)
                        layers[i].Neurons[j].InputSinaps[k].X = x[k];
        }

        public double[] InitialImage(Bitmap bitmap)
        {
            double[] X = new double[400];
            Bitmap im = new Bitmap(bitmap, 20, 20);
            List<int> vs = new List<int>();
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    int n = im.GetPixel(x, y).R;
                    if (n <= 5) { n = 0; vs.Add(n); }
                    else { n = 1; vs.Add(n); }
                }
            }
            for (int i = 0; i < X.Length; i++)
                X[i] = vs[i];
            return X;
        }

        public bool GetResult(Bitmap bitmap)
        {
            doIt(InitialImage(bitmap));
            layers[0].Neurons[0].SetActivation();
            return layers[0].Neurons[0].OutputSinaps[0].X == 0 ? true : false;
        }
    }
}
