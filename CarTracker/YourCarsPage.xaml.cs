using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CarTracker.Models;
using Xamarin.Forms;
using System.Reflection;
using Color = CarTracker.Models.Color;
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
        // Dictionary to get Color from color name.
        public static Dictionary<string, Color> nameToColor = new Dictionary<string, Color>()
        {
            { "Aqua", Color.Aqua },
            { "Black", Color.Black },
            { "Blue", Color.Blue },
            { "Fucshia", Color.Fucshia },
            { "Gray", Color.Gray },
            { "Green", Color.Green },
            { "Lime", Color.Lime },
            { "Maroon", Color.Maroon },
            { "Navy", Color.Navy },
            { "Olive", Color.Olive },
            { "Purple", Color.Purple },
            { "Red", Color.Red },
            { "Silver", Color.Silver },
            { "Teal", Color.Teal },
            { "White", Color.White },
            { "Yellow", Color.Yellow }
        };
        public static List<string> PickerSortingOptions = new List<string>();

        public YourCarsPage() {
            InitializeComponent();
            PopulateSortingPicker();
            PopulateColorPicker();
            yourCarsList.ItemsSource = Cars;
        }

        private void PopulateSortingPicker() {
            foreach (KeyValuePair<string, string> item in SortingAttributes) {
                PickerSortingOptions.Add(item.Key);
            }

            sortPicker.ItemsSource = PickerSortingOptions;
            sortPicker.SelectedItem = PickerSortingOptions[0];
        }

        private void PopulateColorPicker() {
            var colorList = new List<string>(nameToColor.Keys);
            colorPicker.ItemsSource = colorList;
            colorPicker.SelectedItem = colorList[0];
        }

        private void AddNewCarClicked(object sender, System.EventArgs e) {
            popupLoginView.IsVisible = true;
        }

        private void ConfirmNewCar(object sender, System.EventArgs e) {
            Car newCar = new Car(plate.Text, make.Text, model.Text, nameToColor[colorPicker.SelectedItem.ToString()], vin.Text, name.Text);
            Cars.Add(newCar);
            popupLoginView.IsVisible = false;
            testLabel.Text = "Total Cars: " + Cars.Count.ToString();
            ClearEntryFields();
        }
        private void CancelNewCar(object sender, System.EventArgs e) {
            popupLoginView.IsVisible = false;
        }

        private void ClearEntryFields() {
            plate.Text = null;
            make.Text = null;
            model.Text = null;
            vin.Text = null;
            name.Text = null;
        }

        private void SortByOption(object sender, System.EventArgs e) {
            List<Car> tempList = new List<Car>(Cars);
            int minIndex = 0;

            for (int i = 0; i < tempList.Count; i++) {
                minIndex = i;
                for (int unsort = i + 1; unsort < tempList.Count; unsort++) {
                    if (string.Compare(tempList[unsort].GetAttribute(SortingAttributes[sortPicker.SelectedItem.ToString()]), tempList[minIndex].GetAttribute(SortingAttributes[sortPicker.SelectedItem.ToString()])) == -1) {
                        minIndex = unsort;
                    }
                }
                Car tempCar = tempList[minIndex];
                tempList[minIndex] = tempList[i];
                tempList[i] = tempCar;
            }
            Cars = new ObservableCollection<Car>(tempList);
            yourCarsList.ItemsSource = Cars;

        }

    }
}
