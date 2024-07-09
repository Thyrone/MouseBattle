using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum SpellName
{
    SpawnWall, SlowMoose, BumpingBall, ReversePosition
}

public class SpellManager : MonoBehaviour
{

    private float slowFactor;
    public SpellName LeftClickInventory;
    public SpellName RightClickInventory;

    private void Start()
    {
        Mouse.current.WarpCursorPosition(Input.mousePosition);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(SlowMooseEffect(slowFactor, 3f));
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(MoveLogicCrt());

        }

    }

    private void SlowMooseEffect()
    {
    }

    public void SetslowFactor(float _slowfactor)
    {
        slowFactor = _slowfactor;
    }


    IEnumerator MoveLogicCrt()
    {
        yield return MoveCursorToDirectionCrt(Vector3.left, 150, 5);
        yield return null;
        yield return MoveCursorToDirectionCrt(Vector3.right, 350, 5);
    }

    private IEnumerator MoveCursorToDirectionCrt(UnityEngine.Vector3 direction, float speed, float movementTime)
    {
        float startTime = Time.time;

        while (Time.time < startTime + movementTime)
        {
            Debug.Log("Direction");
            UnityEngine.Vector2 newMousePosition = Input.mousePosition + (Time.deltaTime * direction * speed);
            Mouse.current.WarpCursorPosition(newMousePosition);
            yield return null;
        }
    }

    private IEnumerator SlowMooseEffect(float speed, float movementTime)
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

            yield return null;
        }
    }

}
