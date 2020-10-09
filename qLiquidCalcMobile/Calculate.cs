using System.Text;
using System;

namespace Core
{
    public static class Calculate
    {
        public static double Calc(double baseVol, double baseConc, double finalConc)
        {
            //Base volume times base concentration
            double res = baseVol * baseConc;
            //over the final desired concentration
            res = res / finalConc;
            res = Math.Round(res, 3);
            return res;
        }
    }
}