using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    class Layer
    {
        private List<Neuron> neurons;
       // private List<Sinaps> sinaps;

        //internal List<Sinaps> Sinaps { get => sinaps; set => sinaps = value; }
        internal List<Neuron> Neurons { get => neurons; set => neurons = value; }

        public Layer(int size)
        {
            neurons = new List<Neuron> { new Neuron(size) };
        }

        public void IncW()
        {
            int[] n = { -10, 10 };
            Random random = new Random();
            foreach(Sinaps sinaps in neurons[0].InputSinaps)
            {
                int index = random.Next(0, 2);
                if (sinaps.X == 1) sinaps.W += n[index];
            }
        }

        //public void DecW()
        //{
        //    int[] n = { -10, 10 };
        //    Random random = new Random();
        //    foreach (Sinaps sinaps in neurons[0].InputSinaps)
        //    {
        //        int index = random.Next(0, 2);
        //        if (sinaps.X == 1) sinaps.W += n[index];
        //    }
        //}
    }
}
