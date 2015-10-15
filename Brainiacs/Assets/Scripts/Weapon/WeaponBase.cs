using UnityEngine;
using System.Collections;

public class WeaponBase {

    public string ownerName{get;set;}
    public int ammo { get; set; }
    public int charger { get; set; }
    public int damage { get; set; }
    public WeaponEnum weaponType { get; set; }
}
