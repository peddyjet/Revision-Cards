using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AnimatedButton : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler, IPointerDownHandler
{
    public Sprite closed = null;
    public Sprite clicked = null;
    public Sprite hovering = null;
    [Space]
    public UnityEvent unityEvent = null;

    private void Awake()
    {
        GetComponent<Image>().sprite = closed;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        unityEvent.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = clicked;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = hovering;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = closed;
    }
}
