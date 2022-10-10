using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Runtime.InteropServices;

namespace WindowsFormsApp2
{
    public class Filter
    {
        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
            {
                return min;
            }
            if (value > max)
            {
                return max;
            }
            return value;
        }
        public static byte clamp(float value, float min, float max)
        {
            return (byte)(Math.Min(Math.Max(min, value), max));
        }

        public static int clamp(int value, int min, int max)
        {
            return (int)(Math.Min(Math.Max(min, value), max));
        }
        protected int Calculatenewpixelcolor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            int intensity = (sourceColor.R + sourceColor.G + sourceColor.B) / 3;
            return intensity;




        }
        public Bitmap Execute8(Bitmap source)//autocontrast
        {

            Bitmap resultimage = new Bitmap(source.Width, source.Height);
            double _R = 0;
            double _G = 0;
            double _B = 0;
            double _Rmax = 0;
            double _Gmax = 0;
            double _Bmax = 0;
            double _Rmin = 255;
            double _Gmin = 255;
            double _Bmin = 255;

            for (int i = 0; i < source.Width; i++)
            {
                for (int j = 0; j < source.Height; j++)
                {

                    Color color = source.GetPixel(i, j);

                    if (_Rmax < color.R)
                    {
                        _Rmax = color.R;
                    }
                    if (_Rmin > color.R)
                    {
                        _Rmin = color.R;
                    }
                    if (_Gmax < color.G)
                    {
                        _Gmax = color.G;
                    }
                    if (_Gmin > color.G)
                    {
                        _Gmin = color.G;
                    }
                    if (_Bmax < color.B)
                    {
                        _Bmax = color.B;
                    }
                    if (_Bmin > color.B)
                    {
                        _Bmin = color.B;
                    }
                    if (_Rmin < 0)
                    {
                        _Rmin = 0;
                    }
                    if (_Rmax > 255)
                    {
                        _Rmax = 255;
                    }
                    if (_Gmin < 0)
                    {
                        _Gmin = 0;
                    }
                    if (_Gmax > 255)
                    {
                        _Gmax = 255;
                    }
                    if (_Bmin < 0)
                    {
                        _Bmin = 0;
                    }
                    if (_Bmax > 255)
                    {
                        _Bmax = 255;
                    }
                }
            }
            for (int i = 0; i < source.Width; i++)
            {
                for (int j = 0; j < source.Height; j++)
                {
                    Color color = source.GetPixel(i, j);
                    _R = color.R;
                    _G = color.G;
                    _B = color.B;
                    double resR = (_R - _Rmin) * ((255 - 0) / (_Rmax - _Rmin));
                    double resG = (_G - _Gmin) * ((255 - 0) / (_Gmax - _Gmin));
                    double resB = (_B - _Bmin) * ((255 - 0) / (_Bmax - _Bmin));

                    resultimage.SetPixel(i, j, Color.FromArgb((int)resR, (int)resG, (int)resB));
                }
            }

            return resultimage;

        }
        public Bitmap Execute(Bitmap source)//grayscale
        {
            Bitmap resultImage = new Bitmap(source.Width, source.Height);
            for (int i = 0; i < source.Width; i++)
            {
                for (int j = 0; j < source.Height; j++)
                {
                    Color c = source.GetPixel(i, j);
                    int R = c.R;
                    int G = c.G;
                    int B = c.B;
                    float intensity = 0.299f * R + 0.587f * G + 0.114f * B;
                    resultImage.SetPixel(i, j, Color.FromArgb((int)intensity, (int)intensity, (int)intensity));
                }
            }
            return resultImage;

        }
        
        public static Color calculeteNewPixelColor4(Bitmap source, int x, int y)//average
        {
            int ct = 0;
            int radiusX = 1;
            int radiusY = 1;
            float resultR = 0f;
            float resultG = 0f;
            float resultB = 0f;
            byte[] R = new byte[9];
            byte[] G = new byte[9];
            byte[] B = new byte[9];
            int med = (int)(9f / 2);
            for (int l=-1;l<=1;l++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    int idx = Clamp(x + k, 0, source.Width - 1); 
                    int idy = Clamp(y + l, 0, source.Height - 1);
                    Color neyghbour = source.GetPixel(idx,idy);
                    R[ct] = neyghbour.R;
                    G[ct] = neyghbour.G;
                    B[ct] = neyghbour.B;
                    ct++;

                }
            }
           
            resultR = (byte)(Math.Round((decimal)(R.Select(el => (int)el).Sum()) / R.Length));
            resultG = (byte)(Math.Round((decimal)(G.Select(el => (int)el).Sum()) / G.Length));
            resultB = (byte)(Math.Round((decimal)(B.Select(el => (int)el).Sum()) / B.Length));


            return Color.FromArgb((int)resultR, (int)resultG, (int)resultB);


        }
        public Bitmap Execute12(Bitmap source)//average
        {
            Bitmap resultImage = new Bitmap(source.Width, source.Height);


            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    Color color = calculeteNewPixelColor4(source, x, y);
                    resultImage.SetPixel(x, y, color);
                }
            }
            return resultImage;

        }
        public Bitmap Niblack(Bitmap source)//бинаризация Ниблэка
        {
            Bitmap resultimage = new Bitmap(source.Width, source.Height);
            for(int x = 0; x < source.Width; x++)
            {
                for(int y = 0; y < source.Height; y++)
                {
                    Color color = NiblackPixelColor(source, x, y);
                    resultimage.SetPixel(x, y, color);
                    

                }
            }

            return resultimage;
        }
        public Color NiblackPixelColor(Bitmap source, int x,int y)
        {
            float K = -0.2f; //пользовательский параметр
            int radius = 1;
            int W = (1 + 2 * radius) * (1 + 2 * radius);//размер матрицы w^2
            int m;//среднее значение серого
            int s;//стандартное отклонение 
            int brightsum = 0;//сумма яркостей
            int T; //порог
            for(int i = -radius; i <= radius; i++)
            {
                for(int j = -radius; j <= radius; j++)
                {
                    int idX = Clamp(x + i, 0, source.Width-1);
                    int idY= Clamp(y + j, 0, source.Height-1);
                    Color color1 = source.GetPixel(idX, idY);
                    brightsum += (int)color1.R;
                    
                }
            }
            m = (int)(brightsum / W);
            brightsum = 0;
            for (int i = -radius; i <= radius; i++)
            {
                for (int j = -radius; j <= radius; j++)
                {
                    int idX = Clamp(x + i, 0, source.Width - 1);
                    int idY = Clamp(y + j, 0, source.Height - 1);
                    Color color2 = source.GetPixel(idX, idY);
                    brightsum += (int)Math.Pow(color2.R - m, 2);

                }
            }
            s = (int)Math.Sqrt(brightsum / W);
            brightsum = 0;

            T = (int)(m + s * K);

            Color color = source.GetPixel(x, y);
            if (color.R >= T)
            {
                return Color.FromArgb((int)255, (int)255, (int)255);

            }
            else
            {
                return Color.FromArgb((int)0, (int)0, (int)0);
            }
        }
        public Bitmap Threshold(Bitmap source, int value)//установка порога вручную
        {
            Bitmap resultimage = new Bitmap(source.Width, source.Height);
            resultimage = source;
            for (int x = 0; x < source.Width; x++)
            {
                for(int y = 0; y < source.Height; y++)
                {
                    Color color = resultimage.GetPixel(x, y);
                    Color rescolor = Color.FromArgb((int)255, (int)255, (int)255);
                    Color rescolor2 = Color.FromArgb((int)0, (int)0, (int)0);
                    if (color.R >= value)
                    {
                        
                        resultimage.SetPixel(x, y, rescolor);
                    }
                    else
                    {
                        resultimage.SetPixel(x, y, rescolor2);
                    }

                }
            }
            return resultimage;
        }
        
        public Bitmap ottenki_serogo(Bitmap source)//оттенки серого
        {
            Bitmap resultImage = new Bitmap(source.Width, source.Height);
            for (int i = 0; i < source.Width; i++)
            {
                for (int j = 0; j < source.Height; j++)
                {
                    Color c = source.GetPixel(i, j);
                    int R = c.R;
                    int G = c.G;
                    int B = c.B;
                    float intensity = 0.299f * R + 0.587f * G + 0.114f * B;//интенсивность, напряженность
                    resultImage.SetPixel(i, j, Color.FromArgb((int)intensity, (int)intensity, (int)intensity));
                }
            }
            return resultImage;

        }
        public static Color calculeteNewPixelColorM(Bitmap source, int x, int y)//для медианного
        {
            int ct = 0;
            int radiusX = 1;
            int radiusY = 1;
            float resultR = 0f;
            float resultG = 0f;
            float resultB = 0f;
            byte[] R = new byte[9];
            byte[] G = new byte[9];
            byte[] B = new byte[9];
            int med = (int)(9f / 2);
            for (int l = -1; l <= 1; l++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    int idx = Clamp(x + k, 0, source.Width - 1);
                    int idy = Clamp(y + l, 0, source.Height - 1);
                    Color neyghbour = source.GetPixel(idx, idy);
                    R[ct] = neyghbour.R;
                    G[ct] = neyghbour.G;
                    B[ct] = neyghbour.B;
                    ct++;

                }
            }
            Array.Sort(R);
            Array.Sort(G);
            Array.Sort(B);
            resultR = R[med];
            resultG = G[med];
            resultB = B[med];


            return Color.FromArgb((int)resultR, (int)resultG, (int)resultB);


        }
        public Bitmap Execute12M(Bitmap source)//медиана
        {
            Bitmap resultImage = new Bitmap(source.Width, source.Height);


            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    Color color = calculeteNewPixelColor4(source, x, y);
                    resultImage.SetPixel(x, y, color);
                }
            }
            return resultImage;

        }
        public int[] CalculateHistogram(Bitmap image)
        {
            int[] histogram = new int[256];
            for(int x = 0; x < image.Width; x++)
            {
                for(int y = 0; y < image.Height; y++)
                {
                    Color color = image.GetPixel(x, y);
                    histogram[color.R]++;
                }
            }


            return histogram;
        }
    }
}


