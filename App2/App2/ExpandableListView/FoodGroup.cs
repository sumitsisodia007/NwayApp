using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.ExpandableListView
{
    public class FoodGroup : ObservableCollection<Food>
    {
        
        private bool _expanded;
        public string Title { get; set; }
        public int FoodCount { get; set; }
        public string TitleWithCount
        {
            get { return string.Format("{0}({1})", Title, FoodCount); }
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
        public string StateIcon { get { return Expanded ? "icon.png" : "icon_cal.png"; } }
        public FoodGroup(string title, string shortname, bool expanded = true)
        {
            Title = title;
            ShortName = shortname;
            Expanded = expanded;
        }

        private void OnPropertyChanged(string v)
        {
            throw new NotImplementedException();
        }


        public static ObservableCollection<FoodGroup> All { get; set; }

        static FoodGroup()
        {
            ObservableCollection<FoodGroup> Groups = new ObservableCollection<FoodGroup>{

                new FoodGroup("a", "C")
                {
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                },
                new FoodGroup("b", "C")
                {
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                },
                new FoodGroup("c", "C")
                {
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                },
                new FoodGroup("d", "C")
                {
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                    new Food{Name ="Item1" ,Description="Description 1",Icon="icon"},
                },
                new FoodGroup("e", "C")
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

