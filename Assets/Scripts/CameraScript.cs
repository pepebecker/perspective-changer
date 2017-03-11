using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public static CameraScript Instance;

	public float changeTime = 0.5f;

	bool changing;
	float time;

	void Main()
	{
		if (!Instance) {
			Instance = this;
		} else {
			Destroy(gameObject);
		}
	}

	public Camera getCamera()
	{
		return GetComponent<Camera>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab) && !changing)
		{
			changing = true;
			time = 0.0f;
		}
	}

	void LateUpdate()
	{
		if (!changing)
		{
			return;
		}

		var currentlyOrthographic = getCamera().orthographic;
		Matrix4x4 orthoMat, persMat;

		if (currentlyOrthographic)
		{
			orthoMat = getCamera().projectionMatrix;

			getCamera().orthographic = false;
			getCamera().ResetProjectionMatrix();
			persMat = getCamera().projectionMatrix;
		}
		else
		{
			persMat = getCamera().projectionMatrix;

			getCamera().orthographic = true;
			getCamera().ResetProjectionMatrix();
			orthoMat = getCamera().projectionMatrix;
		}

		getCamera().orthographic = currentlyOrthographic;

		time += (Time.deltaTime / changeTime);
		if (time < 1.0f)
		{
			if (currentlyOrthographic)
			{
				getCamera().projectionMatrix = MatrixLerp(orthoMat, persMat, time * time);
			}
			else
			{
				getCamera().projectionMatrix = MatrixLerp(persMat, orthoMat, Mathf.Sqrt(time));
			}
		}
		else
		{
			changing = false;
			getCamera().orthographic = !currentlyOrthographic;
			getCamera().ResetProjectionMatrix();
		}
	}

	Matrix4x4 MatrixLerp(Matrix4x4 from, Matrix4x4 to, float t)
	{
		t = Mathf.Clamp(t, 0.0f, 1.0f);
		var newMatrix = new Matrix4x4();
		newMatrix.SetRow(0, Vector4.Lerp(from.GetRow(0), to.GetRow(0), t));
		newMatrix.SetRow(1, Vector4.Lerp(from.GetRow(1), to.GetRow(1), t));
		newMatrix.SetRow(2, Vector4.Lerp(from.GetRow(2), to.GetRow(2), t));
		newMatrix.SetRow(3, Vector4.Lerp(from.GetRow(3), to.GetRow(3), t));
		return newMatrix;
	}
}
