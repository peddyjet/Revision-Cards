using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler
{

    public KeyValuePair<QuestionCard, Subject> keyValuePair;
    public string question;
    public string answer;
    public string subject;
       

    private Vector3 panelLocation;
    [SerializeField] float percentThreshold = 0.75f;
    [SerializeField] float easing = 0.5f;

    [SerializeField] TextMeshProUGUI subjectQ = null;
    [SerializeField] TextMeshProUGUI questionQ = null;
    [SerializeField] TextMeshProUGUI subjectA = null;
    [SerializeField] TextMeshProUGUI answerA = null;


    new RectTransform transform;
    bool flippedOver = false;
    // Start is called before the first frame update
    void Start()
    {
        questionQ.text = question;
        subjectQ.text = subject;
        subjectA.text = subject;
        answerA.text = answer;
        subjectA.gameObject.SetActive(false);
        answerA.gameObject.SetActive(false);

        transform = GetComponent<RectTransform>();
       // transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Mathf.Abs (Screen.width / 2),Mathf.Abs( Screen.height / 2), 10));
        panelLocation = transform.position;
        
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!flippedOver) { return; }
        Debug.Log(1);
        float difference = eventData.pressPosition.x - eventData.position.x;
        transform.position = panelLocation - new Vector3(difference, 0, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!flippedOver) { return; }
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
            else if(percentage < 0)
            {
                newLocation += new Vector3(3000, 0, 0);
            }

            StartCoroutine(SmoothMove( transform.position, newLocation, easing,()=> { if (isCorrect) { FindObjectOfType<TestMGR>().Right(this); } else { FindObjectOfType<TestMGR>().Wrong(this); } }));
            panelLocation = newLocation;
        }
        else
        {
            StartCoroutine(SmoothMove( transform.position, panelLocation, easing));
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
    IEnumerator SmoothMove(Vector3 startPos, Vector3 endPos, float seconds,Action post_event)
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
    IEnumerator SmoothRotate(float seconds, Action post_event, Action mid_event)
    {
        float t = 0f;
        while (t < 180)
        {
            t++;
            transform.Rotate(new Vector3(1, 0, 0));
            if (t == 90) { mid_event.Invoke(); }
            yield return new WaitForSeconds (seconds / 180);
        }
        post_event.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (flippedOver) { return; }
        flippedOver = true;
        StartCoroutine(SmoothRotate( 0.5f, () => 
        {
            subjectQ.gameObject.SetActive(true);
            subjectA.gameObject.SetActive(false);

            questionQ.gameObject.SetActive(true);
            answerA.gameObject.SetActive(false);

            questionQ.text = answer;

            transform.Rotate(180, 0, 0);
        },
            () => {
                subjectQ.gameObject.SetActive(false);
                subjectA.gameObject.SetActive(true);

                questionQ.gameObject.SetActive(false);
                answerA.gameObject.SetActive(true);
            }));
    }
}
