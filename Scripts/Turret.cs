using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	private Transform target;
	private Enemy enemyComponent;
	private float fireTimer = 0f;


	[Header("General Stats")]
	public float range = 15f;
	public float zeroInSpeed = 10f;


	[Header("Use Bullets")]
	public GameObject bulletPrefab;
	public float roundsPerSecond = 1f;

	[Header("Laser Beamer")]
	public bool useLaser = false;
	public int damageOverTime = 0;
	public float slowingEffect = 0.5f;

	public LineRenderer lineRenderer;
	public ParticleSystem impactEffect;
	public Light laserLight;

	[Header ("Aiming")]
	public string enemyTag = "Enemy";
	public Transform mainBarrel;
	public Transform rotate;

	[Header("Upgraded Turret")]
	public bool isUpgraded = false;
	public GameObject bulletSecondary1Prefab;
	public GameObject bulletSecondary2Prefab;
	public Transform secondaryBarrel1;
	public Transform secondaryBarrel2;

	 


	void Start () 
	{
		InvokeRepeating ("FindTarget", 0f, 0.5f);
	}

	void FindTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag (enemyTag);

		float closest = Mathf.Infinity;
		GameObject closestEnemy = null;

		foreach (GameObject enemy in enemies) 
		{
			float distanceFromEnemy = Vector3.Distance (transform.position, enemy.transform.position);
			if (distanceFromEnemy < closest) 
			{
				closest = distanceFromEnemy;
				closestEnemy = enemy;
			}
		}

		if (closestEnemy != null && closest <= range) {
			target = closestEnemy.transform;
			enemyComponent = closestEnemy.GetComponent<Enemy> ();

		} else 
		{
			target = null;
		}

	}
	
	void Update () 
	{
		if (target == null) 
		{
			if (useLaser)
			{
				if (lineRenderer.enabled) 
				{
					lineRenderer.enabled = false;
					impactEffect.Stop ();
					laserLight.enabled = false;
				}
			}
			return;
		}

		LockTarget ();

		if (useLaser) {
			ActivateLaser ();
		} else
		{
			if (fireTimer <= 0f) 
			{
				Shoot ();
				fireTimer = 1f / roundsPerSecond;
			}

			fireTimer -= Time.deltaTime; 
		}
	
	}

	void LockTarget()
	{
		Vector3 direction = target.position - transform.position; 
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		Vector3 rotation = Quaternion.Lerp(rotate.rotation, lookRotation, Time.deltaTime*zeroInSpeed).eulerAngles;
		rotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);  

	}

	void ActivateLaser()
	{
		enemyComponent.TakeDamage (damageOverTime * Time.deltaTime); 
		enemyComponent.Slow (slowingEffect);

		if (!lineRenderer.enabled)
		{
			lineRenderer.enabled = true;
			impactEffect.Play ();
			laserLight.enabled = true;
		}

		lineRenderer.SetPosition (0, mainBarrel.position);
		lineRenderer.SetPosition (1, target.position);

		Vector3 direction = mainBarrel.position - target.position;
		impactEffect.transform.rotation = Quaternion.LookRotation(direction);
		impactEffect.transform.position = target.position + direction.normalized*0.85f;
	}


	void Shoot()
	{
		GameObject bulletObject = (GameObject)Instantiate (bulletPrefab, mainBarrel.position, mainBarrel.rotation);
		Bullet bullet = bulletObject.GetComponent<Bullet> ();

		if (bullet != null)
			bullet.ChaseTarget (target);
			
		if (isUpgraded) 
		{
			GameObject secBulletObject = (GameObject) Instantiate (bulletSecondary1Prefab, secondaryBarrel1.position, secondaryBarrel1.rotation);
			Bullet secBullet = secBulletObject.GetComponent<Bullet> ();
			if (secBullet != null)
				secBullet.ChaseTarget (target);
			GameObject _secBulletObject = (GameObject) Instantiate (bulletSecondary2Prefab, secondaryBarrel2.position, secondaryBarrel2.rotation);
			Bullet secondaryBullet = _secBulletObject.GetComponent<Bullet> ();
			if (secondaryBullet != null)
				secondaryBullet.ChaseTarget (target);
		}


	}


}
