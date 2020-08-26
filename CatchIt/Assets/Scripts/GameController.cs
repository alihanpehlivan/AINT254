using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{

	public Camera cam;
	public GameObject[] bottles;
	public float timeLeft;
	public Text timerText;
	public GameObject gameOverText;
	public GameObject restartButton;
	public GameObject splashScreen;
	public GameObject startButton;
	public BucketController bucketController;

	private float m_maxWidth;
	private bool m_isPlaying;
	private int bottleCount;

	// Use this for initialization
	void Start()
	{
		if (cam == null)
			cam = Camera.main;

		m_isPlaying = false;
		Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
		float bottleWidth = bottles[0].GetComponent<Renderer>().bounds.extents.x;
		m_maxWidth = targetWidth.x - bottleWidth;

		UpdateTimeText();
	}

	void FixedUpdate()
	{
		if (m_isPlaying)
		{
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0)
				timeLeft = 0;

			UpdateTimeText();
		}
	}

	public void StartGame()
	{
		splashScreen.SetActive(false);
		startButton.SetActive(false);
		m_isPlaying = true;
		bucketController.ToggledControl(true);
		StartCoroutine(Spawn());
	}

	public void EndGame()
	{
		gameOverText.SetActive(true);
		restartButton.SetActive(true);

		bucketController.ToggledControl(false);
		m_isPlaying = false;
	}

	public void BottleCountUpdate()
	{
		bottleCount--;
	}

	IEnumerator Spawn()
	{
		//yield return new WaitForSeconds (2.0f);
		while (timeLeft > 0)
		{
			int rand = Random.Range(1, 4);
			while (rand > 0)
			{
				GameObject bottle = bottles[Random.Range(0, bottles.Length)];
				Vector3 spawnPosition = new Vector3(
					Random.Range(-m_maxWidth, m_maxWidth),
					transform.position.y, 0.0f);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(bottle, spawnPosition, spawnRotation);
				bottleCount++;
				rand--;
				yield return new WaitForSeconds(Random.Range(0.5f, 0.7f));
			}

			yield return new WaitForSeconds(Random.Range(1.0f, 2.0f)); //Wait for 1 or 2 seconds & go for the loop again
		}

		yield return new WaitForSeconds(1.0f);
		EndGame();
	}

	void UpdateTimeText()
	{
		timerText.text = "Time Left\n" + Mathf.RoundToInt(timeLeft);
	}
}