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
    public partial class CarServicePage : ContentPage
    {
        public CarServicePage()
        {
            InitializeComponent();
        }

        async void Last_Services_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new LastService());
        }
    }

}
