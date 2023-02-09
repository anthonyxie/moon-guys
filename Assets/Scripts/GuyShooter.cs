using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyShooter : MonoBehaviour
{
    public GameObject guy;
    public Vector2 corner;
    private float timeTillShot;
    // Start is called before the first frame update
    void Start()
    {
        timeTillShot = Random.Range(3f, 7f);
    }

    // Update is called once per frame
    void Update()
    {
        timeTillShot = timeTillShot - Time.deltaTime;
        if (timeTillShot <= 0) {
            shootGuy();
            timeTillShot = Random.Range(3f, 7f);
        }
    }

    void shootGuy() {
        GameObject fab = Instantiate(guy);
        int s = Random.Range(5, 14);
        fab.transform.position = this.transform.position;
        fab.transform.localEulerAngles = new Vector3(0, 0,Random.Range(0f, 180f));
        float scaler = (float)s * 0.1f;
        fab.transform.localScale = new Vector3(scaler, scaler, scaler);

        Rigidbody2D rb = fab.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(18f,80f) * corner.x, Random.Range(18f,80f) * corner.y));
    
        SpriteRenderer sprite = fab.GetComponent<SpriteRenderer>();
        sprite.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
