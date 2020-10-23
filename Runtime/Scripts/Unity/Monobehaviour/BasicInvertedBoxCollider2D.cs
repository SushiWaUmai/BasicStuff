#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basics
{
    [ExecuteInEditMode]
    public class BasicInvertedBoxCollider2D : MonoBehaviour
    {
        public Vector2 size = Vector2.one;
        public Vector2 center = Vector2.zero;
        public float width = 0.1f;
        [HideInInspector] public BoxCollider2D[] boxColliders = new BoxCollider2D[4];

        private void Reset()
        {
            for (int i = 0; i < 4; i++)
            {
                boxColliders[i] = new GameObject("Collider " + (i + 1)).AddComponent<BoxCollider2D>();
                boxColliders[i].transform.parent = transform;
            }
        }

        private void Update()
        {
            boxColliders[0].offset = center + Vector2.right * size.x / 2;
            boxColliders[1].offset = center + Vector2.left * size.x / 2;
            boxColliders[2].offset = center + Vector2.up * size.y / 2;
            boxColliders[3].offset = center + Vector2.down * size.y / 2;

            boxColliders[0].size = Vector2.up * size.y + Vector2.right * width;
            boxColliders[1].size = Vector2.up * size.y + Vector2.right * width;
            boxColliders[2].size = Vector2.right * size.x + Vector2.up * width;
            boxColliders[3].size = Vector2.right * size.x + Vector2.up * width;
        }
    }
}
#endif