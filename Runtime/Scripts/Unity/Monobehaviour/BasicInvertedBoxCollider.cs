#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basics
{
    [ExecuteInEditMode]
    public class BasicInvertedBoxCollider : MonoBehaviour
    {
        public Vector3 size = Vector3.one;
        public Vector3 center = Vector3.zero;
        public float width = 0;
        [HideInInspector] public BoxCollider[] boxColliders = new BoxCollider[6];

        private void Reset()
        {
            for (int i = 0; i < 6; i++)
            {
                boxColliders[i] = new GameObject("Collider " + (i + 1)).AddComponent<BoxCollider>();
                boxColliders[i].transform.parent = transform;
            }
        }

        private void Update()
        {
            boxColliders[0].center = center + Vector3.right * size.x / 2;
            boxColliders[1].center = center + Vector3.left * size.x / 2;
            boxColliders[2].center = center + Vector3.up * size.y / 2;
            boxColliders[3].center = center + Vector3.down * size.y / 2;
            boxColliders[4].center = center + Vector3.forward * size.z / 2;
            boxColliders[5].center = center + Vector3.back * size.z / 2;

            boxColliders[0].size = Vector3.up * size.y + Vector3.forward * size.z + Vector3.right * width;
            boxColliders[1].size = Vector3.up * size.y + Vector3.forward * size.z + Vector3.right * width;
            boxColliders[2].size = Vector3.right * size.x + Vector3.forward * size.z + Vector3.up * width;
            boxColliders[3].size = Vector3.right * size.x + Vector3.forward * size.z + Vector3.up * width;
            boxColliders[4].size = Vector3.up * size.y + Vector3.right * size.x + Vector3.forward * width;
            boxColliders[5].size = Vector3.up * size.y + Vector3.right * size.x + Vector3.forward * width;
        }
    }
}
#endif