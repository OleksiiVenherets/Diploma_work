using System;
using CubicLinearRsaEncryption.Abstract.Managers;
using CubicLinearRsaEncryption.Models;

namespace CubicLinearRsaEncryption.BusinesLogic.Managers
{
    public class DecryptionManager : IDecryptionManager
    {
        private RsaElements rsaElements;

        private int height;

        private int wigth;

        public RgbaArrays HorisontalDecryptMinusAside(RgbaArrays image, RsaElements rsa)
        {
            this.height = image.AArray.GetLength(0);
            this.wigth = image.AArray.GetLength(1);
            this.rsaElements = rsa;

            return new RgbaArrays
            {
                AArray = this.DecryptOneColourHorisontal(image.AArray, false),
                BArray = this.DecryptOneColourHorisontal(image.BArray, false),
                GArray = this.DecryptOneColourHorisontal(image.GArray, false),
                RArray = this.DecryptOneColourHorisontal(image.RArray, false),
            };
        }

        public RgbaArrays HorisontalDecryptPlusAside(RgbaArrays image, RsaElements rsa)
        {
            this.height = image.AArray.GetLength(0);
            this.wigth = image.AArray.GetLength(1);
            this.rsaElements = rsa;

            return new RgbaArrays
            {
                AArray = this.DecryptOneColourHorisontal(image.AArray, true),
                BArray = this.DecryptOneColourHorisontal(image.BArray, true),
                GArray = this.DecryptOneColourHorisontal(image.GArray, true),
                RArray = this.DecryptOneColourHorisontal(image.RArray, true),
            };
        }

        public RgbaArrays VerticalDecryptMinusAside(RgbaArrays image, RsaElements rsa)
        {
            this.height = image.AArray.GetLength(0);
            this.wigth = image.AArray.GetLength(1);
            this.rsaElements = rsa;

            return new RgbaArrays
            {
                AArray = this.DecryptOneColourVertical(image.AArray, false),
                BArray = this.DecryptOneColourVertical(image.BArray, false),
                GArray = this.DecryptOneColourVertical(image.GArray, false),
                RArray = this.DecryptOneColourVertical(image.RArray, false),
            };
        }

        public RgbaArrays VerticalDecryptPlusAside(RgbaArrays image, RsaElements rsa)
        {
            this.height = image.AArray.GetLength(0);
            this.wigth = image.AArray.GetLength(1);
            this.rsaElements = rsa;

            return new RgbaArrays
            {
                AArray = this.DecryptOneColourVertical(image.AArray, true),
                BArray = this.DecryptOneColourVertical(image.BArray, true),
                GArray = this.DecryptOneColourVertical(image.GArray, true),
                RArray = this.DecryptOneColourVertical(image.RArray, true),
            };
        }

        private long[,] DecryptOneColourHorisontal(long[,] array, bool isPlusStep)
        {
            var decryptedArray = new long[height, wigth];
            if (isPlusStep)
            {
                for (var i = 0; i < height; i++)
                {
                    for (var j = 0; j < wigth - 1; j += 2)
                    {
                        try
                        {
                            var decryptedtPair = DecryptPlusStep(array[i, j], array[i, j + 1]);
                            decryptedArray[i, j] = decryptedtPair.X;
                            decryptedArray[i, j + 1] = decryptedtPair.Y;
                        }

                        catch(Exception)
                        {
                            Console.WriteLine(i + "------" + j);
                        }
                    }
                }
            }
            else
            {
                for (var i = 0; i < height; i++)
                {
                    for (var j = 0; j < wigth - 1; j += 2)
                    {
                        var decryptedtPair = DecryptMinusStep(array[i, j], array[i, j + 1]);
                        decryptedArray[i, j] = decryptedtPair.X;
                        decryptedArray[i, j + 1] = decryptedtPair.Y;
                    }
                }
            }

            return decryptedArray;
        }

        private long[,] DecryptOneColourVertical(long[,] array, bool isPlusStep)
        {
            var decryptedArray = new long[height, wigth];
            if (isPlusStep)
            {
                for (var j = 0; j < height; j++)
                {
                    for (var i = 0; i < wigth - 1; i += 2)
                    {
                        var decryptedPair = DecryptPlusStep(array[i, j], array[i + 1, j]);
                        decryptedArray[i, j] = decryptedPair.X;
                        decryptedArray[i + 1, j] = decryptedPair.Y;
                    }
                }
            }
            else
            {
                for (var j = 0; j < height; j++)
                {
                    for (var i = 0; i < wigth - 1; i += 2)
                    {
                        var decryptedPair = DecryptMinusStep(array[i, j], array[i + 1, j]);
                        decryptedArray[i, j] = decryptedPair.X;
                        decryptedArray[i + 1, j] = decryptedPair.Y;
                    }
                }
            }

            return decryptedArray;
        }

        private DecryptedPair DecryptPlusStep(long u, long v)
        {
            var x = this.GetXPlusStep(u, v);
            return new DecryptedPair
            {
                X = x,
                Y = v - rsaElements.D - x
            };
        }

        private DecryptedPair DecryptMinusStep(long u, long v)
        {
            var x = this.GetXMinusStep(u, v);
            return new DecryptedPair
            {
                X = x,
                Y = x - v + rsaElements.D
            };
        }

        private long GetXPlusStep(long u, long v)
        {
            var result = (u - rsaElements.E - Math.Pow(rsaElements.D - v, 3.0)) / (3 * (rsaElements.D - v));
            var firstX = Convert.ToInt64(result);
            var secondX = Convert.ToInt64(result + rsaElements.D - v);

            //if (firstX < 0 && secondX < 0)
            //    return 0;
            if(firstX > 0 && secondX <= 0)
                return firstX;
            if (secondX > 0 && firstX <= 0)
                return secondX;

            return firstX < secondX ? firstX  : secondX;

        }

        private long GetXMinusStep(long u, long v)
        {
            var discriminator = this.GetDiscriminatorMinusStep(u, v);

            var firstX = Convert.ToInt64(3 * v + rsaElements.D + Math.Sqrt(discriminator) / 6);
            var secondX = Convert.ToInt64(3 * v + rsaElements.D - Math.Sqrt(discriminator) / 6);

            if (firstX < 0 && secondX < 0)
                return 0;
            if (firstX > 0 && secondX <= 0)
                return firstX;
            if (secondX > 0 && firstX <= 0)
                return secondX;

            return firstX < secondX ? firstX : secondX;
        }

        private long GetDiscriminatorMinusStep(long u, long v)
        {
            return Convert.ToInt64(Math.Pow(-(3 * v + rsaElements.D), 2.0) - 12 * (Math.Pow(v + rsaElements.D, 2.0) - (u - rsaElements.E) / (v - rsaElements.D)));
        }
    }
}
