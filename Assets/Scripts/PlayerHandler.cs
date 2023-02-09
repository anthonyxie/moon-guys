using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHandler : MonoBehaviour
{

    public int playerIndex;
    public GameObject gameHandler;
    
    public int speed = 6;
    public TMP_Text tText;

    private GameObject line;
    private Rigidbody2D rb; 
    private GravityBody gb;
    private GameFlowHandler gameFlow;
    private int score = 0;
    private Vector3 spawnPos;
    private Quaternion spawnRot;
    

    // Start is called before the first frame update
    void Start()
    {
        line = this.gameObject.transform.GetChild(1).gameObject;

        gameFlow = gameHandler.GetComponent<GameFlowHandler>();
        gameFlow.onPlay.AddListener(handleOnPlay);
        gameFlow.onStart.AddListener(handleOnStart);
        gameFlow.onSimulation.AddListener(handleOnSimulation);
        gameFlow.onSimulationStart.AddListener(handleOnSimulationStart);
        gameFlow.onExit.AddListener(handleOnExit);
        gameFlow.onEnd.AddListener(handleOnEnd);

        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;

        gb = GetComponent<GravityBody>();

        spawnPos = this.transform.position;
        spawnRot = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.simulated && rb.velocity.magnitude < 0.000001) {
            gameFlow.endSimulation();
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Collectable") {
            other.gameObject.SetActive(false);
            score += 1;
            tText.text = $"Player{playerIndex + 1} Score: {score}";
        }
    }

    void handleOnPlay(int pIndex) {
        if (pIndex == playerIndex) {
            line.SetActive(true);
            line.GetComponent<Line>().lineUpdating = true;
        }
        else {
            line.GetComponent<Line>().lineUpdating = false;
        }
    }

    void handleOnStart() {
        Debug.Log("started");
    }

    void handleOnSimulation() {
        line.GetComponent<Line>().lineUpdating = false;
    }

    void handleOnSimulationStart() {
        Debug.Log("SIMULATION STARTING GOGOGOGO");
        line.SetActive(false);
        rb.simulated = true;
        rb.AddForce(new Vector2(line.GetComponent<Line>().forceArrow.x, line.GetComponent<Line>().forceArrow.y) * speed, ForceMode2D.Impulse);
    }

    void handleOnExit() {
        Debug.Log("pp");
        gb.removeAllOrbiters();
        this.transform.SetPositionAndRotation(spawnPos, spawnRot);
        rb.simulated = false;
    }
    
    void handleOnEnd() {
        gameFlow.sendWiener(playerIndex, score);
    }

}