//namespace WindowsFormsApp2
//{
//  public class Filter
//  {
//   public static int Clamp(int value, int min, int max)
//  {
//if (value < min)
//{
//  return min;
//}
//if (value > max)
// {
//  return max;
//}
//  return value;
//}
//  public static byte clamp(float value, float min, float max)
//    {
//          return (byte)(Math.Min(Math.Max(min, value), max));
//        }

//public static int clamp(int value, int min, int max)
//{
//    return (int)(Math.Min(Math.Max(min, value), max));
//}
//protected int Calculatenewpixelcolor(Bitmap sourceImage, int x, int y)
//{
//    Color sourceColor = sourceImage.GetPixel(x, y);
//    int intensity = (sourceColor.R + sourceColor.G + sourceColor.B) / 3;
//    return intensity;




//}

//public Bitmap Execute2(Bitmap source)//сепия
//{
//    int k = 20;

//    Bitmap resultImage = new Bitmap(source.Width, source.Height);
//    for (int i = 0; i < source.Width; i++)
//    {
//        for (int j = 0; j < source.Height; j++)
//        {
//            Color c = source.GetPixel(i, j);


//            float A = c.A;
//            float R = c.R;
//            float G = c.G;
//            float B = c.B;
//            float intensity = 0.299f * R + 0.587f * G + 0.114f * B;
//            R = intensity + 2 * k;
//            G = intensity + 0.5f * k;
//            B = intensity - k;
//            if (R > 255)
//            {
//                R = 255;
//            }
//            if (B < 0)
//            {
//                B = 0;
//            }
//            if (G > 255)
//            {
//                G = 255;
//            }
//            resultImage.SetPixel(i, j, Color.FromArgb((int)A, (int)R, (int)G, (int)B));

