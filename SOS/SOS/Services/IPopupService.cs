using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOS.Services
{
    public interface IPopupService
    {
        Task<T> ShowPopup<T>(Popup popup);
        void ShowPopup(Popup popup);
    }
}
