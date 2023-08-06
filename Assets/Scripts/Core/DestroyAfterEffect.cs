using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        [SerializeField] GameObject effect = null;

        void Update()
        {
            if (!effect.GetComponent<ParticleSystem>().IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}

