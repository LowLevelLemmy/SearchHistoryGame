using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using EasyButtons;

public class DEBUG_UI_MoreBrowserStats : MonoBehaviour
{
    [SerializeField] GameObject entryPrefab;
    [SerializeField] Transform contentTransf;

    // Start is called before the first frame update
    void OnEnable()
    {
        DisplayAllMatches();
    }

    // [Button]
    void DisplayAllMatches()
    {
        var abba = HistoryGetter.GetHistories();
        foreach (var i in abba)
        {
            string historyFileName = i.Substring(i.LastIndexOf('/') + 1);
            SpawnEntry(historyFileName);
        }
    }

    void SpawnEntry(string keyword)
    {
        // this should be inside StatEntry... im dum 
        var spawned = Instantiate(entryPrefab, contentTransf).GetComponent<StatEntry>();
        spawned.countTxt.text = "";
        spawned.keywordTxt.text = keyword;
        spawned.fromTxt.text = "";
    }
}
