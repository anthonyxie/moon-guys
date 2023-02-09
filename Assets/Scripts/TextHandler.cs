using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameHandler;
    private GameFlowHandler gameFlow;
    private TMP_Text tText;

    void Start()
    {
        gameFlow = gameHandler.GetComponent<GameFlowHandler>();
        gameFlow.onPlay.AddListener(handleOnPlay);
        gameFlow.onStart.AddListener(handleOnStart);
        gameFlow.onSimulation.AddListener(handleOnSimulation);
        gameFlow.onSimulationStart.AddListener(handleOnSimulationStart);
        gameFlow.onExit.AddListener(handleOnExit);
        tText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void handleOnPlay(int pIndex) {
        tText.text = $"Player {pIndex + 1} playing";
    }

    void handleOnStart() {
    }

    void handleOnSimulation() {
        tText.text = $"Round {gameFlow.round} starting...";
    }

    void handleOnSimulationStart() {
        tText.text = $"Round {gameFlow.round} begins!";
    }

    void handleOnExit() {
        tText.text = $"Round {gameFlow.round}";
    }

    public void wiener() {
        tText.text = $"Winner is Player{gameFlow.wiener} with {gameFlow.max} points!";
    }

}
