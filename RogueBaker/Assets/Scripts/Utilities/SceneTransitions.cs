﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public PlayerVectorValue playerStorage;
    private Animator anime;

    private void Start()
    {
        anime = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerStorage.initialValue = playerPosition;
            SceneManager.LoadScene(sceneToLoad);
            anime.SetTrigger("FadeOut");
        }
    }
}
