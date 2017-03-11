using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject _colliderObject;
	public GameObject _bodyObject;
	public float moveSpeed = 5;
	public float airSpeedMultiplier = 0.4f;
	public float jumpForce = 5;

	public bool isGrounded;
	public bool onWall;

	public float wallPosX;


	void Update ()
	{
		_bodyObject.transform.rotation = Quaternion.identity;

		if (!isGrounded)
		{
			RaycastHit hit;
			float groundDistance = 0.5f;

			if (Physics.Raycast(transform.position, Vector3.down, out hit, groundDistance))
			{
				if (hit.collider.tag == "Ground")
				{
					isGrounded = true;
				}
			}

			float wallDistance = 0.7f;

			if (Physics.Raycast(transform.position, Vector3.left, out hit, wallDistance) && hit.collider.tag == "Ground")
			{
				wallPosX = -1;
				onWall = true;
			} else if (Physics.Raycast(transform.position, Vector3.right, out hit, wallDistance) && hit.collider.tag == "Ground")
			{
				wallPosX = 1;
				onWall = true;
			}
			else {
				onWall = false;
			}
		} else {
			onWall = false;
		}
	}

	void FixedUpdate ()
	{
		Rigidbody _rigidbody = GetComponent<Rigidbody>();

		if (isGrounded)
		{
			Vector2 movement = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, Input.GetAxis("Vertical") * moveSpeed);
			_rigidbody.velocity = new Vector3(movement.x, _rigidbody.velocity.y, movement.y);
		}
		else
		{
			float _moveSpeed = moveSpeed * airSpeedMultiplier;
			Vector2 movement = new Vector2(Input.GetAxis("Horizontal") * _moveSpeed, Input.GetAxis("Vertical") * _moveSpeed);
			_rigidbody.velocity = new Vector3(movement.x, _rigidbody.velocity.y, movement.y);
		}

		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			_rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
			isGrounded = false;
		}

		if (Input.GetKeyDown(KeyCode.Space) && onWall)
		{
			_rigidbody.AddForce(new Vector3(jumpForce * -wallPosX * 0.6f, jumpForce * 1.3f, 0), ForceMode.Impulse);
			onWall = false;
		}
	}
}
