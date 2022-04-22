using Volo.Abp.Settings;

namespace MMFinancial.Settings;

public class MMFinancialSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(MMFinancialSettings.MySetting1));
    }
}
