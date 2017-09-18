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
                storage.Put("NotCount", um.NotCount);
                storage.Put("NotCountDate", um.NotCountDate);
                storage.Put("UserName", um.UserName);
                storage.Put("Password", um.Password);
                storage.Put("Company_Name", um.Password);
                
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
                um.NotCount = storage.Get("NotCount", null);
                um.NotCountDate = storage.Get("NotCountDate", null);
                um.UserName = storage.Get("UserName", null);
                um.Password = storage.Get("Password", null);
                um.Company_Name = storage.Get("Company_Name", null);
                
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
    }
}