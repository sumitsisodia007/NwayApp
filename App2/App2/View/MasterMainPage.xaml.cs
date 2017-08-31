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
    public partial class MasterMainPage : MasterDetailPage
    {
        public MasterMainPage()
        {
            InitializeComponent();
            
            this.Master = new MasterPage();
            this.Detail = new NavigationPage(new HomePage());
            App.MasterDetail = this;
        }
    }
}