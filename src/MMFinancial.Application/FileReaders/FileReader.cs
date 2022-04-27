using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MMFinancial.FileReaders
{
     public abstract class FileReader
    {
        public StreamReader Sr;
        public abstract string[] ReadLine();
        public FileReader(StreamReader sr)
        {
            Sr = sr;
        }
    }
}
