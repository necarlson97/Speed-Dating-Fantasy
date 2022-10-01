using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCEOScript : MonoBehaviour {
    // Script that creates and keeps track of all the characters
    // Couldn't think of a better name...

    Dictionary<string, CharacterScript> allCharacters = new Dictionary<string, CharacterScript>();

    // Prefab for making new entries
    public GameObject rolodexEntry;

    void Start() {
        LoadAllCharacters();
        FillRolodex();
    }

    void LoadAllCharacters() {
        // Iterate through the name file, getting all of the
        // names, images, bios, etc

        // Dictionary of names to file_names for images/bios
        var names = LoadNames();

        foreach (var kv in names) {
            var name = kv.Key;
            var fileName = kv.Value;
            allCharacters.Add(name, new CharacterScript(name, fileName));
        }
    }

    void FillRolodex() {
        // TODO create the neccicary entries for the rolodex
        var parent = GameObject.Find("Scroll Content").transform;

        int i=0;
        float y=330f; // Start near top of rolodex
        foreach(var kv in allCharacters) {
            // The x position is either left, middle, or center
            var x = 0f;
            if (i%3==0) x = -300;
            else if (i%3==2) x = 300;

            var character = kv.Value;
            var go = GameObject.Instantiate(
                rolodexEntry, parent.position, Quaternion.identity, parent);
            go.transform.localPosition =  new Vector3(x, y, 0);
            go.transform.Find("Portrait").GetComponent<RawImage>().texture = character.portrait;
            go.transform.Find("Name").GetComponent<Text>().text = character.name;
            go.transform.Find("Personality").GetComponent<Text>().text = character.personality;
            go.transform.Find("Species").GetComponent<Text>().text = character.species;
            go.transform.Find("Job").GetComponent<Text>().text = character.job;

            // Move to next row
            if (i%3==2) y -= 110;

            i++;
        }
    }

    public Dictionary<string, string> LoadNames() {
        // Load from the names file a dict of:
        // name -> species_job_personality (e.g.:)
        // "thorimir": "dwarf_merchant_kindly"
        Dictionary<string, string> names = new Dictionary<string, string>();
        // TODO

        TextAsset nameFile = Resources.Load<TextAsset>("names");

        foreach (var line in nameFile.text.Split('\n')) {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var words = line.Split(' ');
            var name = words[0];
            names.Add(name, words[1]);
        }
        return names;
    }

    public static Texture2D LoadPortrait(string fileName) {
        // Load this character's portrait from the portraits folder
        // TODO
        return Resources.Load<Texture2D>("portraits/"+fileName);
    }
    public static string LoadBio(string fileName) {
        // Load this character's bio from the bios folder
        Debug.Log("bio/"+fileName);
        // TODO 
        return "test";
        // return Resources.Load<TextAsset>("bio/"+fileName).text;
    }

    void Update() {
    }
}
