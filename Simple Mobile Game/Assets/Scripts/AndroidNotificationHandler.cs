using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
using UnityEngine;

public class AndroidNotificationHandler : MonoBehaviour
{
    private const string ChannelId = "notification_channel";

    public void ScheduleNotification(DateTime datetime)
    {
#if UNITY_ANDROID
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel
        {
            Id = ChannelId,
            Name = "Notification Channel",
            Description = "energy channel",
            Importance = Importance.Default
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        AndroidNotification notification = new AndroidNotification
        {
            Title = "Energy Recharged!",
            Text = "Your energy has recharged, come back to play again!",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = datetime
        };

        AndroidNotificationCenter.SendNotification(notification, ChannelId);
#endif
    }
}
