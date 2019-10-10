using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementScore(int value)
    {
        score = score + value;
    }

    void OnGUI()
    {
        if (score > -1)
        {
            GUI.Label(new Rect(10, 40, 100, 50), "Score: " + score);
        }
    }
}
