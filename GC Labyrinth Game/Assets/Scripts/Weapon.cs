using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Variables

    [SerializeField] private float damage = 10f;
    [SerializeField] private new string name = "Default Weapon";

    //[SerializeField] private int attribueSlots;
    //[SerializeField] GameObject[] attributes;

    [SerializeField] Animator anim = null;
    [SerializeField] private AnimationClip attackAnim = null;

    public BoxCollider2D hitBox;

    [System.Serializable]
    public enum weaponType
    {
        melee,
        ranged
    };
    public weaponType type;

    public float Damage { get { return damage; } private set { damage = value; } }
    public string Name { get { return name; } private set { name = value; } }
    //public GameObject AttackAnimation { get { return attackAnimation; } private set { } }

    [SerializeField] GameObject projectile;

    #endregion

    public void WeaponMeleeAttack()
    {
        anim.Play(attackAnim.name);
    }

    public void WeaponRangedAttack()
    {
        Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
    }
}
