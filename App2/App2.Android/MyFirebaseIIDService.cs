﻿using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Firebase.Iid;
using Firebase.Messaging;
using Android.Media;
using Android.Support.V4.App;
using App2.Droid.DependencyService;
using App2.Model;
using Newtonsoft.Json.Linq;
using App2.NativeMathods;

namespace App2.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
      
        const string TAG = "MyFirebaseIIDService";
        public static string RegistrationID { get; set; }
        public override void OnTokenRefresh()
        {
            // Get updated InstanceID token.
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Android.Util.Log.Debug(TAG, "Refreshed token: " + refreshedToken);
            RegistrationID = refreshedToken;
            
            // TODO: Implement this method to send any registration to your app's servers.
            SendRegistrationToServer(refreshedToken);
        }
        void SendRegistrationToServer(string token)
        {
            // Add custom implementation, as needed.
        }
    }

 
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        private static String GROUP_KEY_NOTIFICATION = "group_key_notification";

        string data0, data_image, data_tital, data_msg, data_onclick, temp;

        public override void OnMessageReceived(RemoteMessage message)
        {
            try
            {
                foreach (KeyValuePair<string, string> kvp in message.Data)
                {
                    if (kvp.Key == "0")
                    {
                        data0 = kvp.Value;
                    }
                    
                    //data0 = kvp.Value;
                    //temp = data0;
                    //data0 = data_image;
                    //data_image = data_tital;
                    //data_tital = data_msg;
                    //data_msg = data_onclick;
                    //data_onclick = temp;
                }
                JObject jObj = JObject.Parse(data0);

                string TAGTYPE = jObj["tagtype"].ToString();

                string SITE_NAME = jObj["site_name"].ToString();
                string PARTY_NAME = jObj["party_name"].ToString();
                string COMPANY_NAME = jObj["company_name"].ToString();
                string PARTY_ID = jObj["party_id"].ToString();
                string AMOUNT_RECEIVED = jObj["amount_received"].ToString();
                string SITE_ID = jObj["site_id"].ToString();
                string CURRENT_OUTSTANDING = jObj["current_outstanding"].ToString();
                string INFORMATION_TYPE = jObj["information_type"].ToString();
                string PARTY_OUTSTANDING = jObj["party_outstanding"].ToString();
                UserModel rs = new UserModel {NotCount = jObj["notification_count"].ToString()};
                StaticMethods.SaveLocalData(rs);
                string newMsg = PARTY_NAME.ToUpper() + " : " + AMOUNT_RECEIVED;
                string newTitle;
                switch (TAGTYPE)
                {
                    case "paid":
                        newTitle = "PAID";
                        break;
                    case "receipt":
                        newTitle = "RECEIVED";
                        break;
                    default:
                        newTitle = "CANCELED";
                        break;
                }

                //StaticMethods.NotificationCount++;
                SendNotification( data_onclick, newMsg, newTitle,PARTY_ID,TAGTYPE);
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }

        }
        
        void SendNotification( string _onclick, string nmsg, string ntitle,string _party_id,string _tag_type)
        {
            Random _random = new Random();
            Int32 ss = _random.Next();

            //App.Current.MainPage.Navigation.PushAsync(new PayableChart());
            var intent = new Intent(this, typeof(MainActivity));
            intent.PutExtra("tag_type", _tag_type);
            intent.PutExtra("party_id", _party_id);
            intent.PutExtra("onclick", _onclick);
            intent.PutExtra("msg", nmsg);

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.N)
            {
                intent.AddFlags(ActivityFlags.ClearTop);
                var pendingIntent = PendingIntent.GetActivity(this, ss, intent, PendingIntentFlags.OneShot);
                var defaultSoundUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
                var  notif = new NotificationCompat.Builder(this)
                                .SetContentTitle(ntitle)
                                .SetContentText(nmsg)
                                .SetSmallIcon(Resource.Drawable.n3)
                                .SetColor(Resources.GetColor(Resource.Color.blue))
                                .SetGroupSummary(false)
                                .SetGroup(GROUP_KEY_NOTIFICATION)
                                .SetSound(defaultSoundUri)
                                .SetAutoCancel(true)
                                .SetContentIntent(pendingIntent)
                                .Build();
                var notificationManager = NotificationManagerCompat.From(this);
                notificationManager.Notify(ss, notif);
                var summaryNotification = new NotificationCompat.Builder(this)
                                        .SetContentTitle(ntitle)
                                        .SetContentText(nmsg)
                                        .SetStyle(new NotificationCompat.InboxStyle()
                                                    .AddLine(nmsg)
                                                    .AddLine(nmsg)
                                                    .SetSummaryText("")
                                                    .SetBigContentTitle(""))
                                        .SetSmallIcon(Resource.Drawable.n3)
                                        .SetColor(Resources.GetColor(Resource.Color.blue))
                                        .SetGroup(GROUP_KEY_NOTIFICATION)
                                        .SetGroupSummary(true)
                                        .SetSound(defaultSoundUri)
                                        .Build();
                notificationManager.Notify(123456, summaryNotification);
            }
            else
            {
                //var intent = new Intent(this, typeof(OkayActivity));
                //intent.AddFlags(ActivityFlags.ClearTop);
                //var pendingIntent = PendingIntent.GetActivity(this,ss , intent, PendingIntentFlags.OneShot);
                //var defaultSoundUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
                //var notificationBuilder = new NotificationCompat.Builder(this)
                //    .SetSmallIcon(Resource.Drawable.n3)
                //    .SetContentTitle(ntitle)
                //    .SetContentText(nmsg)
                //    .SetAutoCancel(true)
                //    .SetSound(defaultSoundUri)
                //    .SetContentIntent(pendingIntent);

                //var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;
                //notificationManager.Notify(ss++, notificationBuilder.Build());
                var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;
                var uiIntent = new Intent(this, typeof(MainActivity));
                NotificationCompat.Builder builder = new NotificationCompat.Builder(this);
                var notification = builder.SetContentIntent(PendingIntent.GetActivity(this, 0, uiIntent, 0))
                        .SetSmallIcon(Resource.Drawable.n3)
                        .SetTicker(ntitle)
                        .SetContentTitle(ntitle)
                        .SetContentText(nmsg)
                        .SetAutoCancel(true)
                        .Build();
                notificationManager.Notify(0, notification);
            }
        }
    }
}