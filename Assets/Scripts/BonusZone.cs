using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusZone : MonoBehaviour
{
    public int BonusMovement;

    public int spawnRate;
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ZoneManager.instance.MoveZoneBonus(BonusMovement);
            RemoveBonus();
        }
        if (Input.GetMouseButtonDown(1))
        {
            ZoneManager.instance.MoveZoneBonus(-BonusMovement);
            RemoveBonus();
        }
    }

    private void OnMouseEnter()
    {
        SpellManager.instance.AddNbOver(1);
    }
    private void OnMouseExit()
    {
        SpellManager.instance.AddNbOver(-1);
    }

    public void RemoveBonus()
    {
        Destroy(gameObject);
    }

}
