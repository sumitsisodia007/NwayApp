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
using Gcm.Client;
using Android.Util;
using Android.Support.V4.App;
using Android.Media;
using App2.Droid.DependencyService;

[assembly: Permission(Name = "com.cwd.nwayconstructionerp.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.cwd.nwayconstructionerp.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
namespace App2.Droid.Notification
{
    [BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { "com.cwd.nwayconstructionerp" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "com.cwd.nwayconstructionerp" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "com.cwd.nwayconstructionerp" })]

    public class PushHandlerBroadcastReceiver : GcmBroadcastReceiverBase<GcmService>
    {
        public static string[] SENDER_IDS = new string[] { "600793544961" };
    }

    [Service]
    public class GcmService : GcmServiceBase
    {
        public static string RegistrationID { get; private set; }
        private static String GROUP_KEY_NOTIFICATION = "group_key_notification";
        public const int SERVICE_RUNNING_NOTIFICATION_ID = 10000;
        public GcmService() : base(PushHandlerBroadcastReceiver.SENDER_IDS) { }

        public static string _body { get; set; }
        public static string _title { get; set; }

        protected override void OnRegistered(Context context, string registrationId)
        {
            try
            {
                Log.Verbose("PushHandlerBroadcastReceiver", "GCM Registered: " + registrationId);
                //RegistrationID = registrationId;
            }
            catch (Exception ex)
            {

            }
        }

        public string sendRegister()
        {
            return RegistrationID;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            //var notification = new Notification.Builder(this).Build();
            //StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, notification);
            return base.OnStartCommand(intent, flags, startId);
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            try
            {
                if (intent != null && intent.Extras != null)
                {
                    SendNotification(intent.Extras.Get("message").ToString(),
                                        intent.Extras.Get("title").ToString(),
                                        intent.Extras.Get("ExerciseId").ToString(),
                                        intent.Extras.Get("Id").ToString());
                }
            }
            catch (Exception ex)
            {

            }
        }
        void SendNotification( string nmsg, string ntitle, string _party_id, string _tag_type)
        {
            Random _random = new Random();
            Int32 ss = _random.Next();

            //App.Current.MainPage.Navigation.PushAsync(new PayableChart());

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.N)
            {
                var intent = new Intent(this, typeof(MainActivity));
                intent.PutExtra("tag_type", _tag_type);
                intent.PutExtra("party_id", _party_id);
                intent.PutExtra("msg", nmsg);

                intent.AddFlags(ActivityFlags.ClearTop);
                var pendingIntent = PendingIntent.GetActivity(this, ss, intent, PendingIntentFlags.OneShot);
                var defaultSoundUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
                var notif = new NotificationCompat.Builder(this)
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
                var intent = new Intent(this, typeof(OkayActivity));
                intent.AddFlags(ActivityFlags.ClearTop);
                var pendingIntent = PendingIntent.GetActivity(this, ss, intent, PendingIntentFlags.OneShot);
                var defaultSoundUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
                var notificationBuilder = new NotificationCompat.Builder(this)
                    .SetSmallIcon(Resource.Drawable.n3)
                    .SetContentTitle(ntitle)
                    .SetContentText(nmsg)
                    .SetAutoCancel(true)
                    .SetSound(defaultSoundUri)
                    .SetContentIntent(pendingIntent);

                var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;
                notificationManager.Notify(ss++, notificationBuilder.Build());
            }
        }



        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Log.Error("PushHandlerBroadcastReceiver", "Unregistered RegisterationId : " + registrationId);
        }

        protected override void OnError(Context context, string errorId)
        {
            Log.Error("PushHandlerBroadcastReceiver", "GCM Error: " + errorId);
        }
    }
}
