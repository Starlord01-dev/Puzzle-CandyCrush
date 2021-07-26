using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPath : MonoBehaviour
{
    public string setPath;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.Path = setPath;
    }
}
