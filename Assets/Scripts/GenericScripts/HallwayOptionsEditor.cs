using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HallwayOptions))]
public class HallwayOptionsEditor : Editor
{
    private SerializedProperty _hallway1;
    private SerializedProperty _hallway2;
    private SerializedProperty _hallway3;
    private SerializedProperty _hallway4;

    private SerializedProperty _showWall1;
    private SerializedProperty _showWall2;
    private SerializedProperty _showWall3;
    private SerializedProperty _showWall4;

    public void OnEnable()
    {
        _hallway1 = serializedObject.FindProperty("_hallway1");
        _hallway2 = serializedObject.FindProperty("_hallway2");
        _hallway3 = serializedObject.FindProperty("_hallway3");
        _hallway4 = serializedObject.FindProperty("_hallway4");

        _showWall1 = serializedObject.FindProperty("_showWall1");
        _showWall2 = serializedObject.FindProperty("_showWall2");
        _showWall3 = serializedObject.FindProperty("_showWall3");
        _showWall4 = serializedObject.FindProperty("_showWall4");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        HallwayOptions options = (HallwayOptions)target;

        EditorGUILayout.PropertyField(_hallway1);
        EditorGUILayout.PropertyField(_hallway2);
        EditorGUILayout.PropertyField(_hallway3);
        EditorGUILayout.PropertyField(_hallway4);

        _showWall1.boolValue = EditorGUILayout.ToggleLeft("Wall1", _showWall1.boolValue);
        _showWall2.boolValue = EditorGUILayout.ToggleLeft("Wall2", _showWall2.boolValue);
        _showWall3.boolValue = EditorGUILayout.ToggleLeft("Wall3", _showWall3.boolValue);
        _showWall4.boolValue = EditorGUILayout.ToggleLeft("Wall4", _showWall4.boolValue);

        if(_hallway1.objectReferenceValue != null && _hallway2.objectReferenceValue != null && _hallway3.objectReferenceValue != null && _hallway4.objectReferenceValue != null)
         options.UpdateWalls();

        serializedObject.ApplyModifiedProperties();
    }
}
