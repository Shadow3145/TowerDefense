using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour {

	public Text waveText;

	void Update()
	{
		waveText.text = WaveSpawner.waveIndex + "/" + WaveSpawner.amountOfWaves + " ";
	}

}
