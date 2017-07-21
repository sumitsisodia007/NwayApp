using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.ExpandableListView
{
    public class Food
    {
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        //public string Description { get; set; }
        public List<Description> Obj_MyProperty { get; set; }

    }
    public class Description
    {
        public string MyProperty { get; set; }
    }

  }
