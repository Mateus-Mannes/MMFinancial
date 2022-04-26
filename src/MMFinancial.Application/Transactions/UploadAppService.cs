using Microsoft.AspNetCore.Authorization;
using MMFinancial.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MMFinancial.Transactions
{
    [Authorize(MMFinancialPermissions.UserPermission)]
    public class UploadAppService : ApplicationService, IUploadAppService
    {
        private readonly IRepository<Upload, Guid> _uploadRepository;

        public UploadAppService(IRepository<Upload, Guid> repository)
        {
            _uploadRepository = repository; 
        }

        public async Task<List<UploadDto>> GetListAsync()
        {
            return ObjectMapper.Map<List<Upload>, List<UploadDto>>(await _uploadRepository.GetListAsync());
        }

        public async Task<Guid> CreateAsync(UploadDto upload)
        {
            var _upload = await _uploadRepository.InsertAsync(ObjectMapper.Map<UploadDto, Upload>(upload));
            return _upload.Id;
        }
    }
}
