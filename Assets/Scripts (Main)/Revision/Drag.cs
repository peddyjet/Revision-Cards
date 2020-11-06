using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IPointerClickHandler
{

    public KeyValuePair<QuestionCard, Subject> keyValuePair;
    public string question;
    public string answer;
    public string subject;

    bool flippingover = false;
    private Vector3 panelLocation;

    [SerializeField] TextMeshProUGUI subjectQ = null;
    [SerializeField] TextMeshProUGUI questionQ = null;
    [SerializeField] TextMeshProUGUI subjectA = null;
    [SerializeField] TextMeshProUGUI answerA = null;

    [Space]


    new RectTransform transform;
    bool flippedOver = false;
    float timer = 0f;
    int post = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        GetComponent<DragR>().enabled = false;
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

  /*  void SmoothRotate(float seconds, Action post_event, Action mid_event)
    {
        var increment = seconds / 180;
        for (int i = 0; i < 180; i++)
        {
            Thread.Sleep(Mathf.RoundToInt(increment * 1000));
            transform.Rotate(1, 0, 0);
            if(i == 90) { mid_event.Invoke(); }
        }
        post_event.Invoke();
    }*/

    public void OnPointerClick(PointerEventData eventData)
    {
        if (flippedOver) { return; }
        flippedOver = true;

        flippingover = true;

    
    }

   
        private void Update()
        {
            if (flippingover)
            {
                float speed = 1.0f / 180.0f;
                timer += Time.deltaTime;

            while (timer >= speed)   // If a frame is slower the speed, we loop to catch up
            {
                timer -= speed;
                transform.Rotate(1, 0, 0);
                post++;

                if (post == 90)
                {
                    subjectQ.gameObject.SetActive(false);
                    subjectA.gameObject.SetActive(true);

                    questionQ.gameObject.SetActive(false);
                    answerA.gameObject.SetActive(true);
                }
                if (post >= 180)
                {
                    subjectQ.gameObject.SetActive(true);
                    subjectA.gameObject.SetActive(false);

                    questionQ.gameObject.SetActive(true);
                    answerA.gameObject.SetActive(false);

                    questionQ.text = answer;

                    transform.Rotate(180, 0, 0);
                    GetComponent<DragR>().enabled = true;
                    flippingover = false;
                    post = 0;
                    timer = 0.0f;
                }
            
            }
        
    }
    }
}
