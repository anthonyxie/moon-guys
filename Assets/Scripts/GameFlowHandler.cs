using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MyIntEvent : UnityEvent<int>
{
}

public class GameFlowHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public enum GameState {
        Start,
        PlayerArrows,
        Simulation,
        Simulating,
        Exit,
        End
    }


    public GameState gameState;
    public UnityEvent onStart, onSimulation, onSimulationStart, onExit, onEnd, onWiener;
    public MyIntEvent onPlay;

    private int currPlayer;
    public int maxPlayers;
    public float countdownTime = 2;
    public int round;

    private int maxRounds;
    private int movingPlayers;
    private bool exitable = false;

    public int wiener;
    public int max;
    private int calcingPlayers;
    void Start()
    {
        gameState = GameState.Start;
        maxPlayers = PlayerPrefs.GetInt("Players", 1) - 1;
        currPlayer = 0;
        round = 1;
        maxRounds = PlayerPrefs.GetInt("Rounds", 1);
        movingPlayers = maxPlayers + 1;
        calcingPlayers = maxPlayers + 1;

        wiener = 0;
        max = 0;
    }
 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            switch(gameState) {
                case GameState.Start:
                    onStart.Invoke();
                    gameState = GameState.PlayerArrows;
                    onPlay.Invoke(currPlayer);
                    if (currPlayer == maxPlayers) {
                        //covers the case where you only have one player
                        gameState = GameState.Simulation;
                    }
                    break;
                case GameState.PlayerArrows:
                    currPlayer += 1;
                    onPlay.Invoke(currPlayer);
                    if (currPlayer == maxPlayers) {
                        gameState = GameState.Simulation;
                    }
                    break;
                case GameState.Simulation:
                    onSimulation.Invoke();
                    gameState = GameState.Simulating;
                    StartCoroutine(countdown());
                    break;
                case GameState.Exit:
                    gameState = GameState.PlayerArrows;
                    onPlay.Invoke(currPlayer);
                    break;
                case GameState.End:
                    if (exitable) {
                        SceneManager.LoadScene("MenuScene");
                    }
                    break;


            }
        }
    }

    public void endSimulation() {
        movingPlayers -= 1;
        Debug.Log(movingPlayers);
        if (movingPlayers == 0 && gameState == GameState.Simulating) { //all players stopped moving
            exitOut();
        }    
    }

    public void sendWiener(int index, int score) {
        calcingPlayers -= 1;
        if (score > max) {
            wiener = index;
            max = score;
        }
        if (calcingPlayers == 0) {
            Debug.Log($"Player{wiener + 1} is the wiener with a score of {max}!");
            onWiener.Invoke();
            StartCoroutine(countdown2());
            
        }
    }

    IEnumerator countdown() {
        float timeToMove = countdownTime;
        float t = 0;
        while (t < 1) {
            t += Time.deltaTime / timeToMove;
            yield return null;
        }
        onSimulationStart.Invoke();
        yield return new WaitForSeconds(8);
        if (gameState == GameState.Simulating) {
            exitOut();
        }
    }

    IEnumerator countdown2() {
        yield return new WaitForSeconds(2);
        exitable = true;
    }

    private void exitOut() {
        gameState = GameState.Exit;
        round += 1;
        onExit.Invoke();
        currPlayer = 0;
        movingPlayers = maxPlayers + 1;
        if (round > maxRounds) {
            gameState = GameState.End;
            onEnd.Invoke();
        }       
    }
}
