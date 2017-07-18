
using App2.ExpandableListView;
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

        public MainPage()
        {
            InitializeComponent();
            try
            {
                _allGroups = FoodGroup.All;
                UpdateListContent();
            }
            catch (Exception ex)
            {
                
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            int selectedIndex = _expandedGroups.IndexOf(
               ((FoodGroup)((Button)sender).CommandParameter));
            _allGroups[selectedIndex].Expanded = !_allGroups[selectedIndex].Expanded;
            UpdateListContent();

        }
        private void UpdateListContent()
        {
            _expandedGroups = new ObservableCollection<FoodGroup>();
            foreach (FoodGroup group in _allGroups)
            {
                FoodGroup newGroup = new FoodGroup(group.Title, group.ShortName, group.Expanded);
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
    }
}
