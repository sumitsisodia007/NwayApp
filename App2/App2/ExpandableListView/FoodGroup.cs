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
                new FoodGroup("Carbohydrates","C"){
                    new Food { Name = "pasta", IsVisible = false,Obj_MyProperty=list1 },
                    new Food { Name = "potato", IsVisible = false,Obj_MyProperty=list1},
                    new Food { Name = "bread",  IsVisible = false,Obj_MyProperty=list1},
                    new Food { Name = "rice", IsVisible = false, Obj_MyProperty=list1},
                },
                new FoodGroup("Fruits","F"){
                    new Food { Name = "apple", IsVisible = false,Obj_MyProperty=list1},
                    new Food { Name = "banana",  IsVisible = false,Obj_MyProperty=list1},
                    new Food { Name = "pear", IsVisible = false, Obj_MyProperty=list1},
                },
                new FoodGroup("Vegetables","V"){
                    new Food { Name = "carrot", IsVisible = false,Obj_MyProperty=list1},
                    new Food { Name = "green bean",  IsVisible = false,Obj_MyProperty=list1},
                    new Food { Name = "broccoli",  IsVisible = false,Obj_MyProperty=list1},
                    new Food { Name = "peas",  IsVisible = false,Obj_MyProperty=list1},
                },
                new FoodGroup("Dairy","D"){
                    new Food { Name = "Milk", IsVisible = false,  Obj_MyProperty=list1},
                    new Food { Name = "Cheese", IsVisible = false,Obj_MyProperty=list1},
                    new Food { Name = "Ice Cream", IsVisible = false,Obj_MyProperty=list1},
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
