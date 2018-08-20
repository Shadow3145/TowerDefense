using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

	public Color hoverColor;
	private Color defaultColor;
	public Color notAbleToBuildColor;

	public Vector3 offset;

	private Renderer r;

	public GameObject turret;
	public TurretScheme turretScheme;
	public bool isUpgraded = false;

	Builder builder;

	void Start () 
	{
		r = GetComponent<Renderer>();
		defaultColor = r.material.color;

		builder = Builder.builderInstance;
	}

	public Vector3 GetBuildPosition ()
	{
		return transform.position + offset;
	}

	void OnMouseEnter()
	{
		//We donť want to be able to click on a node if there is our shop canvas iver it
		if (EventSystem.current.IsPointerOverGameObject ())
			return;

		//We can't build if we didn't select a turret
		if (!builder.AbleToBuild)
			return;

		//We don't want to build if we don't have enough money
		if (builder.EnoughMoney) {
			r.material.color = hoverColor;
		} 
		else
		{
			r.material.color = notAbleToBuildColor;
		}

	}

	//Set back color of the node
	void OnMouseExit()
	{
		r.material.color = defaultColor;
	}

	void OnMouseDown()
	{
		//We donť want to be able to click on a node if there is our shop canvas iver it
		if (EventSystem.current.IsPointerOverGameObject ())
			return;

		//Select turret to upgrade or sell
		if (turret != null) 
		{
			builder.SelectNode (this);
			return;
		}

		if (!builder.AbleToBuild)
			return;

		BuildTurret(builder.GetTurret());

	}

	void BuildTurret(TurretScheme scheme)
	{
		if (PlayerStats.money < scheme.cost) 
		{
			return;
		}
		PlayerStats.money -= scheme.cost;
		GameObject _turret = (GameObject) Instantiate (scheme.prefab, GetBuildPosition (), Quaternion.identity);
		turret = _turret;
		turretScheme = scheme;
		turretScheme.totalCost = turretScheme.cost;
		builder.UnselectTurret ();
		 
	}

	public void UpgradeTurret()
	{
		if (PlayerStats.money < turretScheme.upgradeCost) 
		{
			return;
		}

		PlayerStats.money -= turretScheme.upgradeCost;

		Destroy (turret);

		GameObject _turret = (GameObject) Instantiate (turretScheme.upgradedPrefab, GetBuildPosition (), Quaternion.identity);
		turret = _turret;
		turretScheme.totalCost += turretScheme.upgradeCost;
		isUpgraded = true;

	}

	public void SellTurret()
	{
		PlayerStats.money += turretScheme.GetSellAmount ();
		Destroy (turret);
		turretScheme = null;
	}


}
