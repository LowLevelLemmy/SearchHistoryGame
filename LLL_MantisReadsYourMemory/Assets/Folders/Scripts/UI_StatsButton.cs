using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StatsButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // This makes it so they have to play the game at least once for button to appear
        int showButton = PlayerPrefs.GetInt("showStats", 0);
        if (showButton == 0)
        {
            PlayerPrefs.SetInt("showStats", 1);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }
}
