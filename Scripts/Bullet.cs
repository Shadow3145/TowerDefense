using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;

	public float speed;
	public int damage;
	public float explosionRange = 0f;

	public GameObject bulletImpactEffect;

	public void ChaseTarget (Transform _target)
	{
		target = _target;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (target == null) 
		{
			Destroy (gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float frameDistance = speed * Time.deltaTime;

		if (dir.magnitude <= frameDistance) 
		{
			TargetHit ();
			return;
		}

		transform.Translate (dir.normalized * frameDistance, Space.World);
		transform.LookAt (target);
	}

	void TargetHit()
	{
		GameObject effectInstance = (GameObject)Instantiate (bulletImpactEffect, transform.position, transform.rotation);
		Destroy (effectInstance, 1.5f);
		Destroy (gameObject); 
		if (explosionRange > 0)
		{
			Explode ();
		}
		else
			Damage (target);

	}

	void Explode()
	{
		Collider[] colliders = Physics.OverlapSphere (transform.position, explosionRange);
		foreach (Collider collider in colliders) 
		{
			if (collider.tag == "Enemy")
				Damage (collider.transform);
		}
	}

	void Damage (Transform enemy)
	{
		Enemy e = enemy.GetComponent<Enemy> ();
		if (e != null)
		{
			e.TakeDamage (damage);
		}
	}
}
