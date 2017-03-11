using UnityEngine;

public class Elevator : MonoBehaviour
{
	public Vector3 targetPosition;

	public float speed = 2;

	Vector3 startPosition;
	bool inTrigger;
	float time;

	void Start ()
	{
		startPosition = transform.position;
	}

	void OnTriggerEnter (Collider _collider)
	{
		if (_collider.tag == "Player")
		{
			inTrigger = true;
		}
	}

	void OnTriggerExit (Collider _collider)
	{
		if (_collider.tag == "Player")
		{
			inTrigger = false;
		}
	}

	void FixedUpdate ()
	{
		if (inTrigger && time < 1)
		{
			time += 0.01f * speed;
		}

		if (!inTrigger && time > 0)
		{
			time -= 0.01f * speed;
		}

		transform.position = Vector3.Lerp(startPosition, targetPosition, time);
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(targetPosition + Vector3.up / 2, transform.localScale);

		if (Application.isPlaying) {
			Gizmos.DrawLine(startPosition + Vector3.up / 2, targetPosition + Vector3.up / 2);
			Gizmos.DrawWireCube(startPosition + Vector3.up / 2, transform.localScale);
		} else {
			Gizmos.DrawLine(transform.position, targetPosition);
		}
		
	}
}
