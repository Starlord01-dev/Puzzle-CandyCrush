using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUpdater : MonoBehaviour
{
    public GameObject board;
    private TextMeshProUGUI coinText;
    private int CurrentCoins;

    // Start is called before the first frame update
    void Start()
    {
        coinText = GetComponent<TextMeshProUGUI>();
        CurrentCoins = board.GetComponent<CollapseBoard>().coinsCollected;
        coinText.text = "Coins:" + CurrentCoins;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentCoins != board.GetComponent<CollapseBoard>().coinsCollected)
        {
            CurrentCoins = board.GetComponent<CollapseBoard>().coinsCollected;
            coinText.text = "Coins:" + CurrentCoins;
        }
    }
}
