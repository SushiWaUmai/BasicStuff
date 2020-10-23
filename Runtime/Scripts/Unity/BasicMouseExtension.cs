using UnityEngine;
using UnityEngine.EventSystems;

namespace Basics
{
    public static class BasicMouseExtension
    {
        public static GameObject GetSpriteUnderMouse(Camera cam)
        {
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                return hit.collider.gameObject;
            }

            return null;
        }

        public static bool IsMouseOverSprite(Camera cam) => GetSpriteUnderMouse(cam) != null;
        public static bool IsMouseOverUI => EventSystem.current.IsPointerOverGameObject();
    }
}