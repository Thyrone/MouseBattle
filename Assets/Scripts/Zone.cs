using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public Type ZoneType;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Cursor")
            ZoneManager.instance.Movement(ZoneType);
    }

}

