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
        /*
        public void ShowToast(string msg)
        {
            Toast.MakeText(Forms.Context, msg, ToastLength.Long).Show();
        }
        public void SaveLocalData(UserModel um)
        {
            try
            {
                //store

                var prefs = Android.App.Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);
                var storage = prefs.Edit();
                storage.PutString("UserGUID", um.UserGUID);
                storage.PutString("Email", um.Email);
                storage.PutString("FirstName", um.FirstName);
                storage.PutString("LastName", um.LastName);
                storage.PutString("DOB", um.DOB);
                storage.PutString("UserTypeID", um.UserTypeID.ToString());
                storage.PutString("LoginGUID", um.LoginGUID);
                storage.PutString("LoginSessionKey", um.LoginSessionKey);
                storage.PutString("redirect_back_url", um.redirect_back_url);
                storage.PutString("IsSignup", um.IsSignup.ToString());
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

                storage.PutString("UserGUID", "");
                storage.PutString("Email", "");
                storage.PutString("FirstName", "");
                storage.PutString("LastName", "");
                storage.PutString("DOB", "");
                storage.PutString("UserTypeID", "");
                storage.PutString("LoginGUID", "");
                storage.PutString("LoginSessionKey", "");
                storage.PutString("redirect_back_url", "");
                storage.PutString("IsSignup", "");
                storage.Commit();
            }
            catch (Exception)
            {

            }
        }

        public UserModel RetriveLocalData()
        {
            UserModel um = new UserModel();
            try
            {
                //retreive 
                var storage = Android.App.Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);

                um.UserGUID = Convert.ToString(storage.GetString("UserGUID", null));
                um.Email = storage.GetString("Email", null);
                um.FirstName = storage.GetString("FirstName", null);
                um.LastName = storage.GetString("LastName", null);
                um.DOB = storage.GetString("DOB", null);
                um.UserTypeID = Convert.ToInt32(storage.GetString("UserTypeID", null));
                um.LoginGUID = Convert.ToString(storage.GetString("LoginGUID", null));
                um.LoginSessionKey = storage.GetString("LoginSessionKey", null);
                um.redirect_back_url = storage.GetString("redirect_back_url", null);
                um.IsSignup = Convert.ToBoolean(storage.GetString("IsSignup", "false"));

                return um;
            }
            catch (Exception ex)
            {
                return um;
            }

        }
        public void ShowLoader()
        {
            d = new Dialog(Forms.Context);
            d.SetContentView(Resource.Layout.CustomProgressDialog);
            d.SetCancelable(false);
            d.Window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));
            d.Show();

        }
        public void DismissLoader()
        {
            if (d != null)
                d.Dismiss();
        }*/
    }
}