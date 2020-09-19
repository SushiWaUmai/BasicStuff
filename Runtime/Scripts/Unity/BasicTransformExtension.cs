using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basics
{
    public static class BasicTransformExtension
    {
        public static void Reset(this Transform t)
        {
            t.position = Vector3.zero;
            t.rotation = Quaternion.identity;
            t.localScale = Vector3.one;
        }
    }
}