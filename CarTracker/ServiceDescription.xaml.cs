using System;
using System.Collections.Generic;
using CarTracker.Models;

using Xamarin.Forms;

namespace CarTracker
{
    public partial class ServiceDescription : ContentPage
    {
        public ServiceDescription(Service car )
        {
            InitializeComponent();
            BindingContext = car;
        }
        
    }
}
