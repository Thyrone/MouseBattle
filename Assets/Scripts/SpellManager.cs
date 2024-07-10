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

    public Image ImageLeftInventory;
    public Image ImageRightInventory;
    public SpellName LeftClickInventory;
    public SpellName RightClickInventory;

    private bool EffectActualyPlay = false;
    public static SpellManager instance;

    private int nbOver = 0;

    private Sprite emptyRightSprite;
    private Sprite emptyRLeftSprite;
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
        }
        if (_player == Type.Player2)
        {
            RightClickInventory = _spellToSet;
            ImageRightInventory.sprite = _spriteSpell;
            ImageRightInventory.color = ZoneManager.instance.Player2Color;
            ImageRightInventory.SetNativeSize();
            ImageRightInventory.GetComponent<Animator>().SetTrigger("Change");
        }
    }



    IEnumerator LauchEnum(SpellName _spell)
    {
        switch (_spell)
        {
            case SpellName.InvertMouse:
                yield return InvertMouse(5);
                yield return null;
                break;

            case SpellName.SpeedUp:
                yield return SpeedModify(1.1f, 5);
                yield return null;
                break;

            case SpellName.LeftDirection:
                yield return MoveCursorToDirectionCrt(Vector3.left, 150, 5);
                yield return null;
                break;

            case SpellName.SlowMoose:
                yield return SpeedModify(0.01f, 5);
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
            UnityEngine.Vector2 newMousePosition = Input.mousePosition + (Time.deltaTime * direction * speed);
            Mouse.current.WarpCursorPosition(newMousePosition);
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
