using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaScript : MonoBehaviour {

    List<GameObject> enemies = new List<GameObject>();
    public GameObject enemyPrefab;

    void Start() {
        Spawn(true);
    }

    public void Spawn(bool force=false) {
        // Spawn a new enemy, progressivly making things harder

        // TODO is this good?
        var ccs = GameObject.Find("CharacterCEO").GetComponent<CharacterCEOScript>();
        var parent = GameObject.Find("Arena").transform;
        if (ccs.RolodexCount() >= 2 || force) {

            var x = 2.5f;
            if (Random.value > 0.5f) x *= -1;
            var e = GameObject.Instantiate(
                enemyPrefab, Vector3.zero, Quaternion.identity, parent);
            e.transform.localPosition = new Vector3(x, 0, -1);
            enemies.Add(e);
        }
    }

    public void Reset() {
        // Remove all of the enemes, go back to 
        foreach(GameObject e in enemies) {
            GameObject.Destroy(e);
        }
        Spawn(true);
    }
}
