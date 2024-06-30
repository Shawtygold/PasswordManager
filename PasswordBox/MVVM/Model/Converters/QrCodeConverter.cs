using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace PasswordBox.MVVM.Model.Converters
{
    internal class QrCodeConverter
    {
        internal static BitmapSource ConvertQrCodeUrlToBitmapSource(string qrCodeUrl)
        {
            Bitmap qrBitmap = ConvertQrUrlToBitmap(qrCodeUrl);
            return BitmapConverter.ConvertBitmapToBitmapSource(qrBitmap);
        }
        private static Bitmap ConvertQrUrlToBitmap(string qrUrl)
        {
            using (MemoryStream ms = new(Convert.FromBase64String(qrUrl.Replace("data:image/png;base64,", ""))))
            {
                return (Bitmap)Image.FromStream(ms);
            }
        }
    }
}
