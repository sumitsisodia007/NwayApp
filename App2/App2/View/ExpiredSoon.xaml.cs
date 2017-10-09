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
                _Width = calcScreenWidth / 4 ;
            }
            TodayCollationList();
        }
        public void TodayCollationList()
        {

            _receivablList = new List<PartyDetails>();
            try
            {
                _receivablList.Add(new PartyDetails { TxtWidth = _Width, Party = "1111", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "Hold  30 seconds" });
                _receivablList.Add(new PartyDetails { TxtWidth = _Width, Party = "1111", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "Hold for  seconds" });
                _receivablList.Add(new PartyDetails { TxtWidth = _Width, Party = "1111", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "Hold for 30 " });
                _receivablList.Add(new PartyDetails { TxtWidth = _Width, Party = "1111", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "for 30 seconds" });
                _receivablList.Add(new PartyDetails { TxtWidth = _Width, Party = "1111", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "Hold for 30 seconds" });
                _receivablList.Add(new PartyDetails { TxtWidth = _Width, Party = "1111", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "Hold  30 seconds" });
                _receivablList.Add(new PartyDetails { TxtWidth = _Width, Party = "1111", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "Hold for  seconds" });
                _receivablList.Add(new PartyDetails { TxtWidth = _Width, Party = "1111", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "Hold for 30 " });
                _receivablList.Add(new PartyDetails { TxtWidth = _Width, Party = "1111", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "for 30 seconds" });
                _receivablList.Add(new PartyDetails { TxtWidth = _Width, Party = "1111", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "Hold for 30 seconds" });
                ListViewMain.ItemsSource= _receivablList;
               
            }
            catch (Exception )
            {


            }
        }

    }
}