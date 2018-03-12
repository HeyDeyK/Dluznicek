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
        private SQLiteConnection db = new SQLiteConnection("FinalSQLClean.db3");
        private SQLiteConnection db2 = new SQLiteConnection("DataSQLClean4.db3");
        ObservableCollection<TodoItem> itemsFromDb;
        ObservableCollection<Dluh> itemsFromDb2;
        List<Dluh> itemsDluhy = new List<Dluh>();
        ObservableCollection<TodoItem> vysledkyDb2;
        private int cisloDluhu = 0;
        
        readonly DataAccess.DataAccess _dataAccess = new DataAccess.DataAccess();
        public MainWindow()
        {
            InitializeComponent();
            LoadTable();
            date_picker.SelectedDate = DateTime.Today;
            date_picker.SelectedDate = DateTime.Today;
            GetStats(1, 1, 1, "já zaklad");
            LoadDluhy();
            KontrolaDluh();

        }
        public void KontrolaDluh()
        {
            int today_den = (int)DateTime.Now.Day;
            int today_month = (int)DateTime.Now.Month;
            int today_year = (int)DateTime.Now.Year;
            var SelectedDate = new DateTime(today_year, today_month, today_den);
            var vysledek = db2.Table<Dluh>().Where(x => x.Datum <= SelectedDate);
            if(vysledek != null)
            {
                Console.WriteLine( "hahaha");
                string okno = "";
                foreach(var item in vysledek)
                {
                    okno = okno +"Nezaplacená položka: "+ item.Name+" termín: " + item.Datum + "\n";
                }
                if(string.IsNullOrEmpty(okno))
                {

                }
                else
                {
                    MessageBox.Show(okno, "Nezaplacené dluhy");
                }
                

            }
            
        }
        private void LoadCategory()
        {
            vysledkyDb2 = new ObservableCollection<TodoItem>(App.Database.GetItemsAsync().Result);
            foreach(var item in vysledkyDb2)
            {
                ComboBoxItem cboxitem = new ComboBoxItem();
                cboxitem.Content = item.Kategorie;
                cbox.Items.Add(cboxitem);
            }
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (chbox.IsChecked ?? false)
            {
                Dluh dluh = new Dluh()
                {
                    Name = txtName.Text,
                    Item_price = txtCastka.Text,
                    Datum = dluh_picker.SelectedDate,
                    Stav = "Nezaplaceno",
                    Item_sazba = txt_sazba.Text
                };
                _dataAccess.InsertWithChildren(dluh);
                
            }
            else
            {
                string selected = cbox.Text;
                Polozka polozka = new Polozka()
                {
                    Name = txtName.Text,
                    Item_price = txtCastka.Text,
                    Datum = DateTime.Today,
                    Category = new Category()
                    {
                        Name = cbox.Text
                    }
                };

                _dataAccess.InsertWithChildren(polozka);

                Polozka polozky = _dataAccess.GetAllWithChildren<Polozka>(polozka.ID);

                TodoItem item = new TodoItem();
                item.Item_price = txtCastka.Text;
                item.Datum = date_picker.SelectedDate;
                item.Name = txtName.Text;
                item.Kategorie = cbox.Text;

                App.Database.SaveItemAsync(item);
                itemsFromDb.Add(item);
            }
            GetStats(1, 1, 1,"po ulozeni");
            LoadDluhy();

            
        }
        public void LoadDluhy()
        {
            List<Dluh> items = new List<Dluh>();
            int celkova_cena = 0;
            var SelectedDate = new DateTime(1, 1, 1);
            var vysledek = db2.Table<Dluh>().Where(x => x.Datum >= SelectedDate);
            Console.WriteLine(vysledek);

            foreach (var item in vysledek)
            {
                itemsDluhy.Add(item);
                if (item.Stav == "Nezaplaceno")
                {
                    celkova_cena += Convert.ToInt32(item.Item_price);
                }

                //items.Add(new Dluh() { Name = item.Name,Datum=item.Datum,aktdluzi=item.Item_price });
            }
            dluhy_price.Content = celkova_cena.ToString();
            SeznamListViewDluhy.ItemsSource = vysledek;
        }
        public void LoadTable()
        {
            itemsFromDb = new ObservableCollection<TodoItem>(App.Database.GetItemsAsync().Result);
            SeznamListView.ItemsSource = itemsFromDb;
        }
        public void GetStats(int rok,int mesic,int den,string kdo)
        {
            Console.WriteLine("Probiham " + kdo);
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
            GetStats(1, 1, 1, "já all");

        }
        private void Button_Click_Year(object sender, RoutedEventArgs e)
        {
            int today_den = (int)DateTime.Now.Day;
            int today_month = (int)DateTime.Now.Month;
            int today_year = (int)DateTime.Now.Year;
            GetStats(today_year-1, today_month, today_den,"já");
            //Console.WriteLine("YEAR: "+(today_year-1) + "Month: "+ today_month + "Den: "+ today_den);
        }
        private void Button_Click_Month(object sender, RoutedEventArgs e)
        {
            int today_den = (int)DateTime.Now.Day;
            int today_month = (int)DateTime.Now.Month;
            int today_year = (int)DateTime.Now.Year;
            GetStats(today_year, today_month-1, today_den, "já");

            //Console.WriteLine("YEAR: "+(today_year-1) + "Month: "+ today_month + "Den: "+ today_den);
            //https://stackoverflow.com/questions/591752/get-the-previous-months-first-and-last-day-dates-in-c-sharp
        }
        private void Button_Click_Week(object sender, RoutedEventArgs e)
        {
            int today_den = (int)DateTime.Now.Day;
            int today_month = (int)DateTime.Now.Month;
            int today_year = (int)DateTime.Now.Year;
            GetStats(today_year, today_month, today_den-7, "já");
            //Console.WriteLine("YEAR: "+(today_year-1) + "Month: "+ today_month + "Den: "+ today_den);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (TabDluhy.IsSelected)
            {
                
            }
            if(TabStats.IsSelected)
            {
            }
        }

        private void cbox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)cbox2.SelectedItem;
            string value = typeItem.Content.ToString();
            Console.WriteLine(value);
            var vysledek = db.Table<TodoItem>().Where(x => x.Kategorie == value);
            SeznamListView2.ItemsSource = vysledek;
            int celkova_cena = 0;
            foreach (var item in vysledek)
            {
                celkova_cena += Convert.ToInt32(item.Item_price);

            }
            sezPrice.Content = celkova_cena;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            dluh_label.Visibility = Visibility.Visible;
            dluh_picker.Visibility = Visibility.Visible;
            label_sazba.Visibility = Visibility.Visible;
            txt_sazba.Visibility = Visibility.Visible;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            dluh_label.Visibility = Visibility.Hidden;
            dluh_picker.Visibility = Visibility.Hidden;
            label_sazba.Visibility = Visibility.Hidden;
            txt_sazba.Visibility = Visibility.Hidden;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
        
        private void ZiskatIndex(object sender, SelectionChangedEventArgs e)
        {
            cisloDluhu = SeznamListViewDluhy.SelectedIndex ;
            Console.WriteLine("proč" + cisloDluhu);
            
            
        }
        private void ZaplacenoButton(object sender, RoutedEventArgs e)
        {
            Dluh dluh = new Dluh()
            {
                ID = itemsDluhy[cisloDluhu].ID,
                Name = itemsDluhy[cisloDluhu].Name,
                aktdluzi = "0",
                Stav = "Zaplaceno",
                Item_price = itemsDluhy[cisloDluhu].Item_price,
                Item_sazba = itemsDluhy[cisloDluhu].Item_sazba,
                Datum = itemsDluhy[cisloDluhu].Datum
            };
            _dataAccess.UpdateWithChildren(dluh);
            LoadDluhy();

        }
        private void NezaplacenoButton(object sender, RoutedEventArgs e)
        {
            Dluh dluh = new Dluh()
            {
                ID = itemsDluhy[cisloDluhu].ID,
                Name = itemsDluhy[cisloDluhu].Name,
                aktdluzi = "0",
                Stav = "Nezaplaceno",
                Item_price = itemsDluhy[cisloDluhu].Item_price,
                Item_sazba = itemsDluhy[cisloDluhu].Item_sazba,
                Datum = itemsDluhy[cisloDluhu].Datum
            };
            _dataAccess.UpdateWithChildren(dluh);
            LoadDluhy();
        }
    }
}
