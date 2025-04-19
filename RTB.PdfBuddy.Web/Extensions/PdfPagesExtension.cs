using PdfSharpCore.Pdf;

namespace RTB.PdfBuddy.Web.Extensions
{
    public static class PdfPagesExtension
    {
        public static int GetPageIndex(this PdfPages pages, PdfPage page)
        {
            for (int i = 0; i < pages.Count; i++)
            {
                if (pages[i] == page)
                    return i;
            }
            return -1;
        }
    }
}
