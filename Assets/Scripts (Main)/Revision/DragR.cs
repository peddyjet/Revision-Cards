using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragR : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 panelLocation;
    [SerializeField] float percentThreshold = 0.75f;
    [SerializeField] float easing = 0.5f;

    new RectTransform transform;

    private void Awake()
    {
        transform = GetComponent<RectTransform>();
        panelLocation = transform.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
       
            Debug.Log(1);
            float difference = eventData.pressPosition.x - eventData.position.x;
            transform.position = panelLocation - new Vector3(difference, 0, 0);
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       
            Debug.Log(2);
            float percentage = (eventData.pressPosition.x - eventData.position.x) / 3;
            if (Mathf.Abs(percentage) >= percentThreshold)
            {
                bool isCorrect = false;
                Vector3 newLocation = panelLocation;
                if (percentage > 0)
                {
                    isCorrect = true;
                    newLocation += new Vector3(-3000, 0, 0);
                }
                else if (percentage < 0)
                {
                    newLocation += new Vector3(3000, 0, 0);
                }

                StartCoroutine(SmoothMove(transform.position, newLocation, easing, ()
                    =>
                {
                    if (isCorrect) { FindObjectOfType<TestMGR>().Right(GetComponent<Drag>()); }
                    else { FindObjectOfType<TestMGR>().Wrong(GetComponent<Drag>()); }
                }));
                panelLocation = newLocation;
            }
            else
            {
                StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
            }
        
    }

    IEnumerator SmoothMove(Vector3 startPos, Vector3 endPos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
    IEnumerator SmoothMove(Vector3 startPos, Vector3 endPos, float seconds, Action post_event)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
            post_event.Invoke();
        }
    }
}
