using System;
using System.Diagnostics;
using System.Reflection;

namespace Pointers_v2
{
	class MainClass
	{
		private static long MeasureA(int size)
		{
			byte[] image = new byte[size * size * 3];

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			for (int i = 0; i < image.Length;)
			{
				byte grey = (byte)(.299 * image[i + 2] + .587 * image[i + 1] + .114 * image[i]);
				image[i] = grey;
				image[i + 1] = grey;
				image[i + 2] = grey;
				i += 3;
			}
			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		}

		private static long MeasureB(int size)
		{
			byte[] image = new byte[size * size * 3];

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			unsafe
			{
				fixed (byte* p = &image[0])
				{
					for (int i = 0; i < image.Length;)
					{
						byte grey = (byte)(.299 * image[i + 2] + .587 * image[i + 1] + .114 * image[i]);
						p[i] = grey;
						p[i + 1] = grey;
						p[i + 2] = grey;
						i += 3;
					}
				}
			}
			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		}

		private static long MeasureC(int size)
		{
			byte[] image = new byte[size * size * 3];

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			unsafe
			{
				fixed (byte* imgPtr = &image[0])
				{
					byte* p = imgPtr;
					int stopAddress = (int)p + size * size * 3;
					while ((int)p != stopAddress)
					{
						byte grey = (byte)(.299 * p[2] + .587 * p[1] + .114 * p[0]);
						*p = grey;
						*(p + 1) = grey;
						*(p + 2) = grey;
						p += 3;
					}
				}
			}
			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		}

		public static void Main(string[] args)
		{
			for (int size = 512; size < 4096; size += 128)
			{
				// image processing using byte[]
				long elapsedA = MeasureA(size);

				// image processing using byte* and reading by indexer[] 
				long elapsedB = MeasureB(size);

				// image processing using byte* and advancing the pointer
				long elapsedC = MeasureC(size);

				// write results
				Console.WriteLine(string.Format("{0}\t A: {1}\t B: {2}\t C: {3}", size, elapsedA, elapsedB, elapsedC));
			}
		}
	}
}
