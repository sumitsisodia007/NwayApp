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
    public partial class ReceivablePage : ContentPage
    {
        public List<PartyDetails> _receivablList { get; set; }
        public double txtWidth { get; set; }
        public ReceivablePage()
        {
            InitializeComponent();
            if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
            {
                var calcScreenWidth = Application.Current.MainPage.Width;
                var calcScreenHieght = Application.Current.MainPage.Height;
                txtWidth = calcScreenWidth / 4;
                
            }
           CallList();
            
           
        }
        public  void CallList()
        {
            
            _receivablList = new List<PartyDetails>();
            try
            {
                _receivablList.Add(new PartyDetails { Party = "Swan", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "Hold for 30 seconds" });
                _receivablList.Add(new PartyDetails { Party = "Swan", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "Hold for 30 seconds" });
                _receivablList.Add(new PartyDetails { Party = "Swan", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "Hold for 30 seconds" });
                _receivablList.Add(new PartyDetails { Party = "Swan", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "Hold for 30 seconds" });
                _receivablList.Add(new PartyDetails { Party = "Swan", Pre_Outstanding = "on_btn", Today_Receipt = "exercisepng", Cur_Outstanding = "Hold for 30 seconds" });
                listView.ItemsSource = _receivablList;
            }
            catch (Exception ex)
            {


            }
        }
    }
   public class PartyDetails
    {
        public string Party { get; set; }
        public string Pre_Outstanding { get; set; }
        public string Today_Receipt { get; set; }
        public string Cur_Outstanding { get; set; }
    }
}