using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public Text enemiesKilledText;

	void OnEnable()
	{
		enemiesKilledText.text = PlayerStats.enemiesKilled.ToString ();
	}

	public void Retry()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	public void Menu()
	{
		Debug.Log ("Go to menu");
	}

}
