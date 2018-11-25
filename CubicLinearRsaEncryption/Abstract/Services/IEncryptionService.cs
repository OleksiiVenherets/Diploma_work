using CubicLinearRsaEncryption.Models;

namespace CubicLinearRsaEncryption.Abstract.Services
{
    public interface IEncryptionService
    {
        RgbaArrays Encrypt(RgbaArrays image, EncryptionType type);

        RgbaArrays Decrypt(RgbaArrays image, EncryptionType type);
    }
}