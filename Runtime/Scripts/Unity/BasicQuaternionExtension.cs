using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basics
{
    public static class BasicQuaternionExtension
    {
        public static Quaternion Average(this Quaternion[] quaternions)
        {
            if (quaternions == null || quaternions.Length < 1)
                return Quaternion.identity;

            if (quaternions.Length < 2)
                return quaternions[0];

            int count = quaternions.Length;
            float weight = 1.0f / (float)count;
            Quaternion avg = Quaternion.identity;

            for (int i = 0; i < count; i++)
                avg *= Quaternion.Slerp(Quaternion.identity, quaternions[i], weight);

            return avg;
        }
    }
}
