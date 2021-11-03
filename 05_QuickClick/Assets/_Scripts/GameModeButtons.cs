using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Botones

/// <summary>
/// Enum para modo de juego
/// </summary>
public enum GameMode
{
    normal_easy = 1,
    normal_medium = 2,
    normal_hard = 3,
    time = 4 ,
    click = 5,
    infinite = 6
}

public class GameModeButtons : MonoBehaviour
{
    private Button _modeButton;
    private GameManager gameManager;
    public GameMode SelectedGameMode;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
            
        _modeButton = GetComponent<Button>();
        _modeButton.onClick.AddListener(setGameMode);
    }

    /// <summary>
    /// Seleccion de modo de juego o dificultad.
    /// </summary>
    void setGameMode()
    {
        //SelectedGameMode = GameMode.normal_easy;
        
        Debug.Log("El boton "+gameObject.name+" ha sido pulsado");
        gameManager.StartGame(SelectedGameMode);
    }
}
