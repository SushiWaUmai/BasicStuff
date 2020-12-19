using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basics
{
    public static class BasicMathExtension
    {
        public static float Max(params float[] nums)
        {
            float result = -float.MaxValue;

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] > result)
                    result = nums[i];
            }

            return result;
        }

        public static float Min(params float[] nums)
        {
            float result = float.MaxValue;

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] < result)
                    result = nums[i];
            }

            return result;
        }

        public static bool Approximately(this Quaternion val, Quaternion about, float range)
        {
            return Quaternion.Dot(val, about) > 1f - range;
        }

        public static Vector3 Abs(this Vector3 val)
        {
            return new Vector3(Mathf.Abs(val.x), Mathf.Abs(val.y), Mathf.Abs(val.z));
        }

        public static Vector2 Abs(this Vector2 val)
        {
            return new Vector2(Mathf.Abs(val.x), Mathf.Abs(val.y));
        }

        public static float GetAxis(this Vector3 val, Vector3 dir)
        {
            if (dir.x != 0)
                return val.x;
            else if (dir.y != 0)
                return val.y;
            else if (dir.z != 0)
                return val.z;

            return float.NaN;
        }

        public static float[] LinearProbabilityDistribution(this float[] val)
        {
            float[] result = new float[val.Length];

            float sum = val.Sum();

            for (int i = 0; i < result.Length; i++)
                result[i] = val[i] / sum;

            return result;
        }
    }
}