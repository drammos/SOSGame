using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOS.Box
{
    public partial class GridGameBox : ObservableObject
    {

        public GridGameBox(int index)
        {
            this.Index = index;
        }

        public int Index { get; set; }

        [ObservableProperty]
        private string _selectedText;


        public int? Player { get; set; }
    }
}
