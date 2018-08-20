using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour 
{

	public static int money;
	public int startMoney = 400;

	public static int healthPoints;
	public int startHealthPoints = 200;

	public static int enemiesKilled;

	public static int Levels;

	void Start()
	{
		money = startMoney;
		healthPoints = startHealthPoints;

		enemiesKilled = 0;

		Levels = 0;
	}

}
