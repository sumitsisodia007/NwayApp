using App2.APIService;
using App2.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App2.ExpandableListView
{
    public class NotificationGroup : ObservableCollection<NotificationDetails>, INotifyPropertyChanged
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

        public NotificationGroup(string title,string shortname, bool expanded = false)
        {
            Title = title;
            ShortName = shortname;
            Expanded = expanded;
        }
     
        public static ObservableCollection<NotificationGroup> Groups {  set; get; }
        public static ObservableCollection<NotificationGroup> FoodGroups()
        {

            API api = new API();           
            NotificationListMdl mode = api.PostNotification();
            ObservableCollection<NotificationGroup> food = new ObservableCollection<NotificationGroup>();
            foreach (var item in mode.ListNotificationDate)
            {
                NotificationGroup group = new NotificationGroup(item.Date,"o");

                foreach (var item2 in item.ListTags)
                {
                    group.Add(new NotificationDetails() { Name = item2.Tag });
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
        private NotificationDetails _oldfood;
        public void ShowOrHideFoods(NotificationDetails food)
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
