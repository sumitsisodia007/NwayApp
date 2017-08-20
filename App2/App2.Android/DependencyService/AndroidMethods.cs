using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App2.Droid.DependencyService;
using App2.Interface;
using Xamarin.Forms;
using Android.Graphics.Drawables;
using App2.Model;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidMethods))]
namespace App2.Droid.DependencyService
{
    public class AndroidMethods : IAndroidMethods
    {
        Dialog d;
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

        public void SaveLocalData(ResponseModel um)
        {
            try
            {
                //store

                var prefs = Android.App.Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);
                var storage = prefs.Edit();
                storage.PutString("TagType", um.TagType);
                storage.PutString("Error", um.Error);
                storage.PutString("Min_Receipt_Amt", um.Min_Receipt_Amt);
                storage.PutString("Notification_Day_Count", um.Notification_Day_Count);
                storage.PutString("User_Id", um.User_Id);
                storage.PutString("Device_Id", um.Device_Id);
                storage.Commit();
            }
            catch (Exception)
            {

            }
        }

        public void DeleteLocalData()
        {
            try
            {
                //store
                var prefs = Android.App.Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);
                var storage = prefs.Edit();

                storage.PutString("User_Id", "");
                storage.PutString("Notification_Day_Count", "");
                storage.PutString("Min_Receipt_Amt", "");
                storage.PutString("TagType", "");
                storage.PutString("Device_Id", "");
                storage.PutString("Error", "");
                storage.Commit();
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
                //retreive 
                var storage = Android.App.Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);

                um.User_Id = Convert.ToString(storage.GetString("User_Id", null));
                um.TagType = storage.GetString("TagType", null);
                um.Notification_Day_Count= storage.GetString("Notification_Day_Count", null);
                um.Min_Receipt_Amt = storage.GetString("Min_Receipt_Amt", null);
                um.Device_Id = storage.GetString("Device_Id", null);
                um.Error= storage.GetString("Error", null);
                return um;
            }
            catch (Exception ex)
            {
                return um;
            }
        }       
    }
}