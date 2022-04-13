using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace FileActionsDemo
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
            var teste = File(fileDto.Content, "application/octet-stream", fileDto.Name);
            string strFileData = System.Text.Encoding.UTF8.GetString(teste.FileContents, 0, teste.FileContents.Length);
            return File(fileDto.Content, "application/octet-stream", fileDto.Name);
        }
    }
}