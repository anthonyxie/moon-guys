using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeTillDie = 20f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeTillDie = timeTillDie - Time.deltaTime;
        if (timeTillDie <= 0) {
            Destroy(this.gameObject);
        }
    }
}
