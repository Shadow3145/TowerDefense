using UnityEngine;

public class PauseMenu : MonoBehaviour 
{
	public GameObject UI;
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.P)) 
		{
			Toggle ();
		}
	}

	public void Toggle()
	{
		UI.SetActive (!UI.activeSelf);

		if (UI.activeSelf) {
			Time.timeScale = 0f;
		} else 
		{
			Time.timeScale = 1f;
		}
	}
}
