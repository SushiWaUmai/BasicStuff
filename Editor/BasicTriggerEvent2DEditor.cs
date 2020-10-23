using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Basics
{
    [CustomEditor(typeof(BasicTriggerEvent2D))]
    public class BasicTriggerEvent2DEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Getting Object
            BasicTriggerEvent2D bte = (BasicTriggerEvent2D)target;

            EditorGUILayout.PropertyField(serializedObject.FindProperty("triggerOnce"));
            // Ontrigger Property
            EditorGUILayout.PropertyField(serializedObject.FindProperty("TriggerEnter"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("TriggerStay"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("TriggerExit"));

            // Check for tag property
            bte.checkForTag = GUILayout.Toggle(bte.checkForTag, "Check for tags");
            if (bte.checkForTag)
                EditorGUILayout.PropertyField(serializedObject.FindProperty("checkTag"));

            // Save Changes
            if (GUI.changed)
                serializedObject.ApplyModifiedProperties();
        }
    }
}