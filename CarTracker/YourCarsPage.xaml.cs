using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CarTracker.Models;
using Xamarin.Forms;

namespace CarTracker {
    public partial class YourCarsPage : ContentPage {
        public static ObservableCollection<Car> Cars = new ObservableCollection<Car>();
        public YourCarsPage() {
            InitializeComponent();
            yourCarsList.ItemsSource = Cars;
        }


        private void AddNewCarClicked(object sender, System.EventArgs e) {
            popupLoginView.IsVisible = true;
        }

        private void ConfirmNewCar(object sender, System.EventArgs e) {
            Car newCar = new Car(plate.Text, make.Text, model.Text, Models.Color.Gray, vin.Text, name.Text);
            Cars.Add(newCar);
            popupLoginView.IsVisible = false;
            testLabel.Text = "Total Cars: " + Cars.Count.ToString();
            ClearEntryFields();
        }

        private void ClearEntryFields() {
            plate.Text = null;
            make.Text = null;
            model.Text = null;
            carColor.Text = null;
            vin.Text = null;
            name.Text = null;
        }

        private void OnSortClicked(object sender, System.EventArgs e) {
            SortByPlate();
        }

        private void SortByPlate() {
            //Cars.Sort();
        }
        private void SortByMake() {

        }
        private void SortByModel() {

        }
        private void SortByColor() {
             public Color()
            {
                this.Title = "Car Color";
                List<Car> cars = new List<Car>();
                cars.Add(new color() { color = "Beige" });
                cars.Add(new color() { color = "Black" });
                cars.Add(new color() { color = "Blue" });
                cars.Add(new color() { color = "Bronze" });
                cars.Add(new color() { color = "Brown" });
                cars.Add(new color() { color = "Burgundy" });
                cars.Add(new color() { color = "Cream" });
                cars.Add(new color() { color = "Red(Dark)" });
                cars.Add(new color() { color = "Blue(Dark)" });
                cars.Add(new color() { color = "Brown(Dark)" });
                cars.Add(new color() { color = "Green(Dark)" });
                cars.Add(new color() { color = "Grey(Dark)" });
                cars.Add(new color() { color = "Gold" });
                cars.Add(new color() { color = "Green" });
                cars.Add(new color() { color = "Grey" });
                cars.Add(new color() { color = "Blue(Light)" });
                cars.Add(new color() { color = "Brown(Light)" });
                cars.Add(new color() { color = "Green(Light)" });
                cars.Add(new color() { color = "Grey(Light)" });
                cars.Add(new color() { color = "Maroon" });
                cars.Add(new color() { color = "Mauve" });
                cars.Add(new color() { color = "Multiple Colors" });
                cars.Add(new color() { color = "Orange" });
                cars.Add(new color() { color = "Pink" });
                cars.Add(new color() { color = "Purple" });
                cars.Add(new color() { color = "Red" });
                cars.Add(new color() { color = "Rust" });
                cars.Add(new color() { color = "Silver" });
                cars.Add(new color() { color = "Tan" });
                cars.Add(new color() { color = "Teal" });
                cars.Add(new color() { color = "Unknown" });
                cars.Add(new color() { color = "White" });
                cars.Add(new color() { color = "Wine" });
                cars.Add(new color() { color = "Yellow" });

                StackLayout stackLayout = new StackLayout();
                SortByColor = new Picker();
                SortByColor.Title = "Choose Car Color";
                SortByColor.ItemsSource = colors;
                stackLayout.Children.Add(SortByColor);

                Content = stackLayout;
            }
        }
        
        private void SortByVin() {

        }
        private void SortByName() {

        }
        private void ReverseSort() {

        }
    }
}
