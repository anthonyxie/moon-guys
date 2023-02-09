using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter1 : MonoBehaviour
{

    // Start is called before the first frame update
    public string[] difficulties = new string[] { "Easy", "Medium", "Hard" };
    private TextMeshProUGUI theText;
    public int count = 0;
    void Start()
    {
        theText = GetComponent<TextMeshProUGUI>();
        theText.text = difficulties[count];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increment() {
        count += 1;
        theText.text = difficulties[count % 3];
    }

    public void decrement() {
        count = count - 1;
        if (count < 0) {
            count += 3;
        }
        theText.text =  difficulties[count % 3];
    }

    public void submit() {
        PlayerPrefs.SetInt("Difficulty", count % 3);
    }
}
