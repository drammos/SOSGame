using CommunityToolkit.Maui.Views;

namespace SOS.Services
{
    public interface IPopupService
    {
        Task<T> ShowPopup<T>(Popup popup);

        void ShowPopup(Popup popup);
    }
}
