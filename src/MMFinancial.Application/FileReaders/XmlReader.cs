using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MMFinancial.Transactions;

namespace MMFinancial.FileReaders
{
    public class XmlReader : FileReader
    {
        public XmlReader(StreamReader sr) : base(sr)
        {
        }

        public override string[] ReadLine()
        {
            var line = Sr.ReadLine();
            if (line == null || line == "")
            {
                return null;
            }
            if (line.StartsWith("<?xml")){
                Sr.ReadLine();
                line = Sr.ReadLine();
            }
            if (line == null || line == "")
            {
                return null;
            }
            string[] items = new string[TransactionsConsts.TransactionAttributesCount];
            int valueIndex = 0;
            string transactionTag = line.Substring(line.IndexOf('<') + 1, line.IndexOf('>') - line.IndexOf('<') -1);
            if("<"+transactionTag+">" == line)
            {
                return null;
            }
            line = Sr.ReadLine();
            while (line != "</" + transactionTag + ">" && valueIndex < TransactionsConsts.TransactionAttributesCount && line != null && line != "")
            {
                int initialIndex = line.IndexOf('>');
                int endlIndex = line.IndexOf("</");
                if(initialIndex > endlIndex)
                {
                    line = Sr.ReadLine();
                    continue;
                }
                string value = line.Substring(initialIndex + 1, endlIndex - (initialIndex + 1));
                items[valueIndex] = value;
                valueIndex++;
                line = Sr.ReadLine();
            }
            return items;
        }
    }
}
