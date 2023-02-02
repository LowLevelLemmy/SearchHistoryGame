using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class UI_StatsPlacer : MonoBehaviour
{
    [SerializeField] GameObject entryPrefab;
    [SerializeField] Transform contentTransf;

    // Start is called before the first frame update
    void OnEnable()
    {
        DisplayAllMatches();
    }

    [Button]
    void DEBUG_Spawn5()
    {
        for (int i = 0; i < 5; ++i)
        {
            SpawnEntry("abba", 999, "penis");
        }
    }

    [Button]
    void DisplayAllMatches()
    {
        var abba = StatsCollector.GetAllStatKeywordMatches();
        foreach (var i in abba)
        {
            SpawnEntry(i.keyword, i.instances, i.from);
        }
    }

    void SpawnEntry(string keyword, int count, string from)
    {
        var spawned = Instantiate(entryPrefab, contentTransf).GetComponent<StatEntry>();
        spawned.countTxt.text = count.ToString();
        spawned.keywordTxt.text = keyword;
        spawned.fromTxt.text = from;
    }
}
