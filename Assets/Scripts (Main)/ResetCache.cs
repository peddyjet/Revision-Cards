using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCache : MonoBehaviour
{
    [SerializeField] Constants constants = null;
    // Start is called before the first frame update
    void Start()
    {
        constants.focusedShcoolIndex = int.MaxValue;
        constants.mainCore = new MainCore();
        constants.mainCore.shcools = new School[0];
        constants.questionanswercache = new KeyValuePair<string, string>(string.Empty,string.Empty);
        constants.focusedSubjectIndex = int.MaxValue;
        constants.Deserialize();
    }

   
}
