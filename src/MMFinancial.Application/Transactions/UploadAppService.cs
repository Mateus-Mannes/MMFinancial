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
        private readonly ITransactionRepository _transactionRepository;
        private readonly IFileAppService _fileAppService;


        public UploadAppService(IRepository<Upload, Guid> repository, ITransactionRepository transactionRepository, IFileAppService fileAppService)
        {
            _uploadRepository = repository;
            _transactionRepository = transactionRepository;
            _fileAppService = fileAppService;
        }

        public async Task DeleteAsync(Guid id, string fileName)
        {
            // TODO: make cascade delete
            IQueryable<Transaction> queryable = await _transactionRepository.GetQueryableAsync();
            await _transactionRepository.DeleteManyAsync(queryable.Where(x => x.UploadId == id).ToList());
            await _fileAppService.DeleteAsync(fileName);
            await _uploadRepository.DeleteAsync(id);
        }

        public async Task<List<UploadDto>> GetListAsync()
        {
            return ObjectMapper.Map<List<Upload>, List<UploadDto>>(await _uploadRepository.GetListAsync());
        }

        public async Task<Guid> CreateAsync(CreateUploadDto upload)
        {
            var _upload = await _uploadRepository.InsertAsync(ObjectMapper.Map<CreateUploadDto, Upload>(upload));
            return _upload.Id;
        }
    }
}
