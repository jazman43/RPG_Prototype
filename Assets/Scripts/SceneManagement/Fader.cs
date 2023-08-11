using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;





namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvas;
        Coroutine currentActiveFade = null;

        private void Awake()
        {
            canvas = GetComponent<CanvasGroup>();            
        }

        public void FadeOutImmediate()
        {            
            canvas.alpha = 1;
        }

        public Coroutine FadeOut(float time)
        {
            return Fade(1, time);

        }

        public Coroutine FadeIn(float time)
        {
            return Fade(0, time);

        }

        public Coroutine Fade(float target, float time)
        {
            if (currentActiveFade != null)
            {
                StopCoroutine(currentActiveFade);
            }
            currentActiveFade = StartCoroutine(FadeRoutine(target, time));
            return currentActiveFade;
        }

        private IEnumerator FadeRoutine(float target, float time)
        {
            while (!Mathf.Approximately(canvas.alpha, target))
            {
                canvas.alpha = Mathf.MoveTowards(canvas.alpha, target, Time.unscaledDeltaTime / time);
                yield return null;
            }
        }

    }

}
