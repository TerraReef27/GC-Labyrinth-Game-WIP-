using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private new string name = "Default Weapon";
    [SerializeField] private GameObject attack = null;

    private SpriteRenderer sRenderer;

    public float Damage { get { return damage; } set { damage = value; } }
    public string Name { get { return name; } set { name = value; } }
    public GameObject Attack { get { return attack; } set { } }
}