//        }
//    }
//    return resultImage;

//}
//public Bitmap Execute3(Bitmap source)//яркость_изображения
//{
//    int k = 100;
//    Bitmap resultImage = new Bitmap(source.Width, source.Height);
//    for (int i = 0; i < source.Width; i++)
//    {
//        for (int j = 0; j < source.Height; j++)
//        {
//            Color c = source.GetPixel(i, j);
//            float A = c.A;
//            int R = c.R;
//            int G = c.G;
//            int B = c.B;
//            R = R + k;
//            G = G + k;
//            B = B + k;
//            if (R > 255)
//            {
//                R = 255;
//            }
//            if (B < 0)
//            {
//                B = 0;
//            }
//            if (B > 255)
//            {
//                B = 255;
//            }
//            if (G > 255)
//            {
//                G = 255;
//            }
//            resultImage.SetPixel(i, j, Color.FromArgb((int)R, (int)G, (int)B));
//        }
//    }
//    return resultImage;

//}
//public Bitmap Execute4(Bitmap source)//сдвиг на 50 пикселей вправо
//{


//    Bitmap resultImage = new Bitmap(source.Width, source.Height);
//    for (int i = 0; i < 50; i++)
//    {
//        for (int j = 0; j < source.Height; j++)
//        {
//            Color c = source.GetPixel(i, j);

//            resultImage.SetPixel(i, j, Color.FromArgb(0, 0, 0));
//        }
//    }
//    for (int i = 50; i < source.Width; i++)
//    {
//        for (int j = 0; j < source.Height; j++)
//        {

//            Color c = source.GetPixel(i - 50, j);
//            int R = c.R;
//            int G = c.G;
//            int B = c.B;
//            resultImage.SetPixel(i, j, Color.FromArgb(R, G, B));
//        }
//    }
//    return resultImage;

//}
//public static Color calculeteNewPixelColor(Bitmap source, int x, int y)//для тиснения
//{
//    float[,] _kernel = new float[,]{{0,1,0 },
//                       { -1,0,1},
//                        {0,-1,0 } };
//    int radiusX = _kernel.GetLength(0) / 2;
//    int radiusY = _kernel.GetLength(1) / 2;
//    float resultR = 0f;
//    float resultG = 0f;
//    float resultB = 0f;
//    for (int l = -radiusY; l <= radiusY; l++)
//    {
//        for (int k = -radiusX; k <= radiusX; k++)
//        {
//            int idX = clamp(x + k, 0, source.Width - 1);
//            int idY = clamp(y + l, 0, source.Height - 1);
//            Color neighbourColor = source.GetPixel(idX, idY);
//            resultR += neighbourColor.R * _kernel[k + radiusX, l + radiusY];
//            resultB += neighbourColor.B * _kernel[k + radiusX, l + radiusY];
//            resultG += neighbourColor.G * _kernel[k + radiusX, l + radiusY];


