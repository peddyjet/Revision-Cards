using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IPointerClickHandler
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

    [SerializeField] DragR dragR = null;


    new RectTransform transform;
    bool flippedOver = false;
    // Start is called before the first frame update

    private void Awake()
    {
        dragR.enabled = false;
    }
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
       
        StartCoroutine(SmoothRotate( 0.25f, () => 
        {
            subjectQ.gameObject.SetActive(true);
            subjectA.gameObject.SetActive(false);

            questionQ.gameObject.SetActive(true);
            answerA.gameObject.SetActive(false);

            questionQ.text = answer;

            transform.Rotate(180, 0, 0);
            dragR.enabled = true;
            
        },
            () => {
                subjectQ.gameObject.SetActive(false);
                subjectA.gameObject.SetActive(true);

                questionQ.gameObject.SetActive(false);
                answerA.gameObject.SetActive(true);
            }));
    }
}
