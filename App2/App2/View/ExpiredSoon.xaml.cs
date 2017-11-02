using App2.ShowModels;
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
    public partial class ExpiredSoon : ContentPage
    {
        public List<PartyDetails> _receivablList { get; set; }
        public double _Width = 0;
        public ExpiredSoon()
        {
            InitializeComponent();
            if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
            {
                var calcScreenWidth = Application.Current.MainPage.Width;
                var calcScreenHieght = Application.Current.MainPage.Height;
                _Width=
                    lblBookDate.WidthRequest = 
                    lblCustName.WidthRequest = 
                    lblSiteName.WidthRequest =
                    lblTermDate.WidthRequest = calcScreenWidth / 4 - 30;
                //_Width = calcScreenWidth / 4 ;
            }
            ExpiredList();
        }
        public void ExpiredList()
        {
            _receivablList = new List<PartyDetails>();
            try
            {
                _receivablList.Add(new PartyDetails { TxtWidth = _Width, Party = "1111", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "2/12/2017" });
                _receivablList.Add(new PartyDetails { TxtWidth = _Width, Party = "1111", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "22/12/2018" });
                _receivablList.Add(new PartyDetails { TxtWidth = _Width, Party = "1111", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "25/12/2010" });
                _receivablList.Add(new PartyDetails { TxtWidth = _Width, Party = "1111", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "11/12/2009" });
                _receivablList.Add(new PartyDetails { TxtWidth = _Width, Party = "1111", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "17/12/2012" });
                _receivablList.Add(new PartyDetails { TxtWidth = _Width, Party = "1111", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "19/12/2015" });
                _receivablList.Add(new PartyDetails { TxtWidth = _Width, Party = "1111", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "5/12/2017" });
                _receivablList.Add(new PartyDetails { TxtWidth = _Width, Party = "1111", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "7/12/2017" });
                _receivablList.Add(new PartyDetails { TxtWidth = _Width, Party = "1111", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "13/12/2016" });
                _receivablList.Add(new PartyDetails { TxtWidth = _Width, Party = "1111", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "5/12/2018" });
                ListViewMain.ItemsSource= _receivablList;
               
            }
            catch (Exception )
            {


            }
        }

    }
}