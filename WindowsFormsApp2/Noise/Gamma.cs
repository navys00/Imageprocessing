using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Noise


{
    class Gamma
    {
        public static Bitmap Execute(Bitmap sourceImage)
        {
            return Additivemodels.CalculateBitmap(sourceImage, GammaNoise(sourceImage.Width * sourceImage.Height));
        }
        public static float[] GammaNoise(float size)
        {
            float a = 2;
            float b = 5;
            var Gamma = new float[256];
            float sum = 0f;

            for (int i = 0; i < 256; i++)
            {
                double step = (float)i * 0.05;
                if (step >= 0)
                {
                    Gamma[i] = ((float)(double)(Math.Exp(-a * step) * (Math.Pow(a, b) * Math.Pow(step, b - 1)) / factorial(b - 1)));
                }
                else
                {
                    Gamma[i] = 0;
                }
                sum += Gamma[i];
            }

            for (int i = 0; i < 256; i++)
            {
                Gamma[i] /= sum;
                Gamma[i] *= size;
                Gamma[i] = (int)Math.Floor(Gamma[i]);
            }

            return Gamma;
        }
       public static double factorial(double n)
        {
            if (n == 1) return 1;

            return n * factorial(n - 1);
        }
    }
}
