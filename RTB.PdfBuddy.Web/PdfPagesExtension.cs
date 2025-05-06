using PdfSharpCore.Pdf;
using System.Buffers.Text;

namespace RTB.PdfBuddy.Web
{
    public static class PdfPagesExtension
    {
        public static int GetPageIndex(this PdfDocument doc, PdfPage page)
        {
            return doc.Pages.GetPageIndex(page);
        }

        public static int GetPageIndex(this PdfPages pages, PdfPage page)
        {
            for (int i = 0; i < pages.Count; i++)
            {
                if (pages[i] == page)
                    return i;
            }

            return -1;
        }

        public static PdfPage[] GetPagesAsArray(this PdfDocument doc)
        {
            var pages = new PdfPage[doc.PageCount];
            for (int i = 0; i < doc.PageCount; i++)
            {
                pages[i] = doc.Pages[i];
            }
            return pages;
        }

        public static byte[] ExtractPageAsBytes(this PdfPage page)
        {
            // Create a new document
            var singlePageDoc = new PdfDocument();

            // Optionally copy metadata
            singlePageDoc.Info.Title = "Preview Page";

            // Add the page — use DeepCopy to fully clone it
            singlePageDoc.AddPage(page, AnnotationCopyingType.DeepCopy);

            // Save to memory stream
            using var ms = new MemoryStream();
            singlePageDoc.Save(ms, false);
            return ms.ToArray();
        }

        public static string GetOrAssignId(this PdfPage page)
        {
            if (page.Tag is string id)
                return id;

            id = Guid.NewGuid().ToString("N");
            page.Tag = id;
            return id;
        }
    }
}
