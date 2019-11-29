using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CarTracker.Models;
using Xamarin.Forms;
using System.Reflection;
using System.Globalization;
using SQLite;
using System.Linq;
using Color = Xamarin.Forms.Color;
using System.Threading.Tasks;


//using CustomCodeAttributes;

namespace CarTracker {
    public partial class YourCarsPage : ContentPage {

        public static ObservableCollection<Car> Cars = new ObservableCollection<Car>();
        public static Dictionary<string, string> SortingAttributes = new Dictionary<string, string>() {
            {"Sort by license plate", "plate"},
            {"Sort by make", "make"},
            {"Sort by model", "model"},
            {"Sort by VIN", "vin"},
            {"Sort by nickname", "name"}
        };

        public YourCarsPage() {
            InitializeComponent();
            PopulateSortingPicker();
            PopulateColorPicker();

        }

        async void OnDelete(object sender, EventArgs e) {
            var mi = ((MenuItem)sender);
            var mu = mi.CommandParameter as Car;
            await DeleteCar(mu);



            //DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
            //using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            //{
            //    conn.Query<Car>("DELETE FROM Car WHERE Id=" + mu.Id.ToString());
            //    SortByOption(null, null);
            //}
        }

        async Task DeleteCar(Car car)
        {
            var deleteSelected = await DisplayAlert("Are you sure you want to delete this car?", "You cannot undo this action", "Delete", "Cancel");

            if (deleteSelected)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.Query<Car>("DELETE FROM Car WHERE Id=" + car.Id.ToString());
                    SortByOption(null, null);
                }
            }
        }

        protected override void OnAppearing() {
            //base.OnAppearing();
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath)) {
                var carsList = conn.Table<Car>().ToList();

                yourCarsList.ItemsSource = carsList;
            }
        }

        private void PopulateSortingPicker() {
            var PickerSortingOptions = new List<string>(SortingAttributes.Keys);
            sortPicker.ItemsSource = PickerSortingOptions;
            sortPicker.SelectedItem = PickerSortingOptions[0];
        }

        private void PopulateColorPicker() {
            var colorList = new List<string>(Car.nameToColor.Keys);
            colorPicker.ItemsSource = colorList;
            colorPicker.SelectedItem = colorList[0];
        }

        private void AddNewCarClicked(object sender, System.EventArgs e) {
            popupLoginView.IsVisible = true;
        }

        private void ConfirmNewCar(object sender, System.EventArgs e) {
            if (plateEntry.Text == null || makeEntry.Text == null || modelEntry.Text == null || vinEntry.Text == null || nameEntry.Text == null) {
                DisplayAlert("Missing information!", "Please fill in all the fields", "Ok");
            } else {

                Car car = new Car() {
                    plate = plateEntry.Text,
                    make = makeEntry.Text,
                    model = modelEntry.Text,
                    v_color = Car.nameToColor[colorPicker.SelectedItem.ToString()],
                    vin = vinEntry.Text,
                    name = nameEntry.Text
                };

                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath)) {
                    conn.CreateTable<Car>();
                    conn.Insert(car);
                    var carsList = conn.Table<Car>().ToList();

                    yourCarsList.ItemsSource = carsList;
                }

                SortByOption(null, null);

               
                popupLoginView.IsVisible = false;
               
                ClearEntryFields();
                popupLoginView.IsVisible = false;
            }

        }

        private void CancelNewCar(object sender, System.EventArgs e) {
            popupLoginView.IsVisible = false;
        }

        private void ClearEntryFields() {
            plateEntry.Text = null;
            makeEntry.Text = null;
            modelEntry.Text = null;
            vinEntry.Text = null;
            nameEntry.Text = null;
        }

        private void SortByOption(object sender, System.EventArgs e) {
            List<Car> tempList = new List<Car>();

            int minIndex = 0;
            string sortAttribute = SortingAttributes[sortPicker.SelectedItem.ToString()];
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath)) {
                var carsList = conn.Query<Car>("SELECT * FROM Car ORDER BY " + sortAttribute);

                yourCarsList.ItemsSource = carsList;
            }

        }

    }
}
