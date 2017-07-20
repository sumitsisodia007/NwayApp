using App2.ExpandableListView;
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
	public partial class Expand : ContentPage
	{
        
        public Expand ()
		{
			InitializeComponent ();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await this.ScaleTo(0.95, 50, Easing.CubicOut);
            await this.ScaleTo(1, 50, Easing.CubicIn);
            //if (callback != null)
            //    callback.Invoke();
        }
        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var product = e.Item as Product;
            var vm = BindingContext as MainViewModel;
            vm?.ShowOrHidePoducts(product);
        }
        //private void ListViewFood_OnItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    var product = e.Item as Food;
        //    var vm = BindingContext as MainViewModel;
        //    vm?.ShowOrHideFoods(product);
        //}
    }
    
}