using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quadric_Equatation
{
   public class QuadricEquatation
   {
       private double a, b, c; // Coeficients of equatation;
       private double k; // for even second coef
       private double D; // Discriminate

       public QuadricEquatation(double _a, double _b, double _c)
       {
            a = _a;
           b = _b;
            c = _c;
       }

     

       public Tuple<double,double, double> CalculateResults()
       {
        
           if ((int) b % 2 == 0)
           {
               k = b / 2.0;

               D = Math.Pow(k, 2.0) - a * c;

           

               if (D > 0)
               {
                   double x1 = (-k + Math.Sqrt(D)) / a;
                   double x2 = (-k - Math.Sqrt(D)) / a;

                   return Tuple.Create(D, x1, x2);
               }
               else if (D == 0)
               {
                   double x = -k / a;

                   double x1, x2;

                   x1 = x2 = x;

                   return Tuple.Create(D, x1, x2);

               }
               else
               {
                   return Tuple.Create(D, double.NegativeInfinity, double.NegativeInfinity);
               }
           }
           else
           {
               D = Math.Pow(b, 2.0) - 4 * a * c;

             

               if (D > 0)
               {
                   double x1 = (-b + Math.Sqrt(D)) / (2 * a);
                   double x2 = (-b - Math.Sqrt(D)) / (2 * a);

                   return Tuple.Create(D, x1, x2);

               }
               else if (D == 0)
               {
                   double x = -b / (2 * a);

                   double x1, x2;

                   x1 = x2 = x;

                   return Tuple.Create(D, x1, x2);

                }
               else
               {
                   return Tuple.Create(D, double.NegativeInfinity, double.NegativeInfinity);
                }
           }
          

       }
   }
}
