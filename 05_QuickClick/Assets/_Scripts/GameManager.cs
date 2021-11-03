using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //UI texts
using UnityEngine.UI; //Contiene UI
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random; //Para reiniciar

public enum GameState
{
    inGame,
    gameOver,
    pause,
    loading
}

public class GameManager : MonoBehaviour
{
    //Game
    public GameState gameState;
    public GameMode GameMode { get; private set; }
    public string MAX_SCORE = "MAX_SCORE";

    //Gameplay
    public List<GameObject> targetPrefabs;

    [SerializeField] private float MinSpawnRate = 4f;

    [SerializeField] private float MaxSpawnRate = 8f;
    [SerializeField] private int TargetsToSpawn = 1;
    [SerializeField] private int MaxTargetsPerWave = 3;

    private int WavesCount = 0;
    private int livesCount = 3;

    //UI
    public GameObject titleScreen;
    public GameObject scoreUI;
    public GameObject timeUI;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI gameOverText;
    public Button RestartButton;
    public Button FinishButton;
    public List<GameObject> liveImages;

    [SerializeField] private int Time = 60; //In seconds
    private int _score;

    private int Score
    {
        get { return _score; }
        set
        {
            _score = Mathf.Clamp(value, 0, 9999); //Recorta entre dos valores
        }
    }

    //Unity Event
    public void Start()
    {
        ShowMaxScore();
    }

    /// <summary>
    /// Metodo para iniciar la partida
    /// </summary>
    /// <param name="selectedGameMode">Modo de juego seleccionado</param>
    public void StartGame(GameMode selectedGameMode)
    {
        GameMode = selectedGameMode;
        //Si es un modo especial, la dificultad es normal
        int gameDifficultyValue = (int) GameMode;
        if (gameDifficultyValue > 3)
            gameDifficultyValue = (int) GameMode.normal_medium;
        else
        {
            livesCount = Math.Max(livesCount - (gameDifficultyValue - 1), 1);
            for (int i = 0; i < livesCount; i++)
                liveImages[i].SetActive(true);
        }

        Debug.Log("POR DEFECTO TIEMPOS: " + MinSpawnRate + "-" + MaxSpawnRate + " Oleada: " + MaxTargetsPerWave);
        //Cambiar dificultad: Menor tiempo entre oleadas y mayor numero de objetivos
        MinSpawnRate /= gameDifficultyValue;
        MaxSpawnRate /= gameDifficultyValue;
        MaxTargetsPerWave *= gameDifficultyValue;




        Debug.Log("TIEMPOS: " + MinSpawnRate + "-" + MaxSpawnRate + " Oleada: " + MaxTargetsPerWave);
        Debug.Log("Modo seleccionado: " + gameDifficultyValue + ":" + GameMode);

        //Cambio de estado e inicio del juego
        gameState = GameState.inGame;

        titleScreen.gameObject.SetActive(false);

        if (GameMode == GameMode.time)
        {
            timeUI.gameObject.SetActive(true);
            StartCoroutine(StartTime());
        }

        if (GameMode == GameMode.infinite)
            FinishButton.gameObject.SetActive(true);

        scoreUI.gameObject.SetActive(true);

        StartCoroutine(SpawnTarget());

        Score = 0;
        UpdateScore(0);
    }

    IEnumerator SpawnTarget()
    {
        while (gameState == GameState.inGame)
        {
            yield return new WaitForSeconds(Random.Range(MinSpawnRate, MaxSpawnRate));

            int index = 0;
            for (int i = 0; i < TargetsToSpawn; i++)
            {
                index = Random.Range(0, targetPrefabs.Count - 1);
                Instantiate(targetPrefabs[index]);
            }

            WavesCount++;
            if (WavesCount % 3 == 0 && TargetsToSpawn < MaxTargetsPerWave)
                TargetsToSpawn++;
        }
    }

    IEnumerator StartTime()
    {
        while (gameState == GameState.inGame && Time > 0)
        {
            yield return new WaitForSeconds(1);
            Time--;
            timeText.text = $"Time:\n{Time}";
        }

        GameOver(); //Time = 0
    }

    /// <summary>
    /// Actualiza e imprime por pantalla la puntuacion.
    /// </summary>
    /// <param name="pointsToAdd"></param>
    internal void UpdateScore(int pointsToAdd)
    {
        Score += pointsToAdd;
        scoreText.text = $"Score:\n{Score}";
    }

    public void ShowMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt(MAX_SCORE, 0);
        scoreText.text = "Max Score:\n" + maxScore;
    }

    public void SetMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt(MAX_SCORE, 0);
        PlayerPrefs.SetInt("MAX_SCORE", Math.Max(maxScore, _score));
    }

    /// <summary>
    /// Llamar cuando el usuario pìerda la partida
    /// </summary>
    public void GameOver()
    {
        livesCount--;

        if (livesCount >= 0)
        {
            Image liveImage = liveImages[livesCount].GetComponent<Image>();
            var tempColor = liveImage.color;
            tempColor.a = 0.3f; //Transparencia a 30%
            liveImage.color = tempColor;
        }

        if (livesCount <= 0)
        {
            SetMaxScore();

            gameState = GameState.gameOver;
            gameOverText.gameObject.SetActive(true);
            RestartButton.gameObject.SetActive(true);
        }
    }

    //Un boton solo puede llamar funciones vacias
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}