using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Basics
{
    public static class BasicGizmosExtension
    {
        public static void DrawEllipsoid(Vector3 drawPos, Quaternion drawRot, Vector3 size, float strokeSize, Color col)
        {
#if UNITY_EDITOR
            Matrix4x4 orig = Handles.matrix;
            Handles.matrix = Matrix4x4.TRS(drawPos, drawRot, Vector3.one);

            //Y-Z Ring
            Handles.DrawBezier(new Vector3(0, 0 + size.y, 0), new Vector3(0, 0, 0 + size.z), new Vector3(0, size.y, size.z / 2), new Vector3(0, size.y / 2, size.z), col, Texture2D.whiteTexture, strokeSize);
            Handles.DrawBezier(new Vector3(0, 0 + size.y, 0), new Vector3(0, 0, 0 - size.z), new Vector3(0, size.y, -size.z / 2), new Vector3(0, size.y / 2, -size.z), col, Texture2D.whiteTexture, strokeSize);
            Handles.DrawBezier(new Vector3(0, 0 - size.y, 0), new Vector3(0, 0, 0 + size.z), new Vector3(0, -size.y, size.z / 2), new Vector3(0, -size.y / 2, size.z), col, Texture2D.whiteTexture, strokeSize);
            Handles.DrawBezier(new Vector3(0, 0 - size.y, 0), new Vector3(0, 0, 0 - size.z), new Vector3(0, -size.y, -size.z / 2), new Vector3(0, -size.y / 2, -size.z), col, Texture2D.whiteTexture, strokeSize);
            //X-Y Ring
            Handles.DrawBezier(new Vector3(0 + size.x, 0, 0), new Vector3(0, 0 + size.y, 0), new Vector3(size.x, size.y / 2, 0), new Vector3(size.x / 2, size.y, 0), col, Texture2D.whiteTexture, strokeSize);
            Handles.DrawBezier(new Vector3(0 - size.x, 0, 0), new Vector3(0, 0 + size.y, 0), new Vector3(-size.x, size.y / 2, 0), new Vector3(-(size.x / 2), size.y, 0), col, Texture2D.whiteTexture, strokeSize);
            Handles.DrawBezier(new Vector3(0 + size.x, 0, 0), new Vector3(0, 0 - size.y, 0), new Vector3(size.x, -(size.y / 2), 0), new Vector3(size.x / 2, -size.y, 0), col, Texture2D.whiteTexture, strokeSize);
            Handles.DrawBezier(new Vector3(0 - size.x, 0, 0), new Vector3(0, 0 - size.y, 0), new Vector3(-size.x, -(size.y / 2), 0), new Vector3(-(size.x / 2), -size.y, 0), col, Texture2D.whiteTexture, strokeSize);
            //X-Z Ring
            Handles.DrawBezier(new Vector3(0 + size.x, 0, 0), new Vector3(0, 0, 0 + size.z), new Vector3(size.x, 0, size.z / 2), new Vector3(size.x / 2, 0, size.z), col, Texture2D.whiteTexture, strokeSize);
            Handles.DrawBezier(new Vector3(0 - size.x, 0, 0), new Vector3(0, 0, 0 + size.z), new Vector3(-size.x, 0, size.z / 2), new Vector3(-(size.x / 2), 0, size.z), col, Texture2D.whiteTexture, strokeSize);
            Handles.DrawBezier(new Vector3(0 + size.x, 0, 0), new Vector3(0, 0, 0 - size.z), new Vector3(size.x, 0, -(size.z / 2)), new Vector3(size.x / 2, 0, -size.z), col, Texture2D.whiteTexture, strokeSize);
            Handles.DrawBezier(new Vector3(0 - size.x, 0, 0), new Vector3(0, 0, 0 - size.z), new Vector3(-size.x, 0, -(size.z / 2)), new Vector3(-(size.x / 2), 0, -size.z), col, Texture2D.whiteTexture, strokeSize);

            Handles.matrix = orig;
#endif
        }

        public static void DrawWireCube(Vector3 pos, Vector3 size, Quaternion rot)
        {
#if UNITY_EDITOR
            Gizmos.matrix = Matrix4x4.TRS(pos, rot, Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, size);
            Gizmos.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);
#endif
        }

        //        public static void DrawRotationArc(Vector3 position, Quaternion from, Quaternion rotation, float radius)
        //        {
        //#if UNITY_EDITOR
        //            rotation.ToAngleAxis(out float angle, out Vector3 axis);
        //            Handles.DrawWireArc(position, axis, from * Vector3.forward, angle, radius);
        //#endif
        //        }
    }
}