using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using MMFinancial;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using System;
using MMFinancial.Transactions;

namespace MMFinancial.Web.Pages.Transactions
{
    public class UploadModel : AbpPageModel
    {
        [BindProperty]
        public UploadFileDto UploadFileDto { get; set; }

        private readonly IFileAppService _fileAppService;
        private readonly ITransactionAppService _transactionAppService;

        public bool Uploaded { get; set; } = false;

        public bool EmptyFile { get; set; }

        public UploadModel(IFileAppService fileAppService, ITransactionAppService transactionAppService)
        {
            _fileAppService = fileAppService;
            _transactionAppService = transactionAppService;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            EmptyFile = false;
            using (var memoryStream = new MemoryStream())
            {
                await UploadFileDto.File.CopyToAsync(memoryStream);

                await _fileAppService.SaveBlobAsync(
                    new SaveBlobInputDto
                    {
                        Name = UploadFileDto.Name,
                        Content = memoryStream.ToArray()
                    }
                );
            }

            //READ THE FILE:
            var streamDto = await _fileAppService.GetFileStreamAsync(new GetStreamRequestDto { Name = UploadFileDto.Name });
            StreamReader sr = new StreamReader(streamDto._Stream);
            string line = sr.ReadLine();
            if(line == null || line == "" )
            {
                EmptyFile = true;
                return Page();
            }
            string[] lineItems = line.Split(",");
            string shortDate = DateTime.Parse(lineItems[7]).ToShortDateString();

            while (line != null)
            {
                Console.WriteLine(line);
                CreateTransactionDto createTransactionDto = new CreateTransactionDto
                {
                    BankFrom = lineItems[0],
                    BankTo = lineItems[3],
                    AccountFrom = lineItems[2],
                    AccounTo = lineItems[5],
                    AgencyFrom = lineItems[1],
                    AgencyTo = lineItems[4],
                    Value = double.Parse(lineItems[6]),
                    _DateTime = DateTime.Parse(lineItems[7])
                };
                await _transactionAppService.CreateTransactionAsync(createTransactionDto);
                line = sr.ReadLine();
                if(line != null)
                {
                    lineItems = line.Split(',');
                }
                
            }
            

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