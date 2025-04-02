using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PdfSharpCore.Pdf;

namespace RTB.PdfMate.Mobile.ViewModels;

public class MainPageViewModel : ObservableObject
{
    public ObservableCollection<PdfDocument> LoadedDocuments { get; } = [];
    private PdfDocument? _selectedDocument;
    public PdfDocument? SelectedDocument { get => _selectedDocument; set => SetProperty(ref _selectedDocument, value); }
    public IRelayCommand<PdfDocument> AddPdfCommand { get; }
    public IRelayCommand<PdfDocument> RemovePdfCommand { get; }
    public IRelayCommand SplitAllCommand { get; }
    public IRelayCommand OnDropCommand { get; }

    private ObservableCollection<KeyValuePair<string, ObservableCollection<PdfDocument>>> _splitDocuments = new();

    public MainPageViewModel()
    {
        AddPdfCommand = new RelayCommand<PdfDocument>((doc) => { if (doc is not null) LoadedDocuments.Add(doc); });
        RemovePdfCommand = new RelayCommand<PdfDocument>((doc) => { if (doc is not null) LoadedDocuments.Remove(doc); });
        SplitAllCommand = new RelayCommand(SplitAllDocuments);
        OnDropCommand = new RelayCommand<DragEventArgs>(OnDrop);
    }

    private void OpenFile(string? obj)
    {
    }

    private void OnDrop(DragEventArgs? args)
    {
    }

    private void SplitAllDocuments()
    {
        _splitDocuments.Clear();

        for (var docIndex = 0; docIndex < LoadedDocuments.Count; docIndex++)
        {
            var doc = LoadedDocuments[docIndex];

            var splitDoc = new ObservableCollection<PdfDocument>();
            for(var pageIndex = 0; pageIndex < doc.PageCount; pageIndex++)
            {
                var newDoc = new PdfDocument($"{doc.Info.Title}_{pageIndex}");
                newDoc.AddPage(doc.Pages[pageIndex]);
                splitDoc.Add(newDoc);
            }

            _splitDocuments.Add(new KeyValuePair<string, ObservableCollection<PdfDocument>>($"{doc.Info.Title}", splitDoc));
        }
    }
}
