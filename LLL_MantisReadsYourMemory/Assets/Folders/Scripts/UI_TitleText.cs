using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_TitleText : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public StreamerSO so;
    void Start()
    {
        txt.text = so.gameTitle;
    }
}
