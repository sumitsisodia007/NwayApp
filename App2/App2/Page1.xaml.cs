using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        bool isListSelected = false;
        public Page1()
        {
            InitializeComponent();
            //yourButton.BorderRadius = Device.OnPlatform<int>(iOS: 0, Android: 1, WinPhone: 0)
        }

        private void YourButton_Clicked(object sender, EventArgs e)
        {
           // YourButton.BorderRadius = Device.OnPlatform<int>(iOS: 0, Android: 1, WinPhone: 0);
        }
    }
}