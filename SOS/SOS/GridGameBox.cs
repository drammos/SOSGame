using CommunityToolkit.Mvvm.ComponentModel;
namespace SOS.Box
{
    public partial class GridGameBox : ObservableObject
    {
        public int Index { get; set; }

        [ObservableProperty]
        private string _selectedText;

        [ObservableProperty]
        private Color _textColor;

        [ObservableProperty]
        private Color _cellColor;

        public int? Player { get; set; }

        public void SetText(string text, int player)
        {
            this.SelectedText = text;
            this.Player = player;
            if (this.SelectedText == "S")
            {
                this.TextColor = Color.FromHex("#0000FF");
            }
            else
            {
                this.TextColor = Color.FromHex("#FF0000");
            }
        }
        public GridGameBox(int index)
        {
            this.Index = index;
            this.CellColor = Color.FromHex("#fff");
        }

        public void SetDisableColors()
        {
            if (Application.Current.UserAppTheme == AppTheme.Dark)
            {
                this.CellColor = Color.FromHex("#52b788");
            }
            else
            {
                this.CellColor = Color.FromHex("#a9def9");
            }
        }
    }
}
