using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DestroyTimer))]
public class DestroyTimerDropdown : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DestroyTimer script = (DestroyTimer)target;

        GUIContent arrayLable = new GUIContent("StartTrigger");
        script.startTriggerIndex = EditorGUILayout.Popup(arrayLable, script.startTriggerIndex, script.startTrigger);
    }
}
