using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TurnOffButton : MonoBehaviour
{
    public TextMeshProUGUI ButtonText;

    public void TurnOffButtonText()
    {
        ButtonText.enabled = false;
    }
}
