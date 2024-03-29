using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTracker : MonoBehaviour
{
    [Space]
    [Header("Scores")]
    public int gemScore = -1;
    public int ScoreS = 0; // 0
    public int ScoreA = 0; // 1
    public int ScoreB = 0; // 2
    public int ScoreC = 0; // 3

    private Dictionary<int, int> scoresDic = new Dictionary<int, int>();

    [Space]
    [Header("Overige")]
    public float deliveriesMade = 0;
    public float playTime = 0;
    public float gemTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoresDic.Add(0, ScoreS);
        scoresDic.Add(1, ScoreA);
        scoresDic.Add(2, ScoreB);
        scoresDic.Add(3, ScoreC);
    }

    // Update is called once per frame
    void Update()
    {
        gemTime = playTime / deliveriesMade;
        scoresDic[0] = ScoreS;
        scoresDic[1] = ScoreA;
        scoresDic[2] = ScoreB;
        scoresDic[3] = ScoreC;

        foreach (int i in scoresDic.Keys)
        {
            Debug.Log("score : " + i + " = " + scoresDic[i]);
            if (scoresDic[i] > scoresDic[gemScore])
            {
                gemScore = i;
            }
        }
    }

    public void CheckHighstScore()
    {
        foreach (int i in scoresDic.Keys)
        {
            Debug.Log("score : " + i + " = " + scoresDic[i]);
            if (scoresDic[i] > gemScore)
            {
                gemScore = i;
            }
        }
    }
}
