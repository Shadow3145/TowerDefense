using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	private float speed;
	public float startSpeed = 10f;

	private float health;
	public float startHealth = 100;

	public int reward = 50;
	public int damage = 10;

	public GameObject deathEffect;

	private Transform point;
	private int wayPointIndex = 0;

	public Image healthBar;


	void Start () 
	{
		speed = startSpeed;
		health = startHealth;
		point = Waypoints.points [0];
	}
	

	void Update () 
	{
		Vector3 direction = point.position - transform.position;
		transform.Translate (direction.normalized * speed * Time.deltaTime, Space.World);

		if (Vector3.Distance (transform.position, point.position) <= 0.35f) 
		{
			GetNextWaypoint ();		}
	}

	void GetNextWaypoint()
	{
		if (wayPointIndex >= Waypoints.points.Length -1) 
		{
			MakeDamage ();
			return;
		}

		wayPointIndex++;
		point = Waypoints.points [wayPointIndex];
	}

	public void TakeDamage(float amount)
	{
		if (health <= 0)
			return;
		health -= amount;

		healthBar.fillAmount = health / startHealth;

		if (health <= 0) 
		{
			Die ();
		}

	}

	public void Slow(float amount)
	{
		speed = startSpeed * (1f - amount);
	}
	

	public void Die()
	{
		PlayerStats.money += reward;
		PlayerStats.enemiesKilled++;
		GameObject effect = (GameObject)Instantiate (deathEffect, transform.position, Quaternion.identity);
		Destroy (effect, 2f);
		Destroy (gameObject);
		WaveSpawner.enemiesAlive--;
		//Debug.Log ("Enemies alive:" + WaveSpawner.enemiesAlive);
	}

	public void MakeDamage()
	{
		PlayerStats.healthPoints -= damage;
		Destroy (gameObject); 

	}
}
