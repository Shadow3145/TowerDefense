using UnityEngine;

public class CameraController : MonoBehaviour {

	public float panSpeed = 30f;
	public float buffer = 10f;

	public float scrollSpeed = 5f;
	public float minY = 10f;
	public float maxY = 85f;




	void Update () 
	{
		if (GameManager.isGameOver) 
		{
			this.enabled = false;
			return;
		}
					
		if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - buffer)
		{
			transform.Translate (Vector3.forward*panSpeed*Time.deltaTime, Space.World);
		}
		if (Input.GetKey("s") || Input.mousePosition.y <= buffer)
		{
			transform.Translate (Vector3.back*panSpeed*Time.deltaTime, Space.World);
		}
		if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - buffer)
		{
			transform.Translate (Vector3.right*panSpeed*Time.deltaTime, Space.World);
		}
		if (Input.GetKey("a") || Input.mousePosition.x <= buffer)
		{
			transform.Translate (Vector3.left*panSpeed*Time.deltaTime, Space.World);
		}

		float scrollWheel = Input.GetAxis ("Mouse ScrollWheel");

		Vector3 position = transform.position;

		position.y -= scrollWheel * scrollSpeed * Time.deltaTime * 850;
		position.y = Mathf.Clamp (position.y, minY, maxY);
		transform.position = position;


	}
}
