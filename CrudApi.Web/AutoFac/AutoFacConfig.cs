using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace CrudApi.Web.AutoFac
{
    public class AutoFacConfig
    {
        public static IContainer Configure(IServiceCollection services)
        {
            var builder= new ContainerBuilder();

            builder.RegisterAssemblyModules(typeof(AutoFacConfig).Assembly);

            builder.Populate(services);
            var container = builder.Build();
            return container;
        }
    }
}
