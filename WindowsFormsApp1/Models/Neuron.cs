using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    class Neuron
    {
        private int layer;
        private double y_;
        private double sum;
        int limit = 9;
        public List<Sinaps> InputSinaps { get => inputSinaps; set => inputSinaps = value; }
        public List<Sinaps> OutputSinaps { get => outputSinaps; set => outputSinaps = value; }
        double Y_ { get => y_; set => y_ = value; }
        int Layer { get => layer; set => layer = value; }
        public double Sum { get => sum; set => sum = value; }

        private List<Sinaps> inputSinaps;
        private List<Sinaps> outputSinaps;

        public Neuron(int size)
        {
            Random random = new Random();
            inputSinaps = new List<Sinaps>();
            for (int i = 0; i < size; i++)
                inputSinaps.Add(new Sinaps(0));
            outputSinaps = new List<Sinaps>();
            outputSinaps.Add(new Sinaps());
        }

       

        //public bool GetResult(Bitmap bitmap, NetworkNeuron network)
        //{
        //    network.doIt(InitialImage(bitmap));
        //    SetActivation();
        //    return outputSinaps[0].X == 0 ? true : false;
        //}


        double Summator()
        {
            double sum = 0;
            foreach(Sinaps sinaps in inputSinaps)
            {
                sum += sinaps.Transmit();
            }
            return sum;
        }

        double Activation()
        {
            return Summator() >= limit ? 0 : 1;
        }

        public void SetActivation()
        {
            double x;
            x = Activation();
            
            foreach(Sinaps sinaps in outputSinaps)
                sinaps.X = x;
        }

    }
}
