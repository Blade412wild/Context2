using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTracker : MonoBehaviour
{
    [Space]
    [Header("Scores")]
    public int gemScore = 0;
    public int ScoreS = 0;
    public int ScoreA = 0;
    public int ScoreB = 0;
    public int ScoreC = 0;

    [Space]
    [Header("Overige")]
    public int deliveriesMade = 0;
    public float playTime = 0;
    public int gemTime = 0;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
