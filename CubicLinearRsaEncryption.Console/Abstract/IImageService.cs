using System.Drawing;
using CubicLinearRsaEncryption.Models;

namespace CubicLinearRsaEncryption.Console.Abstract
{
    public interface IImageService
    {
        RgbaArrays ArraysPrepareToEncrypt(Bitmap image);

        Bitmap PrepareToOutput(RgbaArrays image);

        Bitmap Read(string path);

        void SaveToFile(string path, Bitmap bitmapImage);
    }
}
