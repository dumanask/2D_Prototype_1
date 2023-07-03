using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum abilityTypes { Attack, Defense, Support }

[CreateAssetMenu(menuName = "Abilities/Ability")]
public class Ability : ScriptableObject
{
    [Header("Ability Information")]
    public Sprite abilityIcon;
    public string abilityName;
    public string abilityDescription;
    public abilityTypes abilityTypes;
    public bool isMelee;
    public string animationsName;
    public Ability baseAbility;
    public Ability connectedAbility;
    public int abilityLevel;

    [Header("Ability Attack")]
    public int damageAmount;
    public float cooldown;

    [Header("Ability Casting")]
    public bool isCastAbility;
    public GameObject castAbility;
    public float abilityRange;
    public float abilityRadius;
    public float abilitySpeed;
    public Transform castPoint;
}
