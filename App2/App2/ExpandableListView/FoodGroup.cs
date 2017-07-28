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

        public string StateIcon
        {
            get { return Expanded ? "Expand_up.png" : "Expand_down.png"; }
        }

        public FoodGroup(string title, string shortName, bool expanded = false)
        {
            Title = title;
            ShortName = shortName;
            Expanded = expanded;
        }
        //public System.Collections.Generic.List<Description> MyProperty { get; set; }


       
        

        public static ObservableCollection<FoodGroup> Groups { private set; get; }
        public static List<Description> description { private set; get; }
        public static List<Description> list1 = new List<Description>();

        static FoodGroup()
        {
            list1.Add(new Description { MyProperty = "Sumit"});
            list1.Add(new Description { MyProperty = "Amit" });
            list1.Add(new Description { MyProperty = "Ankit" });
            list1.Add(new Description { MyProperty = "Pawan" });
            description = list1;
            Groups = new ObservableCollection<FoodGroup>{
                new FoodGroup("22-Jul-2017","C"){
                    new Food { Name = "Paid:8 /Total Amount:3,54,897.00", IsVisible = false,Obj_MyProperty=list1 },
                    new Food { Name = "Invoice Cancelled:2", IsVisible = false,Obj_MyProperty=list1},
                    new Food { Name = "Receipt:8 /Total Amount:4,897.00", IsVisible = false, Obj_MyProperty=list1},
                },
                new FoodGroup("21-Jul-2017","F"){
                    new Food { Name = "Receipt:8 /Total Amount:4,897.00", IsVisible = false,Obj_MyProperty=list1},
                   new Food { Name = "Paid:8 /Total Amount:1,04,897.00", IsVisible = false, Obj_MyProperty=list1},
                },
                new FoodGroup("20-Jul-2017","V"){
                    new Food { Name = "Invoice Cancelled:2", IsVisible = false,Obj_MyProperty=list1},
                    new Food { Name = "Receipt:6 /Total Amount:2,17,897.00",  IsVisible = false,Obj_MyProperty=list1},
                    new Food { Name = "Paid:8 /Total Amount:2,85,897.00",  IsVisible = false,Obj_MyProperty=list1},
                    
                },
                new FoodGroup("19-Jul-2017","D"){
                    new Food { Name = "Receipt:8 /Total Amount:17,897.00", IsVisible = false,  Obj_MyProperty=list1},
                    new Food { Name = "Paid:8 /Total Amount:54,897.00", IsVisible = false,Obj_MyProperty=list1},
                    new Food { Name = "Invoice Cancelled:2", IsVisible = false,Obj_MyProperty=list1},
                }
            };
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
                UpdateFoods(food);
            }
            else
            {
                if (_oldfood != null)
                {
                    // hide previous selected item
                    _oldfood.IsVisible = false;
                    UpdateFoods(_oldfood);
                }
                // show selected item
                food.IsVisible = true;
                UpdateFoods(food);
            }

            _oldfood = food;
        }

        private void UpdateFoods(Food food)
        {
            //var index = Groups.IndexOf(food);
            //Groups.Remove(food);
            //Foods.Insert(index, food);
        }
        
    }
}
