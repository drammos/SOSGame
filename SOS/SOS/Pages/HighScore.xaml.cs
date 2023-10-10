namespace SOS;
using SOS.Models;
using SOS.ViewModel;
using System.Globalization;

public partial class HighScore : ContentPage
{
    public HighScoreViewModel highScoreViewModel;
    public HighScore(HighScoreViewModel highScoreViewModel)
    {
        InitializeComponent();
        this.highScoreViewModel = highScoreViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        List<User> highScores = await highScoreViewModel.TakeAllUsers();
        
        var sortedUsersByScoreDescending = highScores.OrderByDescending(user => user.Score).ToList();
        List<ListItem> listItems = new List<ListItem>();
        for(int i = 0; i < sortedUsersByScoreDescending.Count; i++)
        {
            User user = sortedUsersByScoreDescending[i];
            bool isMe = false;
            if(user.FilePath == null || user.FilePath.Length == 0)
            {
                user.FilePath = "user.png";
            }
            if(App.User.UserName == user.UserName)
            {
                isMe = true;
            }
            ListItem item = new ListItem(user,i+1,isMe);
            listItems.Add(item);
        }

        ViewUsersScores.ItemsSource = listItems;

    }
}

public class ListItem
{
    public User User { get; set; }
    public int Index { get; set; }
    public bool IsMe { get; set; }

    public ListItem(User user, int index, bool isMe)
    {
        this.User = user;
        this.Index = index;
        this.IsMe = isMe;
    }
}


    public class IsColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isMe)
            {
                return isMe ? Color.FromHex("#90e0ef") : Color.FromHex("#ffffff");
            }
            return Color.FromHex("#ffffff");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }