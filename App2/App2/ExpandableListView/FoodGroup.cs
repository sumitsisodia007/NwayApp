using System.Collections.ObjectModel;
using System.ComponentModel;


namespace App2.ExpandableListView
{
    public class FoodGroup : ObservableCollection<Food>, INotifyPropertyChanged
    {
        public static ObservableCollection<FoodGroup> All { private set; get; }
        private bool _expanded;
        public string Title { get; set; }
        public int FoodCount { get; set; }

        public string TitleWithItemCount
        {
            get { return string.Format("{0} ({1})", Title, FoodCount); }
        }

        public string ShortName { get; set; }
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
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(v));
        }

        public string StateIcon { get { return Expanded ? "Expand_up.png" : "Expand_down.png"; } }

       
        public FoodGroup(string title, string shortName, bool expanded = false)
        {
            Title = title;
            ShortName = shortName;
            Expanded = expanded;
        }

        static FoodGroup()
        {
            ObservableCollection<FoodGroup> Groups = new ObservableCollection<FoodGroup>{
                new FoodGroup("Vagitable", "V")
                {
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                },
                new FoodGroup("Fruit", "F")
                {
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                },
                new FoodGroup("Color", "C")
                {
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                },
                new FoodGroup("Animal", "A")
                {
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                },
                new FoodGroup("shopping ", "S")
                {
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                }};
            All = Groups;
        }
    }
}
