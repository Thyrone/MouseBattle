using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusZone : MonoBehaviour
{
    public int BonusMovement;

    public int spawnRate;


    private void Start()
    {
        DelayRemove();
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ZoneManager.instance.MoveZoneBonus(BonusMovement);
            Destroy(gameObject);
        }
        if (Input.GetMouseButtonDown(1))
        {
            ZoneManager.instance.MoveZoneBonus(BonusMovement);
            Destroy(gameObject);

        }
    }

    IEnumerator DelayRemove()
    {
        yield return new WaitForSeconds(4f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Cursor")
        {
            SpellManager.instance.AddNbOver(1);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Cursor")
        {
            SpellManager.instance.AddNbOver(-1);
        }
    }

}
