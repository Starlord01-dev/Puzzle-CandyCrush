using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeEdit_Play : MonoBehaviour
{
    public void ChangeMode()
    {
        FindObjectOfType<EditorBoard>().editMode= !FindObjectOfType<EditorBoard>().editMode;
    }
}
