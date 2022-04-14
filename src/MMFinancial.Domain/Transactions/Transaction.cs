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
        public string  BankFrom { get; set; }
        public string AgencyFrom { get; set; }
        public string AccountFrom { get; set; }
        public string BankTo { get; set; }
        public string AgencyTo { get; set; }
        public string AccounTo { get; set; }
        public double Value { get; set; }
        public DateTime _DateTime { get; set; }
    }
}
