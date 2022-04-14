using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace MMFinancial
{
    public class FileController : AbpController
    {
        private readonly IFileAppService _fileAppService;

        public FileController(IFileAppService fileAppService)
        {
            _fileAppService = fileAppService;
        }

        [HttpGet]
        [Route("download/{fileName}")]
        public async Task<IActionResult> DownloadAsync(string fileName)
        {
            var fileDto = await _fileAppService.GetBlobAsync(new GetBlobRequestDto { Name = fileName });
            return File(fileDto.Content, "application/octet-stream", fileDto.Name);

            //READ THE FILE:
            //var streamDto = await _fileAppService.GetFileStreamAsync(new GetStreamRequestDto { Name = fileName });
            //StreamReader sr = new StreamReader(streamDto._Stream);
            //string line = sr.ReadLine();
        }
    }
}