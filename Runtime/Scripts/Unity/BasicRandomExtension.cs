using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basics
{
    public static class BasicRandomExtension
    {
        public static float Gaussian(float mean, float stdDev)
        {
            float u1 = 1 - Random.value;
            float u2 = 1 - Random.value;
            float randStdNormal = Mathf.Sqrt(-2 * Mathf.Log(u1)) *
                         Mathf.Sin(2 * Mathf.PI * u2);
            return mean + stdDev * randStdNormal;
        }

        public static Color RandomColor(float min, float max)
        {
            return new Color(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
        }

        public static Color RandomColor(float brightness)
        {
            brightness *= 3;
            if (brightness > 3 || brightness < 0)
            {
                Debug.Log("Brightness is over 1 or under 0");
                return new Color();
            }

            float r = Random.Range(0f, brightness > 1 ? 1f : brightness);
            brightness -= r;
            float b = Random.Range(0f, brightness > 1 ? 1f : brightness);
            brightness -= b;
            float g = brightness > 1 ? 1 : brightness;

            return new Color(r, b, g);
        }

        public static int PickOne(float[] prob)
        {
            int index = 0;
            float r = UnityEngine.Random.value;

            while (r > 0)
            {
                r -= prob[index];
                index++;
            }
            index--;

            return index;
        }

        public static Vector3 InsideBounds(Bounds b)
        {
            return b.center + new Vector3(
                Random.Range(b.min.x, b.max.x),
                Random.Range(b.min.y, b.max.y),
                Random.Range(b.min.z, b.max.z)
                );
        }
    }
}