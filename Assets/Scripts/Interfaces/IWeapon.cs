using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    int WeaponLevel { get; set; }

    float WeaponExp { get; set; }

    List<GameObject> SkillList { get; set; }

    void SetWeaponInput();

    void LevelUpWeapon();

    void AddSkillList(GameObject skillObject);
}