//        }
//    }
//    return Color.FromArgb(clamp((int)resultR, 0, 255), clamp((int)resultG, 0, 255), clamp((int)resultB, 0, 255));


//}
//public Bitmap Execute5(Bitmap source)//тиснение
//{
//    Filter filter = new Filter();
//    Bitmap resultImage = new Bitmap(source.Width, source.Height);


//    for (int y = 0; y < source.Height; y++)
//    {
//        for (int x = 0; x < source.Width; x++)
//        {
//            Color color = calculeteNewPixelColor(source, x, y);
//            resultImage.SetPixel(x, y, color);
//        }
//    }
//    // resultImage = filter.Execute(resultImage);
//    // resultImage = filter.Execute3(resultImage);
//    return resultImage;

//}
//public static Color calculeteNewPixelColor2(Bitmap source, int x, int y)//для размытия
//{
//    //float[,] _kernel = new float[,] { {1/9f ,1/9f, 1/9f },
//    //                                  { 1/9f ,1/9f, 1/9f},
//    //                                  {1/9f ,1/9f, 1/9f }};
//    float[,] _kernel = new float[,]{{1/9f,0,0,0,0,0,0,0,0 },
//                                    {0,1/9f,0,0,0,0,0,0,0 },
//                                    {0,0,1/9f,0,0,0,0,0,0 },
//                                    {0,0,0,1/9f,0,0,0,0,0 },
//                                    {0,0,0,0,1/9f,0,0,0,0 },
//                                    {0,0,0,0,0,1/9f,0,0,0 },
//                                    {0,0,0,0,0,0,1/9f,0,0 },
//                                    {0,0,0,0,0,0,0,1/9f,0 },
//                                    {0,0,0,0,0,0,0,0,1/9f }};
//    int radiusX = _kernel.GetLength(0) / 2;
//    int radiusY = _kernel.GetLength(1) / 2;
//    float resultR = 0f;
//    float resultG = 0f;
//    float resultB = 0f;
//    for (int l = -radiusY; l <= radiusY; l++)
//    {
//        for (int k = -radiusX; k <= radiusX; k++)
//        {
//            int idX = Clamp(x + k, 0, source.Width - 1);
//            int idY = Clamp(y + l, 0, source.Height - 1);
//            Color neighbourColor = source.GetPixel(idX, idY);
//            resultR += neighbourColor.R * _kernel[k + radiusX, l + radiusY];
//            resultB += neighbourColor.B * _kernel[k + radiusX, l + radiusY];
//            resultG += neighbourColor.G * _kernel[k + radiusX, l + radiusY];


//        }
//    }
//    return Color.FromArgb(Clamp((int)resultR, 0, 255), Clamp((int)resultG, 0, 255), Clamp((int)resultB, 0, 255));


//}
//public Bitmap Execute6(Bitmap source)//размытие в движении
//{
//    //Filter filter = new Filter();
//    Bitmap resultImage = new Bitmap(source.Width, source.Height);


//    for (int y = 0; y < source.Height; y++)
//    {
//        for (int x = 0; x < source.Width; x++)
//        {
//            Color color = calculeteNewPixelColor2(source, x, y);
//            resultImage.SetPixel(x, y, color);
//        }
//    }
//    return resultImage;

//}
//public Bitmap Execute7(Bitmap source)//серый мир
//{

//    Bitmap resultImage = new Bitmap(source.Width, source.Height);

//    double _R = 0;
//    double _G = 0;
//    double _B = 0;
//    double _RR = 0;
//    double _GG = 0;
//    double _BB = 0;
//    int N = 0;
//    for (int i = 0; i < source.Height; i++)
//    {
//        for (int j = 0; j < source.Width; j++)
//        {
//            N = source.Width * source.Height;
//            Color color = source.GetPixel(j, i);
//            _R += color.R;
//            _G += color.G;
//            _B += color.B;

//        }
//    }
//    _R /= N;
//    _G /= N;
//    _B /= N;

//    double avg = ((_R + _G + _B) / 3);
//    for (int i = 0; i < source.Height; i++)
//    {
//        for (int j = 0; j < source.Width; j++)
//        {

