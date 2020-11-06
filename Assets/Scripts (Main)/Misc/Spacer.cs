using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Spacer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
        GetComponent<TextMeshProUGUI>().text += $"{Environment.NewLine}";
    }
}
