using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarTracker
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
           myCarPic.Source = "carService";
        }
		
		async void YourCarsClicked(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new YourCarsPage());
		}

		async void Next_Page_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new CarServicePage());
        }

    }

}
