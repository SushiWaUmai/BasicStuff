using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basics
{
    public class BasicCameraAlignment : MonoBehaviour
    {
        public AlignmentType alignment;
        public bool alignOnAwake = true;
        public Vector3 size;
        public Camera cam;

        private void Start()
        {
            cam = Camera.main;

            if(alignOnAwake)
                AlignCamera();
        }

        public void AlignCamera()
        {
            if (cam.orthographic)
                cam.orthographicSize = GetAlignedOrthographicSize();
            else
                cam.fieldOfView = GetAlignedFOV();
        }

        public float GetAlignedOrthographicSize()
        {
            float result;
            if (alignment == AlignmentType.Horizontal)
            {
                result = size.x * Screen.height / Screen.width * 0.5f;
                size.y = result * 2;
            }
            else
            {
                result = size.y * 0.5f;
                size.x = result / Screen.height * Screen.width * 2;
            }

            return result;
        }

        public float GetAlignedFOV()
        {
            throw new System.NotImplementedException();
        }

        private void OnDrawGizmosSelected()
        {
            Color c = Color.yellow;
            c.a = 0.3f;
            Gizmos.color = c;

            Vector3 pos = Vector3.zero;
            if (cam != null)
                pos = cam.transform.position;

            Gizmos.DrawCube(pos, size);
        }

        public enum AlignmentType
        {
            Horizontal,
            Vertical
        }
    }
}