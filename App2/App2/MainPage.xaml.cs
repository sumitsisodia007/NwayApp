﻿
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
        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {

            Food food = (Food)e.Item;
            if (food.IsVisible == false)
            {
               
                food.IsVisible = true; UpdateListContent();
            }
            else
            {
                food.IsVisible = false; UpdateListContent();
            }
            //var viewModel = (MyViewModel)cell.BindingContext;

            //Xamarin.Forms.StackLayout img = (Xamarin.Forms.StackLayout)sender;
            //Xamarin.Forms.st objFileImageSource = (Xamarin.Forms.FileImageSource)img.Source;
            //var product = e.Item as Product;
            //var vm = BindingContext as MainViewModel;
            //vm?.ShowOrHidePoducts(product);
        }
    }
}
