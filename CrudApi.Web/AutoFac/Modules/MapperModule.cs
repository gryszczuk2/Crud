using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;

namespace CrudApi.Web.AutoFac.Modules
{
    public class MapperModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(MapperModule).Assembly).As<Profile>();

            builder.Register(context => new MapperConfiguration(cfg =>
                {
                    foreach (var profile in context.Resolve<IEnumerable<Profile>>())
                    {
                        cfg.AddProfile(profile);
                    }
                }
            )).AsSelf().SingleInstance();

            builder.Register(c =>
                    {
                        var context = c.Resolve<IComponentContext>();
                        var config = context.Resolve<MapperConfiguration>();
                        return config.CreateMapper(context.Resolve);
                    }

                ).As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}
