using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class Tab : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    [Space]
    [SerializeField] TabMaster tabMaster = null;
    [Space]
    [SerializeField] UnityEvent openingEvents = null;
    [SerializeField] UnityEvent closingEvents = null;
    [Space]
    [SerializeField] GameObject window = null;

    bool isOn = false;

    // Start is called before the first frame update
    void Awake()
    {
        tabMaster.Subscribe(this);
    }

    public void CloseTab() { closingEvents.Invoke(); window.SetActive(false); GetComponent<Image>().sprite = tabMaster.closed; isOn = false; }
    public void OpenTab() { openingEvents.Invoke(); window.SetActive(true); GetComponent<Image>().sprite = tabMaster.open; isOn = true; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isOn) { CloseTab(); }
        else { tabMaster.OpenTab(this); }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = tabMaster.hovering;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isOn) { GetComponent<Image>().sprite = tabMaster.open; }
        else { GetComponent<Image>().sprite = tabMaster.closed; }
    }
}
