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
        // [END refresh_token]

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

        /**
         * Called when message is received.
         */

        // [START receive_message]
        public override void OnMessageReceived(RemoteMessage message)
        {
            // TODO(developer): Handle FCM messages here.
            // If the application is in the foreground handle both data and notification messages here.
            // Also if you intend on generating your own notifications as a result of a received FCM
            // message, here is where that should be initiated. See sendNotification method below.
            try
            {
                //Android.Util.Log.Debug(TAG, "From: " + message.From);
                Android.Util.Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Title);
                //string not_Body = message.GetNotification().Body;
                var notification = message.GetNotification();
                var data = message.Data;
                var title = notification.Title;
                var body = notification.Body;

                SendNotification(title, body);

                //string not_Title= message.GetNotification().Title;
                //SendNotification(not_Title);
            }
            catch (Exception ex)
            {
                
            }
            
        }
        // [END receive_message]

        /**
         * Create and show a simple notification containing the received FCM message.
         */
        void SendNotification(string messagetitle, string messagebody)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0 /* Request code */, intent, PendingIntentFlags.OneShot);

            var defaultSoundUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
            var notificationBuilder = new NotificationCompat.Builder(this)
                .SetSmallIcon(Resource.Drawable.icon)
                .SetContentTitle(messagetitle)
                .SetContentText("Dard Coded")
                .SetAutoCancel(true)
                .SetSound(defaultSoundUri)
                .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManager.FromContext(this);

            notificationManager.Notify(0 /* ID of notification */, notificationBuilder.Build());
        }
    }
}