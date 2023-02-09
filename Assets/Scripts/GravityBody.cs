using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour
{
    public List<Orbit> orbiters = new List<Orbit>();
    private Rigidbody2D rb;

    public float rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 1;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Orbit orbiter in orbiters) {
            if (!(this.gameObject.tag == "Player" && orbiter.transform.parent.gameObject.tag == "Player")) {
                if (orbiter) {
                    Vector2 grav = Vector2.zero;
                    if (orbiter.isSphere) {
                        grav = (this.transform.position - orbiter.transform.position).normalized;
                    }
                    else {
                        grav = orbiter.transform.up;
                    }
                    Vector2 localUp = this.transform.up;
                    //this is for some quaternion stuff that I do not understand yet!
                    //Quaternion targetrot = Quaternion.FromToRotation(localUp, grav) * transform.rotation;

                    transform.up = Vector2.Lerp(transform.up, grav, rotSpeed * Time.deltaTime);

                    rb.AddForce((-grav * orbiter.gravity) * rb.mass);
                }
            }
        }
        //some drag so that stuff doesn't last forever
    }

    public void removeAllOrbiters() {
        orbiters.Clear();
    }
}
