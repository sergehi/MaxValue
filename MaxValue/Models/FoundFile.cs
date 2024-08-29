using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxValue.Interfaces;

namespace MaxValue.Models
{
    internal class FoundFile : IPropertiesProvider
    {
        private string _fileName;
        private float _fileValue;

        public FoundFile(string name)
        {
            _fileName = name;
            _fileValue =  new System.IO.FileInfo(_fileName).Length;
        }

        #region IPropertiesProvider implementation
        public string GetName() => _fileName;
        public float GetValue() => _fileValue;
        #endregion
    }
}
