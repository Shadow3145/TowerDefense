using UnityEngine;

public class Shop : MonoBehaviour {

	Builder builder;
	public TurretScheme turret;
	public TurretScheme missileLauncher;
	public TurretScheme laserBeamer;

	void Start()
	{
		builder = Builder.builderInstance;
	}

	public void SelectTurret()
	{
		builder.SetTurret (turret);
	}

	public void SelectMissileLauncher()
	{
		builder.SetTurret (missileLauncher);
	}

	public void SelectLaserBeamer()
	{
		builder.SetTurret (laserBeamer);
	}
}
