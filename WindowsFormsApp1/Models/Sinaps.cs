using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    class Sinaps
    {
        private double x;
        private double w;

        public double X { get => x; set => x = value; }
        public double W { get => w; set => w = value; }

        public Sinaps(double x)
        {
            this.x = x;
        }

        public Sinaps(double x, double w)
        {
            this.x = x;
            this.w = w;
        }

        public Sinaps() { }

        public double Transmit()
        {
            return w * x;
        }
    }
}
