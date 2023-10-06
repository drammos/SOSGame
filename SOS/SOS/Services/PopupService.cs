using CommunityToolkit.Maui.Views;

namespace SOS.Services
{
    public class PopupService : IPopupService
    {
        public void ShowPopup(Popup popup)
        {
            Page page = Application.Current?.MainPage ?? throw new NullReferenceException();
            page.ShowPopup(popup);
        }

        public async Task<T> ShowPopup<T>(Popup popup)
        {
            Page page = Application.Current?.MainPage ?? throw new NullReferenceException();
            var result = await page.ShowPopupAsync(popup);
            return (T)result;
        }
    }
}
