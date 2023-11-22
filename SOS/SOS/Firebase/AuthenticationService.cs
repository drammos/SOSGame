using Firebase.Auth;
namespace SOS.Firebase
{
    public class AuthenticationService
    {
        private string webApiKey = "AIzaSyDfi-MO4thwjK3ovRBUK4jfYbUiAVVkwow";
        private string oauthToken = "865933140258-ld1t2tl76c160kagq0aii595j56dvts6.apps.googleusercontent.com";
        private FirebaseAuthProvider authProvider;
        FirebaseAuthLink authLink;

        public AuthenticationService()
        {
             this.authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
        }

        public async Task<bool> Login(string email, string password)
        {
            try
            {
                authLink = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
                await SecureStorage.SetAsync("FirebaseRefreshToken", authLink.FirebaseToken);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Register(string email, string password)
        {
            try
            {
                authLink = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
                await SecureStorage.SetAsync("FirebaseRefreshToken", authLink.FirebaseToken);
                await Application.Current.MainPage.DisplayAlert("Success", "Registration successful", "OK");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public async Task<bool> RegisterWithGoogle()
        {
            try
            {
                //var result = await authProvider.SignInWithGoogleIdTokenAsync( oauthToken);
           
                string oauthToken1 = "865933140258 - 93ja0pcsps9ag4h7spj4bv4dam7ri78d.apps.googleusercontent.com";
                var result = await authProvider.SignInWithOAuthAsync(FirebaseAuthType.Google, oauthToken1);
                await SecureStorage.SetAsync("FirebaseRefreshToken", result.FirebaseToken);
                await Application.Current.MainPage.DisplayAlert("Success", "Registration successful", "OK");
                return true;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("FAILED", ex.ToString(), "OK");
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        
        public async Task<bool> LoginWithGoogle()
        {
            try
            {
                var result = await authProvider.SignInWithOAuthAsync(FirebaseAuthType.Google, oauthToken);
                await SecureStorage.SetAsync("FirebaseRefreshToken", result.FirebaseToken);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
