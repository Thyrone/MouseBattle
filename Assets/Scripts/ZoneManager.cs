using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;


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
    public Image VictoryImage;

    public Color Player1Color;
    public Color Player2Color;
    public UnityEvent winEvent;

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
        if (screenPosition.x + Camera.main.pixelWidth * 0.05 > Camera.main.pixelWidth)
        {
            VictoryText.text = "Yellow Win !";
            VictoryImage.color = Player2Color;
            SoundManager.instance.playVictory_Paul();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;


            winEvent.Invoke();
            //Time.timeScale = 0;
        }

        if (screenPosition.x - Camera.main.pixelWidth * 0.05 <= 0)
        {
            VictoryText.text = "Blue Win !";
            VictoryImage.color = Player1Color;
            SoundManager.instance.playVictory_Cam();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

            winEvent.Invoke();
            // Time.timeScale = 0;
        }
    }

    public void MoveZoneBonus(float _bonusMovement)
    {
        ZonePosition.position += Direction * _bonusMovement;
    }


}
