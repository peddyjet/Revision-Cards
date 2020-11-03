using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckForDownloads : MonoBehaviour
{
    [SerializeField] Button button = null;


    // Start is called before the first frame update
    void Start()
    {
        if (Directory.Exists(@"/storage/emulated/0/Download")) { button.interactable = true; }
        else
        {
            button.interactable = false;
            GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
        }
    }

}
