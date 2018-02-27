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
using Dluznicek.Abstract;
using SQLite;

namespace Dluznicek
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SQLiteConnection db = new SQLiteConnection("TodoSQLite7.db3");
        ObservableCollection<TodoItem> itemsFromDb;
        ObservableCollection<TodoItem> vysledkyDb;
        readonly DataAccess.DataAccess _dataAccess = new DataAccess.DataAccess();
        public MainWindow()
        {
            InitializeComponent();
            LoadTable();
            date_picker.SelectedDate = DateTime.Today;
            GetStats(1, 1, 1);
            Vlozitjakojedno();
            
        }
        private void Vlozitjakojedno()
        {
            //Category seznam = new Category();
            Polozka polozka = new Polozka()
            {
                Item_name = "raketa",
                Item_price = "999",
                Category = new Category()
                {
                    Name = "VSE"
                }
            };  
            /*Polozka polozka2 = new Polozka()
            {
                Item_name = "raketa",
                Item_price = "999",
                Category = new Category()
                {
                    Name = "VSE"
                }
            };*/
            _dataAccess.InsertWithChildren(polozka);
            Polozka polozky = _dataAccess.GetAllWithChildren<Polozka>(polozka.ID);
        }
        private void dalsivlozeni()
        {
            Polozka polozka = new Polozka()
            {
                Item_name = "raketa",
                Item_price = "999"
            };
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
            itemsFromDb = new ObservableCollection<TodoItem>(App.Database.GetItemsAsync().Result);
            SeznamListView.ItemsSource = itemsFromDb;
        }
        public void GetStats(int rok,int mesic,int den)
        {
            itemsFromDb.Clear();
            int celkova_cena = 0;
            var SelectedDate = new DateTime(rok, mesic, den);
            var vysledek = db.Table<TodoItem>().Where(x => x.Datum >= SelectedDate);
            SeznamListView.ItemsSource = vysledek;
            foreach(var item in vysledek)
            {
                celkova_cena += Convert.ToInt32(item.Item_price);
            }
            txt_items_price.Text = celkova_cena.ToString();
        }

        private void Button_Click_All(object sender, RoutedEventArgs e)
        {
            GetStats(1, 1, 1);

        }
        private void Button_Click_Year(object sender, RoutedEventArgs e)
        {
            int today_den = (int)DateTime.Now.Day;
            int today_month = (int)DateTime.Now.Month;
            int today_year = (int)DateTime.Now.Year;
            GetStats(today_year-1, today_month, today_den);
            //Console.WriteLine("YEAR: "+(today_year-1) + "Month: "+ today_month + "Den: "+ today_den);
        }
        private void Button_Click_Month(object sender, RoutedEventArgs e)
        {
            int today_den = (int)DateTime.Now.Day;
            int today_month = (int)DateTime.Now.Month;
            int today_year = (int)DateTime.Now.Year;
            GetStats(today_year, today_month-1, today_den);

            //Console.WriteLine("YEAR: "+(today_year-1) + "Month: "+ today_month + "Den: "+ today_den);
            //https://stackoverflow.com/questions/591752/get-the-previous-months-first-and-last-day-dates-in-c-sharp
        }
        private void Button_Click_Week(object sender, RoutedEventArgs e)
        {
            int today_den = (int)DateTime.Now.Day;
            int today_month = (int)DateTime.Now.Month;
            int today_year = (int)DateTime.Now.Year;
            GetStats(today_year, today_month, today_den-7);
            //Console.WriteLine("YEAR: "+(today_year-1) + "Month: "+ today_month + "Den: "+ today_den);
        }
    }
}
