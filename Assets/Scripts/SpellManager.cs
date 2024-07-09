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
        Cursor.lockState = CursorLockMode.Confined;
        Mouse.current.WarpCursorPosition(Input.mousePosition);
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
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(MoveLogicCrt());
        }
        Debug.Log("Slow");
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
    private void SlowMooseEffect(float speed, float movementTime)
    {
        float startTime = Time.time;

        while (Time.time < startTime + movementTime)
        {
            Debug.Log("Slow");
            // Capturez le mouvement de la souris
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            // Appliquez le facteur de ralentissement
            Vector2 slowMouseDelta = mouseDelta * speed;

            // Obtenez la position actuelle du curseur
            Vector2 currentPosition = Mouse.current.position.ReadValue();

            // Calculez la nouvelle position
            Vector2 newPosition = currentPosition + slowMouseDelta;

            // Déplacez le curseur à la nouvelle position
            Mouse.current.WarpCursorPosition(newPosition);
        }

    }


    IEnumerator MoveLogicCrt()
    {
        yield return InvertMouse(5);
        yield return null;
        yield return MoveCursorToDirectionCrt(Vector3.left, 150, 5);
        yield return null;
        yield return SpeedUp(1.1f, 5);
        //yield return MoveCursorToDirectionCrt(Vector3.right, 350, 5);
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
                yield return SpeedUp(1.1f, 5);
                yield return null;
                break;

            case SpellName.LeftDirection:
                yield return MoveCursorToDirectionCrt(Vector3.left, 150, 5);
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

    private IEnumerator SpeedUp(float speed, float movementTime)
    {

        float startTime = Time.time;
        EffectActualyPlay = true;
        while (Time.time < startTime + movementTime)
        {
            Debug.Log("Direction");
            UnityEngine.Vector2 newMousePosition = new Vector2(
            Input.mousePosition.x + (Mouse.current.delta.ReadUnprocessedValue().x * speed),
            Input.mousePosition.y + (Mouse.current.delta.ReadUnprocessedValue().y * speed));

            /* UnityEngine.Vector2 newMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y) +
             (Time.deltaTime * Mouse.current.delta.ReadUnprocessedValue() * speed);*/

            Mouse.current.WarpCursorPosition(newMousePosition);
            UIManager.instance.FillBackground((Time.time - startTime) / movementTime); yield return null;
        }
        EffectActualyPlay = false;
    }

    private IEnumerator InvertMouse(float movementTime)
    {

        float startTime = Time.time;
        EffectActualyPlay = true;
        while (Time.time < startTime + movementTime)
        {
            Debug.Log("Direction");
            UnityEngine.Vector2 newMousePosition = new Vector2(
            Input.mousePosition.x - (Mouse.current.delta.ReadUnprocessedValue().x),
            Input.mousePosition.y - (Mouse.current.delta.ReadUnprocessedValue().y));

            /* UnityEngine.Vector2 newMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y) +
             (Time.deltaTime * Mouse.current.delta.ReadUnprocessedValue() * speed);*/

            Mouse.current.WarpCursorPosition(newMousePosition);
            UIManager.instance.FillBackground((Time.time - startTime) / movementTime);
            yield return null;
        }
        EffectActualyPlay = false;
    }
}
