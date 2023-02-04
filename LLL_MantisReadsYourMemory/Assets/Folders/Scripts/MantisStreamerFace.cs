using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantisStreamerFace : MonoBehaviour
{
    public StreamerSO so;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.mainTexture = so.mantisFaceTex;
    }
}
