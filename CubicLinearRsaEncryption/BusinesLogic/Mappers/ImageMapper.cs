using System;
using System.Drawing;
using CubicLinearRsaEncryption.Abstract.Mappers;
using CubicLinearRsaEncryption.Models;

namespace CubicLinearRsaEncryption.BusinesLogic.Mappers
{
    public class ImageMapper : IImageMapper
    {
        public Rgba[,] BitmapToRgbaMap(Bitmap image)
        {
            var imageArray = new Rgba[image.Width, image.Height];
            for (var i = 0; i < image.Width; i++)
            {
                for (var j = 0; j < image.Height; j++)
                {
                    var color = image.GetPixel(i, j);
                    imageArray[i, j] = new Rgba
                    {
                        B = Convert.ToInt32(color.B),
                        G = Convert.ToInt32(color.G),
                        R = Convert.ToInt32(color.R),
                        A = Convert.ToInt32(color.A)
                    };
                }
            }

            if (!IsEvenSide(image.Width))
                imageArray = SetArrayToEvenSize(0, imageArray);
            if (!IsEvenSide(image.Height))
                imageArray = SetArrayToEvenSize(1, imageArray);

            return imageArray;
        }

        public RgbaArrays RgbaToRgbaArrays(Rgba[,] image)
        {
            var rgbaArrays = new RgbaArrays
            {
                AArray = new long[image.GetLength(0), image.GetLength(1)],
                BArray = new long[image.GetLength(0), image.GetLength(1)],
                GArray = new long[image.GetLength(0), image.GetLength(1)],
                RArray = new long[image.GetLength(0), image.GetLength(1)]
            };

            for (var i = 0; i < image.GetLength(0); i++)
            {
                for (var j = 0; j < image.GetLength(1); j++)
                {
                    rgbaArrays.AArray[i, j] = image[i, j].A;
                    rgbaArrays.BArray[i, j] = image[i, j].B;
                    rgbaArrays.GArray[i, j] = image[i, j].G;
                    rgbaArrays.RArray[i, j] = image[i, j].R;
                }
            }

            return rgbaArrays;
        }

        public Rgba [,] RgbaArraysToRgba(RgbaArrays rgbaArrays)
        {
            var imageArray = new Rgba[rgbaArrays.AArray.GetLength(0), rgbaArrays.AArray.GetLength(1)];

            for (var i = 0; i < rgbaArrays.AArray.GetLength(0); i++)
            {
                for (var j = 0; j < rgbaArrays.AArray.GetLength(1); j++)
                {
                    imageArray[i, j] = new Rgba
                    {
                        A = rgbaArrays.AArray[i, j],
                        B = rgbaArrays.BArray[i, j],
                        G = rgbaArrays.GArray[i, j],
                        R = rgbaArrays.RArray[i, j],
                    };
                }
            }

            return imageArray;
        }

        public Bitmap RgbaToBitmapMap(Rgba[,] imageArray)
        {
            var image = new Bitmap(imageArray.GetLength(0), imageArray.GetLength(1));
            for (var i = 0; i < imageArray.GetLength(0); i++)
            {
                for (var j = 0; j < imageArray.GetLength(1); j++)
                {
                    image.SetPixel(i, j, Color.FromArgb(Math.Abs(Convert.ToInt32(imageArray[i, j].A % 256)), Math.Abs(Convert.ToInt32(imageArray[i, j].R % 256)), Math.Abs(Convert.ToInt32(imageArray[i, j].G % 256)), Math.Abs(Convert.ToInt32(imageArray[i, j].B % 256))));
                }
            }
            return image;
        }

        private bool IsEvenSide(int side)
        {
            return side % 2 == 0;
        }

        private Rgba[,] SetArrayToEvenSize(int side, Rgba[,] imageArray)
        {
            var newImageArray = new Rgba[0, 0];
            switch (side)
            {
                case 0:
                    newImageArray = new Rgba[imageArray.GetLength(0) + 1, imageArray.GetLength(1)];
                    for (var i = 0; i < imageArray.GetLength(0); i++)
                    {
                        for (var j = 0; j < imageArray.GetLength(1); j++)
                        {
                            newImageArray[i, j] = imageArray[i, j];
                        }
                    }
                    for (var i = 0; i < imageArray.GetLength(1); i++)
                    {
                        newImageArray[imageArray.GetLength(0), i] = imageArray[imageArray.GetLength(0) - 1, i];
                    }

                    break;
                case 1:
                    newImageArray = new Rgba[imageArray.GetLength(0), imageArray.GetLength(1) + 1];
                    for (var i = 0; i < imageArray.GetLength(0); i++)
                    {
                        for (var j = 0; j < imageArray.GetLength(1); j++)
                        {
                            newImageArray[i, j] = imageArray[i, j];
                        }
                    }
                    for (var i = 0; i < imageArray.GetLength(0); i++)
                    {
                        newImageArray[i, imageArray.GetLength(1)] = imageArray[i, imageArray.GetLength(1) - 1];
                    }
                    break;
                default:
                    throw new ArgumentException("The paramether Side is not correct");
            }
            return newImageArray;
        }
    }
}
