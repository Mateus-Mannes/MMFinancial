

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

[Volo.Abp.DependencyInjection.Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IIdentityUserAppService), typeof(IdentityUserAppService), typeof(AppIdentityUserAppService))]
public class AppIdentityUserAppService : IdentityUserAppService
{
    //...
    public AppIdentityUserAppService(
        IdentityUserManager userManager,
        IIdentityUserRepository userRepository,
        IIdentityRoleRepository roleRepository,
        Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Identity.IdentityOptions> identityOptions
    ) : base(
        userManager,
        userRepository,
        roleRepository,
        identityOptions)
    {
    }

    public async override Task<IdentityUserDto> UpdateAsync(Guid id, IdentityUserUpdateDto input)
    {
        if (await IsAdmin(id))
        {
            throw new BusinessException(
                "Cant edit Admin"
            );
        }
        return await base.UpdateAsync(id, input);
    }

    public  async override Task DeleteAsync(Guid id)
    {
        if (await IsAdmin(id))
        {
            throw new BusinessException(
                "Cant delte Admin"
            );
        }

        await base.DeleteAsync(id);
    }

    public async Task<bool> IsAdmin(Guid id)
    {
        var user = await UserManager.FindByIdAsync(id.ToString());
        return user.Email == "admin@abp.io";
    }
}
