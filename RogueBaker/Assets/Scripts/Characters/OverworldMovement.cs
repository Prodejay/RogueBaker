﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldMovement : MonoBehaviour
{
    public float speed = 4f;

    public Vector3 minPlayerBounds, maxPlayerBounds;
    private Vector3 targetPosition;

    private string levelName;

    private bool canMove = true;
    private bool isMoving = false;

    private Animator anime;
    public BoxCollider2D triggerCollider;
    public CircleCollider2D otherCollider;

    public PlayerVectorValue startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();

        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            setTargetPosition();
        }

        if (isMoving)
        {
            canMove = false;
            anime.SetFloat("Speed", 1f);
            triggerCollider.enabled = false;
            otherCollider.enabled = false;
            movePlayer();
        }
        else
        {
            anime.SetFloat("Speed", 0f);
            triggerCollider.enabled = true;
            otherCollider.enabled = true;
            canMove = true;
        }
    }

    void setTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (canMove)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform != null)
                {
                    if (hit.transform.tag == "Level")
                    {
                        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        targetPosition.z = transform.position.z;
                    }

                    if ((targetPosition.x > maxPlayerBounds.x) || (targetPosition.x < minPlayerBounds.x) || (targetPosition.y > maxPlayerBounds.y)
                    || (targetPosition.y < minPlayerBounds.y))
                    {
                        if (targetPosition.x > maxPlayerBounds.x)
                        {
                            float newTarget = targetPosition.x - maxPlayerBounds.x;

                            targetPosition.x = targetPosition.x - newTarget;
                        }
                        else if (targetPosition.y > maxPlayerBounds.y)
                        {
                            float newTarget = targetPosition.y - maxPlayerBounds.y;

                            targetPosition.y = targetPosition.y - newTarget;
                        }
                    }
                    isMoving = true;
                    Debug.Log(targetPosition);
                }                        
            }
        }
    }

    void movePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition,
                speed * Time.deltaTime);

        if(transform.position == targetPosition)
        {
            isMoving = false;
        }
    }
}
