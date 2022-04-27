using Microsoft.AspNetCore.Authorization;
using MMFinancial.Permissions;
using System.IO;
using System.Threading.Tasks; 
using Volo.Abp.Application.Services; 
using Volo.Abp.BlobStoring; 
namespace MMFinancial
{
    [Authorize(MMFinancialPermissions.UserPermission)]
    public class FileAppService: ApplicationService, IFileAppService
    { 
        private readonly IBlobContainer<TransactionContainer> _fileContainer; 
        public FileAppService(IBlobContainer < TransactionContainer > fileContainer){
            _fileContainer = fileContainer; 
        } 
        public async Task SaveBlobAsync(SaveBlobInputDto input){
            await _fileContainer.SaveAsync(input.Name, input.Content, true); 
        } 
        public async Task<BlobDto> GetBlobAsync(GetBlobRequestDto input) { 
            var blob = await _fileContainer.GetAllBytesAsync(input.Name); 
            return new BlobDto{ Name = input.Name,Content = blob};
        } 
        public async Task DeleteAsync(string name)
        {
            await _fileContainer.DeleteAsync(name);
        }

        public async Task<StreamDto> GetFileStreamAsync(GetStreamRequestDto input)
        {
            Stream stream = await _fileContainer.GetAsync(input.Name);
            return new StreamDto { Name = input.Name, _Stream = stream};
        }
    } 
}