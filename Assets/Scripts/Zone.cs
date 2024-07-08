using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public Type ZoneType;
        void OnMouseOver()
        {
            ZoneManager.instance.Movement(ZoneType);
        }

        void OnMouseExit()
        {

        }
    }

