using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Basics
{
    public class BasicUIAnimation : MonoBehaviour
    {
        private float[] startingAlpha;
        private Graphic[] graphics;

        private void Awake()
        {
            graphics = GetComponentsInChildren<Graphic>(true);
            startingAlpha = BasicArrayExtension.Map(graphics, x => x.color.a);
        }

        public void FadeoutUI(float time)
        {
            StartCoroutine(StartFadeout(time));
        }

        public void FadeinUI(float time)
        {
            gameObject.SetActive(true);

            StartCoroutine(StartFadein(time));
        }

        private IEnumerator StartFadeout(float time)
        {
            ResetAlpha();

            while(graphics[0].color.a > 0)
            {
                for(int i = 0; i < graphics.Length; i++)
                {
                    Color c = graphics[i].color;
                    c.a -= Time.deltaTime * startingAlpha[i] / time;
                    graphics[i].color = c; 
                }
                yield return null;
            }

            gameObject.SetActive(false);
        }

        private IEnumerator StartFadein(float time)
        {
            foreach (Graphic g in graphics)
            {
                Color c = g.color;
                c.a = 0;
                g.color = c;
            }

            while (graphics[0].color.a < startingAlpha[0])
            {
                for (int i = 0; i < graphics.Length; i++)
                {
                    Color c = graphics[i].color;
                    c.a += Time.deltaTime * startingAlpha[i] / time;
                    graphics[i].color = c;
                }
                yield return null;
            }
        }

        public void ResetAnimation()
        {
            StopAllCoroutines();
            ResetAlpha();
        }

        private void ResetAlpha()
        {
            for (int i = 0; i < graphics.Length; i++)
            {
                Color c = graphics[i].color;
                c.a = startingAlpha[i];
                graphics[i].color = c;
            }
        }
    }
}