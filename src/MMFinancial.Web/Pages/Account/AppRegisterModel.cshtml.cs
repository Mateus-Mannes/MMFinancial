using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Account.Web.Pages.Account;
using Volo.Abp.Account;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;
using Volo.Abp.Auditing;
using System;
using System.Linq;
using Volo.Abp.Domain.Repositories;

namespace MMFinancial.Web.Pages.Account
{
    public class AppRegisterModel : RegisterModel
    {
        public bool alreadyRegistered;
        private readonly IAccountAppService _accountAppService1;
        private readonly IRepository<IdentityUser> _identityUserRepository;
        public AppRegisterModel(
            IAccountAppService accountAppService,
            IAccountAppService accountAppService1,
            IRepository<IdentityUser> identityUserRepository
            ) : base(
                accountAppService)
        {
            _accountAppService1 = accountAppService;
            _identityUserRepository = identityUserRepository;
        }


        public async override Task<IActionResult> OnPostAsync()
        {
            var queryable = await _identityUserRepository.GetQueryableAsync();
            if (queryable.Any(x => x.Email == Input.EmailAddress || x.UserName == Input.UserName))
            {
                alreadyRegistered = true;
                return Page();
            }
            else
            {
                Random random = new Random();
                Input.Password = (random.Next() % 1000000).ToString() + "FFa*";
                await _accountAppService1.RegisterAsync(
                new RegisterDto
                {
                    AppName = "MVC",
                    EmailAddress = Input.EmailAddress,
                    Password = Input.Password,
                    UserName = Input.UserName
                }
            );
                return Redirect("/Account/Registered");
            }
        }

        public class PostInput
        {
            [Required]
            [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxUserNameLength))]
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxEmailLength))]
            public string EmailAddress { get; set; }
        }
    }
}
