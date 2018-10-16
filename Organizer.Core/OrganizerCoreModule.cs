using System.Reflection;
using Abp.Localization;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Zero;
using Abp.Zero.Configuration;
using Organizer.Authorization;
using Organizer.Authorization.Roles;
using Organizer.MultiTenancy;
using Organizer.Users;

namespace Organizer
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class OrganizerCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            //Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            //Remove the following line to disable multi-tenancy.
            Configuration.MultiTenancy.IsEnabled = OrganizerConsts.MultiTenancyEnabled;

            //Add/remove localization sources here
        //    Configuration.Localization.Languages.Add(new LanguageInfo("ru", "Русский", "famfamfam-flag-ru"));
           //Configuration.Localization.Languages.Clear();
           // Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", "famfamfam-flag-england", true));
           // Configuration.Localization.Languages.Add(new LanguageInfo("ru", "Русский", "famfamfam-flag-ru"));
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    OrganizerConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "Organizer.Localization.Source"
                        )
                    )
                );

            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Authorization.Providers.Add<OrganizerAuthorizationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
