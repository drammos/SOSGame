namespace SOS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartGame : ContentPage
    {
        public StartGame()
        {
            InitializeComponent();
            BindingContext = new SOSViewModel(); // Συνδέστε την σελίδα με το ViewModel
        }


    }
    public class SOSViewModel
    {
        public List<SOSItem> Board { get; set; }

        public SOSViewModel()
        {
            // Δημιουργήστε τον πίνακα 8x8 με αρχικές τιμές
            Board = new List<SOSItem>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Board.Add(new SOSItem { Symbol = "" });
                }
            }
        }
    }

    public class SOSItem
    {
        public string Symbol { get; set; }
    }


}