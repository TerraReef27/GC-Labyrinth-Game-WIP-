using UnityEngine;
using System.Collections;

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
    public AnimationClip AttackAnimation { get { return attackAnim; } private set { attackAnim = value; } }

    [SerializeField] private GameObject projectile = null;
    [SerializeField] private float projectileSpeed = 50f;
    [SerializeField] private int ammoCount = 10;
    [SerializeField] private float reloadDelay = 1f;
    private int currentAmmo = 10;

    private GameObject projectileManager = null;

    private bool isCoolingDown = false;
    [Tooltip("Time till next attack can be used")]
    [SerializeField] private float cooldown = .2f;

    [SerializeField] private float hitstun = .2f;
    [SerializeField] private float knockback = 1f;
    public float Knockback { get { return knockback; } private set { knockback = value; } }
    public float Hitstun { get { return hitstun; } private set { hitstun = value; } }
    #endregion

    void Start()
    {
        currentAmmo = ammoCount;
        if (transform.parent.gameObject.tag == "Player")
            isPlayerWeapon = true;
    }
    void OnEnable()
    {
        projectileManager = GameObject.FindWithTag("ProjectileStorage");
        isCoolingDown = false;
    }

    public void WeaponMeleeAttack()
    {
        if (!isCoolingDown)
        {
            StartCoroutine(Cooldown());
            anim.Play(attackAnim.name);
        }
    }

    public void WeaponRangedAttack()
    {
        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
        else if(!isCoolingDown)
        {
            StartCoroutine(Cooldown());

            GameObject newProjectile = Instantiate(projectile, projectileManager.transform);
            newProjectile.transform.position = gameObject.transform.position;
            newProjectile.transform.rotation = gameObject.transform.rotation;
            newProjectile.GetComponent<Projectile>().ProjectileDamage = damage;
            newProjectile.GetComponent<Projectile>().ProjectileKnockback = knockback;

            newProjectile.GetComponent<Projectile>().SetIsPlayer(isPlayerWeapon);
            newProjectile.GetComponent<Projectile>().Shoot(gameObject.transform.up, projectileSpeed);

            currentAmmo--;
        }
    }

    private IEnumerator Cooldown()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(cooldown);
        isCoolingDown = false;
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadDelay);
        currentAmmo = ammoCount;
    }
}
