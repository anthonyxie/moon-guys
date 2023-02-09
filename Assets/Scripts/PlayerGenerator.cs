using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerFab;
    public GameObject scoreFab;
    public GameObject gameHandler;
    private float[,] locations = new float[,] {{-7.4f,3.7f},{7.4f,3.7f},{-7.4f,-3.7f},{7.4f, -3.7f}};
    private float[,] locations2 = new float[,] {{-7.4f,4.4f},{7.4f,4.4f},{-7.4f,-4.8f},{7.4f, -4.8f}};
    private Color[] colors = new Color[] { new Color(0,1,0,1), new Color(1,0,0,1), new Color(1,1,1,1), new Color(0,0,1,1),  new Color(1,1,0,1), new Color(0, 0, 0, 1)};
    private int numPlayers;
    void Start()
    {
        numPlayers = PlayerPrefs.GetInt("Players", 1);
        for (int i = 0; i < numPlayers; i++) {
            MakeAGuy(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //take in a player index, make the guy
    void MakeAGuy(int i) {
        GameObject player = Instantiate(playerFab);
        GameObject tex = Instantiate(scoreFab);
        tex.name = $"Player{i + 1}Score";
        player.name = $"Player{i + 1}";
        player.GetComponent<PlayerHandler>().playerIndex = i;
        player.GetComponent<PlayerHandler>().gameHandler = gameHandler;
        player.GetComponent<PlayerHandler>().tText = tex.GetComponent<TMP_Text>();
        
        tex.GetComponent<TMP_Text>().text = $"Player{i + 1} Score: 0";
        player.transform.position = new Vector3(locations[i,0], locations[i,1], 0);
        tex.transform.position = new Vector3(locations2[i,0], locations2[i,1], 0);

        SpriteRenderer sprite = player.GetComponent<SpriteRenderer>();
        sprite.color = colors[i];
    }
}
