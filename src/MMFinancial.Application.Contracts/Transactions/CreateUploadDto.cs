﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MMFinancial.Transactions
{
    public class CreateUploadDto
    {
        public DateTime TransactionDate { get; set; }
        public DateTime UploadDate { get; set; }

        public Guid? CreatorId { get; set; }
        public string FileName { get; set; }
    }
}
