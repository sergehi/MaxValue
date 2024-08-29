using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxValue.Models
{
    internal class FileArgs : EventArgs
    {
        public string FilePath { get; }
        public FileArgs(string filePath)
        {
            FilePath = filePath;
        }
    }
}
