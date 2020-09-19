using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Basics
{
    [CustomEditor(typeof(BasicTriggerEvent))]
    public class BasicTriggerEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Getting Object
            BasicTriggerEvent bte = (BasicTriggerEvent)target;

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