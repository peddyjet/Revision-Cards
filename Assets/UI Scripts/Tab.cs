using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tab : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    [Space]
    [SerializeField] TabMaster tabMaster = null;
    [Space]
    [SerializeField] UnityEvent openingEvents = null;
    [SerializeField] UnityEvent closingEvents = null;
    [Space]
    [SerializeField] GameObject window = null;
    [Space] [SerializeField] private Image targetImage;

    bool isOn = false;

    // Start is called before the first frame update
    void Awake()
    {
        tabMaster.Subscribe(this);
    }

    public void CloseTab() { closingEvents.Invoke(); window.SetActive(false); targetImage.color = tabMaster.closed; isOn = false; }
    public void OpenTab() { openingEvents.Invoke(); window.SetActive(true); targetImage.color = tabMaster.open; isOn = true; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isOn) { CloseTab(); }
        else { tabMaster.OpenTab(this); }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetImage.color = tabMaster.hovering;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isOn) { targetImage.color = tabMaster.open; }
        else { targetImage.color = tabMaster.closed; }
    }
}
