using System.Drawing;
using CubicLinearRsaEncryption.Models;

namespace CubicLinearRsaEncryption.Abstract.Services
{
    public interface IImageService
    {
        RgbaArrays ArraysPrepareToEncrypt(Bitmap image);

        Bitmap PrepareToOutput(RgbaArrays image);

        Bitmap Read(string path);

        void SaveToFile(string path, Bitmap bitmapImage);
    }
}