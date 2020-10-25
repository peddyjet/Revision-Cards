using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SettingsMaster : MonoBehaviour
{
    #region Variables
    [SerializeField] Toggle notificationsOn = null;
    [SerializeField] TMP_InputField timeForReminders = null;
    [Space]
    [SerializeField] Dates dontKnow;
    [SerializeField] Dates mightKnow;
    [SerializeField] Dates iKnow;
    [Space]
    [SerializeField] Constants constants;
    #endregion
  public const string SETTINGS_FILE_NAME = "Settings.json";

    // Start is called before the first frame update
    void Start()
    {
        Deserialize();

        if (constants.Useless)
        {
            Serialize();
            StaticSceneLoader.ForceLoadScene(0);
        }
    }

    public void Package()
    {
        Serialize();
        StaticSceneLoader.ForceLoadScene(0);
    }

    public void Serialize()
    {
        var sf = new SavingSystem<SerializableSettings>();
        sf.SaveAsJson(new SerializableSettings
        {
            UseNotifications = notificationsOn.isOn,
            HourToSendNotifications = int.Parse(timeForReminders.text),

            DontKnow = new SerializableDates
            {
                monday = dontKnow.monday.isOn,
                tuesday = dontKnow.tuesday.isOn,
                wednesday = dontKnow.wednesday.isOn,
                thursday = dontKnow.thursday.isOn,
                friday = dontKnow.friday.isOn,
                saturday = dontKnow.saturday.isOn,
                sunday = dontKnow.sunday.isOn
            },
            MightKnow = new SerializableDates
            {
                monday = mightKnow.monday.isOn,
                tuesday = mightKnow.tuesday.isOn,
                wednesday = mightKnow.wednesday.isOn,
                thursday = mightKnow.thursday.isOn,
                friday = mightKnow.friday.isOn,
                saturday = mightKnow.saturday.isOn,
                sunday = mightKnow.sunday.isOn
            },
            IKnow = new SerializableDates
            {
                monday = iKnow.monday.isOn,
                tuesday = iKnow.tuesday.isOn,
                wednesday = iKnow.wednesday.isOn,
                thursday = iKnow.thursday.isOn,
                friday = iKnow.friday.isOn,
                saturday = iKnow.saturday.isOn,
                sunday = iKnow.sunday.isOn
            }

        }, SETTINGS_FILE_NAME);
    }

    public void Deserialize()
    {
        var sf = new SavingSystem<SerializableSettings>();
        var x = sf.LoadAsJson(SETTINGS_FILE_NAME);
        if (x == null) { return; }

        notificationsOn.isOn = x.UseNotifications;
        timeForReminders.text = x.HourToSendNotifications.ToString();

        dontKnow.monday.isOn = x.DontKnow.monday;
        dontKnow.tuesday.isOn = x.DontKnow.tuesday;
        dontKnow.wednesday.isOn = x.DontKnow.wednesday;
        dontKnow.thursday.isOn = x.DontKnow.thursday;
        dontKnow.friday.isOn = x.DontKnow.friday;
        dontKnow.saturday.isOn = x.DontKnow.saturday;
        dontKnow.sunday.isOn = x.DontKnow.sunday;

        mightKnow.monday.isOn = x.MightKnow.monday;
        mightKnow.tuesday.isOn = x.MightKnow.tuesday;
        mightKnow.wednesday.isOn = x.MightKnow.wednesday;
        mightKnow.thursday.isOn = x.MightKnow.thursday;
        mightKnow.friday.isOn = x.MightKnow.friday;
        mightKnow.saturday.isOn = x.MightKnow.saturday;
        mightKnow.sunday.isOn = x.MightKnow.sunday;

        iKnow.monday.isOn = x.IKnow.monday;
        iKnow.tuesday.isOn = x.IKnow.tuesday;
        iKnow.wednesday.isOn = x.IKnow.wednesday;
        iKnow.thursday.isOn = x.IKnow.thursday;
        iKnow.friday.isOn = x.IKnow.friday;
        iKnow.saturday.isOn = x.IKnow.saturday;
        iKnow.sunday.isOn = x.IKnow.sunday;
    }
}
[Serializable]
public struct Dates
{
    public Toggle monday;
    public Toggle tuesday;
    public Toggle wednesday;
    public Toggle thursday;
    public Toggle friday;
    public Toggle saturday;
    public Toggle sunday;
}

[Serializable]
public struct SerializableDates
{
    public bool monday;
    public bool tuesday;
    public bool wednesday;
    public bool thursday;
    public bool friday;
    public bool saturday;
    public bool sunday;
}

[Serializable]
public class SerializableSettings
{
   public SerializableDates DontKnow { get; set; }
   public SerializableDates MightKnow { get; set; }
   public SerializableDates IKnow { get; set; }
   public bool UseNotifications { get; set; }
   public int HourToSendNotifications { get; set; }
}