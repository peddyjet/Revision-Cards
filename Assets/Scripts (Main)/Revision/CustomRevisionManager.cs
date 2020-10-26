using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CustomRevisionManager : MonoBehaviour
{
    [SerializeField] Constants constants = null;
    [Space]
    [SerializeField] GameObject areaToShowSubjects = null;
    [SerializeField] SchoolBox template = null;
    [SerializeField] TMP_Dropdown dropdown = null;
    [Space]
    [SerializeField] Animator error = null;

    Dictionary<string, Subject> subjects;
    Subject currentSubject;

    int ccsi = int.MaxValue;

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
                GameObject game = Instantiate(template.gameObject, areaToShowSubjects.transform);
                game.transform.SetParent(areaToShowSubjects.transform);

                game.GetComponent<TextMeshProUGUI>().text = i.Name;
                game.GetComponent<SchoolBox>().shcool = i;

                areaToShowSubjects.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 123.9061f);

            }
        }

        int csi = int.MaxValue;
        foreach (var item in FindObjectsOfType<SchoolBox>())
        {
            if (item.GetComponent<TextMeshProUGUI>().color == Color.red) { csi = constants.mainCore.shcools.ToList().IndexOf(item.shcool); }

        }
        if (csi == int.MaxValue) { return; }
        if (ccsi != csi)
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

    public void Package()
    {
        if (dropdown.value - 1 < 0) { error.SetTrigger("Fail"); return; }
        constants.focusedShcoolIndex = (uint)ccsi;
        constants.feededSubject = constants.mainCore.shcools[ccsi].Subjects[dropdown.value - 1];

        StaticSceneLoader.ForceLoadScene("Test");
    }

    private void Start()
    {
        constants.Deserialize();
    }

}
