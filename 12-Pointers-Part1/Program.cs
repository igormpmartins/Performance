//original example by Mark Farragher

using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace Pointers
{
    class MainClass
    {
        private static long MeasureA(Bitmap bmp)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color pixel = bmp.GetPixel(x, y);
                    byte grey = (byte)(.299 * pixel.R + .587 * pixel.G + .114 * pixel.B);
                    Color greyPixel = Color.FromArgb(grey, grey, grey);
                    bmp.SetPixel(x, y, greyPixel);
                }
            }
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private static long MeasureB(Bitmap bmp)
        {
            BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            Stopwatch stopwatch = new Stopwatch();

            unsafe
            {
                stopwatch.Start();
                byte* p = (byte*)(void*)bmData.Scan0.ToPointer();
                int stopAddress = (int)p + bmData.Stride * bmData.Height;
                while ((int)p != stopAddress)
                {
                    byte pixel = (byte)(.299 * p[2] + .587 * p[1] + .114 * p[0]);
                    *p = pixel;
                    p++;
                    *p = pixel;
                    p++;
                    *p = pixel;
                    p++;
                }
                stopwatch.Stop();
            }
            bmp.UnlockBits(bmData);
            return stopwatch.ElapsedMilliseconds;
        }

        public static void Main(string[] args)
        {
            long elapsedA = 0;
            long elapsedB = 0;

            // grayscale conversion using getpixel & setpixel
            Bitmap bmpA = (Bitmap)Bitmap.FromFile("lenna.png");
            elapsedA = MeasureA(bmpA);
            bmpA.Save("lenna_bw_a.png");

            // grayscale conversion using pointers
            Bitmap bmpB = (Bitmap)Bitmap.FromFile("lenna.png");
            elapsedB = MeasureB(bmpB);
            bmpB.Save("lenna_bw_b.png");

            // write results
            Console.WriteLine("Grayscale conversion using GetPixel/SetPixel: " + elapsedA.ToString());
            Console.WriteLine("Grayscale conversion using pointers: " + elapsedB.ToString());
        }
    }
}
