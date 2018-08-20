using UnityEngine;
using UnityEngine.UI;

public class Builder : MonoBehaviour {

	//Singleton pattern
	public static Builder builderInstance;

	void Awake()
	{
		if (builderInstance != null)
			return;
		builderInstance = this;
	}

	public GameObject turretPrefab;
	public GameObject missileLauncherPrefab;
	public GameObject laserBeamerPrefab;

	private TurretScheme turretToBuild; 
	private Node selectedNode;
	public NodeUI nodeUI;

	public bool AbleToBuild { get { return turretToBuild != null; }}
	public bool EnoughMoney { get { return PlayerStats.money >= turretToBuild.cost; }}

	public void SetTurret(TurretScheme turret)
	{
		turretToBuild = turret;
		DeselectNode ();
	}

	public TurretScheme GetTurret()
	{
		return turretToBuild;
	}

	public void SelectNode (Node node)
	{
		if (selectedNode == node) 
		{
			DeselectNode ();
		}
		selectedNode = node;
		turretToBuild = null;
		nodeUI.SetNode (node);
	}

	public void DeselectNode ()
	{
		selectedNode = null;
		nodeUI.Hide ();
	}

	public void UnselectTurret()
	{
		turretToBuild = null;
	}

}
