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
    }

    public class YourViewModel
    {
        //This is for Parent List
        private List<ParentListClass> _parentItems;
        public List<ParentListClass> ParentItems
        {
            get { return _parentItems; }
            set { _parentItems = value; }
        }

        public YourViewModel()
        {
            //ParentItems=new List<ParentListClass>
            //{
            //    new ParentListClass{ChildItems=,}
            //}
        }
    }

    public class ParentListClass
    {
        public string ParentTitle { get; set; }

        //This is for child List
        public List<ChildListClass> ChildItems { get; set; }
        public ParentListClass()
        {
            ChildItems = new List<ChildListClass>
            {
                new ChildListClass{ChildSubTitle="Amit" ,ChildTitle="A"},
                new ChildListClass{ChildSubTitle="Amit" ,ChildTitle="A"},
                new ChildListClass{ChildSubTitle="Amit" ,ChildTitle="A"},
            };
        }
    }

    public class ChildListClass
    {
        public string ChildTitle { get; set; }
        public string ChildSubTitle { get; set; }
    }
}