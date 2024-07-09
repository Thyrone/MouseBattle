using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour
{
    public float mouseSpeedFactor = 0.3f;
    private Vector3 lastMousePosition;

    // Start is called before the first frame update
    /*  void Start()
    {
        lastMousePosition = Input.mousePosition;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y < transform.position.y
         && Camera.main.ScreenToWorldPoint(new Vector3(Screen.height, 0, 0)).y < transform.position.y
         )
         {
             // Debug.Log("Camera.main.ScreenToWorldPoint(transform.position).x=" + Camera.main.ScreenToWorldPoint(transform.position).x);
             // Obtenir la position actuelle de la souris
             Vector3 currentMousePosition = Input.mousePosition;

             // Calculer le mouvement de la souris
             Vector3 mouseDelta = currentMousePosition - lastMousePosition;

             // Appliquer le facteur de réduction de vitesse
             mouseDelta *= mouseSpeedFactor;

             // Mettre à jour la position de l'objet avec le mouvement réduit
             transform.position += new Vector3(mouseDelta.x, mouseDelta.y, 0);

             // Mettre à jour la dernière position de la souris
             lastMousePosition = currentMousePosition;
         }
         if (Camera.main.ScreenToWorldPoint(new Vector3(Screen.height, 0, 0)).y > transform.position.y)
         {
             Debug.Log("down");
         } 

        Mouse.current.WarpCursorPosition(new Vector3(Input.mousePosition.x, Input.mousePosition.y * 0.1f, 0f));
    }
*/
    private void Start()
    {
        Mouse.current.WarpCursorPosition(Input.mousePosition);
        //StartCoroutine(MoveLogicCrt());
    }

    IEnumerator MoveLogicCrt()
    {
        yield return MoveCursorToDirectionCrt(Vector3.left, 150, 5);
        yield return null;
        yield return MoveCursorToDirectionCrt(Vector3.right, 350, 5);
    }

    private IEnumerator MoveCursorToDirectionCrt(Vector3 direction, float speed, float movementTime)
    {
        float startTime = Time.time;

        while (Time.time < startTime + movementTime)
        {
            Vector2 newMousePosition = Input.mousePosition + (Time.deltaTime * direction * speed);
            Mouse.current.WarpCursorPosition(newMousePosition);
            yield return null;
        }
    }
}
