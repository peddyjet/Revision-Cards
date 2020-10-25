using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TestMGR : MonoBehaviour
{
    [SerializeField] Constants constants = null;
    List<KeyValuePair<QuestionCard, Subject>> questionCards;
    [SerializeField] Drag cardTemplate = null;

    private void Awake()
    {
        questionCards = new List<KeyValuePair<QuestionCard, Subject>>();

        if (constants.feededSubject == null)
        {
            var school = constants.mainCore.shcools[constants.focusedShcoolIndex];
            foreach (var i in school.Subjects)
            {
                if (i.QuestionCards == null) { continue; }
                foreach (var x in i.QuestionCards)
                {
                    if (x.Proficiency == 1 && constants.bad) { questionCards.Add(new KeyValuePair<QuestionCard, Subject>(x, i)); }
                    if (x.Proficiency == 2 && constants.ok) { questionCards.Add(new KeyValuePair<QuestionCard, Subject>(x, i)); }
                    if (x.Proficiency == 3 && constants.good) { questionCards.Add(new KeyValuePair<QuestionCard, Subject>(x, i)); }
                }
            }
        }
        else
        {
            foreach (var i in constants.feededSubject.QuestionCards)
            {
                questionCards.Add(new KeyValuePair<QuestionCard, Subject>(i, constants.feededSubject));
            }
        }

        StartCoroutine(Init());

    }

    public void Wrong(Drag obj)
    {
        Destroy(obj.gameObject);
        obj.keyValuePair.Key.Proficiency--;
        questionCards.Remove(obj.keyValuePair);

        StartCoroutine(Init());
    }

    public void Right(Drag obj)
    {
        Destroy(obj.gameObject);
        obj.keyValuePair.Key.Proficiency++;
        questionCards.Remove(obj.keyValuePair);

        StartCoroutine(Init());
    }

    public IEnumerator Init()
    {
        if(questionCards.Count < 1) { constants.Serialize(); StaticSceneLoader.ForceLoadScene("Congrats"); yield return null; }

        var newDrag = Instantiate(cardTemplate.gameObject,FindObjectOfType<Canvas>().transform);
        var d = newDrag.GetComponent<Drag>();

        
        KeyValuePair<QuestionCard, Subject> card = questionCards[Random.Range(0, questionCards.Count)];

        d.gameObject.transform.SetParent(FindObjectOfType<Canvas>().transform);
        
        d.keyValuePair = card;
        d.answer = card.Key.Answer;
        d.question = card.Key.Question;
        d.subject = card.Value.Name;
      

        yield return new WaitForSeconds(1);
      //  d.transform.position = new Vector3(0, 737.5f, 0);
    }

}
