using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quadric_Equatation
{
    public struct QuadricResults
    {
        public double x1,x2;
        public double D;

        public QuadricResults(double _x1, double _x2, double _D)
        {
            x1 = _x1;
            x2 = _x1;
            D = _D;
        }
    }
}
