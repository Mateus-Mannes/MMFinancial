using MMFinancial.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MMFinancial.Permissions;

public class MMFinancialPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MMFinancialPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(MMFinancialPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MMFinancialResource>(name);
    }
}
