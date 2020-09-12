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

     

       public QuadricResults CalculateResults()
       {
            QuadricResults results = new QuadricResults();
           if ((int) b % 2 == 0)
           {
               k = b / 2.0;

               D = Math.Pow(k, 2.0) - a * c;

               results.D = D;

               if (D > 0)
               {
                   double x1 = (-k + Math.Sqrt(D)) / a;
                   double x2 = (-k - Math.Sqrt(D)) / a;

                   results.x1 = x1;
                   results.x2 = x2;
               }
               else if (D == 0)
               {
                   double x = -k / a;
                   results.x1 = x;
               }
               else
               {
                   results.x1 = results.x2 = -1;
               }
           }
           else
           {
               D = Math.Pow(b, 2.0) - 4 * a * c;

               results.D = D;

               if (D > 0)
               {
                   double x1 = (-b + Math.Sqrt(D)) / (2 * a);
                   double x2 = (-b - Math.Sqrt(D)) / (2 * a);

                   results.x1 = x1;
                   results.x2 = x2;
               }
               else if (D == 0)
               {
                   double x = -b / (2 * a);
                   results.x1 = x;
               }
               else
               {
                   results.x1 = results.x2 = -1;
               }
           }
           return results;

       }
   }
}
