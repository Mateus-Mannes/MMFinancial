using MMFinancial.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using static Volo.Abp.Identity.IdentityPermissions;

namespace MMFinancial.Permissions;

public class MMFinancialPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MMFinancialPermissions.GroupName);
        //Define your own permissions here. Example:
        var x = myGroup.AddPermission(MMFinancialPermissions.UserPermission, L("Permission:UserPermission"));
      

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MMFinancialResource>(name);
    }
}
