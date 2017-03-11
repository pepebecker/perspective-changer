using UnityEngine;

public class ColliderScript : MonoBehaviour
{
	public GameObject colliderObject;

	float defaultPositionZ;
	bool orthographic;

	void Start () {
		ConvertToOrthographic();
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab)) {
			Toggle();
		}
	}

	void Toggle () {
		if (CameraScript.Instance.getCamera().orthographic && orthographic) {
			ConvertToPerspective();
		} else if(!CameraScript.Instance.getCamera().orthographic && !orthographic) {
			ConvertToOrthographic();
		}
	}
	
	void ConvertToPerspective () {
		colliderObject.transform.localScale = Vector3.one;
		
		Vector3 position = colliderObject.transform.position;
		colliderObject.transform.position = new Vector3(position.x, position.y, defaultPositionZ);

		orthographic = false;
	}
	
	void ConvertToOrthographic () {
		defaultPositionZ = colliderObject.transform.position.z;

		Vector3 scale = transform.localScale;
		colliderObject.transform.localScale = new Vector3(1, 1, 3 / scale.z);

		Vector3 position = colliderObject.transform.position;
		colliderObject.transform.position = new Vector3(position.x, position.y, 0);

		orthographic = true;
	}
}
