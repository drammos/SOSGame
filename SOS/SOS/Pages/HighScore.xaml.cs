namespace SOS;
using SOS.ViewModel;
using System.Globalization;

public partial class HighScore : ContentPage
{
    public HighScoreViewModel highScoreViewModel;
    public HighScore(HighScoreViewModel highScoreViewModel)
    {
        InitializeComponent();
        this.BindingContext = highScoreViewModel;
        this.highScoreViewModel = highScoreViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await highScoreViewModel.InitializeUsers();
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (((SearchBar)sender).SearchCommand?.CanExecute(e.NewTextValue) == true)
            ((SearchBar)sender).SearchCommand?.Execute(e.NewTextValue);
    }
}

public class IsColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
            
        if (value is bool isMe)
        {
            string theme = "#90e0ef";
            if (Application.Current.UserAppTheme == AppTheme.Dark)
            {
                theme = "#52b788";
            } 
            return isMe ? Color.FromHex(theme) : Color.FromHex("#ffffff");
        }
        return Color.FromHex("#ffffff");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}