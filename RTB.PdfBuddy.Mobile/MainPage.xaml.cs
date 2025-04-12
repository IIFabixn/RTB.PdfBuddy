using RTB.PdfBuddy.Mobile.ViewModels;
namespace RTB.PdfBuddy.Mobile;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = new MainPageViewModel();
	}
}