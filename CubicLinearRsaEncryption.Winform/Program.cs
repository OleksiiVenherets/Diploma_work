using Autofac;
using CubicLinearRsaEncryption.Winform.Abstract;
using CubicLinearRsaEncryption.Winform.IoC;
using System;

namespace CubicLinearRsaEncryption.Winform
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
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
