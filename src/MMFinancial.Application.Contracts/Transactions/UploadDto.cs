using System;
using System.Collections.Generic;
using System.Text;

namespace MMFinancial.Transactions
{
    public class UploadDto
    {
        public DateTime TransactionDate { get; set; }
        public DateTime UploadDate { get; set; }

        public Guid? CreatorId { get; set; }
    }
}
