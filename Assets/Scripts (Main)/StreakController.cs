using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StreakController : MonoBehaviour
{

    public const string PATH_FOR_STREAK = "Streak";
    public const string LAST_DATE = "Last Date";
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<TextMeshProUGUI>()) 
        {
            var isValidDate = DateTime.TryParse(PlayerPrefs.GetString(LAST_DATE), out var date);
            if (isValidDate && date < DateTime.Today.AddDays(-1)) { PlayerPrefs.SetInt(PATH_FOR_STREAK, 0); }
            
            GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt(PATH_FOR_STREAK).ToString() + " Days";
            
            
        }
        else
        {
            if(PlayerPrefs.GetString(LAST_DATE) == DateTime.Today.ToString()) { return; }
            PlayerPrefs.SetInt(PATH_FOR_STREAK, PlayerPrefs.GetInt(PATH_FOR_STREAK) + 1);
            PlayerPrefs.SetString(LAST_DATE, DateTime.Today.ToString());
            
        }
    }

}
