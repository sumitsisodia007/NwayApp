
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
        public List<NotificationShow> _receivablList { get; set; }
        public List<NotificationShow> _receiva { get; set; }
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
                    group.Add(new Food() { Name = item2.Tag, TagNotCount = ":" + item2.NotCount, Tag_Amount = item2.Total_Amount, Tag_date = item.Date });
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
                    FoodGroup newGroup = new FoodGroup(group.Title, group.Expanded);
                    //Add the count of food items for Lits Header Titles to use
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
            overlay.IsVisible = true;
            lblName.Text = food.Name;
            TodayCollationList(food);
            await this.ScaleTo(0.95, 50, Easing.CubicOut);
            await this.ScaleTo(1, 50, Easing.CubicIn);
        }

        void OnOKButtonClicked(object sender, EventArgs args)
        {
            overlay.IsVisible = false;
            _receivablList.Clear();
        }

        public void TodayCollationList(Food food)
        {
            _receivablList = new List<NotificationShow>();
            _receiva = new List<NotificationShow>();
            var notification = _notificationModel.ListNotificationDate.Where(o => o.Date == food.Tag_date).ToList();
           
            foreach (var item in notification)
            {
                foreach (var item2 in item.ListTags)
                {
                    foreach (var item3 in item2.Notification)
                    { 
                        _receivablList.Add(new NotificationShow { Amount = item3.Amount_received, txtWidth = _Width, Party_Name = item3.Party_name, Filter_Tag = item2.Tag,Filter_Date=item.Date});
                        //if (item2.Tag == "paid" )
                        //{
                        //    _receivablList.OfType<NotificationShow>().Where(s => s.Filter_Tag== item2.Tag  && s.Filter_Date==item.Date);
                        //    _receiva.Add(new NotificationShow { Amount = item3.Amount_received, txtWidth = _Width, Party_Name = item3.Party_name, Filter_Tag = item2.Tag, Filter_Date = item.Date });
                        //    // _receivablList.Clear();

                        //}
                        //else if(item2.Tag == "receipt")
                        //{

                        //    _receivablList.OfType<NotificationShow>().Where(s => s.Filter_Tag == item2.Tag && s.Filter_Date == item.Date);
                        //    //_receivablList.Clear();
                        //}
                    }
                }

            }
            MainlistView.ItemsSource = _receivablList;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Message", "Comming Soon", "OK");
        }
    }
    public class NotificationShow
    {
        public string Party_Name { get; set; }
        public string Amount { get; set; }
        public string Filter_Tag{ get; set; }
        public string Filter_Date { get; set; }
        public double txtWidth { get; set; }
    }
}
