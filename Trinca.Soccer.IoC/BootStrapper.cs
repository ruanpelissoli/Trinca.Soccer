using SimpleInjector;
using SimpleInjector.Integration.Web;
using Trinca.Soccer.Data;
using Trinca.Soccer.Data.Interfaces;
using Trinca.Soccer.Data.Repository;
using Trinca.Soccer.Services;

namespace Trinca.Soccer.IoC
{
   public class BootStrapper
    {
        public static void RegisterServices(Container container, bool shouldRegisterOwin = false, bool shouldBeSingleton = false)
        {
            var lifestyle = shouldBeSingleton ? Lifestyle.Singleton : new WebRequestLifestyle();

            container.RegisterSingleton<DatabaseContext>();
            container.Register<DatabaseContext.IDbContextFactory, DatabaseContext.DatabaseContextFactory>(lifestyle);

            // Repositories
            container.Register<IEmployeesRepository, EmployeesRepository>(lifestyle);

            //Services
            container.Register<IWebScraper, WebScraper>(lifestyle);
            container.Register<IEmployeesServices, EmployeesServices>(lifestyle);
        }
    }
}
