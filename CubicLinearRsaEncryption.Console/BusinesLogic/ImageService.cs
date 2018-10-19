using System.Drawing;
using System.Drawing.Imaging;
using CubicLinearRsaEncryption.Abstract.Mappers;
using CubicLinearRsaEncryption.Console.Abstract;
using CubicLinearRsaEncryption.Models;

namespace CubicLinearRsaEncryption.Console.BusinesLogic
{
    public class ImageService : IImageService
    {
        private readonly IImageMapper imageMapper;

        public ImageService(IImageMapper imageMapper)
        {
            this.imageMapper = imageMapper;
        }

        public RgbaArrays ArraysPrepareToEncrypt(Bitmap image)
        {
            return this.imageMapper.RgbaToRgbaArrays(this.imageMapper.BitmapToRgbaMap(image));
        }

        public Bitmap PrepareToOutput(RgbaArrays image)
        {
            return this.imageMapper.RgbaToBitmapMap(this.imageMapper.RgbaArraysToRgba(image));
        }

        public Bitmap Read(string path)
        {
            return new Bitmap(path);
        }

        public void SaveToFile(string path, Bitmap bitmapImage)
        {
            var image = (Image) bitmapImage;
            image.Save(path, ImageFormat.Jpeg);
        }
    }
}
