using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace MMFinancial.Transactions
{
    public class Upload : Entity<Guid>
    {
        public DateTime TransactionDate { get; set; }
        public DateTime UploadDate { get; set; }
        public Guid? CreatorId { get; set; }

        private Upload() { }

        public Upload([NotNull]  DateTime transactionDate, [NotNull] DateTime uploadDate, Guid? creatorId)
        {
            TransactionDate = transactionDate;
            UploadDate = uploadDate;
            CreatorId = creatorId;
        }


    }
}
