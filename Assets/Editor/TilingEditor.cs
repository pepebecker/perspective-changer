using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tiling))]
public class TilingEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		Tiling tiling = (Tiling)target;
		
		GUILayout.BeginHorizontal();
		
		if(GUILayout.Button("Tile"))
		{
			tiling.Tile();
		}
		
		if(GUILayout.Button("Reset")) {
			tiling.Reset();
		}
		
		GUILayout.EndHorizontal();
	}
}