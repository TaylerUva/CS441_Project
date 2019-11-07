using System;
using Xamarin.Forms;
using System.IO;
using Xamarin.Forms.Xaml;
using CarTracker.Models;

namespace CarTracker {
    public partial class App : Application {
        private static CarDatabase carDatabase;

        public static CarDatabase CarDatabase
        {
            get
            {
                if(carDatabase == null)
                {
                    carDatabase = new CarDatabase(Path.Combine(
                          Environment.GetFolderPath(
                              Environment.SpecialFolder.LocalApplicationData),
                              "CarDatabase.db3"));
                }
                return carDatabase;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}