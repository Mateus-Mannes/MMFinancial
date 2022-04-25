using System;
using System.Collections.Generic;
using System.Text;

namespace MMFinancial.Transactions
{
    public class AccountMovimentationDto
    {
        public string Bank { get; set; }
        public string Agency{ get; set; }
        public string Account { get; set; }
        public string  Type { get; set; }
        public double ValueMoved { get; set; }
    }
}
