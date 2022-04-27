using System.IO;
using System.Threading.Tasks; 
using Volo.Abp.Application.Services; 
namespace MMFinancial
{ 
    public interface IFileAppService : IApplicationService
    { 
        Task SaveBlobAsync(SaveBlobInputDto input);
        Task<BlobDto> GetBlobAsync(GetBlobRequestDto input);
        Task<StreamDto> GetFileStreamAsync(GetStreamRequestDto input);

        Task DeleteAsync(string name);
    } 
}