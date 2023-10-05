using System;
using System.Drawing;
using System.IO;

namespace ImageConverter
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			const int colorBits = 4;

			// args = new string[] { "images/pco_grayscale.bmp" };

			Console.WriteLine("Convert image to 4 bit color hex data C-style array.");

			if (args.Length < 1)
			{
				Console.WriteLine("usage:");
				Console.WriteLine("  mono ImageConverter.exe image.bmp > image.c");
				Console.WriteLine();
				return;
			}

			string filename = args[0];
			if (!File.Exists(filename))
			{
				Console.WriteLine("File \"{0}\" not exists.", filename);
				Console.WriteLine();
				return;
			}

			Console.WriteLine("Convert file {0:s}:", filename);
			Console.WriteLine();

			Image image = Image.FromFile(filename);
			Bitmap bitmap = new Bitmap(image);

			Console.WriteLine("\t\t/// \"{0:s}\" {1:d}x{2:d}x{3:d}",
					filename, bitmap.Width, bitmap.Height, colorBits);
			Console.Write("\t");

			int i = 0;
			for (int y = 0; y < bitmap.Height; y++)
				for (int x = 0; x < bitmap.Width; x += 8 / colorBits)
				{
					Color color1 = bitmap.GetPixel(x, y);
					Color color2 = bitmap.GetPixel(x + 1, y);
					int c1 = color1.R >> 4;
					int c2 = color2.R >> 4;
					int c = (c1 << 4) | c2;

					Console.Write("0x{0:x2}", c);
					if (x < bitmap.Width - 2 || y < bitmap.Height - 1)
						Console.Write(", ");
					if (i++ % 16 == 16 - 1)
						Console.Write(Environment.NewLine + "\t");
				}

			Console.WriteLine();
			Console.WriteLine();
			// Console.Read();
		}
	}
}

/*

	usage:
mono ImageConverter.exe images/pco_bw.bmp > image.c

*/
