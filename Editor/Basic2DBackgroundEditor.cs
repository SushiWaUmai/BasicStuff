using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Basics
{
    [CustomEditor(typeof(Basic2DBackground))]
    public class Basic2DBackgroundEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Basic2DBackground backgroundParent = (Basic2DBackground)target;

            if(GUILayout.Button("Generate Background"))
            {
                Vector2 size = Vector3.Scale(backgroundParent.appearance.bounds.size, backgroundParent.transform.localScale);

                Vector2 extends = new Vector2(size.x * backgroundParent.tiles.x / 2f, size.y * backgroundParent.tiles.y / 2f);

                GameObject go = new GameObject("Background Holder");
                go.transform.parent = backgroundParent.transform;
                go.transform.position = backgroundParent.transform.position;
                go.transform.rotation = backgroundParent.transform.rotation;

                for (int x = 0; x < backgroundParent.tiles.x; x++)
                {
                    for (int y = 0; y < backgroundParent.tiles.y; y++)
                    {
                        // Create Background Tile
                        Transform tr = new GameObject("Background (" + x + ", " + y + ")").transform;

                        // Set Parent
                        tr.parent = go.transform;

                        // Create Sprite Renderer
                        SpriteRenderer sp = tr.gameObject.AddComponent<SpriteRenderer>();
                        sp.sprite = backgroundParent.appearance;
                        sp.color = backgroundParent.color;
                        sp.sortingOrder = -60;
                        
                        // Set position of the Sprite
                        tr.localPosition = new Vector3((x + 0.5f) * size.x - extends.x, (y + 0.5f) * size.y - extends.y, 0);
                    }
                }
            }

            if(GUILayout.Button("Clear Background"))
            {
                int count = backgroundParent.transform.childCount;
                int pos = 0;
                while(pos < count)
                {
                    GameObject go = backgroundParent.transform.GetChild(pos).gameObject;
                    if (go.name == "Background Holder")
                        DestroyImmediate(go);
                    pos++;
                }
            }
        }
    }
}