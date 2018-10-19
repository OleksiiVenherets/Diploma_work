using Autofac;
using CubicLinearRsaEncryption.Console.Abstract;
using CubicLinearRsaEncryption.Console.IoC;

namespace CubicLinearRsaEncryption.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var container = AutofacConfig.ConfigureContainer();
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();
                app.Run();
            }
        }
    }
}
