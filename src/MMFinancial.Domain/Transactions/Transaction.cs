using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace MMFinancial.Transactions
{
    public class Transaction : AuditedAggregateRoot<Guid>
    {
        public string BankFrom { get; set; }
        public string AgencyFrom { get; set; }
        public string AccountFrom { get; set; }
        public string BankTo { get; set; }
        public string AgencyTo { get; set; }
        public string AccounTo { get; set; }
        public double Value { get; set; }
        public DateTime _DateTime { get; set; }
        public Guid UploadId { get; set; }

        private Transaction()
        { }


        public Transaction(Guid id, string bankFrom, string agencyFrom, string accountFrom, string bankTo, string agencyTo, string accounTo, double value, DateTime dateTime, Guid uploadId) : base(id)
        {
            BankFrom = bankFrom;
            AgencyFrom = agencyFrom;
            AccountFrom = accountFrom;
            BankTo = bankTo;
            AgencyTo = agencyTo;
            AccounTo = accounTo;
            Value = value;
            _DateTime = dateTime;
            UploadId = uploadId;
        }

        public Transaction(Guid creatorId,Guid id, string bankFrom, string agencyFrom, string accountFrom, string bankTo, string agencyTo, string accounTo, double value, DateTime dateTime, Guid uploadId) : base(id)
        {
            CreatorId = creatorId;
            BankFrom = bankFrom;
            AgencyFrom = agencyFrom;
            AccountFrom = accountFrom;
            BankTo = bankTo;
            AgencyTo = agencyTo;
            AccounTo = accounTo;
            Value = value;
            _DateTime = dateTime;
            UploadId = uploadId;
        }
    }

}
