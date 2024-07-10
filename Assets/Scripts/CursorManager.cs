using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance;

    private Vector3 objectPosition;
    public float speed = 0.1f;
    private float initalSpeed;
    private bool invert = false;
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
    void Start()
    {

        // Cachez le curseur de la souris et verrouillez-le au centre de l'écran
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        initalSpeed = speed;
        // Initialiser la position de l'objet
        objectPosition = transform.position;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Center");
            objectPosition = Vector3.zero;
            transform.position = objectPosition;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Center");
            invert = !invert;
        }
        // Obtenez le delta de la souris
        float mouseDeltaX = Input.GetAxis("Mouse X");
        float mouseDeltaY = Input.GetAxis("Mouse Y");

        if (!invert)
            // Appliquez le delta à la position de l'objet
            objectPosition += new Vector3(mouseDeltaX, mouseDeltaY, 0) * speed;
        else
            objectPosition += new Vector3(-mouseDeltaX, -mouseDeltaY, 0) * speed;


        // Mettez à jour la position de l'objet
        transform.position = objectPosition;
    }

    public void SetInitalSpeed()
    {
        speed = initalSpeed;
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    public void SetInvert(bool _invert)
    {
        invert = _invert;
    }

    public Vector3 GetCursorPosition()
    {
        return objectPosition;
    }

    public void SetCursorPosition(Vector3 _position)
    {
        objectPosition = _position;
    }


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
    }*/
}
