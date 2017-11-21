using System;
using Android.Content;
using Android.Widget;
using App2.Droid.DependencyService;
using App2.Interface;
using Xamarin.Forms;
using App2.Model;
using App2.NativeMathods;


[assembly: Dependency(typeof(AndroidMethods))]
namespace App2.Droid.DependencyService
{
    public class AndroidMethods : IAndroidMethods
    {
        
        public string GetIdentifier()
        {
            var id = Android.OS.Build.Serial;
            return id;
        }
        
        public void ShowToast(string msg)
        {
            Toast.MakeText(Forms.Context, msg, ToastLength.Long).Show();
        }

        public string GetTokan()
        {
            var id = MyFirebaseIIDService.RegistrationID;
            return id;
            
        }

        public void SaveLocalData(UserModel um)
        {
            try
            {
                var prefs = Android.App.Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);
                var storage = prefs.Edit();
                storage.PutString("TagType", um.TagType);
                storage.PutString("Error", um.Error);
                storage.PutString("MinReceiptAmt", um.MinReceiptAmt);
                storage.PutString("NotificationDayCount", um.NotificationDayCount);
                storage.PutString("UserId", um.UserId);
                storage.PutString("DeviceId", um.DeviceId);
                storage.PutString("NotCount", um.NotCount);
                storage.PutString("NotCountDate", um.NotCountDate);
                storage.PutString("UserName", um.UserName);
                storage.PutString("Password", um.Password);
                storage.PutString("CompanyIndex", um.CompanyIndex);
                storage.PutString("CompanyName", um.CompanyName);

                storage.PutString("SetCancelDays", um.SetCancelDays);
                storage.PutString("SetExpireDays", um.SetExpireDays);
                storage.Commit();
            }
            catch (Exception exception)
            {
                StaticMethods.ShowToast(exception.Message);
            }
        }

        public void DeleteLocalData()
        {
            try
            {
                //store
                var prefs = Android.App.Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);
                var storage = prefs.Edit();

                storage.PutString("UserId", "");
                storage.PutString("NotificationDayCount", "");
                storage.PutString("MinReceiptAmt", "");
                storage.PutString("TagType", "");
                storage.PutString("DeviceId", "");
                storage.PutString("Error", "");
                storage.PutString("NotCount", "");
                storage.PutString("NotCountDate", "");
                storage.PutString("UserName", "");
                storage.PutString("Password", "");
                storage.PutString("CompanyIndex", "");
                storage.PutString("CompanyName", "");
                storage.PutString("SetCancelDays", "");
                storage.PutString("SetExpireDays", "");
                storage.Commit();
            }
            catch (Exception exception)
            {
                StaticMethods.ShowToast(exception.Message);
            }
        }

        public UserModel RetriveLocalData()
        {
            UserModel um = new UserModel();
            try
            {
                //retreive 
                var storage = Android.App.Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);

                um.UserId = Convert.ToString(storage.GetString("UserId", null));
                um.TagType = storage.GetString("TagType", null);
                um.NotificationDayCount= storage.GetString("NotificationDayCount", null);
                um.MinReceiptAmt = storage.GetString("MinReceiptAmt", null);
                um.DeviceId = storage.GetString("DeviceId", null);
                um.Error= storage.GetString("Error", null);
                um.NotCount= storage.GetString("NotCount", null);
                um.NotCountDate= storage.GetString("NotCountDate", null);
                um.UserName = storage.GetString("UserName", null);
                um.Password = storage.GetString("Password", null);
                um.CompanyIndex = storage.GetString("CompanyIndex", null);
                um.CompanyName = storage.GetString("CompanyName", null);
                um.SetExpireDays = storage.GetString("SetExpireDays", null);
                um.SetCancelDays = storage.GetString("SetCancelDays", null);
                return um;
            }
            catch (Exception)
            {
                return um;
            }
        }       
    }
}