

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Validation;
using System.Linq;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Emailing;
using Volo.Abp.ObjectExtending;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using MMFinancial.Transactions;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.ObjectMapping;

[Volo.Abp.DependencyInjection.Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IIdentityUserAppService), typeof(IdentityUserAppService), typeof(AppIdentityUserAppService))]
public class AppIdentityUserAppService : IdentityUserAppService
{
    //...
    private readonly IRepository<Volo.Abp.Identity.IdentityUser> _appIdentityUserRepository;
    private readonly IEmailSender _emailSender;
    public AppIdentityUserAppService(
        IRepository<Volo.Abp.Identity.IdentityUser> appIdentityUserRepository,
        IEmailSender emailSender,
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
        _emailSender = emailSender;
        _appIdentityUserRepository = appIdentityUserRepository;
    }

    public async override Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input)
    {
        await IdentityOptions.SetAsync();
        var user = new Volo.Abp.Identity.IdentityUser(
            GuidGenerator.Create(),
            input.UserName,
            input.Email,
            CurrentTenant.Id
        );
        input.MapExtraPropertiesTo(user);
        (await UserManager.CreateAsync(user, input.Password)).CheckErrors();
        await UpdateUserByInput(user, input);
        (await UserManager.UpdateAsync(user)).CheckErrors();
        await CurrentUnitOfWork.SaveChangesAsync();

            await AppEmailSender.SendEmailAsync("Setting Password", "Your password is: " + input.Password, user.Email);
     
   
        return ObjectMapper.Map<Volo.Abp.Identity.IdentityUser, IdentityUserDto>(user);
    }

    public async override Task<PagedResultDto<IdentityUserDto>> GetListAsync(GetIdentityUsersInput input)
    {
        IQueryable<Volo.Abp.Identity.IdentityUser> queryable = await _appIdentityUserRepository.GetQueryableAsync();
        var count = await UserRepository.GetCountAsync(input.Filter);
        List<Volo.Abp.Identity.IdentityUser> list = queryable.Where(x => x.Email != "admin@abp.io").ToList();

        return new PagedResultDto<IdentityUserDto>(
            count,
            ObjectMapper.Map<List<Volo.Abp.Identity.IdentityUser>, List<IdentityUserDto>>(list)
        );
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
