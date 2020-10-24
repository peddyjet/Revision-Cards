using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof (TextMeshProUGUI))]
public class SubjectBox : MonoBehaviour, IPointerClickHandler
{
    [HideInInspector] public Subject subject;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameObject.GetComponent<TextMeshProUGUI>().color == Color.red) { gameObject.GetComponent<TextMeshProUGUI>().color = Color.white; return; }
        foreach (var i in FindObjectsOfType<SubjectBox>())
        {
            i.gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
    }
}
