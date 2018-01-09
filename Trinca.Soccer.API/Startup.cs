using System.Web.Http;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Trinca.Soccer.API.App_Start;
using Trinca.Soccer.API.Mapping;

[assembly: OwinStartup(typeof(Trinca.Soccer.API.Startup))]
namespace Trinca.Soccer.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Resolve dependencies
            SimpleInjectorWebApiInitializer.Initialize();

            MappingConfig.Initialize();

            // Change to camelcase (json default)
            var config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;

            // Remove the XML formatter (we just need JSON) 
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}