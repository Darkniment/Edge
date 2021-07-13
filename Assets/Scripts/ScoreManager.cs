using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager instance;
    public Text scoreText;
	public int score = 0;
	public void Awake(){
		instance = this;
	}
	public void Start()
    {
     scoreText.text = score.ToString();
	 
    }
	
	public void AddPoint()
    {
        score ++;
		scoreText.text = score.ToString();
    }
}
