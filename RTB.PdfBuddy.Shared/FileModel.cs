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
        public string Name { get; set; } = string.Empty;
        public long Size { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public int PageCount { get; set; }
        public Byte[]? FileContent { get; set; } = null;
    }

    public static class FileModelExtensions
    {
        public static void GetPageCount(this FileModel model, Stream? stream)
        {
            if (stream is null)
            {
                Console.WriteLine("Stream is null");
                model.PageCount = 0;
                return;
            }

            try
            {
                using var pdfDocument = PdfReader.Open(stream, PdfDocumentOpenMode.ReadOnly);
                Console.WriteLine($"PageCount: {pdfDocument.PageCount}");
                model.PageCount = pdfDocument.PageCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Handle exceptions (e.g., invalid file format, file not found, etc.)
                model.PageCount = 0;
            }
        }
    }
}
