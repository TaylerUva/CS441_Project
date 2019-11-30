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

namespace CarTracker {
    public partial class YourCarsPage : ContentPage {

        public static Dictionary<string, string> SortingAttributes = new Dictionary<string, string>() {
            {"Sort by nickname ↑", "name ASC"},
            {"Sort by nickname ↓", "name DESC"},
            {"Sort by license plate ↑", "plate ASC"},
            {"Sort by license plate ↓", "plate DESC"},
            {"Sort by make ↑", "make ASC"},
            {"Sort by make ↓", "make DESC"},
            {"Sort by model ↑", "model ASC"},
            {"Sort by model ↓", "model DESC"},
            {"Sort by VIN ↑", "vin ASC"},
            {"Sort by VIN ↓", "vin DESC"},
        };

        readonly SQLiteConnection sqlConn;

        public YourCarsPage() {
            sqlConn = new SQLiteConnection(App.FilePath);
            InitializeComponent();
            PopulateSortingPicker();
            PopulateColorPicker();
        }

        async void OnDelete(object sender, EventArgs e) {
            var mi = ((MenuItem)sender);
            var mu = mi.CommandParameter as Car;
            await DeleteCar(mu);

        }

        async Task DeleteCar(Car car) {
            var deleteSelected = await DisplayAlert("Are you sure you want to delete this car?", "You cannot undo this action", "Delete", "Cancel");

            if (deleteSelected) {
                sqlConn.Query<Car>("DELETE FROM Car WHERE Id=" + car.Id.ToString());
                SortCars(null, null);
            }
        }

        protected override void OnAppearing() {
            yourCarsList.ItemsSource = sqlConn.Table<Car>().ToList();
            SortCars(null, null);
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
            newCarPopup.IsVisible = true;
        }

        private void ConfirmNewCar(object sender, System.EventArgs e) {
            if (plateEntry.Text == null || makeEntry.Text == null || modelEntry.Text == null || vinEntry.Text == null || nameEntry.Text == null) {
                DisplayAlert("Missing information!", "Please fill in all the fields", "Ok");
            } else {

                Car car = new Car(plateEntry.Text, makeEntry.Text, modelEntry.Text, Car.nameToColor[colorPicker.SelectedItem.ToString()], vinEntry.Text, nameEntry.Text);

                sqlConn.CreateTable<Car>();
                sqlConn.Insert(car);

                yourCarsList.ItemsSource = sqlConn.Table<Car>().ToList();

                SortCars(null, null);

                newCarPopup.IsVisible = false;

                ClearEntryFields();
                newCarPopup.IsVisible = false;
            }

        }

        private void CancelNewCar(object sender, System.EventArgs e) {
            newCarPopup.IsVisible = false;
        }

        private void ClearEntryFields() {
            plateEntry.Text = null;
            makeEntry.Text = null;
            modelEntry.Text = null;
            vinEntry.Text = null;
            nameEntry.Text = null;
        }

        private void SortCars(object sender, System.EventArgs e) {
            string sortAttribute = SortingAttributes[sortPicker.SelectedItem.ToString()];
            var carsList = sqlConn.Query<Car>("SELECT * FROM Car ORDER BY " + sortAttribute);

            yourCarsList.ItemsSource = carsList;

        }

    }
}
