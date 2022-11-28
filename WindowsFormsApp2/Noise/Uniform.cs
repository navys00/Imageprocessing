using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Noise
{
   internal abstract class Uniform
    {

        public static Bitmap Execute(Bitmap sourceImage)
        {
            return Additivemodels.CalculateBitmap(sourceImage, MakeUniform(sourceImage.Width * sourceImage.Height));
        }
        public static float[] MakeUniform(int size)
        {
            int a = 32;
            int b = 120;
            float sum = 0f;
            float[] uniform = new float[256];
            for (int i = 0; i < 256; i++)
            {

                int step = i;
                if (step >= a && step <= b)
                {
                    uniform[i] = (1 / ((float)(b - a)));
                }
                else
                {
                    uniform[i] = 0;
                }
                sum += uniform[i];
            }

            for (int i = 0; i < 256; i++)
            {
                uniform[i] /= sum;
                uniform[i] *= size;
                uniform[i] = (int)Math.Floor(uniform[i]);
            }
            return uniform;

        }
    }
}
