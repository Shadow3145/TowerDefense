using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static bool isGameOver;

	public GameObject gameOverUI;

	void Start()
	{
		isGameOver = false;
	}

	void Update () 
	{
		if (isGameOver)
			return;
		
		if (Input.GetKeyDown("e"))
				EndGame();

		if (PlayerStats.healthPoints <= 0)
		{
			EndGame ();
		}
	}

	void EndGame()
	{
		isGameOver = true;

		gameOverUI.SetActive (true);

	}
}
