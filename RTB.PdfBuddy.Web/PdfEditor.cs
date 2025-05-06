using System;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

namespace RTB.PdfBuddy.Web;

public class PdfEditor()
{
    public List<PdfDocument> Files { get; set; } = [];


    public void MovePagesToFile(PdfDocument file, params PdfPage[] pages)
    {
        foreach(var page in pages)
        {
            var addedPage = file.AddPage(page);
            addedPage.Tag = page.GetOrAssignId(); // Reassign the tag to maintain ID consistency

            // Remove the page from the original document
            var originalDocument = page.Owner;
            if (originalDocument != null)
            {
                originalDocument.Pages.Remove(page);
                if (originalDocument.PageCount == 0)
                {
                    Files.Remove(originalDocument);
                }
            }

            Console.WriteLine($"Page moved to file: {page} -> {file}");
        }
    }

    public void RemovePageFromFile(PdfDocument doc, PdfPage page)
    {
        doc.Pages.Remove(page);
    }

    public void MovePagesToNewFile(params PdfPage[] pages)
    {
        // Create a new empty PDF document
        var doc = new PdfDocument();
        doc.Info.Title = GetUniqueFileName("New Document");

        // We'll map old tags to maintain IDs
        var pageTags = new List<string>();

        // Add the pages to the new document
        foreach(var page in pages)
        {
            pageTags.Add(page.GetOrAssignId()); // store tag/id it for later reassignment

            var importedPage = doc.AddPage(page, AnnotationCopyingType.DeepCopy);
            var originalDocument = page.Owner;
            if (originalDocument != null)
            {
                // Remove the page from the original document
                originalDocument.Pages.Remove(page);
                if (originalDocument.PageCount == 0)
                {
                    Files.Remove(originalDocument);
                }
            }

            Console.WriteLine($"Page moved to new file: {page}");
        }

        // Save the document to a memory stream
        using var memoryStream = new MemoryStream();
        doc.Save(memoryStream, false); // Save the document to the stream
        memoryStream.Position = 0; // Reset the stream position

        // Reopen the document in Import mode (if needed for further operations)
        var importedDoc = PdfReader.Open(memoryStream, PdfDocumentOpenMode.Import);
        Console.WriteLine(importedDoc.Pages[0].Tag);

        // Re-assign the tags in same order
        for (int i = 0; i < importedDoc.PageCount && i < pageTags.Count; i++)
        {
            importedDoc.Pages[i].Tag = pageTags[i];
        }

        Files.Add(importedDoc);
    }

    public void MovePageToFileAtIndex(PdfDocument file, int index, params PdfPage[] pages)
    {
        foreach(var page in pages)
        {
            var pageCount = file.PageCount;
            if (index < 0 || index > pageCount)
            {
                Console.WriteLine($"Invalid index {index} for MovePageToFileAtIndex. Clamping...");
                index = Math.Clamp(index, 0, pageCount);
            }

            if (page.Owner == file)
            {
                // Moving within the same file
                var currentIndex = file.Pages.GetPageIndex(page);
                if (currentIndex == index || currentIndex == index - 1)
                    return;

                file.Pages.MovePage(currentIndex, index);
            }
            else
            {
                // Moving from a different file
                file.Pages.Insert(index, page);
                var originalDoc = page.Owner;
                originalDoc?.Pages.Remove(page);
                if (originalDoc?.PageCount == 0)
                {
                    Files.Remove(originalDoc);
                }
            }

            Console.WriteLine($"Page moved to file at index {index}: {page} -> {file}");
        }
    }

    public bool CanSplit => Files.Count > 0 && Files.Any(doc => doc.PageCount > 1);
    public void SplitAll()
    {
        if (!CanSplit) return;
        // Create a copy of the Files collection to iterate over
        var filesCopy = Files.ToList();

        foreach (var file in filesCopy)
        {
            Split(file);
        }
    }

    public void Split(PdfDocument doc)
    {
        if (doc.PageCount <= 1) return;

        for (int i = doc.PageCount - 1; i >= 0; i--) // Iterate backward
        {
            var page = doc.Pages[i];
            MovePagesToNewFile(page);
        }
    }

    public bool CanMerge => Files.Count > 1;
    public void MergeAll()
    {
        if (!CanMerge) return;

        var doc = Files.First();
        // Create a copy of the Files collection to iterate over
        var filesCopy = Files.ToList();
        foreach (var file in filesCopy.Skip(1))
        {
            for (var i = file.PageCount - 1; i >= 0; i--) // Iterate backward
            {
                var page = file.Pages[i];
                MovePagesToFile(doc, page);
            }
        }
    }

    public void AddFile(PdfDocument doc)
    {
        doc.Info.Title = GetUniqueFileName(doc.Info.Title ?? "New Document");
        Files.Add(doc);
    }

    public void AddFileRange(IEnumerable<PdfDocument> docs)
    {
        foreach(var doc in docs) doc.Info.Title = GetUniqueFileName(doc.Info.Title ?? "New Document");

        Files.AddRange(docs);
    }

    public void RemoveFile(PdfDocument doc)
    {
        if (!Files.Contains(doc)) return;
        Files.Remove(doc);
    }

    public void OnFileNameChange(PdfDocument file,string newName)
    {
        file.Info.Title = GetUniqueFileName(newName);
    }

    public string GetUniqueFileName(string baseName)
    {
        var existingNames = Files.Select(f => f.Info.Title).ToHashSet();
        var uniqueName = baseName;
        var counter = 1;

        while (existingNames.Contains(uniqueName))
        {
            uniqueName = $"{baseName} ({counter++})";
        }

        return uniqueName;
    }
}
