using System;
using System.Collections.Generic;
using System.Text;

namespace MMFinancial.Transactions
{
    public class TransactionDto
    {
        public string BankFrom { get; set; }
        public string AgencyFrom { get; set; }
        public string AccountFrom { get; set; }
        public string BankTo { get; set; }
        public string AgencyTo { get; set; }
        public string AccounTo { get; set; }
        public double Value { get; set; }
        public DateTime _DateTime { get; set; }
    }
}
