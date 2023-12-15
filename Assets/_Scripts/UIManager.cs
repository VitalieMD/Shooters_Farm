using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts
{
    /// <summary>
    /// When enemy Instantiate it has more power and life
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        public static UIManager uiManagerInstance;

        public enum GameState
        {
            MainMenu,
            Paused,
            GameLost,
            Playing
        }

        public GameState currentState;
        [SerializeField] private GameObject pausePanel, loserPanel, uiPanel, mainMenuPanel;
        [SerializeField] private Slider _healthSlider;

        [SerializeField] private TextMeshProUGUI _bulletsLeftText,
            scoreText,
            textToFlicker,
            record,
            lostPanelScore;
        public int score, highScore;
        [SerializeField] private Slider volumeSlider;
        public AudioMixer mixer;

        private void Awake()
        {
            if (uiManagerInstance == null)
            {
                uiManagerInstance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                CheckGameState(GameState.MainMenu);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                CheckGameState(GameState.Playing);
            }

            if (PlayerPrefs.HasKey("SaveScore"))
                highScore = PlayerPrefs.GetInt("SaveScore", highScore);

            if (!PlayerPrefs.HasKey("musicVolume"))
            {
                PlayerPrefs.SetFloat("musicVolume", 1);
                Load();
            }
            else
            {
                Load();
            }

            scoreText.text = score.ToString();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SetPausePanel();
            }

            FlickeringText();
            record.text = highScore.ToString();
            lostPanelScore.text = score.ToString();
        }

        public void CheckGameState(GameState gameState)
        {
            currentState = gameState;
            switch (currentState)
            {
                case GameState.MainMenu:
                    Time.timeScale = 1;
                    GameManager.gameManagerInstance.isPaused = false;
                    MainMenuActive();
                    break;

                case GameState.Paused:
                    PauseActive();
                    Time.timeScale = 0;
                    GameManager.gameManagerInstance.isPaused = true;
                    break;

                case GameState.Playing:
                    Time.timeScale = 1;
                    PlayActive();
                    GameManager.gameManagerInstance.isPaused = false;
                    break;

                case GameState.GameLost:
                    Time.timeScale = 0;
                    LostActive();
                    GameManager.gameManagerInstance.isPaused = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void MainMenuActive()
        {
            pausePanel.SetActive(false);
            loserPanel.SetActive(false);
            uiPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }

        private void PauseActive()
        {
            pausePanel.SetActive(true);
            loserPanel.SetActive(false);
            uiPanel.SetActive(false);
            mainMenuPanel.SetActive(false);
        }

        private void PlayActive()
        {
            pausePanel.SetActive(false);
            loserPanel.SetActive(false);
            uiPanel.SetActive(true);
            mainMenuPanel.SetActive(false);
        }

        private void LostActive()
        {
            pausePanel.SetActive(false);
            loserPanel.SetActive(true);
            uiPanel.SetActive(false);
            mainMenuPanel.SetActive(false);
        } 

        public void StartLevel()
        {
            SceneManager.LoadScene(1);
            CheckGameState(GameState.Playing);
        }

        public void GoMainMenu()
        {
            SceneManager.LoadScene(0);
            CheckGameState(GameState.MainMenu);
        }

        public void ExitApp()
        {
            Application.Quit();
        }
        public void SetPausePanel()
        {
            if (currentState == GameState.Playing)
                CheckGameState(GameState.Paused);
            else if (currentState == GameState.Paused)
                CheckGameState(GameState.Playing);
        }

        public void GameLost()
        {
            CheckGameState(GameState.GameLost);
        }

        public void SetVolumeSlider(float value)
        {
            mixer.SetFloat("GameVolume", Mathf.Log10(value) * 20);
            Save();
        }

        private void FlickeringText()
        {
            textToFlicker.color = Color.Lerp(Color.clear, Color.black, Mathf.PingPong(Time.time, 1));
        }
        public void UpdateScore(int points)
        {
            score += points;
            scoreText.text = score.ToString();
            HighScore();
        }

        private void HighScore()
        {
            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("SaveScore", highScore);
            }
        }

        private void Load()
        {
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }

        private void Save()
        {
            PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        }

        public void SetHealth(float health)
        {
            _healthSlider.value = health;
        }

        public void SetBullets(int bulletsLeft)
        {
            _bulletsLeftText.text = bulletsLeft.ToString();
        }
    }
}