using System;
using Microsoft.AspNetCore.Components.Forms;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
namespace RTB.PdfBuddy.Web.Extensions;

public static class BrowserFileExtension
{
    public static Task<PdfDocument> LoadPdfDocument(this IBrowserFile file)
    {
        if (file is null)
        {
            throw new ArgumentNullException(nameof(file), "File cannot be null.");
        }

        if (file.ContentType != "application/pdf")
        {
            throw new InvalidOperationException("The file is not a PDF document.");
        }

        if (file.Size == 0)
        {
            throw new InvalidOperationException("The file is empty.");
        }

        return Task.Run(async () =>
        {
            using var stream = file.OpenReadStream(file.Size); // Read the entire file
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream); // Copy to a MemoryStream
            memoryStream.Position = 0; // Reset the position to the beginning
            var document = PdfReader.Open(memoryStream, PdfDocumentOpenMode.Import);
            document.Info.Title = file.Name;
            foreach(var page in document.Pages)
            {
                page.GetOrAssignId();
            }

            return document;
        });
    }
}
