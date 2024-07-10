using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public enum SpellName
{
    None, SpawnWall, SlowMoose, InvertMouse, ReversePosition, SpeedUp,
    LeftDirection, RightDirection, UpDirection, DownDirection,
    UpLeftDirection, UpRightDirection, DownLeftDirection, DownRightDirection
}

public class SpellManager : MonoBehaviour
{
    [Header("Refs")]
    public Image ImageLeftInventory;
    public Image ImageRightInventory;


    [Header("Forces")]

    [SerializeField]
    private float speedUpForce;
    [SerializeField]
    private float slowDownForce;

    [SerializeField]
    private float directionForce;

    [Header("Times (in sec)")]
    [SerializeField]
    private float speedUpTime;
    [SerializeField]
    private float slowDownTime;

    [SerializeField]
    private float directionTime;

    [SerializeField]
    private float invertTime;

    private SpellName LeftClickInventory;
    private SpellName RightClickInventory;
    private bool EffectActualyPlay = false;

    private int nbOver = 0;

    private Sprite emptyRightSprite;
    private Sprite emptyRLeftSprite;

    public static SpellManager instance;
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
    }

    public void AddNbOver(int nbToAdd)
    {
        nbOver += nbToAdd;
    }
    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Confined;

        emptyRightSprite = ImageRightInventory.sprite;
        emptyRLeftSprite = ImageLeftInventory.sprite;
        ImageLeftInventory.GetComponent<Animator>().SetTrigger("Change");
        ImageRightInventory.GetComponent<Animator>().SetTrigger("Change");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LaunchSpeel(Type.Player1);
        }

        if (Input.GetMouseButtonDown(1))
        {
            LaunchSpeel(Type.Player2);

        }
        //Debug.Log(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.S))
        {
            // SlowMooseEffect(slowFactor, 3f);
        }
    }


    public void LaunchSpeel(Type _player)
    {
        if (nbOver <= 0 && !EffectActualyPlay)
        {
            if (_player == Type.Player1 && LeftClickInventory != SpellName.None)
            {
                StartCoroutine(LauchEnum(LeftClickInventory));
                UIManager.instance.SetBackgroundIndicator(ImageLeftInventory.sprite);
                LeftClickInventory = SpellName.None;
                ImageLeftInventory.sprite = emptyRLeftSprite;
                ImageLeftInventory.color = Color.white;
                ImageLeftInventory.SetNativeSize();

            }

            if (_player == Type.Player2 && RightClickInventory != SpellName.None)
            {
                StartCoroutine(LauchEnum(RightClickInventory));
                UIManager.instance.SetBackgroundIndicator(ImageRightInventory.sprite);
                RightClickInventory = SpellName.None;
                ImageRightInventory.sprite = emptyRightSprite;
                ImageRightInventory.color = Color.white;
                ImageRightInventory.SetNativeSize();
            }

        }
    }

    public void SetInvotory(SpellName _spellToSet, Type _player, Sprite _spriteSpell)
    {
        if (_player == Type.Player1)
        {
            LeftClickInventory = _spellToSet;
            ImageLeftInventory.sprite = _spriteSpell;
            ImageLeftInventory.color = ZoneManager.instance.Player1Color;
            ImageLeftInventory.SetNativeSize();
            ImageLeftInventory.GetComponent<Animator>().SetTrigger("Change");
            SoundManager.instance.playInventory();
        }
        if (_player == Type.Player2)
        {
            RightClickInventory = _spellToSet;
            ImageRightInventory.sprite = _spriteSpell;
            ImageRightInventory.color = ZoneManager.instance.Player2Color;
            ImageRightInventory.SetNativeSize();
            ImageRightInventory.GetComponent<Animator>().SetTrigger("Change");
            SoundManager.instance.playInventory();
        }
    }



    IEnumerator LauchEnum(SpellName _spell)
    {
        switch (_spell)
        {
            case SpellName.InvertMouse:
                SoundManager.instance.playReverse();
                yield return InvertMouse(invertTime);
                yield return null;
                break;

            case SpellName.SpeedUp:
                SoundManager.instance.playSpeedUp();
                yield return SpeedModify(speedUpForce, speedUpTime);
                yield return null;
                break;

            case SpellName.LeftDirection:
                SoundManager.instance.playMoveLeft();
                yield return MoveCursorToDirectionCrt(Vector3.left, directionForce, directionTime);
                yield return null;
                break;

            case SpellName.RightDirection:
                SoundManager.instance.playMoveRight();
                yield return MoveCursorToDirectionCrt(Vector3.right, directionForce, directionTime);
                yield return null;
                break;

            case SpellName.SlowMoose:
                SoundManager.instance.playSlowDown();
                yield return SpeedModify(slowDownForce, slowDownTime);
                yield return null;
                break;

        }

    }
    private IEnumerator MoveCursorToDirectionCrt(UnityEngine.Vector3 direction, float speed, float movementTime)
    {
        float startTime = Time.time;
        EffectActualyPlay = true;
        while (Time.time < startTime + movementTime)
        {
            Debug.Log("Direction");
            UnityEngine.Vector2 newMousePosition = CursorManager.instance.GetCursorPosition()
             + (Time.deltaTime * direction * speed);
            CursorManager.instance.SetCursorPosition(newMousePosition);
            UIManager.instance.FillBackground((Time.time - startTime) / movementTime);
            yield return null;
        }
        EffectActualyPlay = false;
    }

    private IEnumerator SpeedModify(float speed, float movementTime)
    {

        float startTime = Time.time;
        EffectActualyPlay = true;
        while (Time.time < startTime + movementTime)
        {
            CursorManager.instance.SetSpeed(speed);
            UIManager.instance.FillBackground((Time.time - startTime) / movementTime); yield return null;
        }
        CursorManager.instance.SetInitalSpeed();
        EffectActualyPlay = false;
    }

    private IEnumerator InvertMouse(float movementTime)
    {

        float startTime = Time.time;
        EffectActualyPlay = true;
        while (Time.time < startTime + movementTime)
        {
            CursorManager.instance.SetInvert(true);
            UIManager.instance.FillBackground((Time.time - startTime) / movementTime);
            yield return null;
        }
        CursorManager.instance.SetInvert(false);
        EffectActualyPlay = false;
    }
}
