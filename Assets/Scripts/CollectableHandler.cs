using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private GameObject gameHandler;
    private GameFlowHandler gameFlow;
    private Vector3 spawnPos;
    private Quaternion spawnRot;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;

        gameHandler = GameObject.Find("EventManager");
        gameFlow = gameHandler.GetComponent<GameFlowHandler>();
        gameFlow.onSimulationStart.AddListener(handleOnSimulationStart);
        gameFlow.onExit.AddListener(handleOnExit);

        spawnPos = this.transform.position;
        spawnRot = this.transform.rotation;
    }


    void handleOnSimulationStart() {
        rb.simulated = true;
        Debug.Log("simulating");
    }

    void handleOnExit() {
        this.transform.SetPositionAndRotation(spawnPos, spawnRot);
        rb.simulated = false;
        this.gameObject.SetActive(true);
    }



}
