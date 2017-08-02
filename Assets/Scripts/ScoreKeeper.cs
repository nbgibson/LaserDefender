using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

    private Text myText;

    public static int score = 0;

    public void Start()
    {
        myText = GetComponent<Text>();
        Reset();
    }

    public void Score(int points)
    {
        score += points;
        myText.text = score.ToString();
    }

    public static void Reset()
    {
        score = 0;
    }
}
