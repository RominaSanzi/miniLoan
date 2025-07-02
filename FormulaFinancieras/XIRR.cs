using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FormulaFinanciera
{
   public class xirr
    {
        public const double tol = 0.001;
        public delegate double fx(double x);
        
        public static fx composeFunctions(fx f1, fx f2)
        {
            return (double x) => f1(x) + f2(x);
        }

        public static fx f_xirr(double p, double dt, double dt0)
        {
            return (double x) => p * Math.Pow((1.0 + x), ((dt0 - dt) / 365.0));
        }

        public static fx df_xirr(double p, double dt, double dt0)
        {
            return (double x) => (1.0 / 365.0) * (dt0 - dt) * p * Math.Pow((x + 1.0), (((dt0 - dt) / 365.0) - 1.0));
        }

        public static fx total_f_xirr(double[] payments, double[] days)
        {
            fx resf = (double x) => 0.0;

            for (int i = 0; i < payments.Length; i++)
            {
                resf = composeFunctions(resf, f_xirr(payments[i], days[i], days[0]));
            }

            return resf;
        }

        public static fx total_df_xirr(double[] payments, double[] days)
        {
            fx resf = (double x) => 0.0;

            for (int i = 0; i < payments.Length; i++)
            {
                resf = composeFunctions(resf, df_xirr(payments[i], days[i], days[0]));
            }

            return resf;
        }

        public static double Newtons_method(double guess, fx f, fx df)
        {
            double x0 = guess;
            double x1 = 0.0;
            double err = 1e+100;

            while (err > tol)
            {
                x1 = x0 - f(x0) / df(x0);
                err = Math.Abs(x1 - x0);
                x0 = x1;
            }

            return x0;
        }

        public static void Main(string[] args)
        {
            double[] payments = { -8097.17, 1823.61, 1821.01, 1818.26, 1815.36, 1812.29, 1809.05 }; // payments
            double[] days = { 0, 31, 61, 92, 122, 153, 184 }; // days of payment (as day of year)
            double xirr = Newtons_method(0.1,
                                         total_f_xirr(payments, days),
                                         total_df_xirr(payments, days));

            Console.WriteLine("XIRR value is {0}", xirr);

        }
    }
}
