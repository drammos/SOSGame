namespace SOS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartGame : ContentPage
    {
        public StartGame()
        {
            Console.WriteLine("eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee1\n");
            InitializeComponent();
            Console.WriteLine("eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee2\n");
            BindingContext = new SOSViewModel(); // Συνδέστε την σελίδα με το ViewModel
        }


    }
    public class SOSViewModel
    {
        public List<SOSItem> Board { get; set; }

        public SOSViewModel()
        {

            Console.WriteLine("eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee3\n");
            // Δημιουργήστε τον πίνακα 8x8 με αρχικές τιμές
            Board = new List<SOSItem>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.WriteLine("eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee3 ii == {0} kai jj == {1}\n", i, j);
                    Board.Add(new SOSItem { Symbol = "Χ" });
                }
            }
        }
    }

    public class SOSItem
    {
        public string Symbol { get; set; }
    }


}