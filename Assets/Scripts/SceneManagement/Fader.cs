using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;





namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvas;

        private void Awake()
        {
            canvas = GetComponent<CanvasGroup>();            
        }

        public void FadeOutImmediate()
        {
            canvas.alpha = 1;
        }

        public IEnumerator FadeOut(float time)
        {
            while (canvas.alpha < 1)
            {
                canvas.alpha += Time.deltaTime / time;

                yield return null;
            }
            
        }

        public IEnumerator FadeIn(float time)
        {
            while (canvas.alpha > 0)
            {
                canvas.alpha -= Time.deltaTime / time;

                yield return null;
            }

        }
    }

}