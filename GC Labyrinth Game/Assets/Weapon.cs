﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Weapons")]
public class Weapon : ScriptableObject
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackDelay = 10f;
    [SerializeField] private new string name = "Default Weapon";

    [SerializeField] private int attribueSlots;
    [SerializeField] GameObject[] attributes;
    [SerializeField] private GameObject attackAnimation = null;

    [SerializeField] private float hitboxX = 1f;
    [SerializeField] private float hitboxY = 1f;

    [System.Serializable]
    public enum weaponType
    {
        sword,
        gun
    };
    public weaponType type;

    private SpriteRenderer sRenderer;

    public float Damage { get { return damage; } private set { damage = value; } }
    public float AttackDelay { get { return attackDelay; } private set { attackDelay = value; } }
    public string Name { get { return name; } private set { name = value; } }
    public GameObject AttackAnimation { get { return attackAnimation; } private set { } }

    public float HitboxX { get { return hitboxX; } private set { } }
    public float HitboxY { get { return hitboxY; } private set { } }
}
