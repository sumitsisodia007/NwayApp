
using App2.APIService;
using App2.ExpandableListView;
using App2.Model;
using App2.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App2
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<FoodGroup> _allGroups;
        private ObservableCollection<FoodGroup> _expandedGroups;
        NotificationListMdl _notificationModel;
        /// <summary>
        /// Second List
        /// </summary>
        public List<ABCDemo> _receivablList { get; set; }
        public double _Width = 0;

        public ObservableCollection<NotificationListMdl> _NotificationListMdl;
        public MainPage()
        {
            InitializeComponent();
            if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
            {
                var calcScreenWidth = Application.Current.MainPage.Width;
                var calcScreenHieght = Application.Current.MainPage.Height;
                _Width = calcScreenWidth / 3;
            }
            API api = new API();
            _notificationModel = api.PostNotification();


            ObservableCollection<FoodGroup> food = new ObservableCollection<FoodGroup>();


            foreach (var item in _notificationModel.ListNotificationDate)
            {
                FoodGroup group = new FoodGroup(item.Date + " (" + item.NotCount + ")");

                foreach (var item2 in item.ListTags)
                {
                    group.Add(new Food() { Name = item2.Tag, Description = item.Date });
                }

                food.Add(group);
            }



            _allGroups = food;
            UpdateListContent();
        }

        private void HeaderTapped(object sender, EventArgs args)
        {
            try
            {
                int selectedIndex = _expandedGroups.IndexOf(
                ((FoodGroup)((Button)sender).CommandParameter));
                _allGroups[selectedIndex].Expanded = !_allGroups[selectedIndex].Expanded;
                UpdateListContent();
            }
            catch (Exception ex)
            {

                DisplayAlert("Message", ex.Message, "OK");
            }
        }

        private void UpdateListContent()
        {
            try
            {
                _expandedGroups = new ObservableCollection<FoodGroup>();
                foreach (FoodGroup group in _allGroups)
                {
                    //Create new FoodGroups so we do not alter original list
                    FoodGroup newGroup = new FoodGroup(group.Title, group.ShortName, group.Expanded);
                    //Add the count of food items for Lits Header Titles to use
                    newGroup.FoodCount = group.Count;
                    if (group.Expanded)
                    {
                        foreach (Food food in group)
                        {
                            newGroup.Add(food);
                        }
                    }
                    _expandedGroups.Add(newGroup);
                }
                GroupedView.ItemsSource = _expandedGroups;
            }
            catch (Exception ex)
            {
                DisplayAlert("Message", ex.Message, "OK");
            }

        }

        private async void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {

            Food food = (Food)e.Item;
            //if (food.IsVisible == false)
            //{

            //    food.IsVisible = true;
            //    UpdateListContent();
            //}
            //else
            //{
            //    food.IsVisible = false;
            //    UpdateListContent();
            //}
            overlay.IsVisible = true;
            lblName.Text = food.Name;
            TodayCollationList(food);
            await this.ScaleTo(0.95, 50, Easing.CubicOut);
            await this.ScaleTo(1, 50, Easing.CubicIn);
            
        }

        void OnOKButtonClicked(object sender, EventArgs args)
        {
            overlay.IsVisible = false;

        }

        async void OnCancelButtonClicked(object sender, EventArgs args)
        {
            await overlay.ScaleTo(0.95, 50, Easing.CubicOut);
            await overlay.ScaleTo(1, 50, Easing.CubicIn);
            overlay.IsVisible = false;
        }

        //SecondListview fill
        public void TodayCollationList(Food food)
        {
            _receivablList = new List<ABCDemo>();

            var notification =   _notificationModel.ListNotificationDate.Where(o => o.Date == food.Description).ToList();

            foreach (var item in notification)
            {
                foreach (var item2 in item.ListTags)
                {
                    foreach (var item3 in item2.Notification)
                    {
                        _receivablList.Add(new ABCDemo { Amount = item3.Amount_received, txtWidth = _Width, MyProperty = item3.Customer_name });
                    }
                }
                
            }



            //try
            //{
            //    _receivablList.Add(new ABCDemo { Amount = 50000.00, txtWidth = _Width, MyProperty = "RASNIDHI KUMAR & BROS" });
            //    _receivablList.Add(new ABCDemo { Amount = 250000.00, txtWidth = _Width, MyProperty = "KEHWMS ENGINEERING PVT. LTD." });
            //    _receivablList.Add(new ABCDemo { Amount = 520000.00, txtWidth = _Width, MyProperty = "PUJAB WATER SUPPLIERS INDORE" });
            //    _receivablList.Add(new ABCDemo { Amount = 5000.00, txtWidth = _Width, MyProperty = "BOMBAY HARDWARE" });
            //    _receivablList.Add(new ABCDemo { Amount = 10000.00, txtWidth = _Width, MyProperty = "JE ELECTRICAL" });
            //    _receivablList.Add(new ABCDemo { Amount = 750000.00, txtWidth = _Width, MyProperty = "SATGURU OIL PVR. LTD." });
            //    _receivablList.Add(new ABCDemo { Amount = 250000.00, txtWidth = _Width, MyProperty = "RASNIDHI KUMAR & BROS" });
            //    _receivablList.Add(new ABCDemo { Amount = 45000.00, txtWidth = _Width, MyProperty = "PUJAB WATER SUPPLIERS INDORE" });
            //    _receivablList.Add(new ABCDemo { Amount = 33000.00, txtWidth = _Width, MyProperty = "SATGURU OIL PVR. LTD." });
            //    _receivablList.Add(new ABCDemo { Amount = 17000.00, txtWidth = _Width, MyProperty = "BOMBAY HARDWARE" });
            //    _receivablList.Add(new ABCDemo { Amount = 19000.00, txtWidth = _Width, MyProperty = "PUJAB WATER SUPPLIERS INDORE" });
            //    _receivablList.Add(new ABCDemo { Amount = 25000.00, txtWidth = _Width, MyProperty = "JE ELECTRICAL" });
            //    _receivablList.Add(new ABCDemo { Amount = 37000.00, txtWidth = _Width, MyProperty = "JE ELECTRICAL" });
            //    _receivablList.Add(new ABCDemo { Amount = 89000.00, txtWidth = _Width, MyProperty = "SATGURU OIL PVR. LTD." });
            //    _receivablList.Add(new ABCDemo { Amount = 73000.00, txtWidth = _Width, MyProperty = "RASNIDHI KUMAR & BROS" });
            //    _receivablList.Add(new ABCDemo { Amount = 65000.00, txtWidth = _Width, MyProperty = "SATGURU OIL PVR. LTD." });
            //    _receivablList.Add(new ABCDemo { Amount = 56000.00, txtWidth = _Width, MyProperty = "BOMBAY HARDWARE" });
            //    _receivablList.Add(new ABCDemo { Amount = 49000.00, txtWidth = _Width, MyProperty = "PUJAB WATER SUPPLIERS INDORE" });
            //    _receivablList.Add(new ABCDemo { Amount = 81000.00, txtWidth = _Width, MyProperty = "JE ELECTRICAL" });
            //    _receivablList.Add(new ABCDemo { Amount = 10000.00, txtWidth = _Width, MyProperty = "RASNIDHI KUMAR & BROS" });
            //    _receivablList.Add(new ABCDemo { Amount = 69000.00, txtWidth = _Width, MyProperty = "RASNIDHI KUMAR & BROS" });
            //    _receivablList.Add(new ABCDemo { Amount = 67000.00, txtWidth = _Width, MyProperty = "BOMBAY HARDWARE" });
            //    _receivablList.Add(new ABCDemo { Amount = 70000.00, txtWidth = _Width, MyProperty = "RASNIDHI KUMAR & BROS" });
            //    _receivablList.Add(new ABCDemo { Amount = 50000.00, txtWidth = _Width, MyProperty = "PUJAB WATER SUPPLIERS INDORE" });
            //    _receivablList.Add(new ABCDemo { Amount = 12000.00, txtWidth = _Width, MyProperty = "BOMBAY HARDWARE" });
            //    _receivablList.Add(new ABCDemo { Amount = 32000.00, txtWidth = _Width, MyProperty = "PUJAB WATER SUPPLIERS INDORE" });
            //    _receivablList.Add(new ABCDemo { Amount = 27000.00, txtWidth = _Width, MyProperty = "RASNIDHI KUMAR & BROS" });
            //    _receivablList.Add(new ABCDemo { Amount = 57000.00, txtWidth = _Width, MyProperty = "RASNIDHI KUMAR & BROS" });
                MainlistView.ItemsSource = _receivablList;
            //}
            //catch (Exception ex)
            //{


            //}
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Message","Comming Soon","OK");
        }
    }
    public class ABCDemo
    {
        public string MyProperty { get; set; }
        public string Amount { get; set; }
        public double txtWidth { get; set; }
    }
}
