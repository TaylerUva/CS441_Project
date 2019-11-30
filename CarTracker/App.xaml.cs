using System;
using Xamarin.Forms;
using System.IO;
using Xamarin.Forms.Xaml;
using CarTracker.Models;
using SQLite;

namespace CarTracker {
    public partial class App : Application {

        public static string FilePath;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

        }

        public App(string filePath)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            FilePath = filePath;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Car>();
            }
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