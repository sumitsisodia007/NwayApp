using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using App2.iOS.DependencyService;
using App2.Interface;
using BigTed;

[assembly: Xamarin.Forms.Dependency(typeof(IosMethods))]
namespace App2.iOS.DependencyService
{
    public class IosMethods : IIosMethods
    {
        private string Key = "fazza_driver";
        public IosMethods()
        {
        }

        public string GetIdentifier()
        {
            var id = UIKit.UIDevice.CurrentDevice.IdentifierForVendor.AsString();
            return id;
        }


        public void ShowToast(string msg)
        {
            BTProgressHUD.ShowToast(msg, ProgressHUD.MaskType.Black, false, 2000);
        }
        public void ShowLoader()
        {
            BTProgressHUD.Show();
        }
        public void DismissLoader()
        {
            BTProgressHUD.Dismiss();
        }
        //public void SaveLocalData(UserModel um)
        //{
        //    try
        //    {
        //        var storage = SimpleStorage.EditGroup(Key);
        //        storage.Put("UserGUID", um.UserGUID);
        //        storage.Put("Email", um.Email);
        //        storage.Put("FirstName", um.FirstName);
        //        storage.Put("LastName", um.LastName);
        //        storage.Put("DOB", um.DOB);
        //        storage.Put("UserTypeID", um.UserTypeID.ToString());
        //        storage.Put("LoginGUID", um.LoginGUID);
        //        storage.Put("LoginSessionKey", um.LoginSessionKey);
        //        storage.Put("redirect_back_url", um.redirect_back_url);
        //        storage.Put("IsSignup", um.IsSignup);
        //        storage.Put("DeviceToken", um.DeviceToken);
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}
        //public UserModel RetriveLocalData()
        //{
        //    UserModel um = new UserModel();
        //    try
        //    {
        //        var storage = SimpleStorage.EditGroup(Key);
        //        um.UserGUID = Convert.ToString(storage.Get("UserGUID", null));
        //        um.Email = storage.Get("Email", null);
        //        um.FirstName = storage.Get("FirstName", null);
        //        um.LastName = storage.Get("LastName", null);
        //        um.DOB = storage.Get("DOB", null);
        //        um.UserTypeID = Convert.ToInt32(storage.Get("UserTypeID", null));
        //        um.LoginGUID = Convert.ToString(storage.Get("LoginGUID", null));
        //        um.LoginSessionKey = storage.Get("LoginSessionKey", null);
        //        um.redirect_back_url = storage.Get("redirect_back_url", null);
        //        um.IsSignup = storage.Get("IsSignup", false);
        //        um.DeviceToken = storage.Get("DeviceToken", "");

        //        return um;
        //    }
        //    catch (Exception)
        //    {
        //        return um;
        //    }
        //}
        //public void DeleteLocalData()
        //{
        //    string values = string.Empty;
        //    try
        //    {

        //        var storage = SimpleStorage.EditGroup(Key);
        //        storage.Delete(Key);

        //    }
        //    catch (Exception)
        //    {

        //    }
        //}
    }
}