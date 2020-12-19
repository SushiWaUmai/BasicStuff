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
        /// <summary>
        /// Draws a Ellipsoid in Editor Gizmos Window
        /// </summary>
        /// <param name="drawPos"></param>
        /// <param name="drawRot"></param>
        /// <param name="size"></param>
        /// <param name="strokeSize"></param>
        /// <param name="col"></param>
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

        /// <summary>
        /// Draws a Wire Cube
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="size"></param>
        /// <param name="rot"></param>
        public static void DrawWireCube(Vector3 pos, Vector3 size, Quaternion rot)
        {
#if UNITY_EDITOR
            Matrix4x4 orig = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(pos, rot, Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, size);
            Gizmos.matrix = orig;
#endif
        }
    }
}