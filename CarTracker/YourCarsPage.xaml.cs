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

        public void OnDelete(object sender, EventArgs e) {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }

        protected override void OnAppearing() {
            //base.OnAppearing();
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {

                //conn.CreateTable<Car>();
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

                //using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                //{
                //    conn.CreateTable<Car>();

                //}
                popupLoginView.IsVisible = false;
                //Car newCar = new Car(plate.Text, make.Text, model.Text, Car.nameToColor[colorPicker.SelectedItem.ToString()], vin.Text, name.Text);
                //Cars.Add(newCar);
                //popupLoginView.IsVisible = false;
                //ClearEntryFields();
            }

            ClearEntryFields();
            popupLoginView.IsVisible = false;
            
        }

        //*******Storing to DB
        /*
        async void ConfirmNewCar(object sender, System.EventArgs e)
        {
            //Car newCar = new Car(plate.Text, make.Text, model.Text, Car.nameToColor[colorPicker.SelectedItem.ToString()], vin.Text, name.Text);
            //Cars.Add(newCar);
            //popupLoginView.IsVisible = false;
            //testLabel.Text = "Total Cars: " + Cars.Count.ToString();
            //ClearEntryFields();
            if (!string.IsNullOrWhiteSpace(plate.Text))
            {
                await App.CarDatabase.SaveCarAsync(new StoredCarsModel
                {
                    Plate = plate.Text,
                    Make = make.Text,
                    Model = model.Text,
                    V_Color = Car.nameToColor[colorPicker.SelectedItem.ToString()],
                    Vin = vin.Text,
                    Name = name.Text
                });
            }
            popupLoginView.IsVisible = false;
            ClearEntryFields();
            yourCarsList.ItemsSource = await App.CarDatabase.GetCarAsync();
        }*/
        //************


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
            //List<Car> tempList = new List<Car>(Cars);

            List<Car> tempList = new List<Car>();

            int minIndex = 0;
            string sortAttribute = SortingAttributes[sortPicker.SelectedItem.ToString()];
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                //conn.CreateTable<Car>();
                //conn.Insert(car);
                var carsList = conn.Query<Car>("SELECT * FROM Car ORDER BY " + sortAttribute);
                //var carsList = conn.Table<Car>().ToList();
                //tempList = carsList;

                yourCarsList.ItemsSource = carsList;
            }

            //for (int i = 0; i < tempList.Count; i++) {
            //    minIndex = i;
            //    for (int unsort = i + 1; unsort < tempList.Count; unsort++) {
            //        if (string.Compare(tempList[unsort].GetAttribute(SortingAttributes[sortPicker.SelectedItem.ToString()]), tempList[minIndex].GetAttribute(SortingAttributes[sortPicker.SelectedItem.ToString()])) == -1) {
            //            minIndex = unsort;
            //        }
            //    }
            //    Car tempCar = tempList[minIndex];
            //    tempList[minIndex] = tempList[i];
            //    tempList[i] = tempCar;
            //}
            //Cars = new ObservableCollection<Car>(tempList);
            //yourCarsList.ItemsSource = Cars;

        }

    }
}
