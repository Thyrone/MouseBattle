using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item : MonoBehaviour
{
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
        if (Input.GetMouseButtonDown(0)) Debug.Log("Pressed left click.");
        if (Input.GetMouseButtonDown(1)) Debug.Log("Pressed right click.");
    }


}
