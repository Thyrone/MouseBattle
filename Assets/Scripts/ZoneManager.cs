using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Type { Player1, Player2 };

public class ZoneManager : MonoBehaviour
{
    public Transform ZonePosition;

    public static ZoneManager instance;

    public Vector3 Direction;

    public float Speed;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

    }

    public void Movement(Type ZoneType)
    {
        if (ZoneType == Type.Player1)
        {
            ZonePosition.position += Direction * Speed * Time.deltaTime ;
        }

        if (ZoneType == Type.Player2)
        {
            ZonePosition.position -= Direction * Speed * Time.deltaTime;
        }
    }

}
