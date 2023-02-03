using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using EasyButtons;

public class DEBUG_UI_GameStatPlacer : MonoBehaviour
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
            SpawnEntry("abba", 999);
        }
    }

    [Button]
    void DisplayAllMatches()
    {
        var abba = GamesFinder.GetGamesInstalled();
        foreach (var i in abba)
        {
            SpawnEntry(i.gameDir, 1);
        }
    }

    void SpawnEntry(string keyword, int count)
    {
        var spawned = Instantiate(entryPrefab, contentTransf).GetComponent<StatEntry>();
        spawned.countTxt.text = count.ToString();
        spawned.keywordTxt.text = keyword;
    }
}
