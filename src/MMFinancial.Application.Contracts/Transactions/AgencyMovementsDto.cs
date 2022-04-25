using System;
using System.Collections.Generic;
using System.Text;

namespace MMFinancial.Transactions
{
    public class AgencyMovementsDto
    {
        public string Bank { get; set; }
        public string Agency { get; set; }
        public string Type { get; set; }
        public double ValueMoved { get; set; }
    }
}
