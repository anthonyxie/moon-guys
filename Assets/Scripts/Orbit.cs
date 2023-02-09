using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public float gravity;
    public bool isSphere;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<GravityBody>()) {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            other.GetComponent<GravityBody>().orbiters.Add(this.GetComponent<Orbit>());
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.GetComponent<GravityBody>()) {
            other.GetComponent<GravityBody>().orbiters.Remove(this.GetComponent<Orbit>());
        }
    }


}
