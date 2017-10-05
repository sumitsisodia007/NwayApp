using System;

using Foundation;
using UIKit;
using App2.NativeMathods;
using App2.Model;
using App2.Helper;
using ImageCircle.Forms.Plugin.iOS;

namespace App2.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        
        protected UIWindow window;

        public static string RegistrationID { get; set; }

        //public string DeviceTokenset { get { return deviceToken; } }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            
            ImageCircleRenderer.Init();
            if (options != null)
            {
                // check for a local notification
                if (options.ContainsKey(UIApplication.LaunchOptionsLocalNotificationKey))
                {

                    UILocalNotification localNotification = options[UIApplication.LaunchOptionsLocalNotificationKey] as UILocalNotification;
                    if (localNotification != null)
                    {
                        new UIAlertView(localNotification.AlertAction, localNotification.AlertBody, null, "OK", null).Show();
                        // reset our badge
                        UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
                    }
                }
                // check for a remote notification
                if (options.ContainsKey(UIApplication.LaunchOptionsRemoteNotificationKey))
                {

                    NSDictionary remoteNotification = options[UIApplication.LaunchOptionsRemoteNotificationKey] as NSDictionary;
                    if (remoteNotification != null)
                    {
                        //new UIAlertView(remoteNotification.AlertAction, remoteNotification.AlertBody, null, "OK", null).Show();
                    }
                }
            }
            PrepareRemoteNotification();
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(33, 150, 243);
            UINavigationBar.Appearance.TintColor = UIColor.White;
            UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes()
            {
                ForegroundColor = UIColor.White
            };
            return base.FinishedLaunching(app, options);
        }

        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            
            try
            {
                if (userInfo != null && userInfo.ContainsKey(new NSString("aps")))
                {
                    NSDictionary aps = userInfo.ObjectForKey(new NSString("aps")) as NSDictionary;
                    NSDictionary extra = aps.ObjectForKey(new NSString("extra")) as NSDictionary;
                    int Amount_received = -1;
                    string Company_name = "";
                    int Current_outstanding = -1;
                    string TagType = "";
                    int Notification_count = -1;
                    string Party_id = "";
                    string Party_name = "";
                    int siteId = -1;
                    int Party_outstanding = -1;
                    string siteName = "";
                    string siteShortName = "";
                    if (extra != null && extra.ContainsKey(new NSString("tagtype")))
                    {
                        TagType = (extra[new NSString("tagtype")] as NSObject).ToString();
                    }
                    if (extra != null && extra.ContainsKey(new NSString("amount_received")))
                    {
                        var amtrec = (extra[new NSString("amount_received")] as NSObject).ToString();
                        int.TryParse(amtrec, out Amount_received);
                    }

                    if (extra != null && extra.ContainsKey(new NSString("site_id")))
                    {
                        var siteid = (extra[new NSString("site_id")] as NSObject).ToString();
                        int.TryParse(siteid, out siteId);
                    }
                    if (extra != null && extra.ContainsKey(new NSString("site_name")))
                    {
                        siteName = (extra[new NSString("site_name")] as NSObject).ToString();
                    }
                    if (extra != null && extra.ContainsKey(new NSString("site_short_name")))
                    {
                        siteShortName = (extra[new NSString("site_short_name")] as NSObject).ToString();
                    }

                    if (extra != null && extra.ContainsKey(new NSString("company_name")))
                    {
                        Company_name = (extra[new NSString("company_name")] as NSObject).ToString();
                    }
                    if (extra != null && extra.ContainsKey(new NSString("current_outstanding")))
                    {
                        var curout = (extra[new NSString("current_outstanding")] as NSObject).ToString();
                        int.TryParse(curout, out Current_outstanding);
                    }
                    if (extra != null && extra.ContainsKey(new NSString("notification_count")))
                    {
                        var notcount = (extra[new NSString("notification_count")] as NSObject).ToString();
                        int.TryParse(notcount, out Notification_count);
                        UserModel res = StaticMethods.GetLocalSavedData();
                        var d2 = DateTime.Now.ToString("dd/MM/yyyy");
                        res.NotCountDate = d2.ToString();
                       // StaticMethods.NotificationCount= Notification_count;
                        res.NotCount = Notification_count.ToString();
                        StaticMethods.SaveLocalData(res);

                    }
                    if (extra != null && extra.ContainsKey(new NSString("party_id")))
                    {
                        Party_id = (extra[new NSString("party_id")] as NSObject).ToString();
                        //int.TryParse(pId, out Party_id);
                    }
                    if (extra != null && extra.ContainsKey(new NSString("party_name")))
                    {
                        Party_name = (extra[new NSString("party_name")] as NSObject).ToString();
                    }
                    if (extra != null && extra.ContainsKey(new NSString("party_outstanding")))
                    {
                        string pOut = (extra[new NSString("party_outstanding")] as NSObject).ToString();
                        int.TryParse(pOut, out Party_outstanding);
                    }

                    NavigationMdl mdl = new NavigationMdl
                    {
                        DeviceId = StaticMethods.GetDeviceidentifier()
                    };
                    if (mdl.DeviceId == "unknown")
                    {
                        mdl.DeviceId = "123456";
                    }
                    mdl.CompanyName= Company_name;
                    mdl.PartyId = Party_id;
                    mdl.IsNotification = true;

                    //foreach (var item in mdl.SiteIdMdls)
                    //{
                    //    mdl.SiteIdMdls.Add(new SiteIdMdl
                    //    {
                    //        SiteShortName = siteShortName,
                    //        SiteName = siteName,
                    //        SiteId = siteId,
                    //    });
                    //}

                    switch (TagType)
                    {
                        case "receipt":
                            mdl.PageTitle = "Receivable";
                            mdl.TagType= EnumMaster.TagtypereceivableOutstanding;
                            //  App.Current.MainPage.Navigation.PushModalAsync(new View.PayablePage(mdl));
                            break;
                        case "paid":
                            mdl.PageTitle = "Payable";
                            mdl.TagType = EnumMaster.TagtypepayableOutstanding;
                            //  App.Current.MainPage.Navigation.PushModalAsync(new View.PayablePage(mdl));
                            break;
                        case "booking_entry":
                            LoadApplication(new App());
                            break;
                        case "booking_end":
                            LoadApplication(new App());
                            break;
                        case "invoice_cancelletion":
                            LoadApplication(new App());
                            break;
                        case "invoice_event":
                            LoadApplication(new App());
                            break;
                    }
                    // StaticMethods.SaveLocalNotification(mdl);

                    UIAlertView alert = new UIAlertView("You have Receive Notification.", null, null, NSBundle.MainBundle.LocalizedString("Cancel", "Cancel"),
                                              NSBundle.MainBundle.LocalizedString("OK", "Go"));
                    alert.Show();
                    alert.Clicked += (sender, buttonArgs) =>
                    {
                        if (buttonArgs.ButtonIndex == 1)
                        {
                            //App.Current.MainPage.Navigation.PushModalAsync(new View.PayablePage(mdl));
                            LoadApplication(new App(mdl));
                        }
                        else
                        {
                            LoadApplication(new App());
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }

        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            var DeviceToken = deviceToken.Description;
            if (!string.IsNullOrWhiteSpace(DeviceToken))
            {
                DeviceToken = DeviceToken.Trim('<').Trim('>').Replace(" ", "");
                DeviceToken = DeviceToken.Replace(" ", "");
                RegistrationID = DeviceToken.ToString();
                Console.WriteLine(DeviceToken);
            }

            var oldDeviceToken = NSUserDefaults.StandardUserDefaults.StringForKey("PushDeviceToken");
            if (string.IsNullOrEmpty(oldDeviceToken) || !oldDeviceToken.Equals(DeviceToken))
            {

            }
            NSUserDefaults.StandardUserDefaults.SetString(DeviceToken, "PushDeviceToken");
        }


        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            new UIAlertView("Error registering push notifications", error.LocalizedDescription, null, "OK", null).Show();
        }


        private void PrepareRemoteNotification()
        {
            try
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
                {
                    var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
                                       UIUserNotificationType.Alert |
                                       UIUserNotificationType.Badge |
                                       UIUserNotificationType.Sound,
                        new NSSet());

                    UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
                    UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
                    UIApplication.SharedApplication.RegisterForRemoteNotifications();
                }
                else
                {
                    UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert |
                                                                 UIRemoteNotificationType.Badge |
                                                                 UIRemoteNotificationType.Sound;
                    UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
