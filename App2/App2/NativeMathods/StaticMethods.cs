using App2.Interface;
using App2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App2.NativeMathods
{
    public static class StaticMethods
    {
        public static string DeviceToken = string.Empty;

        public static string getDeviceidentifier()
        {
            string strDeviceIdentifier = "";
            if (Device.OS == TargetPlatform.iOS)
            {
                strDeviceIdentifier = DependencyService.Get<IIosMethods>().GetIdentifier();
            }
            else
            {
                strDeviceIdentifier = DependencyService.Get<IAndroidMethods>().GetIdentifier();
            }
            return strDeviceIdentifier;
        }

        public static string getTokan()
        {
            string strDeviceIdentifier = "";
            if (Device.OS == TargetPlatform.iOS)
            {
              //  strDeviceIdentifier = DependencyService.Get<IIosMethods>().GetIdentifier();
            }
            else
            {
                strDeviceIdentifier =  DependencyService.Get<IAndroidMethods>().GetTokan();
            }
            return strDeviceIdentifier;
        }

        public static void ShowToast(string msg)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (Device.OS == TargetPlatform.iOS)
                {
                    DependencyService.Get<IIosMethods>().ShowToast(msg);
                }
                else
                {

                    DependencyService.Get<IAndroidMethods>().ShowToast(msg);
                }
            });
        }

        public static ResponseModel GetLocalSavedData()
        {
            ResponseModel um = null;
            try
            {
                if (Device.OS == TargetPlatform.iOS)
                {
                    um = DependencyService.Get<IIosMethods>().RetriveLocalData();
                    return um;
                }
                else
                {
                    um = DependencyService.Get<IAndroidMethods>().RetriveLocalData();
                    return um;
                }
            }
            catch (Exception)
            {
                return um;
            }


        }

        public static void SaveLocalData(ResponseModel um)
        {
            try
            {
                if (Device.OS == TargetPlatform.iOS)
                {
                    DependencyService.Get<IIosMethods>().SaveLocalData(um);

                }
                else
                {
                    DependencyService.Get<IAndroidMethods>().SaveLocalData(um);

                }
            }
            catch (Exception)
            {

            }
        }

        public static void DeleteLocalData()
        {
            try
            {
                if (Device.OS == TargetPlatform.iOS)
                {
                    DependencyService.Get<IIosMethods>().DeleteLocalData();

                }
                else
                {
                    DependencyService.Get<IAndroidMethods>().DeleteLocalData();

                }
            }
            catch (Exception)
            {

            }
        }
        
    }
}
