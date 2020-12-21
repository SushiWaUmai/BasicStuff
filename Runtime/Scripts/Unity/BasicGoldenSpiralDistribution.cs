using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basics
{
    public static class BasicGoldenSpiralDistribution
    {
        public const float goldenRatio = 1.6180339887f;

        public static Vector2[] SunflowerUnitDisk(int numPoints)
        {
            if (numPoints < 0)
                return null;

            Vector2[] result = new Vector2[numPoints];

            for (int i = 0; i < numPoints; i++)
            {
                float index = i + 0.5f;
                float r = Mathf.Sqrt(index / numPoints);
                float theta = Mathf.PI * goldenRatio * 2 * index;

                result[i] = new Vector2(r * Mathf.Sin(theta), r * Mathf.Cos(theta));
            }

            return result;
        }

        public static Vector3[] SunflowerUnitSphere(int numPoints)
        {
            if (numPoints < 0)
                return null;

            Vector3[] result = new Vector3[numPoints];

            for (int i = 0; i < numPoints; i++)
            {
                float index = i + 0.5f;
                float phi = Mathf.Acos(1 - 2 * index / numPoints);
                float theta = Mathf.PI * goldenRatio * 2 * index;

                result[i] = new Vector3(Mathf.Cos(theta) * Mathf.Sin(phi), Mathf.Sin(theta) * Mathf.Sin(phi), Mathf.Cos(phi));
            }

            return result;
        }
    }
}
