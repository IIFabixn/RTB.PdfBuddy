using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharpCore.Pdf.IO;

namespace RTB.PdfBuddy.Shared
{
    public class FileModel
    {
        private string? _originalFileName;
        private string? _name;
        public string Name 
        { 
            get => string.IsNullOrEmpty(_name) ? _originalFileName ?? string.Empty : _name;
            set 
            {
                _originalFileName ??= value;
                _name = value;
            }
        }
        public string? OriginalName => _originalFileName ?? string.Empty;
        public long Size { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public int PageCount { get; set; }
    }
}
