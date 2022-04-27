using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MMFinancial.FileReaders
{
    public class CsvReader : FileReader
    {
        public CsvReader(StreamReader sr) : base(sr)
        {
        }

        public override string[] ReadLine()
        {

            var line = Sr.ReadLine();
            string[] lineItems;
            if (line != null && line != "")
            {
                lineItems = line.Split(',');
                return lineItems;
            }
            return null;
        }
    }
}
