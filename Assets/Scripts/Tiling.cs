using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Tiling : MonoBehaviour
{
	public GameObject quads;

	Quaternion FRONT	= Quaternion.Euler(  0,   0, 0);
	Quaternion BACK		= Quaternion.Euler(  0, 180, 0);
	Quaternion LEFT		= Quaternion.Euler(  0,  90, 0);
	Quaternion RIGHT	= Quaternion.Euler(  0, -90, 0);
	Quaternion TOP		= Quaternion.Euler( 90,   0, 0);
	Quaternion BOTTOM	= Quaternion.Euler(-90,   0, 0);

	List<Renderer> renderers = new List<Renderer>();
	List<Material> defaultMaterials = new List<Material>();
	bool loaded;

	void Load ()
	{
		defaultMaterials.Clear();
		renderers = quads.GetComponentsInChildren<Renderer>().ToList();
		foreach (Renderer rdr in renderers)
		{
			defaultMaterials.Add(rdr.sharedMaterial);
		}
		loaded = true;
	}

	public void Tile ()
	{
		if (!loaded) Load();

		foreach (Renderer rdr in renderers)
		{
			var mat = new Material(rdr.sharedMaterial);
			var rot = rdr.transform.localRotation;
			if (rot == FRONT || rot == BACK)
			{
				mat.SetTextureScale("_MainTex", new Vector2(transform.localScale.x, transform.localScale.y));
				rdr.material = mat;
			}
			if (rot == LEFT || rot == RIGHT)
			{
				mat.SetTextureScale("_MainTex", new Vector2(transform.localScale.z, transform.localScale.y));
				rdr.material = mat;
			}
			if (rot == TOP || rot == BOTTOM)
			{
				mat.SetTextureScale("_MainTex", new Vector2(transform.localScale.x, transform.localScale.z));
				rdr.material = mat;
			}
		}
	}

	public void Reset()
	{
		for (var i = 0; i < renderers.Count; i++)
		{
			renderers[i].material = defaultMaterials[i];
		}
	}
}
