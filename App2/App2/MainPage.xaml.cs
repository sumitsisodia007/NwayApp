
using App2.ExpandableListView;
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
        /// <summary>
        /// Second List
        /// </summary>
        public List<ABCDemo> _receivablList { get; set; }
        public double _Width = 500;

        public MainPage()
        {
            InitializeComponent();
            _allGroups = FoodGroup.Groups;
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
            await this.ScaleTo(0.95, 50, Easing.CubicOut);
            await this.ScaleTo(1, 50, Easing.CubicIn);
            TodayCollationList();
        }
       
        void OnOKButtonClicked(object sender, EventArgs args)
        {
            overlay.IsVisible = false;
           
        }

        void OnCancelButtonClicked(object sender, EventArgs args)
        {
            overlay.IsVisible = false;
        }

        //SecondListview fill
        public void TodayCollationList()
        {

            _receivablList = new List<ABCDemo>();
            try
            {
                _receivablList.Add(new ABCDemo { MyProperty = "Hold  30 seconds" });
                _receivablList.Add(new ABCDemo {MyProperty= "Hold for  seconds" });
                _receivablList.Add(new ABCDemo {MyProperty= "Hold for 30 " });
                _receivablList.Add(new ABCDemo {MyProperty= "for 30 seconds" });
                _receivablList.Add(new ABCDemo { MyProperty="Sumit" });
                _receivablList.Add(new ABCDemo { MyProperty = "Hold  30 seconds" });
                _receivablList.Add(new ABCDemo { MyProperty = "Hold for  seconds" });
                _receivablList.Add(new ABCDemo { MyProperty = "Hold for 30 " });
                _receivablList.Add(new ABCDemo { MyProperty = "for 30 seconds" });
                _receivablList.Add(new ABCDemo { MyProperty = "Hold  30 seconds" });
                _receivablList.Add(new ABCDemo { MyProperty = "Hold for  seconds" });
                _receivablList.Add(new ABCDemo { MyProperty = "Hold for 30 " });
                _receivablList.Add(new ABCDemo { MyProperty = "for 30 seconds" });
                _receivablList.Add(new ABCDemo { MyProperty = "Sumit" });
                _receivablList.Add(new ABCDemo { MyProperty = "Hold  30 seconds" });
                _receivablList.Add(new ABCDemo { MyProperty = "Hold for  seconds" });
                _receivablList.Add(new ABCDemo { MyProperty = "Hold for 30 " });
                _receivablList.Add(new ABCDemo { MyProperty = "for 30 seconds" });
                _receivablList.Add(new ABCDemo { MyProperty = "Hold  30 seconds" });
                _receivablList.Add(new ABCDemo { MyProperty = "Hold for  seconds" });
                _receivablList.Add(new ABCDemo { MyProperty = "Hold for 30 " });
                _receivablList.Add(new ABCDemo { MyProperty = "for 30 seconds" });
                _receivablList.Add(new ABCDemo { MyProperty = "Sumit" });
                _receivablList.Add(new ABCDemo { MyProperty = "Hold  30 seconds" });
                _receivablList.Add(new ABCDemo { MyProperty = "Hold for  seconds" });
                _receivablList.Add(new ABCDemo { MyProperty = "Hold for 30 " });
                _receivablList.Add(new ABCDemo { MyProperty = "for 30 seconds" });

                _receivablList.Add(new ABCDemo { MyProperty = "Sumit" });
                MainlistView.ItemsSource = _receivablList;
            }
            catch (Exception ex)
            {


            }
        }
    }
    public class ABCDemo
    {
        public string MyProperty { get; set; }
    }
}
