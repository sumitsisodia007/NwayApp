using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.ViewModel
{
   public class ViewModelObject
    {
        public ObservableCollection<OuterObject> OuterCollection { get; }
    }

    public class OuterObject
    {
        ObservableCollection<OuterObject> _Outerdata { get; set; }

        public string OuterTitle { get; set; }
        public ObservableCollection<InnerObject> InnerCollection { get; set; }
        public void newMethod()
        {
            ObservableCollection<OuterObject> _data = new ObservableCollection<OuterObject>();
            for (int i = 1; i <= 5; i++)
            {
                OuterObject _mainItems = new OuterObject();
                _mainItems.OuterTitle = "Main" + i.ToString();
                _mainItems.InnerCollection = new ObservableCollection<InnerObject>();
                for (int j = 1; j <= 3; j++)
                {
                    InnerObject _subItems = new InnerObject()
                    {
                        InnerTitle = "SubItem" + i.ToString()
                    };
                    _mainItems.InnerCollection.Add(_subItems);
                }
                 _data.Add(_mainItems);
            }
        }
    }

    public class InnerObject
    {
        public string InnerTitle { get; set; }
        
    }
}
