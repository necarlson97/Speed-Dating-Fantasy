using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private float speed = 30f;
    public float shiftTime = 0f;
    private Vector3 homePos;
    public float recenterTimer = 0f;

    private bool forceRecenter = false;

    public AudioClip answerHit;
    public AudioClip enemyHit;
    
    void Start() {
        homePos = transform.position;    
    }

    void Update() {
        Move();

        // Recharge shift boost
        if (shiftTime > 0) shiftTime -= Time.deltaTime;
        if (recenterTimer > 0) recenterTimer -= Time.deltaTime;
    }

    void Move() {
        // WASD movement, normalized to speed
        // TODO add arrow keys, controller input, etc
        if (forceRecenter) {
            ForceMove();
            return;
        }

        var move = new Vector2(0, 0);
        if (Input.GetKey("w") || Input.GetKey("up")) move.y += 1;
        if (Input.GetKey("s") || Input.GetKey("down")) move.y -= 1;
        if (Input.GetKey("a") || Input.GetKey("left")) move.x -= 1;
        if (Input.GetKey("d") || Input.GetKey("right")) move.x += 1;

        // Debugging, force all toys into holes
        if (Input.GetKey("escape")) Application.Quit();

        // Don't have diagnonals be faster
        move = move.normalized * speed;

        if (Input.GetKey("left shift") && shiftTime <= 0) {
            move *= 100;
            shiftTime = 1f;
        }

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        GetComponent<Rigidbody2D>().AddForce(move * Time.deltaTime * 100);
    }

    private void ForceMove() {
        // Slowly bring player back to center
        transform.position = Vector3.Lerp(transform.position, homePos, Time.deltaTime * 10);
        if (Vector3.Distance(transform.position, homePos) < 0.1f) {
            transform.position = homePos;
            forceRecenter = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {

        // If we just recentered, we shouldn't double trigger
        // TODOD gross
        if (recenterTimer > 0f) return;

        var name = other.gameObject.name;
        // TODO particles and sounds effects
        if (other.gameObject.name.Contains("Enemy")) {
            Recenter();
            PlayPartilce("Enemy Hit");
            PlayAudio(enemyHit);
        }

        // TODO
        if (other.gameObject.name.Contains("Up Text")) {
            Recenter();
            PlayPartilce("Answer Hit");
            PlayAudio(answerHit);
            GameObject.Find("Phone").GetComponent<PhoneScript>().UpText();
        } else if (other.gameObject.name.Contains("Down Text")) {
            Recenter();
            PlayPartilce("Answer Hit");
            GameObject.Find("Phone").GetComponent<PhoneScript>().DownText();
        }
    }

    public void Recenter() {
        forceRecenter = true;
        recenterTimer = 0.5f;
    }

    public void PlayPartilce(string p) {
        // Find the child particle system with the name 'p'
        transform.Find(p).GetComponent<ParticleSystem>().Play();
    }

    public void PlayAudio(AudioClip c){
        var source = GetComponentInChildren<AudioSource>();
        source.pitch = Random.Range(0.9f, 1.1f);
        source.clip = c;
        source.Play();
    }
}
