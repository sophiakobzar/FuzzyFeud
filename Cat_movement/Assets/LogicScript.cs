using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text score;
    public int num_Fish=0;
    // Start is called before the first frame update
    [ContextMenu("Increase Score")]
    public void addScore()
    {
        playerScore++;
        score.text = playerScore.ToString();
    }
    public void updateFFishNum(int num)
    {
        num_Fish = num_Fish + num;
    }
}
