using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RPG.core
{
    public class PeristentObjectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject persistentObjectPrefab;
        
        [SerializeField] private bool staticHasSpawend;

        static bool hasSpawned = false;

        private void Awake()
        {
            if (hasSpawned) return;

            SpawnPersistentObjects();

            hasSpawned = true;
        }
        private void Update()
        {
            staticHasSpawend = hasSpawned;
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            
            DontDestroyOnLoad(persistentObject);
        }
    }
}

