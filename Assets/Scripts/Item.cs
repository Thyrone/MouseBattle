using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item : MonoBehaviour
{
    public SpellName spell;
    public Sprite spriteSpell;
    public float spawnRate;
    public float speed;


    private float treshold = 2;
    private float screenDown;
    void Start()
    {
        screenDown = Camera.main.ScreenToWorldPoint(new Vector3(Screen.height, 0, 0)).y - treshold;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
    }

    void Update()
    {
        if (transform.position.y < screenDown)
            Destroy(gameObject);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpellManager.instance.SetInvotory(spell, Type.Player1, spriteSpell);
            Debug.Log("Pressed left click.");
        }
        if (Input.GetMouseButtonDown(1))
        {
            SpellManager.instance.SetInvotory(spell, Type.Player2, spriteSpell);
            Debug.Log("Pressed right click.");
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

}
