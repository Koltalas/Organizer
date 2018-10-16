using Abp.Application.Navigation;
using Abp.Localization;
using Organizer.Authorization;

namespace Organizer.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See .cshtml and .js files under App/Main/views/layout/header to know how to render menu.
    /// </summary>
    public class OrganizerNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "Home",
                        new LocalizableString("HomePage", OrganizerConsts.LocalizationSourceName),
                        url: "#/",
                        icon: "fa fa-home",
                        requiresAuthentication: true
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Tenants",
                        L("Tenants"),
                        url: "#tenants",
                        icon: "fa fa-globe",
                        requiredPermissionName: PermissionNames.Pages_Tenants
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Users",
                        L("Users"),
                        url: "#users",
                        icon: "fa fa-users",
                        requiredPermissionName: PermissionNames.Pages_Users
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Calendar",
                        new LocalizableString("Calendar", OrganizerConsts.LocalizationSourceName),
                        url: "#/calendar",
                        icon: "fa fa-calendar"
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Notes",
                        new LocalizableString("Notes", OrganizerConsts.LocalizationSourceName),
                        url: "#/notes",
                        icon: "fa fa-calendar-o"
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Contacts",
                        new LocalizableString("Contacts", OrganizerConsts.LocalizationSourceName),
                        url: "#/contacts",
                        icon: "fa fa-book"
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "ToDo",
                        new LocalizableString("ToDo", OrganizerConsts.LocalizationSourceName),
                        url: "#/todo",
                        icon: "fa fa-reorder"
                        )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, OrganizerConsts.LocalizationSourceName);
        }
    }
}
