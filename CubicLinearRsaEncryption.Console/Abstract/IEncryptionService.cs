using CubicLinearRsaEncryption.Console.Models;
using CubicLinearRsaEncryption.Models;

namespace CubicLinearRsaEncryption.Console.Abstract
{
    public interface IEncryptionService
    {
        RgbaArrays Encrypt(RgbaArrays image, EncryptionType type);

        RgbaArrays Decrypt(RgbaArrays image, EncryptionType type);
    }
}
