using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using TMPro;
using UnityEngine;

public class AdvSet : MonoBehaviour
{
    [SerializeField] GameObject areaToPutFiles = null;
    [SerializeField] BasicBox template = null;
    [SerializeField] Constants constants = null;

    [Space]
    [Header("Animators")]
    [SerializeField] Animator sm = null;
    [SerializeField] Animator ss, sl, fe, fp = null;

    [Space]
    [Header("Music")]
    [SerializeField] ClipAtPointR pointR = null;
    [SerializeField] AudioClip suc, fail = null;

    public const string FILE_EXTENSTION = ".RevisionCards";

    private void Start()
    {
        UpdateDownloadsArea();

    }

    private void UpdateDownloadsArea()
    {
        //         Path  File Name
        Dictionary<string, string> paths = new Dictionary<string, string>();

         foreach (var i in Directory.GetFiles(@"/storage/emulated/0//Download"))
          {
              if (Path.GetExtension(i) == FILE_EXTENSTION)
              {
                  paths.Add(i, Path.GetFileName(i));
              }
          }


        foreach (var i in paths)
        {
            GameObject game = Instantiate(template.gameObject, areaToPutFiles.transform);
            game.transform.SetParent(areaToPutFiles.transform);

            game.GetComponent<TextMeshProUGUI>().text = i.Value;
            game.GetComponent<BasicBox>().arg = i.Key;

            areaToPutFiles.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 123.9061f);
        }


    }

    public void SaveCurrentFile()
    {
        constants.Deserialize();

        string epath = "Package" + Random.Range(0, 10000) + FILE_EXTENSTION;
        string path = @"/storage/emulated/0//Download/" + epath;

        try
        {
            File.Copy(Application.persistentDataPath + "/SaveFiles/SaveFile.X1SaveFile", path);

            
        }
        catch
        {
            pointR.PlayClipAtPoint(fail); fe.SetTrigger("Fail");
        }

        GameObject game = Instantiate(template.gameObject, areaToPutFiles.transform);
        game.transform.SetParent(areaToPutFiles.transform);

        game.GetComponent<BasicBox>().arg = path;
        game.GetComponent<TextMeshProUGUI>().text = epath;


        areaToPutFiles.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 123.9061f);
        pointR.PlayClipAtPoint(suc);
        ss.SetTrigger("Success");

    }

    public void Load()
    {
        string path = string.Empty;

        BasicBox basicBox = null;
        foreach (var i in FindObjectsOfType<BasicBox>())
        {
            if(i.GetComponent<TextMeshProUGUI>().color == Color.red)
            {
                basicBox = i;
                break;
            }
        }
        if(basicBox == null) { pointR.PlayClipAtPoint(fail); fp.SetTrigger("Fail"); return; }
        try
        {
        path = (string)basicBox.arg;
        
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(MainCore));
                constants.mainCore = dataContractJsonSerializer.ReadObject(fs) as MainCore;
            }

            constants.Serialize();
            pointR.PlayClipAtPoint(suc);
            sl.SetTrigger("Success");
        }
        catch
        {
            pointR.PlayClipAtPoint(fail);
            fe.SetTrigger("Fail");
        }
    }

    public void Merge()
    {
        constants.Deserialize();
        MainCore main = constants.mainCore;
        MainCore commit;

        try
        {
            #region FindCommit
            string path = string.Empty;

            BasicBox basicBox = null;
            foreach (var i in FindObjectsOfType<BasicBox>())
            {
                if (i.GetComponent<TextMeshProUGUI>().color == Color.red)
                {
                    basicBox = i;
                    break;
                }
            }
            if (basicBox == null) { pointR.PlayClipAtPoint(fail); fp.SetTrigger("Fail"); return; }
            path = (string)basicBox.arg;

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(MainCore));
                commit = dataContractJsonSerializer.ReadObject(fs) as MainCore;
            }
            #endregion

            #region StageMerges
            //         commit  main
            var merges = new List<KeyValuePair<School, School>>();
            foreach (var school in commit.shcools)
            {
                bool success = false;
                foreach (var ms in main.shcools)
                {
                    if (school.Name == ms.Name) { merges.Add(new KeyValuePair<School, School> (school, ms)); success = true; break; }
                }
                if (!success) { var i = main.shcools.ToList(); i.Add(school); main.shcools = i.ToArray(); }
            }
            #endregion

            #region Merge
            foreach (var merge in merges)
            {
                var mergeSubjects = new List<KeyValuePair<Subject, Subject>>();

                // Commit, Main
                foreach (var x in merge.Key.Subjects)
                {
                    bool success = false;
                    foreach (var y in merge.Value.Subjects)
                    {
                        if (x.Name == y.Name) { mergeSubjects.Add(new KeyValuePair<Subject, Subject>( x, y)); success = true; break; }
                    }
                    if (!success) { var i = merge.Value.Subjects.ToList(); i.Add(x); merge.Value.Subjects = i.ToArray(); }
                }

                foreach (var mergesubject in mergeSubjects)
                {
                    foreach (var x in mergesubject.Key.QuestionCards)
                    {
                        bool failiure = false;
                        foreach (var y in mergesubject.Value.QuestionCards)
                        {
                            if (x.Question == y.Question) { failiure = true; break; }
                        }
                        if (!failiure) { var i = mergesubject.Value.QuestionCards.ToList(); i.Add(x); mergesubject.Value.QuestionCards = i.ToArray(); }
                    }
                }

            }
            #endregion

            #region Clean_Up
            constants.mainCore = main;
            constants.Serialize();
            #endregion
        }
        catch
        {
            pointR.PlayClipAtPoint(fail);
            fe.SetTrigger("Fail");
            return;
        }
        
        
            pointR.PlayClipAtPoint(suc);
            sm.SetTrigger("Success");
        
    }

    public void Home()
    {
        StaticSceneLoader.ForceLoadScene(0);
    }
}
