using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feeder : MonoBehaviour
{
    [SerializeField] Constants constants = null;

    public void Go()
    {
        constants.Deserialize();

        var sf = new SavingSystem<SerializableSettings>();
        var x = sf.LoadAsJson(SettingsMaster.SETTINGS_FILE_NAME);

        if (x == null)
        {
            constants.Useless = true;
            StaticSceneLoader.ForceLoadScene("NotificationSettings");
        }
        else
        {
            var day = DateTime.Today.DayOfWeek;

            bool b = false;
            bool o = false;
            bool g = false;

            switch (day)
            {
                case DayOfWeek.Monday: { if (x.DontKnow.monday) { b = true; } if (x.MightKnow.monday) { o = true; } if (x.IKnow.monday) { g = true; } break; }
                case DayOfWeek.Tuesday: { if (x.DontKnow.tuesday) { b = true; } if (x.MightKnow.tuesday) { o = true; } if (x.IKnow.tuesday) { g = true; } break; }
                case DayOfWeek.Wednesday: { if (x.DontKnow.wednesday) { b = true; } if (x.MightKnow.wednesday) { o = true; } if (x.IKnow.wednesday) { g = true; } break; }
                case DayOfWeek.Thursday: { if (x.DontKnow.thursday) { b = true; } if (x.MightKnow.thursday) { o = true; } if (x.IKnow.thursday) { g = true; } break; }
                case DayOfWeek.Friday: { if (x.DontKnow.friday) { b = true; } if (x.MightKnow.friday) { o = true; } if (x.IKnow.friday) { g = true; } break; }
                case DayOfWeek.Saturday: { if (x.DontKnow.saturday) { b = true; } if (x.MightKnow.saturday) { o = true; } if (x.IKnow.saturday) { g = true; } break; }
                case DayOfWeek.Sunday: { if (x.DontKnow.sunday) { b = true; } if (x.MightKnow.sunday) { o = true; } if (x.IKnow.sunday) { g = true; } break; }
            }

            constants.bad = b;
            constants.ok = o;
            constants.good = g;

            StaticSceneLoader.ForceLoadScene("OpenSchoolForTesting");
        }

       
    }

    public void Awake()
    {
        if (constants.Useless)
        {
            constants.Useless = false;

            var sf = new SavingSystem<SerializableSettings>();
            var x = sf.LoadAsJson(SettingsMaster.SETTINGS_FILE_NAME);

            var day = DateTime.Today.DayOfWeek;

            bool b = false;
            bool o = false;
            bool g = false;

            switch (day)
            {
                case DayOfWeek.Monday: { if (x.DontKnow.monday) { b = true; } if (x.MightKnow.monday) { o = true; } if (x.IKnow.monday) { g = true; } break; }
                case DayOfWeek.Tuesday: { if (x.DontKnow.tuesday) { b = true; } if (x.MightKnow.tuesday) { o = true; } if (x.IKnow.tuesday) { g = true; } break; }
                case DayOfWeek.Wednesday: { if (x.DontKnow.wednesday) { b = true; } if (x.MightKnow.wednesday) { o = true; } if (x.IKnow.wednesday) { g = true; } break; }
                case DayOfWeek.Thursday: { if (x.DontKnow.thursday) { b = true; } if (x.MightKnow.thursday) { o = true; } if (x.IKnow.thursday) { g = true; } break; }
                case DayOfWeek.Friday: { if (x.DontKnow.friday) { b = true; } if (x.MightKnow.friday) { o = true; } if (x.IKnow.friday) { g = true; } break; }
                case DayOfWeek.Saturday: { if (x.DontKnow.saturday) { b = true; } if (x.MightKnow.saturday) { o = true; } if (x.IKnow.saturday) { g = true; } break; }
                case DayOfWeek.Sunday: { if (x.DontKnow.sunday) { b = true; } if (x.MightKnow.sunday) { o = true; } if (x.IKnow.sunday) { g = true; } break; }
            }

            constants.bad = b;
            constants.ok = o;
            constants.good = g;

            StaticSceneLoader.ForceLoadScene("OpenSchoolForTesting");
        }
    }

}
