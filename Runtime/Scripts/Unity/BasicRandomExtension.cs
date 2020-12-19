using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basics
{
    public static class BasicRandomExtension
    {
        /// <summary>
        /// Return a random Gaussian Value
        /// </summary>
        /// <param name="mean"></param>
        /// <param name="stdDev"></param>
        /// <returns></returns>
        public static float Gaussian(float mean, float stdDev)
        {
            float u1 = 1 - Random.value;
            float u2 = 1 - Random.value;
            float randStdNormal = Mathf.Sqrt(-2 * Mathf.Log(u1)) *
                         Mathf.Sin(2 * Mathf.PI * u2);
            return mean + stdDev * randStdNormal;
        }

        /// <summary>
        /// Returns a random color where rbg values are between a certain range
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Color RandomColor(float min, float max)
        {
            return new Color(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
        }

        /// <summary>
        /// Returns a random color where the sum of the rgb values equals brightness
        /// </summary>
        /// <param name="brightness"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns random index of a probability distribution array
        /// </summary>
        /// <param name="prob"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns random vector inside a bounding box
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
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