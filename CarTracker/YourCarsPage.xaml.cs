﻿using System;
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

        public YourCarsPage() {
            InitializeComponent();
            PopulateSortingPicker();
            PopulateColorPicker();
            yourCarsList.ItemsSource = Cars;
        }

        //*****For populating from database*****
        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();
        //    yourCarsList.ItemsSource = await App.CarDatabase.GetCarAsync();
        //}

        //**********************

        private void PopulateYourCarsList() {

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
            Car newCar = new Car(plate.Text, make.Text, model.Text, Car.nameToColor[colorPicker.SelectedItem.ToString()], vin.Text, name.Text);
            Cars.Add(newCar);
            popupLoginView.IsVisible = false;
            testLabel.Text = "Total Cars: " + Cars.Count.ToString();
            ClearEntryFields();
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
