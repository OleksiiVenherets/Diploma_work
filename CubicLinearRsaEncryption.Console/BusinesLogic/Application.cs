using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CubicLinearRsaEncryption.Console.Abstract;
using CubicLinearRsaEncryption.Console.Models;

namespace CubicLinearRsaEncryption.Console.BusinesLogic
{
    public class Application : IApplication
    {
        private readonly IImageService imageService;

        private readonly IEncryptionService encryptionService;

        public Application(IImageService imageService, IEncryptionService encryptionService)
        {
            this.imageService = imageService;
            this.encryptionService = encryptionService;
        }

        public void Run()
        {
            System.Console.WriteLine(Constants.GetImagePathMessage);
            var path = System.Console.ReadLine();

            var image = this.imageService.Read(path);

            System.Console.WriteLine(Constants.GetOutputImagePath);
            var outputPath = System.Console.ReadLine();


            var preparedToEncryptionImage = this.imageService.ArraysPrepareToEncrypt(image);

            var encryptedImage = this.encryptionService.Encrypt(preparedToEncryptionImage, EncryptionType.HorisontalPlusAside);

            var outputEncryptedImage = this.imageService.PrepareToOutput(encryptedImage);

            this.imageService.SaveToFile(outputPath + Constants.EncryptedFileName, outputEncryptedImage);

            var decryptedImage = this.encryptionService.Decrypt(encryptedImage, EncryptionType.HorisontalPlusAside);

            var outputDecryptedImage = this.imageService.PrepareToOutput(decryptedImage);

            this.imageService.SaveToFile(outputPath + Constants.DecryptedFileName, outputDecryptedImage);

            System.Console.ReadKey();
        }
    }
}
