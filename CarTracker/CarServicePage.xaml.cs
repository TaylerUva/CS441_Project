using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarTracker.Models;
using Xamarin.Forms;
using SQLite;
using System.Reflection;
using System.Globalization;

namespace CarTracker {
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class CarServicePage : ContentPage {
        public static Dictionary<string, string> SortingStatement = new Dictionary<string, string>() {
            { "Sort by date", "date" },
            { "Sort by mileage", "mileage" },
            { "Sort by location", "location" },
            { "Sort by car", "car" }
        };
        readonly SQLiteConnection sqlConn;

        public CarServicePage() {
            sqlConn = new SQLiteConnection(App.FilePath);
            InitializeComponent();
            PopulateSortingPicker();
        }

        private void PopulateSortingPicker() {
            var PickerStatementOption = new List<string>(SortingStatement.Keys);
            sortingPicker.ItemsSource = PickerStatementOption;
            sortingPicker.SelectedItem = PickerStatementOption[0];
        }

        private void PopulateCarPicker() {
            var nicknames = new List<string>();
            foreach (Car element in sqlConn.Table<Car>().ToList()) {
                nicknames.Add(element.name);
            }
            carPicker.ItemsSource = nicknames;
        }

        private async void AddNewCarClicked(object sender, System.EventArgs e) {
            PopulateCarPicker();
            if (carPicker.ItemsSource.Count == 0) {
                var addNewCarSelected = await DisplayAlert("You don't have any cars to service!", "Please add a car to service", "Add New Car", "Cancel");
                if (addNewCarSelected) await Navigation.PushAsync(new YourCarsPage());
            } else {
                newServicePopup.IsVisible = true;
            }
        }

        private void ConfirmNewService(object sender, System.EventArgs e) {
            if (date.Date.ToString() == null || mileage.Text == null || location.Text == null || description.Text == null) {
                DisplayAlert("Missing information!", "Please fill in all the fields", "Ok");
            } else {
                Service newService = new Service(date.Date, int.Parse(mileage.Text), location.Text, description.Text, carPicker.SelectedItem.ToString());
                sqlConn.CreateTable<Service>();
                sqlConn.Insert(newService);

                serviceRecordsList.ItemsSource = sqlConn.Table<Service>().ToList();

                newServicePopup.IsVisible = false;
                SortRecords(null, null);
                ClearEntryFields();
            }

        }

        protected override void OnAppearing() {
            serviceRecordsList.ItemsSource = sqlConn.Table<Service>().ToList();
            SortRecords(null, null);
        }

        private void ClearEntryFields() {
            date.Date = DateTime.Now;
            mileage.Text = null;
            location.Text = null;
            description.Text = null;
        }

        async void OnCellTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e) {
            var content = e.Item as Service;
            await Navigation.PushAsync(new ServiceDescription(content));
        }


        private void SortRecords(object sender, System.EventArgs e) {
            string sortAttribute = SortingStatement[sortingPicker.SelectedItem.ToString()];
            var carsList = sqlConn.Query<Service>("SELECT * FROM Service ORDER BY " + sortAttribute);

            serviceRecordsList.ItemsSource = carsList;
        }

        private void CancelService(object sender, System.EventArgs e) {
            ClearEntryFields();
            newServicePopup.IsVisible = false;

        }

        private void TextChange(object sender, TextChangedEventArgs e) {
            var entry = (Entry)sender;
            try {

                if (entry.Text.Length > 7) {
                    string entryText = entry.Text;

                    entry.TextChanged -= TextChange;

                    entry.Text = e.OldTextValue;
                    entry.TextChanged += TextChange;
                } else if (!entry.Text.All(char.IsDigit)) {
                    string entryText = entry.Text;

                    entry.TextChanged -= TextChange;

                    entry.Text = e.OldTextValue;
                    entry.TextChanged += TextChange;
                    return;
                }
                string strName = entry.Text;

                if (strName.Contains(".") || strName.Contains("-")) {
                    strName = strName.Replace(".", "").Replace("-", "");
                    entry.Text = strName;
                }
            } catch (Exception ex) {
                Console.WriteLine("Exception caught: {0}", ex);
            }
        }
        async void OnDelete(object sender, EventArgs e) {
            var mi = ((MenuItem)sender);
            var deleteService = mi.CommandParameter as Service;
            await DeleteService(deleteService);

        }

        async Task DeleteService(Service service) {
            var deleteSelected = await DisplayAlert("Are you sure you want to delete this service?", "You cannot undo this action", "Delete", "Cancel");

            if (deleteSelected) {
                sqlConn.Query<Service>("DELETE FROM Service WHERE Id=" + service.Id.ToString());
                SortRecords(null, null);
            }
        }


    }

}