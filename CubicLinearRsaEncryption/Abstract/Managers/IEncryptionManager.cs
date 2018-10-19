using CubicLinearRsaEncryption.Models;

namespace CubicLinearRsaEncryption.Abstract.Managers
{
    public interface IEncryptionManager
    {
        RgbaArrays HorisontalEncryptPlusAside(RgbaArrays image, RsaElements rsa);

        RgbaArrays HorisontalEncryptMinusAside(RgbaArrays image, RsaElements rsa);

        RgbaArrays VerticalEncryptPlusAside(RgbaArrays image, RsaElements rsa);

        RgbaArrays VerticalEncryptMinusAside(RgbaArrays image, RsaElements rsa);
    }
}
