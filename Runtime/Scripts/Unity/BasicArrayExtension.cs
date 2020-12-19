using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Basics
{
    public static class BasicArrayExtension
    {
        /// <summary>
        /// Multiplies two arrays together
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static float[] Multiply(float[] left, float[] right)
        {
            if (left.Length == right.Length)
            {
                for (int i = 0; i < left.Length; i++)
                {
                    left[i] *= right[i];
                }
                return left;
            }
            else
            {
                throw new ArithmeticException("Arrays does not have the same length");
            }
        }

        /// <summary>
        /// Returns the scaled result
        /// </summary>
        /// <param name="result"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static float[] Scale(this float[] result, params float[] factor)
        {
            float f = 1;
            foreach (float fl in factor)
            {
                f *= fl;
            }
            for (int i = 0; i < result.Length; i++)
            {
                result[i] *= f;
            }
            return result;
        }

        /// <summary>
        /// Applies the method to all numbers in array
        /// </summary>
        /// <param name="input"></param>
        /// <param name="f"></param>
        public static Toutput[] Map<Tinput, Toutput>(this Tinput[] input, Func<Tinput, Toutput> f)
        {
            Toutput[] result = new Toutput[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                result[i] = f.Invoke(input[i]);
            }
            return result;
        }

        /// <summary>
        /// Applies the method to all numbers in array
        /// </summary>
        /// <param name="input"></param>
        /// <param name="f"></param>
        public static Toutput[] Map<Tinput, Toutput>(this Tinput[,] input, Func<Tinput, Toutput> f)
        {
            Toutput[] result = new Toutput[input.Length];
            for (int x = 0; x < input.GetLength(0); x++)
            {
                for (int y = 0; y < input.GetLength(1); y++)
                {
                    result[x * input.GetLength(0) + y] = f.Invoke(input[x, y]);
                }
            }
            return result;
        }


        /// <summary>
        /// Applies the method to all numbers in array
        /// </summary>
        /// <param name="input"></param>
        /// <param name="f"></param>
        public static void Map<T>(this T[] input, Action<T> f)
        {
            for (int i = 0; i < input.Length; i++)
            {
                f.Invoke(input[i]);
            }
        }

        /// <summary>
        /// Applies the method to all numbers in array
        /// </summary>
        /// <param name="input"></param>
        /// <param name="f"></param>
        public static Toutput[] Map<Tinput, Toutput>(this Tinput[] input, Func<Tinput, int, Toutput> f)
        {
            Toutput[] result = new Toutput[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                result[i] = f.Invoke(input[i], i);
            }
            return result;
        }

        /// <summary>
        /// Applies the method to all numbers in array
        /// </summary>
        /// <param name="input"></param>
        /// <param name="f"></param>
        public static void Map<T>(this T[] input, Action<T, int> f)
        {
            for (int i = 0; i < input.Length; i++)
            {
                f.Invoke(input[i], i);
            }
        }

        /// <summary>
        /// Check for following condition for each element in array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static List<T> Filter<T>(this T[] input, Func<T, bool> f)
        {
            List<T> result = new List<T>();
            for (int i = 0; i < input.Length; i++)
            {
                if (f.Invoke(input[i]))
                {
                    result.Add(input[i]);
                }
            }
            return result;
        }

        /// <summary>
        /// Returns a appropriate string from array
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ArrayToString(this object[] input)
        {
            string result = "{ ";

            for (int i = 0; i < input.Length; i++)
            {
                if (i != input.Length - 1) result += input[i].ToString() + ", ";
                else result += input[i];
            }

            result += " }";
            return result;
        }

        /// <summary>
        /// Returns a appropriate string from array
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ArrayToString(this double[] input)
        {
            string result = "{ ";

            for (int i = 0; i < input.Length; i++)
            {
                if (i != input.Length - 1) result += input[i] + ", ";
                else result += input[i];
            }

            result += " }";
            return result;
        }

        /// <summary>
        /// Returns a appropriate string from array
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ArrayToString(this float[] input)
        {
            string result = "{ ";

            for (int i = 0; i < input.Length; i++)
            {
                if (i != input.Length - 1) result += input[i] + ", ";
                else result += input[i];
            }

            result += " }";
            return result;
        }

        /// <summary>
        /// Returns a appropriate string from two dimensional array
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ArrayToString(this object[,] input)
        {
            string result = "{ ";

            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input.Length; x++)
                {
                    if (y != input.Length - 1) result += input[x, y].ToString() + ", ";
                    else result += input[x, y];
                }
                result += "\n";
            }

            result += " }";
            return result;
        }

        /// <summary>
        /// Sums all numbers in float array
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static float Sum(this float[] nums)
        {
            float result = 0;

            foreach (float f in nums)
                result += f;

            return result;
        }

        /// <summary>
        /// Sums all numbers in float array
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static double Sum(this double[] nums)
        {
            double result = 0;

            foreach (double d in nums)
                result += d;

            return result;
        }

        /// <summary>
        /// Sums all return value of System.Func
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obs"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static float Sum<T>(this T[] obs, Func<T, float> f)
        {
            float result = 0;

            foreach (T t in obs)
                result += f.Invoke(t);

            return result;
        }

        /// <summary>
        /// Sums all return value of System.Func
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obs"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static double Sum<T>(this T[] obs, Func<T, double> f)
        {
            double result = 0;

            foreach (T t in obs)
                result += f.Invoke(t);

            return result;
        }

        /// <summary>
        /// Converts a two dimentional array to a one dimentional array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T[] ToOneDimensionalArray<T>(this T[,] array)
        {
            T[] result = new T[array.Length];

            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    result[x * array.GetLength(0) + y] = array[x, y];
                }
            }

            return result;
        }

        /// <summary>
        /// creates a range array
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static int[] Range(int size)
        {
            return Range(0, size);
        }

        /// <summary>
        /// Creates a range array
        /// </summary>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <param name="steps"></param>
        /// <returns></returns>
        public static int[] Range(int start, int size, int steps = 1)
        {
            int[] result = new int[size];

            for (int i = start; i < size + start; i += steps)
                result[(i / steps) - start] = i;

            return result;
        }
    }
}