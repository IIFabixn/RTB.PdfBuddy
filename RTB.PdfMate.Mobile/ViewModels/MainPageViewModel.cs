using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

namespace RTB.PdfMate.Mobile.ViewModels;

public class MainPageViewModel : ObservableObject
{
    public ObservableCollection<PdfDocument> LoadedDocuments { get; } = [];
    private PdfDocument? _selectedDocument;
    public PdfDocument? SelectedDocument { get => _selectedDocument; set => SetProperty(ref _selectedDocument, value); }
    public IAsyncRelayCommand OpenFileExplorerCommand { get;}

    private ObservableCollection<KeyValuePair<string, ObservableCollection<PdfDocument>>> _splitDocuments = new();

    public MainPageViewModel()
    {
        OpenFileExplorerCommand = new AsyncRelayCommand(OpenFileExplorer);
    }

    private async Task OpenFileExplorer()
    {
        var result = await FilePicker.PickMultipleAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Pdf,
        });

        foreach (var file in result)
        {
            var inputDocument = PdfReader.Open(file.FullPath, PdfDocumentOpenMode.Import);
            if (inputDocument != null)
                LoadedDocuments.Add(inputDocument);
        }
    }
}
