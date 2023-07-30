using UnityEngine;



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


        public void Spawn(Transform rightHandTransform, Transform leftHandTransform, Animator animator)
        {
            if(equippedPrefabObject != null && isRightHanded)
            {
                Instantiate(equippedPrefabObject, rightHandTransform);


            }else if(equippedPrefabObject !=null && !isRightHanded)
            {
                Instantiate(equippedPrefabObject, leftHandTransform);
            }
            

            if(overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController;
            }
            
        }

        public float GetDamageToDo()
        {
            return damageToDo;
        }
        public float GetWeaponRange()
        {
            return weaponRange;
        }
    }
}

