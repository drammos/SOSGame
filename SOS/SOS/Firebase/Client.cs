using SOS.Models;
//using Plugin.CloudFirestore;

namespace SOS.Firebase
{
    public class Client
    {

        //private const string ProjectId = "sosgame-8dd1e";
        //private const string CollectionName = "Users";
        //// Εδώ ορίζουμε το URL της βάσης δεδομένων Firebase
        //private const string FirebaseUrl = "https://sosgame-8dd1e-default-rtdb.firebaseio.com/";

        public Client() 
        {
            ConnectToFirestore();
        }

        

        private async void ConnectToFirestore()
        {
            User user = new User()
            {
                UserName = "pr",
                Password = "1234",
                Email = "p@gmail.com",
                Theme = "Dark",
                FilePath = "ela.png",
                Score = 1

            };
            //await CrossCloudFirestore.Current
            //    .Instance
            //    .Collection("Users")
            //    //.Document("d@gmail.com")
            //    .AddAsync(user);
        }
    }
}
