using App2.ExpandableListView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public MainPage()
        {
            InitializeComponent();
            try
            {
                _allGroups = new ObservableCollection<FoodGroup>();
                _allGroups = FoodGroup.All;
                UpdateListCounted();
            }
            catch (Exception ex)
            {
                
            }
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            int selectIndex = _expandedGroups.IndexOf(
                ((FoodGroup)((Button)sender).CommandParameter));
            _allGroups[selectIndex].Expanded = !_allGroups[selectIndex].Expanded;

        }
        private void UpdateListCounted()
        {
            _expandedGroups = new ObservableCollection<FoodGroup>();
            foreach(FoodGroup group in _allGroups)
            {
                FoodGroup new_group = new FoodGroup(group.Title, group.ShortName, group.Expanded);
                new_group.FoodCount = group.Count;
                if (group.Expanded)
                {
                    foreach(Food food in group)
                    {
                        new_group.Add(food);
                    }
                }_expandedGroups.Add(new_group);

            }
            GroupView.ItemsSource = _expandedGroups;
        }
    }
}
