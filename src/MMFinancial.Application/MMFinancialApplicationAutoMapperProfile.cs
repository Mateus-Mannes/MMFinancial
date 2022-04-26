using AutoMapper;
using MMFinancial.Transactions;

namespace MMFinancial;

public class MMFinancialApplicationAutoMapperProfile : Profile
{
    public MMFinancialApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Transaction, TransactionDto>();
        CreateMap<UploadDto, Upload>();
        CreateMap<Upload, UploadDto>();
    }
}
