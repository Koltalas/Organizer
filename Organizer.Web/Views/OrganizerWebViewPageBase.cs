using Abp.Web.Mvc.Views;

namespace Organizer.Web.Views
{
    public abstract class OrganizerWebViewPageBase : OrganizerWebViewPageBase<dynamic>
    {

    }

    public abstract class OrganizerWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected OrganizerWebViewPageBase()
        {
            LocalizationSourceName = OrganizerConsts.LocalizationSourceName;
        }
    }
}