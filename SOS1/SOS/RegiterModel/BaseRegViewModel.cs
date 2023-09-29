using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOS.RegisterModel
{
    public partial class BaseRegViewModel : ObservableObject
    {
        [ObservableProperty]
        public bool _isBusy;


        [ObservableProperty]
        public string _title;
    }
}