//            Color color2 = source.GetPixel(j, i);
//            _RR = avg / _R * color2.R;
//            _GG = avg / _G * color2.G;
//            _BB = avg / _B * color2.B;
//            if (_RR > 255)
//            {
//                _RR = 255;
//            }
//            if (_GG > 255)
//            {
//                _GG = 255;
//            }
//            if (_BB > 255)
//            {
//                _BB = 255;
//            }
//            if (_RR < 0)
//            {
//                _RR = 0;
//            }
//            if (_GG < 0)
//            {
//                _GG = 0;
//            }
//            if (_BB < 0)
//            {
//                _BB = 0;
//            }

//            resultImage.SetPixel(j, i, Color.FromArgb((int)_RR, (int)_GG, (int)_BB));

//        }
//    }
//    return resultImage;

//}
//public Bitmap Execute8(Bitmap source)//autolevels
//{

//    Bitmap resultimage = new Bitmap(source.Width, source.Height);
//    double _R = 0;
//    double _G = 0;
//    double _B = 0;
//    double _Rmax = 0;
//    double _Gmax = 0;
//    double _Bmax = 0;
//    double _Rmin = 255;
//    double _Gmin = 255;
//    double _Bmin = 255;

//    for (int i = 0; i < source.Width; i++)
//    {
//        for (int j = 0; j < source.Height; j++)
//        {

//            Color color = source.GetPixel(i, j);

//            if (_Rmax < color.R)
//            {
//                _Rmax = color.R;
//            }
//            if (_Rmin > color.R)
//            {
//                _Rmin = color.R;
//            }
//            if (_Gmax < color.G)
//            {
//                _Gmax = color.G;
//            }
//            if (_Gmin > color.G)
//            {
//                _Gmin = color.G;
//            }
//            if (_Bmax < color.B)
//            {
//                _Bmax = color.B;
//            }
//            if (_Bmin > color.B)
//            {
//                _Bmin = color.B;
//            }
//            if (_Rmin < 0)
//            {
//                _Rmin = 0;
//            }
//            if (_Rmax > 255)
//            {
//                _Rmax = 255;
//            }
//            if (_Gmin < 0)
//            {
//                _Gmin = 0;
//            }
//            if (_Gmax > 255)
//            {
//                _Gmax = 255;
//            }
//            if (_Bmin < 0)
//            {
//                _Bmin = 0;
//            }
//            if (_Bmax > 255)
//            {
//                _Bmax = 255;
//            }
//        }
//    }
//    for (int i = 0; i < source.Width; i++)
//    {
//        for (int j = 0; j < source.Height; j++)
//        {
//            Color color = source.GetPixel(i, j);
//            _R = color.R;
//            _G = color.G;
//            _B = color.B;
//            double resR = (_R - _Rmin) * ((255 - 0) / (_Rmax - _Rmin));
//            double resG = (_G - _Gmin) * ((255 - 0) / (_Gmax - _Gmin));
//            double resB = (_B - _Bmin) * ((255 - 0) / (_Bmax - _Bmin));

//            resultimage.SetPixel(i, j, Color.FromArgb((int)resR, (int)resG, (int)resB));
//        }
//    }

//    return resultimage;

//}
//public Bitmap Execute9(Bitmap source)//идеальный отражатель
//{

//    Bitmap resultimage = new Bitmap(source.Width, source.Height);
//    double _R = 0;
//    double _G = 0;
//    double _B = 0;
//    double _Rmax = 0;
//    double _Gmax = 0;
//    double _Bmax = 0;

//    for (int i = 0; i < source.Width; i++)
//    {
//        for (int j = 0; j < source.Height; j++)
//        {

//            Color color = source.GetPixel(i, j);

//            if (_Rmax < color.R)
//            {
//                _Rmax = color.R;
//            }
//            if (_Gmax < color.G)
//            {
//                _Gmax = color.G;
//            }
//            if (_Bmax < color.B)
//            {
//                _Bmax = color.B;
//            }
//            if (_Rmax > 255)
//            {
//                _Rmax = 255;
//            }
//            if (_Gmax > 255)
//            {
//                _Gmax = 255;
//            }
//            if (_Bmax > 255)
//            {
//                _Bmax = 255;
//            }
//        }
//    }
//    for (int i = 0; i < source.Width; i++)
//    {
//        for (int j = 0; j < source.Height; j++)
//        {
//            Color color = source.GetPixel(i, j);
//            _R = color.R;
//            _G = color.G;
//            _B = color.B;
//            double resR = (_R * (255 / _Rmax));
//            double resG = (_G * (255 / _Gmax));
//            double resB = (_B * (255 / _Bmax));

//            resultimage.SetPixel(i, j, Color.FromArgb((int)resR, (int)resG, (int)resB));
//        }
//    }

//    return resultimage;

//}

//public static Color calculeteNewPixelColor3(Bitmap source, int x, int y)//для расширения
//{

