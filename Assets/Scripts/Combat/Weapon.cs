using UnityEngine;
using RPG.Attributes;
using RPG.Inventories;
using RPG.Progression;
using System.Collections.Generic;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName ="Weapon", menuName = "Jareds/make new Weapon", order =0)]
    public class Weapon : EquipableItem, IModProvider
    {
        [SerializeField] private AnimatorOverrideController overrideController = null;
        [SerializeField] private OnWeaponEquipment equippedPrefabObject = null;
        [SerializeField] private float damageToDo = 2f;
        [SerializeField] private float percentageBonus = 2f;
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private bool isRightHanded = true;
        [SerializeField] private Projectile projectile =null;


        const string weaponName = "Weapon";

        public OnWeaponEquipment Spawn(Transform rightHandTransform, Transform leftHandTransform, Animator animator)
        {
            DestroyPreviosWeapon(rightHandTransform, leftHandTransform);

            OnWeaponEquipment weapon = null;

            if(equippedPrefabObject != null)
            {
                weapon = Instantiate(equippedPrefabObject, HandTransform(rightHandTransform,leftHandTransform));
                weapon.gameObject.name = weaponName;
            }

            var overRideAnimation = animator.runtimeAnimatorController as AnimatorOverrideController;

            if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController;
            }
            else if (overRideAnimation != null)
            {                
                animator.runtimeAnimatorController = overRideAnimation.runtimeAnimatorController;               
            }
            return weapon;
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

        public void SpawnProjectile(Transform rightHandTransform, Transform leftHandTransform, Health target, GameObject instigator, float calcuatedDamage)
        {
            Projectile projectileCreate = Instantiate(projectile, HandTransform(rightHandTransform, leftHandTransform).position , Quaternion.identity);
            projectileCreate.SetTarget(target,instigator , calcuatedDamage);
            
        }

        public float GetDamageToDo()
        {
            return damageToDo;
        }

        public float GetPercentageBonus()
        {
            return percentageBonus;
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

        public IEnumerable<float> GetAdditiveMod(Stats stats)
        {
            if (stats == Stats.Damage)
            {
                yield return damageToDo;
            }
        }

        public IEnumerable<float> GetPercentageMod(Stats stats)
        {
            if (stats == Stats.Damage)
            {
                yield return percentageBonus;
            }
        }
    }
}

