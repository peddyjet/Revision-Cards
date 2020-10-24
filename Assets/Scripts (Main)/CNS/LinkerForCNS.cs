using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LinkerForCNS : MonoBehaviour
{
    List<Subject> cachedSubjects;
    [SerializeField] Constants constants = null;
    [Space]
    [SerializeField] GameObject areaToShowSubjects = null;
    [SerializeField] SubjectBox template = null;
    [Space]
    [SerializeField] TextMeshProUGUI title = null;
    

    private void Awake()
    {
        cachedSubjects = new List<Subject>();
    }

    public void AddToTempSubjects(string name) => cachedSubjects.Add(new Subject { Name = name });
    public void AddToTempSubjects(TextMeshProUGUI name) { cachedSubjects.Add(new Subject { Name = name.text }); name.text = string.Empty; }
    public void RemoveFromTempSubjects(Subject subject) => cachedSubjects.Remove(subject);
    public void AssistedRemove()
    {
        foreach (var i in FindObjectsOfType<SubjectBox>())
        {
            if (i.gameObject.GetComponent<TextMeshProUGUI>().color == Color.red) { RemoveFromTempSubjects(i.subject); return; }
        }
    }
    public void Save()
    {
        if(constants.focusedShcoolIndex != int.MaxValue)
        {
            var i = constants.mainCore.shcools.ToList();
            i.Remove(constants.mainCore.shcools[constants.focusedShcoolIndex]); constants.mainCore.shcools = i.ToArray();
        }

        var newSchcool = new School();
        newSchcool.Name = title.text;
        newSchcool.Subjects = cachedSubjects.ToArray();


       if (constants.mainCore.shcools == null) { constants.mainCore.shcools = new School[] {newSchcool }; }
       else { var i = constants.mainCore.shcools.ToList(); i.Add(newSchcool); constants.mainCore.shcools = i.ToArray(); }

        constants.Serialize();

        StaticSceneLoader.ForceLoadScene(0);
    }

    public void Update()
    {
        List<Subject> alreadyAdded = new List<Subject>();

        foreach (var i in FindObjectsOfType<SubjectBox>())
        {
            if (!cachedSubjects.Contains(i.subject)) { Destroy(i.gameObject); areaToShowSubjects.GetComponent<RectTransform>().sizeDelta -= new Vector2(0, 123.9061f); continue; }
            else { alreadyAdded.Add(i.subject); }
        }

        foreach (var i in cachedSubjects)
        {
            if (alreadyAdded.Contains(i)) { continue; }
            else
            {
                GameObject game = Instantiate(template.gameObject, areaToShowSubjects.transform);
                game.transform.SetParent(areaToShowSubjects.transform);

                game.GetComponent<TextMeshProUGUI>().text = i.Name;
                game.GetComponent<SubjectBox>().subject = i;

                areaToShowSubjects.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 123.9061f);
                
            }
        }
    }
    public void Start()
    {
        constants.Deserialize();
        if (constants.focusedShcoolIndex == int.MaxValue) { return; }
        else
        {
            FindObjectOfType<TMP_InputField>().text = constants.mainCore.shcools[constants.focusedShcoolIndex].Name;
            cachedSubjects = constants.mainCore.shcools[constants.focusedShcoolIndex].Subjects.ToList();
        }
    }
}
