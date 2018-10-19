using CubicLinearRsaEncryption.Models;

namespace CubicLinearRsaEncryption.Abstract.Managers
{
    public interface IDecryptionManager
    {
        RgbaArrays HorisontalDecryptPlusAside(RgbaArrays image, RsaElements rsa);

        RgbaArrays HorisontalDecryptMinusAside(RgbaArrays image, RsaElements rsa);

        RgbaArrays VerticalDecryptPlusAside(RgbaArrays image, RsaElements rsa);

        RgbaArrays VerticalDecryptMinusAside(RgbaArrays image, RsaElements rsa);
    }
}
