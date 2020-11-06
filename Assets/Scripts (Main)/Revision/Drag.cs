using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IPointerClickHandler
{

    public KeyValuePair<QuestionCard, Subject> keyValuePair;
    public string question;
    public string answer;
    public string subject;


    private Vector3 panelLocation;

    [SerializeField] TextMeshProUGUI subjectQ = null;
    [SerializeField] TextMeshProUGUI questionQ = null;
    [SerializeField] TextMeshProUGUI subjectA = null;
    [SerializeField] TextMeshProUGUI answerA = null;

    [Space]


    new RectTransform transform;
    bool flippedOver = false;
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
     //  transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Mathf.Abs (Screen.width / 2),Mathf.Abs( Screen.height / 2), 10));
        panelLocation = transform.position;
        
       
    }

    #region Bodge
    IEnumerator SmoothRotate(float seconds, Action post_event, Action mid_event)
    {
        var increment = seconds / 180;
        for (int i = 0; i < 180; i++)
        {
            yield return new WaitForSeconds(increment);
            transform.Rotate(1, 0, 0);
            if(i == 90) { mid_event.Invoke(); }
        }
        post_event.Invoke();
    }
    #endregion

    public void OnPointerClick(PointerEventData eventData)
    {
        if (flippedOver) { return; }
        flippedOver = true;

        #region Bodge
        StartCoroutine( SmoothRotate( 0.5f, () => 
         {
             subjectQ.gameObject.SetActive(true);
             subjectA.gameObject.SetActive(false);

             questionQ.gameObject.SetActive(true);
             answerA.gameObject.SetActive(false);

             questionQ.text = answer;

             transform.Rotate(180, 0, 0);
             GetComponent<DragR>().enabled = true;

         },
             () => {
                 subjectQ.gameObject.SetActive(false);
                 subjectA.gameObject.SetActive(true);

                 questionQ.gameObject.SetActive(false);
                 answerA.gameObject.SetActive(true);
             }));
        #endregion

      //  GetComponent<Animator>().SetTrigger("Go");
    }

    public void Middle()
    {
        subjectQ.gameObject.SetActive(false);
        subjectA.gameObject.SetActive(true);

        questionQ.gameObject.SetActive(false);
        answerA.gameObject.SetActive(true);
    }
    public void End()
    {
        gameObject.AddComponent(typeof(DragR));

        Destroy(GetComponent<Animator>());
    }

    public void Error() => throw new Exception();
}
