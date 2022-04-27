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
using MMFinancial.FileReaders;

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
                FileReader fileReader = new CsvReader(sr);
                string[] lineItems = fileReader.ReadLine();
                if (lineItems == null)
                {
                    EmptyFile = true;
                    return Page();
                }
                DateTime firstDate;
                while (lineItems.Contains("") || lineItems.Contains(null))
                {
                    lineItems = fileReader.ReadLine();
                }
                firstDate = DateTime.Parse(lineItems[7]);
                if (await _transactionAppService.hasDate(firstDate))
                {
                    AlreadyUploadedDate = true;
                    return Page();
                }
                string fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + UploadFileDto.Name;
                var uploadId = await _uploadAppService.CreateAsync(new CreateUploadDto { TransactionDate = firstDate, UploadDate = DateTime.Now, CreatorId = CurrentUser.Id, FileName = fileName });
                while (lineItems != null)
                {
                    if (!lineItems.Contains("") && !lineItems.Contains(null))
                    {
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
                    lineItems = fileReader.ReadLine();
                }

                byte[] content = stream.GetAllBytes();

                await _fileAppService.SaveBlobAsync(
                    new SaveBlobInputDto
                    {
                        Name = fileName,
                        Content = content
                    }
                );
            }
            await _unitOfWorkManager.Current.SaveChangesAsync();
            UploadsList = await _uploadAppService.GetListAsync();
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