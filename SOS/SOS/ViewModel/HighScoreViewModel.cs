using SOS.Services;
using SOS.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace SOS.ViewModel
{
    public partial class HighScoreViewModel : BaseViewModel
    {
        public IUsersRepo Users;

        public ObservableCollection<ListItem> ListItems { get; set; } = new ObservableCollection<ListItem>();

        private List<ListItem> InitializeList {  get; set; }

        public HighScoreViewModel(IUsersRepo users)
        {
            this.Users = users;
            InitializeList = new List<ListItem>();
        }

        [RelayCommand]
        public async Task SearchUsers(string Text)
        {
            ListItems.Clear();

            if (string.IsNullOrEmpty(Text))
            {
                foreach (var us in InitializeList)
                {
                    ListItems.Add(us);
                }
            }
            else
            {
                for (int i = 0; i < InitializeList.Count; i++)
                {
                    string username = InitializeList[i].User.UserName;
                    if (username.Contains(Text))
                    {
                        ListItem item = InitializeList[i];
                        bool first = false;
                        bool second = false;
                        bool third = false;
                        if (i == 0) first = true;
                        else if (i == 1) second = true;
                        else if (i == 2) third = true;
                        item.IsFirst = first;
                        item.IsSecond = second;
                        item.IsThird = third;

                        ListItems.Add(item);
                    }
                }
            }
        }

        [RelayCommand]
        public async Task InitializeUsers()
        {
            
            ListItems.Clear();
            InitializeList = new List<ListItem>();
            List<User> list = await this.Users.TakeAllUsers();
            var sortedUsersByScoreDescending = list.OrderByDescending(user => user.Score).ToList();
            for (int i = 0; i < sortedUsersByScoreDescending.Count; i++)
            {
                User user = sortedUsersByScoreDescending[i];
                bool isMe = false;
                if (App.User.UserName == user.UserName)
                {
                    isMe = true;
                }
                bool first = false;
                bool second = false;
                bool third = false;
                if (i == 0) first = true;
                else if (i == 1) second = true;
                else if (i == 2) third = true;
                ListItem item = new ListItem(user, i + 1, isMe, first, second, third);
                ListItems.Add(item);
                InitializeList.Add(item);
            }
        }
    }

    public class ListItem
    {
        public User User { get; set; }
        public int Index { get; set; }
        public bool IsMe { get; set; }
        public bool IsFirst { get; set; }
        public bool IsSecond { get; set; }
        public bool IsThird { get; set; }

        public ListItem(User user, int index, bool isMe, bool first, bool second, bool third)
        {
            this.User = user;
            this.Index = index;
            this.IsMe = isMe;
            this.IsFirst = first;
            this.IsSecond = second;
            this.IsThird = third;
        }
    }

}