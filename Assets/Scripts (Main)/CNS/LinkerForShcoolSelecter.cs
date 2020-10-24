using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LinkerForShcoolSelecter : MonoBehaviour
{
    [SerializeField] Constants constants = null;
    [Space]
    [SerializeField] GameObject areaToShowSubjects = null;
    [SerializeField] SchoolBox template = null;

    // Update is called once per frame
    void Update()
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
    }
    public void Package()
    {
        SchoolBox shcoolBox = null;
        foreach (var item in FindObjectsOfType<SchoolBox>())
        {
            if (item.GetComponent<TextMeshProUGUI>().color == Color.red) { shcoolBox = item; break; }
        }
        if (shcoolBox == null) { return; }

        constants.focusedShcoolIndex = (uint)constants.mainCore.shcools.ToList().IndexOf(shcoolBox.shcool);

        StaticSceneLoader.ForceLoadScene("CreateNewSchool");
    }
    public void Package(string scene)
    {
        SchoolBox shcoolBox = null;
        foreach (var item in FindObjectsOfType<SchoolBox>())
        {
            if (item.GetComponent<TextMeshProUGUI>().color == Color.red) { shcoolBox = item; break; }
        }
        if (shcoolBox == null) { return; }

        constants.focusedShcoolIndex = (uint)constants.mainCore.shcools.ToList().IndexOf(shcoolBox.shcool);

        StaticSceneLoader.ForceLoadScene(scene);
    }
    public void RemoveShcool()
    {
        SchoolBox shcoolBox = null;
        foreach (var item in FindObjectsOfType<SchoolBox>())
        {
            if(item.GetComponent<TextMeshProUGUI>().color == Color.red) { shcoolBox = item; break; }
        }
        if (shcoolBox == null) { return; }

        var i = constants.mainCore.shcools.ToList();
        i.Remove(shcoolBox.shcool);
        constants.mainCore.shcools = i.ToArray();
        constants.Serialize();
    }
    private void Start()
    {
        constants.Deserialize();
    }
}
