using System;
using CubicLinearRsaEncryption.Abstract.Managers;
using CubicLinearRsaEncryption.Abstract.Services;
using CubicLinearRsaEncryption.Models;

namespace CubicLinearRsaEncryption.BusinesLogic.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly IRsaManager rsaManager;

        private readonly IDecryptionManager decryptionManager;

        private readonly IEncryptionManager encryptionManager;

        private RsaElements rsaElements;

        public EncryptionService(IRsaManager rsaManager, IDecryptionManager decryptionManager,
            IEncryptionManager encryptionManager)
        {
            this.rsaManager = rsaManager;
            this.decryptionManager = decryptionManager;
            this.encryptionManager = encryptionManager;
        }

        public RgbaArrays Encrypt(RgbaArrays image, EncryptionType type)
        {
            this.rsaElements = this.rsaManager.GetRsaElements();
            var encryptedImage = new RgbaArrays();
            switch (type)
            {
                case EncryptionType.HorisontalPlusAside:
                    encryptedImage = this.encryptionManager.HorisontalEncryptPlusAside(image, rsaElements);
                    break;
                case EncryptionType.HorisontalMinusAside:
                    encryptedImage = this.encryptionManager.HorisontalEncryptMinusAside(image, rsaElements);
                    break;
                case EncryptionType.VerticalPlusAside:
                    encryptedImage = this.encryptionManager.VerticalEncryptPlusAside(image, rsaElements);
                    break;
                case EncryptionType.VericalMinusAside:
                    encryptedImage = this.encryptionManager.VerticalEncryptMinusAside(image, rsaElements);
                    break;
                default:
                    throw new ArgumentException(Constants.EncryptionExceptionMessage);
            }

            return encryptedImage;
        }

        public RgbaArrays Decrypt(RgbaArrays image, EncryptionType type)
        {
            var decryptedImage = new RgbaArrays();
            switch (type)
            {
                case EncryptionType.HorisontalPlusAside:
                    decryptedImage = this.decryptionManager.HorisontalDecryptPlusAside(image, rsaElements);
                    break;
                case EncryptionType.HorisontalMinusAside:
                    decryptedImage = this.decryptionManager.HorisontalDecryptMinusAside(image, rsaElements);
                    break;
                case EncryptionType.VerticalPlusAside:
                    decryptedImage = this.decryptionManager.VerticalDecryptPlusAside(image, rsaElements);
                    break;
                case EncryptionType.VericalMinusAside:
                    decryptedImage = this.decryptionManager.VerticalDecryptMinusAside(image, rsaElements);
                    break;
                default:
                    throw new ArgumentException(Constants.DecryptionExceptionMessage);
            }

            return decryptedImage;
        }
    }
}