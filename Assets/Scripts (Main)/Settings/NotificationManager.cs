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

        for (double i = 1; i < 7; i++)
        {
            bool inToNextYear = (DateTime.Today.DayOfYear + i > 365 && !DateTime.IsLeapYear(DateTime.Today.Year))
                || (DateTime.Today.DayOfYear + i > 366 && DateTime.IsLeapYear(DateTime.Today.Year));

            int year = DateTime.Today.Year;
            if (inToNextYear) { year++; }

            int day = (int)i + DateTime.Today.Day;
            if (DateTime.DaysInMonth(year,DateTime.Today.Month) - day < 0) { day = Mathf.Abs(DateTime.DaysInMonth(year, DateTime.Today.Month) - day); }

            int month = DateTime.Today.Month;
            if (inToNextYear) { month++; }
            if (DateTime.Today.Day + i != day) { month++; }

            AndroidNotification androidNotification = new AndroidNotification()
            {
                Title = "It's time to revise!",
                Text = "Remember to look through your revision cards!",
                SmallIcon = "mains",
                LargeIcon = "mainl",
                FireTime = new DateTime(year, month, day, 12, 0, 0)
            };
            AndroidNotificationCenter.SendNotification(androidNotification, "default_channel");
        }
       
        
       
    }
}
