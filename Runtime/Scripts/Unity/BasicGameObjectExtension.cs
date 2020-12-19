using System.Collections.Generic;
using UnityEngine;

namespace Basics
{
    public static class BasicGameObjectExtension
    {
        /// <summary>
        /// Finds Inactive GameObject by the name of the GameObject
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject FindInActiveObjectByName(string name)
        {
            Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>();
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].hideFlags == HideFlags.None)
                {
                    if (objs[i].name == name)
                    {
                        return objs[i].gameObject;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Finds Inactive GameObjects by the name of the GameObject
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<GameObject> FindInActiveObjectsByName(string name)
        {
            List<GameObject> result = new List<GameObject>();
            Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>();
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].hideFlags == HideFlags.None)
                {
                    if (objs[i].name == name)
                    {
                        result.Add(objs[i].gameObject);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Finds Inactive GameObject by the tag of the GameObject
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static GameObject FindInActiveObjectByTag(string tag)
        {
            Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>();
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].hideFlags == HideFlags.None)
                {
                    if (objs[i].CompareTag(tag))
                    {
                        return objs[i].gameObject;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Finds Inactive GameObjects by the tag of the GameObject
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static List<GameObject> FindInActiveObjectsByTag(string tag)
        {
            List<GameObject> result = new List<GameObject>();
            Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>();
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].hideFlags == HideFlags.None)
                {
                    if (objs[i].CompareTag(tag))
                    {
                        result.Add(objs[i].gameObject);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Finds inactive GameObject by the layer of the GameObject
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static GameObject FindInActiveObjectByLayer(int layer)
        {
            Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>();
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].hideFlags == HideFlags.None)
                {
                    if (objs[i].gameObject.layer == layer)
                    {
                        return objs[i].gameObject;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Finds inactive GameObjects by the layer of the GameObject
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static List<GameObject> FindInActiveObjectsByLayer(int layer)
        {
            List<GameObject> result = new List<GameObject>();
            Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>();
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].hideFlags == HideFlags.None)
                {
                    if (objs[i].gameObject.layer == layer)
                    {
                        result.Add(objs[i].gameObject);
                    }
                }
            }
            return result;
        }
    }
}