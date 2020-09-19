using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basics
{
    public static class BasicColorExtension
    {
        public static float GetMinColorComponent(this Color c)
        {
            return BasicMathExtension.Min(c.r, c.g, c.b);
        }

        public static Color GetComplementary(this Color c)
        {
            Color result = new Color();

            float sum = c.maxColorComponent + c.GetMinColorComponent();

            result.r = sum - c.r;
            result.g = sum - c.g;
            result.b = sum - c.b;

            return result;
        }
    }
}