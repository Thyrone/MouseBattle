using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum Type { Player1, Player2, Both };

public class ZoneManager : MonoBehaviour
{
    public Transform ZonePosition;

    public static ZoneManager instance;

    public Vector3 Direction;

    public float Speed;

    private float InitialZoneSpeed;

    [SerializeField] private float ScreenBorder;

    private Camera _camera;

    public TMP_Text VictoryText;

    public Color Player1Color;
    public Color Player2Color;

    private float screenRight, screenLeft;

    public void Start()
    {
        screenLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        InitialZoneSpeed = Speed;
    }

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
        Debug.Log("Zone.Position=" + ZonePosition.position);
        EndGame();
    }

    public void Movement(Type ZoneType)
    {
        if (ZoneType == Type.Player1)
        {
            ZonePosition.position -= Direction * Speed * Time.deltaTime;
        }

        if (ZoneType == Type.Player2)
        {
            ZonePosition.position += Direction * Speed * Time.deltaTime;
        }

        Vector2 screenPosition = _camera.WorldToScreenPoint(ZonePosition.position);
        if (screenPosition.x < Camera.main.pixelWidth * 0.25f || screenPosition.x > Camera.main.pixelWidth * 0.75f)
        {
            Speed = InitialZoneSpeed / 2;
        }
        else
        {
            Speed = InitialZoneSpeed;
        }
    }

    public void EndGame()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(ZonePosition.position);
        if (screenPosition.x + ScreenBorder > Camera.main.pixelWidth)
        {
            VictoryText.text = "Victoire des Jaunes !";
            Time.timeScale = 0;
        }

        if (screenPosition.x - ScreenBorder <= 0)
        {
            VictoryText.text = "Victoire des Bleus !";
            Time.timeScale = 0;
        }
    }

    public void MoveZoneBonus(float _bonusMovement)
    {
        ZonePosition.position += Direction * _bonusMovement;
    }


}
