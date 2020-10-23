using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Basics
{
    [CustomEditor(typeof(BasicInvertedBoxCollider2D))]
    public class BasicInvertedBoxCollider2DEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            BasicInvertedBoxCollider2D ibc2D = (BasicInvertedBoxCollider2D)target;

            if (GUILayout.Button("Generate Colliders"))
            {
                for (int i = 0; i < 4; i++)
                {
                    ibc2D.boxColliders[i] = new GameObject("Collider " + (i + 1)).AddComponent<BoxCollider2D>();
                    ibc2D.boxColliders[i].transform.parent = ibc2D.transform;
                }

                Undo.RecordObjects(BasicArrayExtension.Map(ibc2D.GetComponentsInChildren<Transform>(), x => x.gameObject), "Generate Colliders");
            }
        }
    }
}
