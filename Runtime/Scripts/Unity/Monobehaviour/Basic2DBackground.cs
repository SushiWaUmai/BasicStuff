using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basics
{
    public class Basic2DBackground : MonoBehaviour
    {
        public Sprite appearance;
        public Vector2Int tiles;
        public Color color;

        private Transform camTransform;
        private Vector2 size;

        private void Start()
        {
            camTransform = Camera.main.transform;
            size = Vector2.Scale(appearance.bounds.size, transform.localScale);
        }

        private void FixedUpdate()
        {
            if (camTransform.position.x > transform.position.x + size.x)
            {
                transform.position = (Vector2)transform.position + Vector2.right * size.x * (int)(Mathf.Abs(camTransform.position.x - transform.position.x) / size.x);
            }
            else if (camTransform.position.x < transform.position.x - size.x)
            {
                transform.position = (Vector2)transform.position + Vector2.left * size.x * (int)(Mathf.Abs(camTransform.position.x - transform.position.x) / size.x);
            }

            if (camTransform.position.y > transform.position.y + size.y)
            {
                transform.position = (Vector2)transform.position + Vector2.up * size.y * (int)(Mathf.Abs(camTransform.position.y - transform.position.y) / size.y);
            }
            else if (camTransform.position.y < transform.position.y - size.y)
            {
                transform.position = (Vector2)transform.position + Vector2.down * size.y * (int)(Mathf.Abs(camTransform.position.y - transform.position.y) / size.y);
            }
        }
    }
}