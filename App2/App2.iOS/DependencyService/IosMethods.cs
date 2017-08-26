using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using App2.iOS.DependencyService;
using App2.Interface;
using BigTed;
using App2.Model;
using PerpetualEngine.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(IosMethods))]
namespace App2.iOS.DependencyService
{
    public class IosMethods : IIosMethods
    {
        
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
        public string GetTokan()
        {
            var id = AppDelegate.RegistrationID;
            return id;
        }
        /*
        public void ShowLoader()
        {
            BTProgressHUD.Show();
        }
        public void DismissLoader()
        {
            BTProgressHUD.Dismiss();
        }/**/

        public void SaveLocalData(ResponseModel um)
        {
            try
            {
                var storage = SimpleStorage.EditGroup(Key);
                storage.Put("Min_Receipt_Amt", um.Min_Receipt_Amt);
                storage.Put("Notification_Day_Count", um.Notification_Day_Count);
                storage.Put("TagType", um.TagType);
                storage.Put("User_Id", um.User_Id);
                storage.Put("Device_Id", um.Device_Id);
                storage.Put("Error", um.Error);
            }
            catch (Exception)
            {

            }
        }
        public ResponseModel RetriveLocalData()
        {
            ResponseModel um = new ResponseModel();
            try
            {
                var storage = SimpleStorage.EditGroup(Key);
                um.Min_Receipt_Amt = Convert.ToString(storage.Get("Min_Receipt_Amt", null));
                um.Notification_Day_Count = storage.Get("Notification_Day_Count", null);
                um.TagType = storage.Get("TagType", null);
                um.User_Id = storage.Get("User_Id", null);
                um.Device_Id = storage.Get("Device_Id", null);
                um.Error = storage.Get("Error", null);
                return um;
            }
            catch (Exception)
            {
                return um;
            }
        }
        private string Key = "fazza_driver";
        public void DeleteLocalData()
        {
            string values = string.Empty;
            try
            {
                var storage = SimpleStorage.EditGroup(Key);
                storage.Delete(Key);
            }
            catch (Exception)
            {

            }
        }
        private string Key1 = "fazza";
        public NavigationMdl RetriveLocalNotification()
        {
            NavigationMdl um = new NavigationMdl();
            try
            {
                var storage = SimpleStorage.EditGroup(Key1);
                um.Device_id = Convert.ToString(storage.Get("Device_id", null));
                um.Company_name= storage.Get("Company_name", null);
                um.Party_Name = storage.Get("Party_Name", null);
                um.Party_id = storage.Get("Party_id", null);
                um.Tag_type= storage.Get("Tag_type", null);
                um.Page_Title = storage.Get("Page_Title", null);
                return um;
            }
            catch (Exception)
            {
                return um;
            }
        }

        public void SaveLocalNotification(NavigationMdl um)
        {
            try
            {
                var storage = SimpleStorage.EditGroup(Key1);
                storage.Put("Device_id", um.Device_id);
                storage.Put("Company_name", um.Company_name);
                storage.Put("Party_Name", um.Party_Name);
                storage.Put("Party_id", um.Party_id);
                storage.Put("Tag_type", um.Tag_type);
                storage.Put("Page_Title", um.Page_Title);
            }
            catch (Exception)
            {

            }
        }

        public void DeleteLocalNotification()
        {
            string values = string.Empty;
            try
            {
                var storage = SimpleStorage.EditGroup(Key1);
                storage.Delete(Key);
            }
            catch (Exception)
            {

            }
        }
    }
}