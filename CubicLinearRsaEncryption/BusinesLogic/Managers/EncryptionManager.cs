using System;
using CubicLinearRsaEncryption.Abstract.Managers;
using CubicLinearRsaEncryption.Models;

namespace CubicLinearRsaEncryption.BusinesLogic.Managers
{
    public class EncryptionManager : IEncryptionManager
    {
        private RsaElements rsaElements;

        private int height;

        private int wigth;

        public RgbaArrays HorisontalEncryptPlusAside(RgbaArrays image, RsaElements rsa)
        {
            this.height = image.AArray.GetLength(0);
            this.wigth = image.AArray.GetLength(1);
            this.rsaElements = rsa;

            return new RgbaArrays
            {
                AArray = this.EncryptOneColourHorisontal(image.AArray, true),
                BArray = this.EncryptOneColourHorisontal(image.BArray, true),
                GArray = this.EncryptOneColourHorisontal(image.GArray, true),
                RArray = this.EncryptOneColourHorisontal(image.RArray, true),
            };
        }

        public RgbaArrays HorisontalEncryptMinusAside(RgbaArrays image, RsaElements rsa)
        {
            this.height = image.AArray.GetLength(0);
            this.wigth = image.AArray.GetLength(1);
            this.rsaElements = rsa;

            return new RgbaArrays
            {
                AArray = this.EncryptOneColourHorisontal(image.AArray, false),
                BArray = this.EncryptOneColourHorisontal(image.BArray, false),
                GArray = this.EncryptOneColourHorisontal(image.GArray, false),
                RArray = this.EncryptOneColourHorisontal(image.RArray, false),
            };
        }

        public RgbaArrays VerticalEncryptPlusAside(RgbaArrays image, RsaElements rsa)
        {
            this.height = image.AArray.GetLength(0);
            this.wigth = image.AArray.GetLength(1);
            this.rsaElements = rsa;

            return new RgbaArrays
            {
                AArray = this.EncryptOneColourVertical(image.AArray, true),
                BArray = this.EncryptOneColourVertical(image.BArray, true),
                GArray = this.EncryptOneColourVertical(image.GArray, true),
                RArray = this.EncryptOneColourVertical(image.RArray, true),
            };
        }

        public RgbaArrays VerticalEncryptMinusAside(RgbaArrays image, RsaElements rsa)
        {
            this.height = image.AArray.GetLength(0);
            this.wigth = image.AArray.GetLength(1);
            this.rsaElements = rsa;

            return new RgbaArrays
            {
                AArray = this.EncryptOneColourVertical(image.AArray, false),
                BArray = this.EncryptOneColourVertical(image.BArray, false),
                GArray = this.EncryptOneColourVertical(image.GArray, false),
                RArray = this.EncryptOneColourVertical(image.RArray, false),
            };
        }

        private long[,] EncryptOneColourHorisontal(long[,] array, bool isPlusStep)
        {
            var encryptedArray = new long[height, wigth];
            if (isPlusStep)
            {
                for (var i = 0; i < height; i++)
                {
                    for (var j = 0; j < wigth - 1; j += 2)
                    {
                        var encryptedtPair = EncryptPlusStep(array[i, j], array[i, j + 1]);
                        encryptedArray[i, j] = encryptedtPair.U;
                        encryptedArray[i, j + 1] = encryptedtPair.V;
                    }
                }
            }
            else
            {
                for (var i = 0; i < height; i++)
                {
                    for (var j = 0; j < wigth - 1; j += 2)
                    {
                        var encryptedtPair = EncryptMinusStep(array[i, j], array[i, j + 1]);
                        encryptedArray[i, j] = encryptedtPair.U;
                        encryptedArray[i, j + 1] = encryptedtPair.V;
                    }
                }
            }

            return encryptedArray;

        }

        private long[,] EncryptOneColourVertical(long[,] array, bool isPlusStep)
        {
            var encryptedArray = new long[height, wigth];
            if (isPlusStep)
            {
                for (var j = 0; j < height; j++)
                {
                    for (var i = 0; i < wigth - 1; i += 2)
                    {
                        var outputPair = EncryptPlusStep(array[i, j], array[i + 1, j]);
                        encryptedArray[i, j] = outputPair.U;
                        encryptedArray[i + 1, j] = outputPair.V;
                    }
                }
            }
            else
            {
                for (var j = 0; j < height; j++)
                {
                    for (var i = 0; i < wigth - 1; i += 2)
                    {
                        var outputPair = EncryptMinusStep(array[i, j], array[i + 1, j]);
                        encryptedArray[i, j] = outputPair.U;
                        encryptedArray[i + 1, j] = outputPair.V;
                    }
                }
            }

            return encryptedArray;
        }

        private EncryptedPair EncryptPlusStep(long x, long y)
        {
            return new EncryptedPair
            {
                U = Convert.ToInt64(Math.Pow(x, 3.0) + Math.Pow(y, 3.0) + this.rsaElements.E),
                V = x + y + this.rsaElements.D
            };
        }

        private EncryptedPair EncryptMinusStep(long x, long y)
        {
            return new EncryptedPair
            {
                U = Convert.ToInt64(Math.Pow(x, 3.0) - Math.Pow(y, 3.0) + this.rsaElements.E),
                V = x - y + this.rsaElements.D
            };
        }

    }
}
