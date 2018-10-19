using System.Drawing;
using CubicLinearRsaEncryption.Models;

namespace CubicLinearRsaEncryption.Abstract.Mappers
{
    public interface IImageMapper
    {
        Rgba [,] BitmapToRgbaMap(Bitmap image);

        RgbaArrays RgbaToRgbaArrays(Rgba[,] image);

        Rgba[,] RgbaArraysToRgba(RgbaArrays image);

        Bitmap RgbaToBitmapMap(Rgba[,] image);
    }
}
