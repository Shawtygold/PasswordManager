using Microsoft.Win32;

namespace PasswordBox.MVVM.Model
{
    class DataWorker
    {
        //метод получающий изображение
        internal static string GetImage()
        {
            string image = "";

            try
            {
                OpenFileDialog openFileDialog = new();

                openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == true)
                    image = openFileDialog.FileName;
            }
            catch (Exception ex)
            {
                Messagebox.Show(ex.Message);
            }

            return image;
        }
    }
}
