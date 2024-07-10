using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Item : MonoBehaviour
{
    public SpellName spell;
    public Sprite spriteSpell;
    public float spawnRate;
    public float speed;
    public UnityEvent EventOnClick;


    private float treshold = 2;
    private float screenDown;

    private bool mouseInObject = false;
    void Start()
    {
        screenDown = Camera.main.ScreenToWorldPoint(new Vector3(Screen.height, 0, 0)).y - treshold;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
    }

    void Update()
    {
        if (transform.position.y < screenDown)
            Destroy(gameObject);

        if (mouseInObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SpellManager.instance.SetInvotory(spell, Type.Player1, spriteSpell);
                EventOnClick.Invoke();
                Debug.Log("Pressed left click.");
            }
            if (Input.GetMouseButtonDown(1))
            {
                SpellManager.instance.SetInvotory(spell, Type.Player2, spriteSpell);
                EventOnClick.Invoke();
                Debug.Log("Pressed right click.");
            }

        }

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

}
