using App2.APIService;
using App2.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace App2.ExpandableListView
{
    public class FoodGroup : ObservableCollection<Food>, INotifyPropertyChanged
    {
        private bool _expanded;
        public string Title { get; set; }
        public string ShortName { get; set; }

        public string TitleWithItemCount
        {
            get { return string.Format(Title); }
        }
        public bool Expanded
        {
            get { return _expanded; }
            set
            {
                if (_expanded != value)
                {
                    _expanded = value;
                    OnPropertyChanged("Expanded");
                    OnPropertyChanged("StateIcon");
                }
            }
        }

        public string StateIcon
        {
            get { return Expanded ? "Expand_up.png" : "Expand_down.png"; }
        }

        public FoodGroup(string title,string shortname, bool expanded = false)
        {
            Title = title;
            ShortName = shortname;
            Expanded = expanded;
        }
     
        public static ObservableCollection<FoodGroup> Groups {  set; get; }
        public static ObservableCollection<FoodGroup> FoodGroups()
        {

            API api = new API();
            NotificationListMdl mode = api.PostNotification();
            ObservableCollection<FoodGroup> food = new ObservableCollection<FoodGroup>();
            foreach (var item in mode.ListNotificationDate)
            {
                FoodGroup group = new FoodGroup(item.Date,"D");

                foreach (var item2 in item.ListTags)
                {
                    group.Add(new Food() { Name = item2.Tag });
                }
            }

            Groups = food;
            return Groups;          
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Food _oldfood;
        public void ShowOrHideFoods(Food food)
        {
            if (_oldfood == food)
            {
                // click twice on the same item will hide it
                food.IsVisible = !food.IsVisible;
            }
            else
            {
                if (_oldfood != null)
                {
                    // hide previous selected item
                    _oldfood.IsVisible = false;
                }
                // show selected item
                food.IsVisible = true;
            }

            _oldfood = food;
        }
    }
}
