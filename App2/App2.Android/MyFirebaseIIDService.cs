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
using Firebase.Iid;
using Android.Util;
using Firebase.Messaging;
using Android.Media;
using Android.Support.V4.App;
using Xamarin.Forms;
using App2.Droid.DependencyService;
using Newtonsoft.Json.Linq;
using Org.Json;
using Newtonsoft.Json;

namespace App2.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        AndroidMethods am = new AndroidMethods();
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

        string data0, data_image, data_tital, data_msg, data_onclick, temp;
        // [START receive_message]
        public override void OnMessageReceived(RemoteMessage message)
        {
            // TODO(developer): Handle FCM messages here.
            // If the application is in the foreground handle both data and notification messages here.
            // Also if you intend on generating your own notifications as a result of a received FCM
            // message, here is where that should be initiated. See sendNotification method below.
            //message.Data
            try
            {
                foreach (KeyValuePair<string, string> kvp in message.Data)
                {
                    data0 = kvp.Value;
                    temp = data0;
                    data0 = data_image;
                    data_image = data_tital;
                    data_tital = data_msg;
                    data_msg = data_onclick;
                    data_onclick = temp;
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

                string New_Msg = PARTY_NAME.ToUpper() + " : " + AMOUNT_RECEIVED;
                string New_Title = TAGTYPE.ToUpper();
                
                SendNotification(data0, data_image, data_tital, data_msg, data_onclick, New_Msg, New_Title);
            }
            catch (Exception ex)
            {
                
            }
            
        }
        // [END receive_message]

        /**
         * Create and show a simple notification containing the received FCM message.
         */
        void SendNotification(string a, string b, string c, string d, string e, string nmsg, string ntitle)
        {
            Random _random = new Random();
            Int32 ss = _random.Next();

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
            {
                //After Lollipop Version
            }
            else
            {
                //  pre-Lollipop Version
            }

            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this,ss , intent, PendingIntentFlags.OneShot);

            var defaultSoundUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
            var notificationBuilder = new NotificationCompat.Builder(this)
                .SetSmallIcon(Resource.Drawable.icon)
                .SetContentTitle(ntitle)
                .SetContentText(nmsg)
                .SetAutoCancel(true)
                .SetSound(defaultSoundUri)
                .SetContentIntent(pendingIntent);

            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;
            notificationManager.Notify(ss++, notificationBuilder.Build());
        }
    }
}