using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;

namespace Organizer.Api
{
    [DependsOn(typeof(AbpWebApiModule), typeof(OrganizerApplicationModule))]
    public class OrganizerWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(OrganizerApplicationModule).Assembly, "app")
                .Build();

     //       Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder.For<IEventAppService>("eventsystem/event").Build();


            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
        }
    }
}
