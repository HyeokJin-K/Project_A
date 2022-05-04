using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSword : MeleeWeapon, IWeapon
{

    public Vector3 colSize;
    BoxCollider2D weaponCol;


    private void Start()
    {
        weaponCol = gameObject.AddComponent<BoxCollider2D>();
        weaponCol.isTrigger = false;
        weaponCol.autoTiling = true;
        weaponCol.size = colSize;

        print(weaponCol);
    }

    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(gameObject.transform.position, colSize);
    }
}
