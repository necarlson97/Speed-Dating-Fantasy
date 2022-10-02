using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public AudioClip buttonHit;

    Vector3 target;
    void Start() {
        target = transform.position;
    }
    
    void Update() {
        // Smoothly move twoards desired area, snapping in place
        if (Vector3.Distance(transform.position, target) < 0.01f) {
            transform.position = target;
        } else {
            var speed = Time.deltaTime * 8;
            transform.position = Vector3.Lerp(transform.position, target, speed);
        }
        

        // TODO just for testing
        if (Input.GetKeyDown("1")) {
            GoTo("Center");
        } else if (Input.GetKeyDown("2")) {
            GoTo("Up");
        } else if (Input.GetKeyDown("3")) {
            GoTo("Down");
        } else if (Input.GetKeyDown("4")) {
            GoTo("Left");
        } else if (Input.GetKeyDown("5")) {
            GoTo("Right");
        } else if (Input.GetKeyDown("6")) {
            GoTo("Up Left");
        } else if (Input.GetKeyDown("6")) {
            GoTo("Up Right");
        }
    }

    public void GoTo(string s) {
        // Go to a sepecific canvas
        target = GameObject.Find(s+" Canvas").transform.position;
        // Keep camera back
        target.z = -10;
        PlayAudio(buttonHit);
    }

    public void PlayAudio(AudioClip c){
        var source = GetComponentInChildren<AudioSource>();
        source.pitch = Random.Range(0.9f, 1.1f);
        source.clip = c;
        source.Play();
    }
}
