using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamController : MonoBehaviour
{
    int currentCam = 0;
    [SerializeField] CinemachineVirtualCamera[] vcams;
    void Start()
    {
        vcams = Object.FindObjectsOfType<CinemachineVirtualCamera>();
        StartCoroutine(SwitchCams());
    }

    IEnumerator SwitchCams()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            currentCam = (currentCam + 1) % vcams.Length;
            SwitchToCam(currentCam);
        }
    }

    void SwitchToCam(int camIndex)
    {
        for (int i = 0; i < vcams.Length; ++i)
        {
            if (i == camIndex)
                vcams[i].Priority = 1;
            else
                vcams[i].Priority = -1;
        }
    }
}
