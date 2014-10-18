using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FineWine.Domain.Repositories;

namespace FineWine.Infrastructure.Installers
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var domainAssembly = Assembly.GetAssembly(typeof(IRepository));

            container.Register(
                Classes.FromAssembly(domainAssembly)
                    .BasedOn(typeof(IRepository))
                    .WithService
                    .DefaultInterfaces()
                    .LifestylePerWebRequest()
                );
        }
    }
}