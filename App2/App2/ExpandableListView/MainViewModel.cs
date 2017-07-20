using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.ExpandableListView
{
    public class MainViewModel
    {
        private Product _oldProduct;
       // private Food _oldfood;
        public ObservableCollection<Product> Products { get; set; }
       // public ObservableCollection<Food> Foods { get; set; }

        public MainViewModel()
        {
            //Foods =new ObservableCollection<Food>
            //{
            //new Food {Namef="sumit",IsVisibleF=false,},
            //new Food {Namef="sumit",IsVisibleF=false,},
            //new Food {Namef="sumit",IsVisibleF=false,},
            //new Food {Namef="sumit",IsVisibleF=false,},
            //new Food {Namef="sumit",IsVisibleF=false,},
            //};
            Products = new ObservableCollection<Product>
            {
                new Product
                {
                    Name = "Surface Book",
                    IsVisible = false,
                },
                new Product
                {
                    Name = "Mac Book Pro",
                    IsVisible = false,
                },
                new Product
                {
                    Name = "Surface Laptop",
                    IsVisible = false,
                },
                new Product
                {
                    Name = "X1 Carbon",
                    IsVisible = false,
                },
            };
        }

        //public void ShowOrHideFoods(Food food)
        //{
        //    if (_oldfood== food)
        //    {
        //        // click twice on the same item will hide it
        //        food.IsVisibleF = !food.IsVisibleF;
        //        UpdateFoods(food);
        //    }
        //    else
        //    {
        //        if (_oldfood!= null)
        //        {
        //            // hide previous selected item
        //            _oldfood.IsVisibleF = false;
        //            UpdateFoods(_oldfood);
        //        }
        //        // show selected item
        //        food.IsVisibleF = true;
        //        UpdateFoods(food);
        //    }

        //    _oldfood= food;
        //}
        public void ShowOrHidePoducts(Product product)
        {
            if (_oldProduct == product)
            {
                // click twice on the same item will hide it
                product.IsVisible = !product.IsVisible;
                UpdateProducts(product);
            }
            else
            {
                if (_oldProduct != null)
                {
                    // hide previous selected item
                    _oldProduct.IsVisible = false;
                    UpdateProducts(_oldProduct);
                }
                // show selected item
                product.IsVisible = true;
                UpdateProducts(product);
            }

            _oldProduct = product;
        }

        private void UpdateProducts(Product product)
        {
            var index = Products.IndexOf(product);
            Products.Remove(product);
            Products.Insert(index, product);
        }
        //private void UpdateFoods(Food food)
        //{
        //    var index = Foods.IndexOf(food);
        //    Foods.Remove(food);
        //    Foods.Insert(index, food);
        //}
    }
}
