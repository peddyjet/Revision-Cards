using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TMPro;
using UnityEngine;

public class OSSACManager : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown = null;
    [SerializeField] Constants constants = null;
    [Space]
    [SerializeField] GameObject areaToShowSubjects = null;
    [SerializeField] GameObject templateForSubjects = null;
    [Space]
    [SerializeField] GameObject areaToShowCards = null;
    [SerializeField] GameObject templateForCards = null;
    [Space]
    [SerializeField] Animator error = null;


    Dictionary<string, Subject> subjects;
    Subject currentSubject;

    int ccsi = int.MaxValue;
    private void HandleSchoolChange()
    {
        subjects = new Dictionary<string, Subject>();
        School school = constants.mainCore.shcools[constants.focusedShcoolIndex];

        foreach (var i in school.Subjects)
        {
            subjects.Add(i.Name, i);
        }
        dropdown.AddOptions(subjects.Keys.ToList());
    }

    private void Start()
    {
        constants.Deserialize();
    }

    private void Update()
    {
        List<School> alreadyAdded = new List<School>();

        foreach (var i in FindObjectsOfType<SchoolBox>())
        {
            if (!constants.mainCore.shcools.ToList().Contains(i.shcool)) { Destroy(i.gameObject); areaToShowSubjects.GetComponent<RectTransform>().sizeDelta -= new Vector2(0, 123.9061f); continue; }
            else { alreadyAdded.Add(i.shcool); }
        }

        foreach (var i in constants.mainCore.shcools)
        {
            if (alreadyAdded.Contains(i)) { continue; }
            else
            {
                GameObject game = Instantiate(templateForSubjects.gameObject, areaToShowSubjects.transform);
                game.transform.SetParent(areaToShowSubjects.transform);

                game.GetComponent<TextMeshProUGUI>().text = i.Name;
                game.GetComponent<SchoolBox>().shcool = i;

                areaToShowSubjects.GetComponent<RectTransform>().sizeDelta += new Vector2(0, game.GetComponent<RectTransform>().sizeDelta.y);

            }
        }

        int csi = int.MaxValue;
        foreach (var item in FindObjectsOfType<SchoolBox>())
        {
            if (item.GetComponent<TextMeshProUGUI>().color == Color.red) { csi = constants.mainCore.shcools.ToList().IndexOf(item.shcool); }
            
        }
        if (csi == int.MaxValue) { return; }
        if(ccsi != csi)
        {
            dropdown.ClearOptions();
            dropdown.AddOptions(new List<TMP_Dropdown.OptionData> { new TMP_Dropdown.OptionData("Please Choose A Subject") });
            subjects = new Dictionary<string, Subject>();
            School school = constants.mainCore.shcools[csi]; 
            foreach (var i in school.Subjects)
            {
                subjects.Add(i.Name, i);
            }

            dropdown.AddOptions(subjects.Keys.ToList());
        }
        ccsi = csi; 

        if (dropdown.value == 0) { return; }

      
        List<QuestionCard> alreadyAdded2 = new List<QuestionCard>();

        foreach (var i in FindObjectsOfType<CardBox>())
        {
            if (!constants.mainCore.shcools[csi].Subjects[dropdown.value-1].QuestionCards.ToList().Contains(i.card))
            { Destroy(i.gameObject); areaToShowCards.GetComponent<RectTransform>().sizeDelta -= new Vector2(0, 123.9061f); continue; }
            else { alreadyAdded2.Add(i.card); }
        }

        if(constants.mainCore.shcools[csi].Subjects[dropdown.value - 1].QuestionCards == null) { return; }
        foreach (var i in constants.mainCore.shcools[csi].Subjects[dropdown.value - 1].QuestionCards)
        {
            if (alreadyAdded2.Contains(i)) { continue; }
            else
            {
                GameObject game = Instantiate(templateForCards.gameObject, areaToShowCards.transform);
                game.transform.SetParent(areaToShowCards.transform);

                game.GetComponent<TextMeshProUGUI>().text = i.Question;
                game.GetComponent<CardBox>().card = i;

                areaToShowCards.GetComponent<RectTransform>().sizeDelta += new Vector2(0, game.GetComponent<RectTransform>().sizeDelta.y);

            }
        }


    }

    public void DiscardCard()
    {
        CardBox cardBox = null;
        foreach (var item in FindObjectsOfType<CardBox>())
        {
            if (item.GetComponent<TextMeshProUGUI>().color == Color.red) { cardBox = item; break; }
        }
        if (cardBox == null) { return; }

        var i = constants.mainCore.shcools[ccsi].Subjects[dropdown.value - 1].QuestionCards.ToList();
        i.Remove(cardBox.card);
        constants.mainCore.shcools[ccsi].Subjects[dropdown.value - 1].QuestionCards = i.ToArray();
        constants.Serialize();
    }

    public void Package(string scene)
    {
        CardBox cardBox = null;
        foreach (var item in FindObjectsOfType<CardBox>())
        {
            if (item.GetComponent<TextMeshProUGUI>().color == Color.red) { cardBox = item; break; }
        }

        if (dropdown.value - 1 < 0 || cardBox == null) { error.SetTrigger("Fail"); return; }
        constants.focusedShcoolIndex = (uint)ccsi;
        constants.focusedSubjectIndex = (uint)dropdown.value - 1;
        constants.questionanswercache = new KeyValuePair<string, string>(cardBox.card.Question, cardBox.card.Answer);

        StaticSceneLoader.ForceLoadScene(scene);
    }


    public void ChangeSubject(Int32 id)
    {
        try
        {
            currentSubject = subjects[dropdown.options[id].text];
        }
        catch
        {
            string name;
            if (currentSubject != null) { name = currentSubject.Name; }
            else { return; }
            dropdown.value = dropdown.options.Select(option => option.text).ToList().IndexOf(currentSubject.Name);
            return;
        }

    }
}
