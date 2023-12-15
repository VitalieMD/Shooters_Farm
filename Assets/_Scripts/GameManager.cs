using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;
using _Scripts;
using _Scripts.Shooting;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;

    public static int rifleBulletsLeft = 30;
    public static int shotgunBulletsLeft = 4;
    public static int pistolBulletsLeft = 9;
    private float timer;
    public bool isPaused;
    [SerializeField] private GunSystem _gunSystem;
    public static float gameTime;

    private void Awake()
    {
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
        _gunSystem = SceneManager.GetActiveScene().buildIndex == 1 ? GameObject.Find("Player").GetComponent<GunSystem>() : null;
        if (rifleBulletsLeft <= 0)
        {
            timer += Time.deltaTime;
            if (timer >= 4)
            {
                rifleBulletsLeft = 30;
                _gunSystem!.equippedGun.bulletsAmount = rifleBulletsLeft;
                timer = 0;
            }
        }

        if (pistolBulletsLeft <= 0)
        {
            timer += Time.deltaTime;
            if (timer >= 3)
            {
                pistolBulletsLeft = 9;
                _gunSystem!.equippedGun.bulletsAmount = pistolBulletsLeft;
                timer = 0;
            }
        }

        if (shotgunBulletsLeft <= 0)
        {
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                shotgunBulletsLeft = 4;
                _gunSystem!.equippedGun.bulletsAmount = shotgunBulletsLeft;
                timer = 0;
            }
        }
    }


}