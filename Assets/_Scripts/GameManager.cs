using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;

    public static int rifleBulletsLeft = 30;
    public static int shotgunBulletsLeft = 4;
    public static int pistolBulletsLeft = 9;
    private float timer;
    public bool isPaused;
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


}