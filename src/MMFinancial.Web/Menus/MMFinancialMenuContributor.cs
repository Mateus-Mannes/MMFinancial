using System.Threading.Tasks;
using MMFinancial.Localization;
using MMFinancial.MultiTenancy;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace MMFinancial.Web.Menus;

public class MMFinancialMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<MMFinancialResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                MMFinancialMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        context.Menu.AddItem(
    new ApplicationMenuItem(
        "Transactions",
        l["Menu:MMFinancial"],
        icon: "fa fa-book"
    ).AddItem(
        new ApplicationMenuItem(
            "MMFinancial.Transactions.Upload",
            l["Menu:Upload"],
            url: "/Transactions/Upload"
        )
    )
);
    }
}
