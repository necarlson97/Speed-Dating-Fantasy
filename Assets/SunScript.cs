using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunScript : MonoBehaviour {

    // Keep track of time we have left,
    // and where to be in the sky
    float startTime = -1;
    Vector3 target;

    // What gradient to change for the sky
    Gradient gradient = new Gradient();
    GradientColorKey[] gradientColors = new GradientColorKey[2] {
        new GradientColorKey(new Color(231f/255f, 210f/255f, 247f/255f), 0f),
        new GradientColorKey(new Color(240f/255f, 110f/255f, 50f/255f), 1f)
    };
    GradientAlphaKey[] gradientAlpha = new GradientAlphaKey[2] {
        new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f)
    };

    
    void Start() {
        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        gradient.SetKeys(gradientColors, gradientAlpha);
    }

    // Update is called once per frame
    void Update() {
        Move();
        SetSkyColor();
    }

    public void StartTimer() {
        // 10s timer starts now
        startTime = Time.time;
    }

    public void Reset(bool softReset=false) {
        // Disable sun timer, and reset to beginning
        // - could be soft, in which case, time sets
        // back to 10s, but keeps counting
        startTime = -1;
        target = new Vector3(-1, 16,11);

        PlayPartilce("Reset Sun");
        if (softReset) StartTimer();
    }

    void Move() {
        // As the timer runs out, move the sun down the sky

        var timeElapsed = Time.time - startTime;
        // May not be started yet
        if (startTime == -1) timeElapsed = 0;

        var timeLeft = 10 - timeElapsed;
        var ratioLeft = timeLeft / 10;


        // Top of screen is 14, bottom is 6.5 (?)
        var max = 16;
        var min = 6.5;
        var y = min + ratioLeft * (max - min);

        target = new Vector3(-1, (float) y, 11);

        var speed = Time.deltaTime * 2;
        transform.position = Vector3.Lerp(transform.position, target, speed);

        GetComponentInChildren<Text>().text = Mathf.Round(timeLeft)+"s";

        // give em .5s leeway
        if (timeLeft < -0.5) {
            GameObject.Find("Phone").GetComponent<PhoneScript>().OutOfTime();
        }
    }

    void SetSkyColor() {
        // Have the bg color of the game change by the
        // 'time of day'
        var timeElapsed = Time.time - startTime;
        // May not be started yet
        if (startTime == -1) timeElapsed = 0;
        var ratioElapsed = timeElapsed / 10;
            
        var cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        cam.backgroundColor = gradient.Evaluate(ratioElapsed);
    }

    public void PlayPartilce(string p) {
        // Find the child particle system with the name 'p'
        transform.Find(p).GetComponent<ParticleSystem>().Play();
    }
}
