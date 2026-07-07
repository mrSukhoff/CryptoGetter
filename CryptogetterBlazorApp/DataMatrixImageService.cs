using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ZXing;
using ZXing.Common;

namespace CryptogetterBlazorApp;

public class DataMatrixImageService
{
	public byte[] GeneratePng(string code, int width = 300, int height = 300)
	{
		var writer = new BarcodeWriterPixelData
		{
			Format = BarcodeFormat.DATA_MATRIX,
			Options = new EncodingOptions
			{
				Width = width,
				Height = height,
				Margin = 0,
				PureBarcode = true
			}
		};

		var pixelData = writer.Write(code);

		using var bitmap = new Bitmap(
			pixelData.Width,
			pixelData.Height,
			PixelFormat.Format32bppRgb);

		var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

		var bmpData = bitmap.LockBits(
			rect,
			ImageLockMode.WriteOnly,
			bitmap.PixelFormat);

		Marshal.Copy(pixelData.Pixels, 0, bmpData.Scan0, pixelData.Pixels.Length);

		bitmap.UnlockBits(bmpData);

		using var stream = new MemoryStream();
		bitmap.Save(stream, ImageFormat.Png);

		return stream.ToArray();
	}
}