using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
    public static void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
}
