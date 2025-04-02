using RTB.PdfMate.Mobile.ViewModels;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using System.Threading.Tasks;

namespace RTB.PdfMate.Mobile;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = new MainPageViewModel();
	}

    private void DropGestureRecognizer_Drop(object sender, DropEventArgs e)
    {
    }

    private void DropGestureRecognizer_DragOver(object sender, DragEventArgs e)
    {
    }
}

