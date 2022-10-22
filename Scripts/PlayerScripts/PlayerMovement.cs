using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed;

    [SerializeField] private float normalSpeed;
    [SerializeField] private float sprintSpeed;

    [SerializeField] private bool isSprinting;

    private bool canMove;

    void Update()
    {
        moveSpeed = isSprinting ? sprintSpeed : normalSpeed;

        // Rotate player to mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        if (!GameManager.isGamePaused)
        {
            transform.up = lookDir;
        }

        // Convert to Vector3 -> Vector2
        Vector2 mousePosVector2 = mousePos;
        Vector2 transformVector2 = transform.position;



        if (Vector2.Distance(transformVector2, mousePosVector2) > 0.1f)
        {
            canMove = true;
        } else
        {
            canMove = false;
        }

        if (Input.GetKey(KeyCode.W) && canMove)
        {
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        } else
        {
            isSprinting = false;
        }
    }
}
