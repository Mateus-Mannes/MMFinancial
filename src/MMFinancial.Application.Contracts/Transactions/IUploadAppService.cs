using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MMFinancial.Transactions
{
    public interface IUploadAppService : IApplicationService
    {
        Task<Guid> CreateAsync(CreateUploadDto upload);
        Task<List<UploadDto>> GetListAsync();
        Task DeleteAsync(Guid id, string name);
    }
}
