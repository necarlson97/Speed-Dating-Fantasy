using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    float speed = 1f;
    Vector2 move;
    float stuckTimer = 1f;

    public AudioClip wallHit;
    
    void Start() {
        // Get random direciton
        // TODO at higher diffuculites, move faster

        // Move at the start, then bounce, and never loose acceleration
        move = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        move.Normalize();
        move *= speed;
    }

    void FixedUpdate() {
        GetComponent<Rigidbody2D>().AddForce(move);

        if (GetComponent<Rigidbody2D>().velocity.magnitude < 0.01f) {
            stuckTimer -= Time.fixedDeltaTime;
        } else {
            stuckTimer = 1f;   
        }
        if (stuckTimer < 0f) {
            move *= -1;
            GetComponent<Rigidbody2D>().AddForce(move * 3);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var name = other.gameObject.name;
        // Change direction when near wall
        if (other.gameObject.name.Contains("Wall")) {
            move *= -1;
            PlayAudio(wallHit);
        }
    }

    public void PlayAudio(AudioClip c){
        var source = GetComponentInChildren<AudioSource>();
        source.pitch = Random.Range(0.9f, 1.1f);
        source.clip = c;
        source.Play();
    }
}
