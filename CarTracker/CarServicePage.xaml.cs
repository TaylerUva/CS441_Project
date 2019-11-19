using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using CarTracker.Models;

namespace CarTracker
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class CarServicePage : ContentPage
    {
        public static ObservableCollection<Service> Services = new ObservableCollection<Service>();

        public CarServicePage()
        {
            InitializeComponent();
            yourCarsList.ItemsSource = Services;
        }

        private void AddNewCarClicked(object sender, System.EventArgs e)
        {
            ServiceView.IsVisible = true;
        }

        private void ConfirmNewName(object sender, System.EventArgs e)
        {
            Service newService = new Service(Convert.ToInt32(millage.Text), location.Text, description.Text, car.Text);
            Services.Add(newService);
            ServiceView.IsVisible = false;
            testLabel.Text = "Total Service: " + Services.Count.ToString();
            ClearEntryFields();
        }

        private void ClearEntryFields()
        {
            
            millage.Text = null;
            location.Text = null;
            description.Text = null;
            car.Text = null;
        }

        private void OnSortClicked(object sender, System.EventArgs e)
        {

        }

        DatePicker datePicker = new DatePicker
        {
            MinimumDate = new DateTime(2018, 1, 1),
            MaximumDate = new DateTime(2018, 12, 31),
            Date = new DateTime(2018, 6, 21)
        };

        private void TextChange(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            try
            {

                if (entry.Text.Length > 7)
                {
                    string entryText = entry.Text;

                    entry.TextChanged -= TextChange;

                    entry.Text = e.OldTextValue;
                    entry.TextChanged += TextChange;
                }
                else if (!entry.Text.All(char.IsDigit))
                {
                    string entryText = entry.Text;

                    entry.TextChanged -= TextChange;

                    entry.Text = e.OldTextValue;
                    entry.TextChanged += TextChange;
                    return;
                }
                string strName = entry.Text;

                if (strName.Contains(".") || strName.Contains("-"))
                {
                    strName = strName.Replace(".", "").Replace("-", "");
                    entry.Text = strName;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: {0}", ex);
            }
        }

    }

   

}
