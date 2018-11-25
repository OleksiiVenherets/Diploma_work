using Autofac;
using CubicLinearRsaEncryption.Winform.Abstract;
using CubicLinearRsaEncryption.Winform.BusinessLogic;
using CubicLinearRsaEncryption.Abstract.Managers;
using CubicLinearRsaEncryption.Abstract.Mappers;
using CubicLinearRsaEncryption.BusinesLogic.Managers;
using CubicLinearRsaEncryption.BusinesLogic.Mappers;

namespace CubicLinearRsaEncryption.Winform.IoC
{
    public static class AutofacConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MyApplication>().As<IApplication>();

            builder.RegisterType<ImageService>().As<IImageService>();
            builder.RegisterType<EncryptionService>().As<IEncryptionService>();

            builder.RegisterType<RsaManager>().As<IRsaManager>();
            builder.RegisterType<PrimeNumberManager>().As<IPrimeNumberManager>();
            builder.RegisterType<EncryptionManager>().As<IEncryptionManager>();
            builder.RegisterType<DecryptionManager>().As<IDecryptionManager>();

            builder.RegisterType<ImageMapper>().As<IImageMapper>();

            return builder.Build();
        }
    }
}
