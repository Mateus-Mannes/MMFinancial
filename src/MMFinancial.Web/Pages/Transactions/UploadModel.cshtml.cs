using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using MMFinancial;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using System;
using System.Linq;
using MMFinancial.Transactions;
using System.Collections.Generic;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace MMFinancial.Web.Pages.Transactions
{
    public class UploadModel : AbpPageModel
    {
        [BindProperty]
        public UploadFileDto UploadFileDto { get; set; }

        private readonly IFileAppService _fileAppService;
        private readonly ITransactionAppService _transactionAppService;
        private readonly IUploadAppService _uploadAppService;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public bool AlreadyUploadedDate { get; set; }

        public bool EmptyFile { get; set; }
        public IEnumerable<UploadDto> UploadsList { get; set; }

        public UploadModel(IFileAppService fileAppService, ITransactionAppService transactionAppService, IUploadAppService uploadAppService, IUnitOfWorkManager unitOfWorkManager)
        {
            _fileAppService = fileAppService;
            _transactionAppService = transactionAppService;
            _uploadAppService = uploadAppService;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task OnGetAsync()
        {
            UploadsList = await _uploadAppService.GetListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            UploadsList = await _transactionAppService.GetUploadsHistoryAsync();
            EmptyFile = false;
            AlreadyUploadedDate = false;
            using (var stream = UploadFileDto.File.OpenReadStream())
            {
                StreamReader sr = new StreamReader(stream);
                string line = sr.ReadLine();
                if (line == null || line == "")
                {
                    EmptyFile = true;
                    return Page();
                }
                DateTime firstDate;
                while (line.Contains(",,"))
                {
                    line = sr.ReadLine();
                }
                firstDate = DateTime.Parse(line.Split(',')[7]);
                if (await _transactionAppService.hasDate(firstDate))
                {
                    AlreadyUploadedDate = true;
                    return Page();
                }
                var uploadId = await _uploadAppService.CreateAsync(new UploadDto { TransactionDate = firstDate, UploadDate = DateTime.Now, CreatorId = CurrentUser.Id });
                while (line != null)
                {
                    if (!line.Contains(",,"))
                    {
                        string[] lineItems = line.Split(",");
                        DateTime date = DateTime.Parse(lineItems[7]);
                        if (date.Date == firstDate.Date)
                        {
                            CreateTransactionDto createTransactionDto = new CreateTransactionDto
                            {
                                BankFrom = lineItems[0],
                                BankTo = lineItems[3],
                                AccountFrom = lineItems[2],
                                AccounTo = lineItems[5],
                                AgencyFrom = lineItems[1],
                                AgencyTo = lineItems[4],
                                Value = double.Parse(lineItems[6]),
                                _DateTime = DateTime.Parse(lineItems[7]),
                                UploadId = uploadId
                            };
                            await _transactionAppService.CreateTransactionAsync(createTransactionDto);
                        }
                    }
                    line = sr.ReadLine();
                }

                byte[] content = stream.GetAllBytes();

                await _fileAppService.SaveBlobAsync(
                    new SaveBlobInputDto
                    {
                        Name = UploadFileDto.Name,
                        Content = content
                    }
                );
            }
            UploadsList = await _transactionAppService.GetUploadsHistoryAsync();
            return Page();
        }
    }

    public class UploadFileDto
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile File { get; set; }

        [Required]
        //[Display(Name = "Filename")]
        public string Name { get; set; }
    }
}