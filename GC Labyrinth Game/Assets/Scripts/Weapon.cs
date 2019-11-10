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
    private bool isPlayerWeapon = false;

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

    [SerializeField] private GameObject projectile = null;
    [SerializeField] private float projectileSpeed = 50f;

    private GameObject projectileManager = null;
    #endregion

    void Start()
    {
        if (transform.parent.gameObject.tag == "Player")
            isPlayerWeapon = true;
    }
    void OnEnable()
    {
        projectileManager = GameObject.FindWithTag("ProjectileStorage");
    }

    public void WeaponMeleeAttack()
    {
        anim.Play(attackAnim.name);
    }

    public void WeaponRangedAttack()
    {
        GameObject newProjectile = Instantiate(projectile, projectileManager.transform);
        newProjectile.transform.position = gameObject.transform.position;
        newProjectile.transform.rotation = gameObject.transform.rotation;
        newProjectile.GetComponent<Projectile>().ProjectileDamage = damage;

        newProjectile.GetComponent<Projectile>().SetIsPlayer(isPlayerWeapon);
        newProjectile.GetComponent<Projectile>().Shoot(gameObject.transform.up, projectileSpeed);
    }
}
