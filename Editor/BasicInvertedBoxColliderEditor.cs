using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Basics
{
    [CustomEditor(typeof(BasicInvertedBoxCollider))]
    public class BasicInvertedBoxColliderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            BasicInvertedBoxCollider ibc = (BasicInvertedBoxCollider)target;

            if (GUILayout.Button("Generate Colliders"))
            {
                for (int i = 0; i < 6; i++)
                {
                    ibc.boxColliders[i] = new GameObject("Collider " + (i + 1)).AddComponent<BoxCollider>();
                    ibc.boxColliders[i].transform.parent = ibc.transform;
                }

                Undo.RecordObjects(BasicArrayExtension.Map(ibc.GetComponentsInChildren<Transform>(), x => x.gameObject), "Generate Colliders");
            }
        }
    }
}
