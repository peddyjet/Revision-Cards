using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CardMakerManager : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown = null;
    [SerializeField] TMP_InputField question = null;
    [SerializeField] TMP_InputField answer = null;
    [Space]
    [SerializeField] Constants constants = null;
    [Space]
    [SerializeField] Animator success = null;
    [SerializeField] Animator failiure = null;
    [Space]
    [SerializeField] AudioClip successac = null;
    [SerializeField] AudioClip failiureac = null;

    Dictionary<string, Subject> subjects;
    Subject currentSubject;
    private void Start()
    {
        subjects = new Dictionary<string, Subject>();
        School school = constants.mainCore.shcools[constants.focusedShcoolIndex];

        foreach (var i in school.Subjects)
        {
            subjects.Add(i.Name, i);
        }

        dropdown.AddOptions(subjects.Keys.ToList());

        if (constants.questionanswercache.Key != "" && constants.questionanswercache.Value != "" && constants.focusedShcoolIndex != int.MaxValue && constants.focusedSubjectIndex != int.MaxValue)
        {
            question.text = constants.questionanswercache.Key;
            answer.text = constants.questionanswercache.Value;
            dropdown.value = dropdown.options.Select(option => option.text).ToList().IndexOf(constants.mainCore.shcools[constants.focusedShcoolIndex].Subjects[constants.focusedSubjectIndex].Name);
        }
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
    public void Save()
    {
        if (dropdown.value == 0 || question.text == string.Empty || answer.text == string.Empty) { FindObjectOfType<ClipAtPointR>().PlayClipAtPoint(failiureac); failiure.SetTrigger("Fail"); return; }

        if (constants.questionanswercache.Key != "" && constants.questionanswercache.Value != "" && constants.focusedShcoolIndex != int.MaxValue && constants.focusedSubjectIndex != int.MaxValue)
        {
           var i = constants.mainCore.shcools[constants.focusedShcoolIndex].Subjects[constants.focusedSubjectIndex].QuestionCards.ToList();
           var x = i.Select(q => q.Question).ToList().IndexOf(constants.questionanswercache.Key);
           i.RemoveAt(x);
           constants.mainCore.shcools[constants.focusedShcoolIndex].Subjects[constants.focusedSubjectIndex].QuestionCards = i.ToArray();
        }

        var subjectsTrue = constants.mainCore.shcools[constants.focusedShcoolIndex].Subjects.ToList();
        if (subjectsTrue[subjectsTrue.IndexOf(currentSubject)].QuestionCards == null)
        { subjectsTrue[subjectsTrue.IndexOf(currentSubject)].QuestionCards = new QuestionCard[] { new QuestionCard { Question = question.text, Answer = answer.text, Proficiency = 1 } }; }
        else
        {
            var i = subjectsTrue[subjectsTrue.IndexOf(currentSubject)].QuestionCards.ToList();
            i.Add(new QuestionCard { Question = question.text, Answer = answer.text, Proficiency = 1 });
            subjectsTrue[subjectsTrue.IndexOf(currentSubject)].QuestionCards = i.ToArray();
        }
        constants.mainCore.shcools[constants.focusedShcoolIndex].Subjects = subjectsTrue.ToArray();
        constants.Serialize();
        FindObjectOfType<ClipAtPointR>().PlayClipAtPoint(successac);
        success.SetTrigger("Success");
        question.text = string.Empty;
        answer.text = string.Empty;
    }
}
