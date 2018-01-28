using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Rail))]
public class RailEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Rail rail = (Rail)target;

        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.normal.textColor = Color.red;

        GUILayout.BeginVertical("box");
        GUILayout.Label("Options");

        rail.gizmo = EditorGUILayout.Toggle(new GUIContent("View Gizmo", ""), rail.gizmo);
        rail.nextRail = EditorGUILayout.ObjectField(rail.nextRail, typeof(Rail), true) as Rail;

        if (GUILayout.Button("Add node"))
        {
            GameObject go = new GameObject();
            go.transform.SetParent(rail.transform);
            rail.ResetRail();
            var cam = SceneView.lastActiveSceneView.camera;
            go.transform.position = cam.transform.position;
            go.transform.rotation = cam.transform.rotation;
        }

        if (GUILayout.Button("Reorder Nodes"))
        {
            rail.ResetRail();
        }

        if (GUILayout.Button("Reset rail", style))
        {
            foreach (var node in rail.nodes)
            {
                if (node.GetComponent<Rail>() == null) DestroyImmediate(node.gameObject);
            }
            rail.ResetRail();
        }
    }
}
