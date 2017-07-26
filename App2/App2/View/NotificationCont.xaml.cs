using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationCont : ContentPage
    {
        public NotificationCont()
        {
            InitializeComponent();
        }
        public NotificationCont(string name)
        {
            InitializeComponent();
            this.Title = name;
        }
    }
}