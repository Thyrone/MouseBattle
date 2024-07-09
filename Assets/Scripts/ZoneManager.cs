using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum Type { Player1, Player2 };

public class ZoneManager : MonoBehaviour
{
    public Transform ZonePosition;

    public static ZoneManager instance;

    public Vector3 Direction;

    public float Speed;

    [SerializeField] private float ScreenBorder;

    private Camera _camera;

    public TMP_Text VictoryText;

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

        _camera = Camera.main;
    }

    private void Update()
    {
        EndGame();
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

    public void EndGame()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(ZonePosition.position);
        if (screenPosition.x > Camera.main.pixelWidth)
        {
            Debug.Log("Victoire des bleus !");
            VictoryText.text = "Victoire des bleus !";
            Time.timeScale = 0;
        }

        if (screenPosition.x <= 0)
        {
            Debug.Log("Victoire des Jaunes !");
            VictoryText.text = "Victoire des Jaunes !";
            Time.timeScale = 0;
        }
    }


}
