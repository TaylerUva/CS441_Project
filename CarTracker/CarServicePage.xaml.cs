using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarTracker.Models;
using Xamarin.Forms;

namespace CarTracker {
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class CarServicePage : ContentPage {
        public static ObservableCollection<Service> Services = new ObservableCollection<Service>();
        public static Dictionary<string, string> SortingStatement = new Dictionary<string, string>() { { "Sort by data", "data" }, { "Sort by millage", "millage" }, { "Sort by location", "location" }, { "Sort by description", "description" }, { "Sort by car", "car" }
        };

        public CarServicePage() {
            InitializeComponent();
            PopulateStatementPicker();
            yourCarsList.ItemsSource = Services;
        }

        private void PopulateStatementPicker() {
            var PickerStatementOption = new List<string>(SortingStatement.Keys);
            statementPicker.ItemsSource = PickerStatementOption;
            statementPicker.SelectedItem = PickerStatementOption[0];
        }

        private void AddNewCarClicked(object sender, System.EventArgs e) {
            ServiceView.IsVisible = true;
        }

        private void ConfirmNewName(object sender, System.EventArgs e) {
            Service newService = new Service(date.Date.ToString(), millage.Text, location.Text, description.Text, car.Text);
            Services.Add(newService);
            ServiceView.IsVisible = false;
            ClearEntryFields();
        }

        private void ClearEntryFields() {
            date.Date = DateTime.Now;
            millage.Text = null;
            location.Text = null;
            description.Text = null;
            car.Text = null;
        }

        async void OnCellTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e) {
            await Navigation.PushAsync(new ServiceDescription());
        }

        private void OnSortClicked(object sender, System.EventArgs e) {
            List<Service> tempList = new List<Service>(Services);
            int minIndex = 0;

            for (int i = 0; i < tempList.Count; i++) {
                minIndex = i;
                for (int unsort = i + 1; unsort < tempList.Count; unsort++) {
                    if (string.Compare(tempList[unsort].GetStatement(SortingStatement[statementPicker.SelectedItem.ToString()]), tempList[minIndex].GetStatement(SortingStatement[statementPicker.SelectedItem.ToString()])) == -1) {
                        minIndex = unsort;
                    }
                }
                Service tempCar = tempList[minIndex];
                tempList[minIndex] = tempList[i];
                tempList[i] = tempCar;
            }
            Services = new ObservableCollection<Service>(tempList);
            yourCarsList.ItemsSource = Services;

        }

        private void CancelService(object sender, System.EventArgs e) {
            ClearEntryFields();
            ServiceView.IsVisible = false;

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

    }

}