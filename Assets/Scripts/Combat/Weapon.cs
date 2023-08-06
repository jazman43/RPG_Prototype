using UnityEngine;
using RPG.core;
using System;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName ="Weapon", menuName = "RPG_UnNamed_Project/make new Weapon", order =0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private AnimatorOverrideController overrideController = null;
        [SerializeField] private GameObject equippedPrefabObject = null;
        [SerializeField] private float damageToDo = 2f;
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private bool isRightHanded = true;
        [SerializeField] private Projectile projectile =null;


        const string weaponName = "Weapon";

        public void Spawn(Transform rightHandTransform, Transform leftHandTransform, Animator animator)
        {
            DestroyPreviosWeapon(rightHandTransform, leftHandTransform);

            if(equippedPrefabObject != null)
            {
                GameObject weapon = Instantiate(equippedPrefabObject, HandTransform(rightHandTransform,leftHandTransform));
                weapon.name = weaponName;
            }            

            if(overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController;
            }
            
        }

        private void DestroyPreviosWeapon(Transform rightHandTransform, Transform leftHandTransform)
        {
            Transform previosWeapon = rightHandTransform.Find(weaponName);
            if(previosWeapon == null)
            {
                previosWeapon = leftHandTransform.Find(weaponName);

            }
            if (previosWeapon == null) return;


            previosWeapon.name = "DELETING";
            Destroy(previosWeapon.gameObject);

        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void SpawnProjectile(Transform rightHandTransform, Transform leftHandTransform, Health target)
        {
            Projectile projectileCreate = Instantiate(projectile, HandTransform(rightHandTransform, leftHandTransform).position , Quaternion.identity);
            projectile.SetTarget(target, damageToDo);
        }

        public float GetDamageToDo()
        {
            return damageToDo;
        }
        public float GetWeaponRange()
        {
            return weaponRange;
        }

        private Transform HandTransform(Transform rightHandTransform, Transform leftHandTransform)
        {
            Transform handTran;

            if (isRightHanded)
            {
                handTran = rightHandTransform;
            }
            else
            {
                handTran = leftHandTransform;
            }

            return handTran;
        }
    }
}

