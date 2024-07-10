using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BonusZone : MonoBehaviour
{
    public TMP_Text bonusText;
    public float BonusMovement;

    public int spawnRate;

    private bool mouseInObject = false;

    private float InitialBonusMovement;
    private void Start()
    {
        InitialBonusMovement = BonusMovement;
        bonusText.text = "x2";
    }

    private void Update()
    {
        if (mouseInObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ZoneManager.instance.MoveZoneBonus(BonusMovement);
                Destroy(gameObject);
            }
            if (Input.GetMouseButtonDown(1))
            {
                ZoneManager.instance.MoveZoneBonus(-BonusMovement);
                Destroy(gameObject);
            }
        }
    }

    public void Remove()
    {
        StartCoroutine(DelayRemove());
    }
    IEnumerator DelayRemove()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Cursor")
        {
            mouseInObject = true;
            SpellManager.instance.AddNbOver(1);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Cursor")
        {
            mouseInObject = false;
            SpellManager.instance.AddNbOver(-1);
        }
    }
    private void ZonePoints(float i)
    {
        BonusMovement = InitialBonusMovement * i;
        bonusText.text = "x" + (2 * i).ToString();
    }
}
