﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    bool playerInRange;
    bool alreadyTalked;
    bool convoStarted;
    public bool thisDialogueDone;

    // Update is called once per frame
    void Update()
    {
        continueTalk();
    }

    void continueTalk()
    {
        if (!FindObjectOfType<DialogueManager>().ConvoFinished && Input.GetButtonDown("Attack") && playerInRange)
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();

            if (FindObjectOfType<DialogueManager>().ConvoFinished)
            {
                thisDialogueDone = true;
                alreadyTalked = true;
            }
        }
    }

    void startTalk()
    {
        if (!FindObjectOfType<DialogueManager>().ConvoFinished)
        {
            if (!convoStarted)
            {
                FindObjectOfType<DialogueManager>().StartConvo(dialogue);
                convoStarted = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            if (!alreadyTalked)
            {
                startTalk();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
