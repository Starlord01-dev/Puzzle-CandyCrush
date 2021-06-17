using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUpdater : MonoBehaviour
{
    public GameObject board;
    private TextMeshProUGUI scoreText;
    private int CurrentScore;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        CurrentScore = board.GetComponent<CollapseBoard>().Score;
        scoreText.text = "Score:" + CurrentScore;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentScore!= board.GetComponent<CollapseBoard>().Score)
        {
            CurrentScore = board.GetComponent<CollapseBoard>().Score;
            scoreText.text = "Score:" + CurrentScore ;
        }
    }
}
