using UnityEngine;

[System.Serializable]

public class TurretScheme {

	public GameObject prefab;
	public int cost;

	public GameObject upgradedPrefab;
	public int upgradeCost;

	public int totalCost;

	public int GetSellAmount()
	{
		return totalCost / 2;
	}

}
