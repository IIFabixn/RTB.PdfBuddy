using RTB.PdfMate.Mobile.ViewModels;
namespace RTB.PdfMate.Mobile;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = new MainPageViewModel();
	}
}