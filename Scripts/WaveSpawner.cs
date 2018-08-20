using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	public Wave[] waves;

	public static int enemiesAlive = 0;

	public Transform spawnPoint;

	public float timer = 20f;
	private float countdown = 10f;

	public static int waveIndex;
	public static int amountOfWaves;

	void Start()
	{
		amountOfWaves = waves.Length;
		waveIndex = 0;
	}

	void Update()
	{
		if (waveIndex == waves.Length) 
		{
			if (enemiesAlive == 0) 
			{
				//Debug.Log ("Enemies alive:" + enemiesAlive);
				Debug.Log ("WinLevel!");
			}
			return;
		}

		if (countdown <= 0f) 
		{
			//Debug.Log ("Incoming!");
			countdown = timer;
			StartCoroutine (WaveSpawn ());
			waveIndex++;
			return;
		}

		countdown -= Time.deltaTime;
	}

	IEnumerator WaveSpawn()
	{
		countdown = timer;
		Wave wave = waves[waveIndex];

		enemiesAlive += wave.amount;
		//Debug.Log (enemiesAlive);

		for (int i = 0; i < wave.amount; i++) 
		{
			SpawnEnemy (wave.enemy);
			yield return new WaitForSeconds (1f/wave.spawnRate);
		}

	}

	void SpawnEnemy(GameObject enemy)
	{
		Instantiate (enemy, spawnPoint.position, spawnPoint.rotation);
	}







}
