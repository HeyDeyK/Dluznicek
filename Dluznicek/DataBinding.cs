using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dluznicek
{
    class DataBinding : INotifyPropertyChanged
    {
        public ObservableCollection<TodoItem> PersonsData1 { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand ClickCommand { get; }

        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
