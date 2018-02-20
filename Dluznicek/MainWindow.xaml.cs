using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SQLite;

namespace Dluznicek
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<TodoItem> itemsFromDb;
        ObservableCollection<TodoItem> vysledkyDb;
        public MainWindow()
        {
            InitializeComponent();
            LoadTable();
            date_picker.SelectedDate = DateTime.Today;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            TodoItem item = new TodoItem();
            item.Item_price = txtCastka.Text;
            item.Datum = date_picker.SelectedDate;
            item.Item_name = txtName.Text;
            App.Database.SaveItemAsync(item);
            itemsFromDb.Add(item);
            SeznamListView.ItemsSource = itemsFromDb;
        }
        public void LoadTable()
        {
            itemsFromDb = new ObservableCollection<TodoItem>(App.Database.GetItemLastDay().Result);
            SeznamListView.ItemsSource = itemsFromDb;
        }
        public void DejVysledek()
        {
            var SelectedDate = new DateTime(2018, 2, 20);
            var vysledek = App.Database.Table<TodoItem>().Where(x => x.Datum >= SelectedDate);
        }
    }
}
