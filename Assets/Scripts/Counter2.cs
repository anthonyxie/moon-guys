using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter2 : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI theText;
    public int count = 1;
    void Start()
    {
        theText = GetComponent<TextMeshProUGUI>();
        theText.text = $"{count}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increment() {
        if (count < 4) {
            count += 1;
            theText.text = $"{count}";
        }
    }

    public void decrement() {
        if (count > 1) {
            count = count - 1;
            theText.text = $"{count}";
        }
    }

    public void submit() {
        PlayerPrefs.SetInt("Rounds", count);
    }
}
