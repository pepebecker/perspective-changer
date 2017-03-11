using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ColliderScript))]
public class ColliderScriptEditor : Editor
{
	public override void OnInspectorGUI()
	{
		ColliderScript colScript = (ColliderScript)target;
		
		GUILayout.BeginHorizontal();
		
		if(GUILayout.Button("2D Mode"))
		{
			//colScript.blocks.Clear();
			//colScript.LoadColliders();
			//colScript.SetCollidersFor2DMode();
		}
		
		if(GUILayout.Button("3D Mode"))
		{
			//colScript.SetCollidersFor3DMode();
		}
		
		GUILayout.EndHorizontal();
	}
}