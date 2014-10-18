using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FineWine.Domain.Services;

namespace FineWine.Infrastructure.Installers
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var domainAssembly = Assembly.GetAssembly(typeof(IService));

            container.Register(
                Classes.FromAssembly(domainAssembly)
                    .BasedOn(typeof(IService))
                    .WithService
                    .DefaultInterfaces()
                    .LifestylePerWebRequest()
                );
        }
    }
}