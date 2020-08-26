using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
	public Text scoreText;
	public int bottleValue;

	private int score;

	// Use this for initialization
	void Start()
	{
		score = 0;
		UpdateScore();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Bomb")
			score -= bottleValue * 2;
		else
			score += bottleValue;

		UpdateScore();
	}

	void UpdateScore()
	{
		scoreText.text = "Total Score\n" + score;
	}
}