//    float[,] _kernel = new float[,]{ {0,1,0 },
//                                        {1,1,1 },
//                                        {0,1,0 },};
//    int radiusX = _kernel.GetLength(0) / 2;
//    int radiusY = _kernel.GetLength(1) / 2;
//    float resultR = 0f;
//    float resultG = 0f;
//    float resultB = 0f;
//    for (int l = -radiusY; l <= radiusY; l++)
//    {
//        for (int k = -radiusX; k <= radiusX; k++)
//        {
//            int idX = Clamp(x + k, 0, source.Width - 1);
//            int idY = Clamp(y + l, 0, source.Height - 1);
//            Color neighbourColor = source.GetPixel(idX, idY);
//            resultR += neighbourColor.R * _kernel[k + radiusX, l + radiusY];
//            resultB += neighbourColor.B * _kernel[k + radiusX, l + radiusY];
//            resultG += neighbourColor.G * _kernel[k + radiusX, l + radiusY];


//        }
//    }
//    return Color.FromArgb(Clamp((int)resultR, 0, 255), Clamp((int)resultG, 0, 255), Clamp((int)resultB, 0, 255));


//}
//public static Bitmap delation(Bitmap source, float[,] matrix, int size)
//{
//    Bitmap resultImage = new Bitmap(source.Width, source.Height);
//    Color sourcecolor;
//    Color maxcolor = Color.FromArgb(0, 0, 0);
//    for(int x= 0; x < source.Width; x++)
//    {
//        for(int y = 0; y < source.Height ; y++)
//        {
//            maxcolor = Color.FromArgb(0, 0, 0);
//            int max = 0;
//            for(int i = -1; i <=1; i++)
//            {
//                for (int j = -1; j <= 1; j++)
//                {
//                    if (matrix[i + 1, j +1]==1)
//                    {

//                        sourcecolor = source.GetPixel(Clamp(i + x, 0, source.Width - 1), Clamp(j + y, 0, source.Height - 1));
//                        if (sourcecolor.R > maxcolor.R)
//                        {
//                            maxcolor = sourcecolor;
//                        }
//                    }
//                }
//            }
//            resultImage.SetPixel(x, y, maxcolor);
//        }
//    }

//    return resultImage;


//}
//public  Bitmap Execute10(Bitmap source)//расишрение
//{
//    //Bitmap resultImage = new Bitmap(source.Width, source.Height);
//    int size = 3;
//    float[,] matrix = { {0,1,0 },{1,1,1 },{0,1,0 } };
//    return delation(source, matrix, size);

//}
//public static Bitmap delation2(Bitmap source, float[,] matrix, int size)
//{
//    Bitmap resultImage = new Bitmap(source.Width, source.Height);
//    Color sourcecolor;
//    Color mmincolor = Color.FromArgb(255, 255, 255);
//    for (int x = 0; x < source.Width; x++)
//    {
//        for (int y = 0; y < source.Height; y++)
//        {
//            mmincolor = Color.FromArgb(255, 255, 255);
//            int max = 0;
//            for (int i = -1; i <= 1; i++)
//            {
//                for (int j = -1; j <= 1; j++)
//                {
//                    if (matrix[i + 1, j + 1] == 1)
//                    {

//                        sourcecolor = source.GetPixel(Clamp(i + x, 0, source.Width - 1), Clamp(j + y, 0, source.Height - 1));
//                        if (sourcecolor.R < mmincolor.R)
//                        {
//                            mmincolor = sourcecolor;
//                        }
//                    }
//                }
//            }
//            resultImage.SetPixel(x, y, mmincolor);
//        }
//    }

//    return resultImage;


//}


//public Bitmap Execute11(Bitmap source)//сужение
//{
//    //Bitmap resultImage = new Bitmap(source.Width, source.Height);
//    int size = 3;
//    float[,] matrix = { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 1, 0 } };
//    return delation2(source, matrix, size);

