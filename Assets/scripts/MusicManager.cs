using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private DeathManager deathManager;
    void Start()
    {
        deathManager = FindObjectOfType<DeathManager>();
    }

    private void Update()
    {
        if (deathManager.isDead)
        {
            var music = GetComponent<AudioSource>();
            music.volume = 0.005f;
            return;
        }
    }
}
