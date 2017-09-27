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
                storage.Put("MinReceiptAmt", um.MinReceiptAmt);
                storage.Put("NotificationDayCount", um.NotificationDayCount);
                storage.Put("TagType", um.TagType);
                storage.Put("UserId", um.UserId);
                storage.Put("DeviceId", um.DeviceId);
                storage.Put("Error", um.Error);
                storage.Put("NotCount", um.NotCount);
                storage.Put("NotCountDate", um.NotCountDate);
                storage.Put("UserName", um.UserName);
                storage.Put("Password", um.Password);
                storage.Put("CompanyName", um.CompanyName);
                storage.Put("CompanyIndex", um.CompanyIndex);
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
                um.MinReceiptAmt = Convert.ToString(storage.Get("MinReceiptAmt", null));
                um.NotificationDayCount = storage.Get("NotificationDayCount", null);
                um.TagType = storage.Get("TagType", null);
                um.UserId = storage.Get("UserId", null);
                um.DeviceId = storage.Get("DeviceId", null);
                um.Error = storage.Get("Error", null);
                um.NotCount = storage.Get("NotCount", null);
                um.NotCountDate = storage.Get("NotCountDate", null);
                um.UserName = storage.Get("UserName", null);
                um.Password = storage.Get("Password", null);
                um.CompanyIndex = storage.Get("CompanyIndex", null);
                um.CompanyName= storage.Get("CompanyName", null);
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