//}

        //public  Bitmap Execute14(Bitmap source)
        //{
        //    Bitmap resultImage = new Bitmap(source.Width, source.Height);
        //    float[,] xkernel = { {-1,0,1 }
        //                        ,{-2,0,2 },
        //                            {-1,0,1 } };
        //    float[,] ykernel = { { -1, -2, -1 },
        //                        { 0, 0, 0 }, 
        //                        { 1, 2, 1 } };
            
        //    int width = source.Width;
        //    int height = source.Height;

            
        //    BitmapData srcData = source.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            
        //    int bytes = srcData.Stride * srcData.Height;

           
        //    byte[] pixelBuffer = new byte[bytes];
        //    byte[] resultBuffer = new byte[bytes];

            
        //    IntPtr srcScan0 = srcData.Scan0;
 
        //    System.Runtime.InteropServices.Marshal.Copy(srcScan0, pixelBuffer, 0, bytes);

           
        //    source.UnlockBits(srcData);
            
           
        //    double xr = 0.0;
        //    double xg = 0.0;
        //    double xb = 0.0;
        //    double yr = 0.0;
        //    double yg = 0.0;
        //    double yb = 0.0;
        //    double rt = 0.0;
        //    double gt = 0.0;
        //    double bt = 0.0;

            
        //    int filterOffset = 1;
        //    int calcOffset = 0;
        //    int byteOffset = 0;

           
        //    for (int OffsetY = filterOffset; OffsetY < height - filterOffset; OffsetY++)
        //    {
        //        for (int OffsetX = filterOffset; OffsetX < width - filterOffset; OffsetX++)
        //        {
                    
        //            xr = xg = xb = yr = yg = yb = 0;
                    

                    
        //            byteOffset = OffsetY * srcData.Stride + OffsetX * 4;
        //            for (int filterY = -filterOffset; filterY <= filterOffset; filterY++)
        //            {
        //                for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
        //                {
        //                    calcOffset = byteOffset + filterX * 4 + filterY * srcData.Stride;
        //                    xb += (double)(pixelBuffer[calcOffset]) * xkernel[filterY + filterOffset, filterX + filterOffset];
        //                    xg += (double)(pixelBuffer[calcOffset + 1]) * xkernel[filterY + filterOffset, filterX + filterOffset];
        //                    xr += (double)(pixelBuffer[calcOffset + 2]) * xkernel[filterY + filterOffset, filterX + filterOffset];
        //                    yb += (double)(pixelBuffer[calcOffset]) * ykernel[filterY + filterOffset, filterX + filterOffset];
        //                    yg += (double)(pixelBuffer[calcOffset + 1]) * ykernel[filterY + filterOffset, filterX + filterOffset];
        //                    yr += (double)(pixelBuffer[calcOffset + 2]) * ykernel[filterY + filterOffset, filterX + filterOffset];
        //                }
        //            }

                   
        //            bt = Math.Sqrt((xb * xb) + (yb * yb));
        //            gt = Math.Sqrt((xg * xg) + (yg * yg));
        //            rt = Math.Sqrt((xr * xr) + (yr * yr));

                   
        //            if (bt > 255) bt = 255;
        //            if (bt < 0) bt = 0;
        //            if (gt > 255) gt = 255;
        //            if (gt < 0) gt = 0;
        //            if (rt > 255) rt = 255;
        //            if (rt < 0) rt = 0;

                    
        //            resultBuffer[byteOffset] = (byte)(bt);
        //            resultBuffer[byteOffset + 1] = (byte)(gt);
        //            resultBuffer[byteOffset + 2] = (byte)(rt);
        //            resultBuffer[byteOffset + 3] = 255;
        //        }
        //    }
           
                     
        //    BitmapData resultData = resultImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            
        //    System.Runtime.InteropServices.Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
        //    resultImage.UnlockBits(resultData);                      
        //    return resultImage;
        //}
        //public static Color calculeteNewPixelColor6(Bitmap source, int x, int y)//для собеля
        //{

        //    float[,] Xkernel = new float[,]{ {-1,0,1 },
        //                                        {-2,0,2 },
        //                                        {-1,0,1 },};


        //    float[,] Ykernel = new float[,]{ {-1,-2,-1 },
        //                                        {0,0,0 },
        //                                        {1,2,1 },};
        //    int radiusX = Xkernel.GetLength(0) / 2;
        //    int radiusY = Xkernel.GetLength(1) / 2;
        //    int radiusX2 = Ykernel.GetLength(0) / 2;
        //    int radiusY2 = Ykernel.GetLength(1) / 2;
        //    float resultR = 0f;
        //    float resultG = 0f;
        //    float resultB = 0f;
        //    float resultR2 = 0f;
        //    float resultG2 = 0f;
        //    float resultB2 = 0f;
        //    for (int l = -radiusY; l <= radiusY; l++)
        //    {
        //        for (int k = -radiusX; k <= radiusX; k++)
        //        {
        //            int idX = Clamp(x + k, 0, source.Width - 1);
        //            int idY = Clamp(y + l, 0, source.Height - 1);
        //            Color neighbourColor = source.GetPixel(idX, idY);
        //            resultR += neighbourColor.R * Xkernel[k + radiusX, l + radiusY];
        //            resultB += neighbourColor.B * Xkernel[k + radiusX, l + radiusY];
        //            resultG += neighbourColor.G * Xkernel[k + radiusX, l + radiusY];


        //        }
        //    }
        //    for (int l = -radiusY2; l <= radiusY2; l++)
        //    {
        //        for (int k = -radiusX2; k <= radiusX2; k++)
        //        {
        //            int idX2 = Clamp(x + k, 0, source.Width - 1);
        //            int idY2 = Clamp(y + l, 0, source.Height - 1);
        //            Color neighbourColor = source.GetPixel(idX2, idY2);
        //            resultR2 += neighbourColor.R * Ykernel[k + radiusX2, l + radiusY2];
        //            resultB2 += neighbourColor.B * Ykernel[k + radiusX2, l + radiusY2];
        //            resultG2 += neighbourColor.G * Ykernel[k + radiusX2, l + radiusY2];


        //        }
        //    }
        //    float superresultR = 0F;
        //    float superresultG = 0F;
        //    float superresultB = 0F;
        //    superresultR = (float)Math.Sqrt(resultR*resultR+resultR2*resultR2);
        //    superresultG = (float)Math.Sqrt(resultG*resultG + resultG2*resultG2);
        //    superresultB = (float)Math.Sqrt(resultB*resultB + resultB2*resultB2);
        //    superresultR = Clamp((int)superresultR, 0, 255);
        //    superresultB = Clamp((int)superresultB, 0, 255);
        //    superresultG = Clamp((int)superresultG, 0, 255);


        //    return Color.FromArgb((int)superresultR, (int)superresultG, (int)superresultB);


        //}

        //public Bitmap Execute13(Bitmap source)//собеля
        //{
        //    Bitmap resultImage = new Bitmap(source.Width, source.Height);
        //    for(int i =0; i < source.Width; i++)
        //    {
        //        for(int j = 0; j < source.Height; j++)
        //        {
        //            Color color = calculeteNewPixelColor6(source, i, j);
        //            resultImage.SetPixel(i, j, color);
        //        }
        //    }
        //    return resultImage;


        //}
        //public static Color calculeteNewPixelColor7(Bitmap source, int x, int y)//для щарра
        //{

        //    float[,] Xkernel = new float[,]{ {-3,0,3 },
        //                                        {-10,0,10 },
        //                                        {-3,0,3 },};


        //    float[,] Ykernel = new float[,]{ {-3,-10,-3 },
        //                                        {0,0,0 },
        //                                        {3,10,3 },};
        //    int radiusX = Xkernel.GetLength(0) / 2;
        //    int radiusY = Xkernel.GetLength(1) / 2;
        //    int radiusX2 = Ykernel.GetLength(0) / 2;
        //    int radiusY2 = Ykernel.GetLength(1) / 2;
        //    float resultR = 0f;
        //    float resultG = 0f;
        //    float resultB = 0f;
        //    float resultR2 = 0f;
        //    float resultG2 = 0f;
        //    float resultB2 = 0f;
        //    for (int l = -radiusY; l <= radiusY; l++)
        //    {
        //        for (int k = -radiusX; k <= radiusX; k++)
        //        {
        //            int idX = Clamp(x + k, 0, source.Width - 1);
        //            int idY = Clamp(y + l, 0, source.Height - 1);
        //            Color neighbourColor = source.GetPixel(idX, idY);
        //            resultR += neighbourColor.R * Xkernel[k + radiusX, l + radiusY];
        //            resultB += neighbourColor.B * Xkernel[k + radiusX, l + radiusY];
        //            resultG += neighbourColor.G * Xkernel[k + radiusX, l + radiusY];


        //        }
        //    }
        //    for (int l = -radiusY2; l <= radiusY2; l++)
        //    {
        //        for (int k = -radiusX2; k <= radiusX2; k++)
        //        {
        //            int idX2 = Clamp(x + k, 0, source.Width - 1);
        //            int idY2 = Clamp(y + l, 0, source.Height - 1);
        //            Color neighbourColor = source.GetPixel(idX2, idY2);
        //            resultR2 += neighbourColor.R * Ykernel[k + radiusX2, l + radiusY2];
        //            resultB2 += neighbourColor.B * Ykernel[k + radiusX2, l + radiusY2];
        //            resultG2 += neighbourColor.G * Ykernel[k + radiusX2, l + radiusY2];


        //        }
        //    }
        //    float superresultR = 0F;
        //    float superresultG = 0F;
        //    float superresultB = 0F;
        //    superresultR = (float)Math.Sqrt(resultR * resultR + resultR2 * resultR2);
        //    superresultG = (float)Math.Sqrt(resultG * resultG + resultG2 * resultG2);
        //    superresultB = (float)Math.Sqrt(resultB * resultB + resultB2 * resultB2);
        //    superresultR = Clamp((int)superresultR, 0, 255);
        //    superresultB = Clamp((int)superresultB, 0, 255);
        //    superresultG = Clamp((int)superresultG, 0, 255);


        //    return Color.FromArgb((int)superresultR, (int)superresultG, (int)superresultB);


        //}

        //public Bitmap Execute15(Bitmap source)//щарра
        //{
        //    Bitmap resultImage = new Bitmap(source.Width, source.Height);
        //    for (int i = 0; i < source.Width; i++)
        //    {
        //        for (int j = 0; j < source.Height; j++)
        //        {
        //            Color color = calculeteNewPixelColor6(source, i, j);
        //            resultImage.SetPixel(i, j, color);
        //        }
        //    }
        //    return resultImage;


        //}

  


