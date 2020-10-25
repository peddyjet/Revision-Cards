using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{

    private void Awake()
    {
        AndroidNotificationCenter.CancelAllNotifications();

        var sf = new SavingSystem<SerializableSettings>();
        var x = sf.LoadAsJson(SettingsMaster.SETTINGS_FILE_NAME);
        if (x == null) { SendNotifications(); }
        else
        {
            if (!x.UseNotifications) { return; }
            SendNotifications();
        }
    }

    public void SendNotifications()
    {

        var defaultNotificationChannel = new AndroidNotificationChannel()
        {
            Id = "default_channel",
            Name = "Default Channel",
            Description = "For Generic Notifications",
            Importance = Importance.Default
        };

        AndroidNotificationCenter.RegisterNotificationChannel(defaultNotificationChannel);
      
            AndroidNotification androidNotification = new AndroidNotification()
            {
                Title = "It's time to revise!",
                Text = "Remember to look through your revision cards!",
                SmallIcon = "MainS",
                LargeIcon = "MainL",
                RepeatInterval = new System.TimeSpan(1, 0, 0, 0, 0),
                FireTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month,DateTime.Today.Day,12,0,0)
            };
        AndroidNotificationCenter.SendNotification(androidNotification, "default_channel");

        
       
    }
}
