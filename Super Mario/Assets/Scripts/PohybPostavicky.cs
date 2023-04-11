using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PohybPostavicky : MonoBehaviour
{
    //Definov�n� kamery
    private new Camera camera;
    private new Rigidbody2D rigidbody;

    //Definov�n� pohybu, sk�k�n� a gravitace
    public float moveSpeed = 8f; //8 sn�mk�
    public float maxJumpHeight = 5f; //5 sn�mk�
    public float maxJumpTime = 1f; //1 sekunda
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f) ,2);

    //Prom�nn�, kter� zji��uj� jestli Mario sk��e nebo je na zemi
    public bool grounded { get; private set; }
    public bool jumping { get; private set; } 


    private float inputAxis;
    private Vector2 velocity;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        camera = Camera.main; 
    }

    //Unity vol� tuhle funkci po ka�d�m framu ve h�e, kontrolujeme INPUT (kontrola jestli se Mario h�be, sk��e atd...)
    private void Update()
    {
        HorizontalMovement();

        grounded = rigidbody.Raycast(Vector2.down);

        if (grounded)
        {
            GroundedMovement();
        }

        ApplyGravity(); 
    }

    //Horizont�ln� pohyb
    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);
    }

    //Definujeme to, �e chceme sko�it jen kdy� jsme na zemi
    private void GroundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            jumping = true;
        }
    }

    //Definov�n� gravitace
    private void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;

        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    //Zablokov�n� kamery a Maria (Mario se nem��e vr�tit zp�t s kamerou)
    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

        rigidbody.MovePosition(position); 
    }
}
