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
            using (var stream = UploadFileDto.File.OpenReadStream())
            {
                StreamReader sr = new StreamReader(stream);
                FileReader fileReader = GetFileReader(UploadFileDto.Name, sr);
                string[] lineItems = fileReader.ReadLine();
                if (!await ValidFileAsync(lineItems, fileReader))
                {
                    UploadsList = await _transactionAppService.GetUploadsHistoryAsync();
                    return Page();
                }
                DateTime firstDate = DateTime.Parse(lineItems[7]);
                string fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + UploadFileDto.Name;
                var uploadId = await _uploadAppService.CreateAsync(new CreateUploadDto { TransactionDate = firstDate, UploadDate = DateTime.Now, CreatorId = CurrentUser.Id, FileName = fileName });
                await ReadAndInsertTransactions(fileReader, lineItems, firstDate, uploadId);
                await SaveBlobAsync(stream, fileName);
            }
            await _unitOfWorkManager.Current.SaveChangesAsync();
            UploadsList = await _uploadAppService.GetListAsync();
            return Page();
        }

        private FileReader GetFileReader(string fileName, StreamReader sr)
        {
            if(fileName.Split('.').Last().ToLower() == "csv")
            {
                return new CsvReader(sr);
            }
            else
            {
                return new XmlReader(sr);
            }
        }

        private async Task<bool> ValidFileAsync(string[] lineItems, FileReader fileReader)
        {
            if (lineItems == null)
            {
                EmptyFile = true;
                return false;
            }
            while (lineItems.Contains("") || lineItems.Contains(null))
            {
                lineItems = fileReader.ReadLine();
            }
            DateTime firstDate;
            firstDate = DateTime.Parse(lineItems[7]);
            if (await _transactionAppService.hasDate(firstDate))
            {
                AlreadyUploadedDate = true;
                return false;
            }
            return true;

        }

        private async Task ReadAndInsertTransactions(FileReader fileReader, string[] lineItems, DateTime firstDate, Guid uploadId)
        {
            while (lineItems != null)
            {
                if (!lineItems.Contains("") && !lineItems.Contains(null))
                {
                    DateTime date = DateTime.Parse(lineItems[7]);
                    if (date.Date == firstDate.Date)
                    {
                        CreateTransactionDto createTransactionDto = LineItemsToTransactionCreateDto(lineItems, uploadId);
                        await _transactionAppService.CreateTransactionAsync(createTransactionDto);
                    }
                }
                lineItems = fileReader.ReadLine();
            }
        }

        private CreateTransactionDto LineItemsToTransactionCreateDto(string[] lineItems, Guid uploadId)
        {
            return new CreateTransactionDto
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
        }

        private async Task SaveBlobAsync(Stream stream, string fileName)
        {
            byte[] content = stream.GetAllBytes();

            await _fileAppService.SaveBlobAsync(
                new SaveBlobInputDto
                {
                    Name = fileName,
                    Content = content
                }
            );
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