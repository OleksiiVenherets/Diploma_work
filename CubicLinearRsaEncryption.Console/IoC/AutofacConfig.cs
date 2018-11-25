using Autofac;
using CubicLinearRsaEncryption.Abstract.Managers;
using CubicLinearRsaEncryption.Abstract.Mappers;
using CubicLinearRsaEncryption.Abstract.Services;
using CubicLinearRsaEncryption.BusinesLogic.Managers;
using CubicLinearRsaEncryption.BusinesLogic.Mappers;
using CubicLinearRsaEncryption.BusinesLogic.Services;
using CubicLinearRsaEncryption.Console.Abstract;
using CubicLinearRsaEncryption.Console.BusinesLogic;

namespace CubicLinearRsaEncryption.Console.IoC
{
    public static class AutofacConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Application>().As<IApplication>();

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
