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
using Volo.Abp.Account;
using Volo.Abp.Account.Emailing;

[Volo.Abp.DependencyInjection.Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IAccountAppService), typeof(AccountAppService), typeof(AppAccountAppService))]
public class AppAccountAppService : AccountAppService
{
    //...
    private readonly IRepository<Volo.Abp.Identity.IdentityUser> _appIdentityUserRepository;
    private readonly IEmailSender _emailSender;
    public AppAccountAppService(
        IRepository<Volo.Abp.Identity.IdentityUser> appIdentityUserRepository,
        IEmailSender emailSender,
        IdentityUserManager userManager,
        IIdentityRoleRepository roleRepository,
        IAccountEmailer accountEmailer,
        IdentitySecurityLogManager identitySecurityLogManager,
        Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Identity.IdentityOptions> identityOptions
    ) : base(
        userManager,
        roleRepository,
        accountEmailer,
        identitySecurityLogManager,
        identityOptions)
    {
        _emailSender = emailSender;
        _appIdentityUserRepository = appIdentityUserRepository;
    }

    public async override Task<IdentityUserDto> RegisterAsync(RegisterDto input)
    {
        var user = await base.RegisterAsync(input);
        await EmailSenderService.SendEmailAsync("Setting Password", "Your password is: " + input.Password, user.Email);
        return user;
    }
}