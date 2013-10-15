using System;
using System.IO;

namespace ppm
{
	class PPMTest
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Trying to write binary file...");

			var header = "P6";
			int width = 200;
			int height = 200;

			Random rnd = new Random();

			var finalData = header + Environment.NewLine + width + " " + height + Environment.NewLine + "255" + Environment.NewLine;
			var data = ConvertToBinary (finalData);
			Array.Resize (ref data, data.Length + (width * height * 3));

			for (int row = 0; row < (height); row++) {
				for (int x = 0; x < (width); x++) {
					int start = data.Length - (width * height * 3) + (x*3) + (row*width*3);
					data [start] = (byte) rnd.Next(0, 255);
					data [start + 1] = (byte) rnd.Next(0, 255);
					data [start + 2] = (byte) rnd.Next(0, 255);
				}
			}

			WriteBinaryFile("bitmap.ppm", data);
		}

		public static byte[] ConvertToBinary(string str)
		{
			System.Text.ASCIIEncoding  encoding = new System.Text.ASCIIEncoding();
			return encoding.GetBytes(str);
		}

		public static void WriteBinaryFile (string fileName, byte[] binary)
		{
			if (binary == null) {
				return;
			}

			if (File.Exists (fileName)) {
				File.Delete (fileName);
			}

			using (FileStream fs = new FileStream(fileName, FileMode.Create)) {
				using (BinaryWriter bw = new BinaryWriter(fs)) {
					bw.Write (binary);
				}
			}
		}
	}
}
