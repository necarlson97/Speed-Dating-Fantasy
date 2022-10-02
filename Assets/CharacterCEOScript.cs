using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CharacterCEOScript : MonoBehaviour {
    // Script that creates and keeps track of all the characters
    // Couldn't think of a better name...

    Dictionary<string, CharacterScript> allCharacters = new Dictionary<string, CharacterScript>();
    Dictionary<string, CharacterScript> unseenCharacters = new Dictionary<string, CharacterScript>();

    // Prefab for making new entries
    public GameObject rolodexEntry;

    void Start() {
        LoadAllCharacters();
        FillAllRolodex();
    }

    void LoadAllCharacters() {
        // Iterate through the name file, getting all of the
        // names, images, bios, etc

        // Dictionary of names to file_names for images/bios
        var names = LoadNames();

        // TODO shuffle names

        foreach (var kv in names) {
            var name = kv.Key;
            var fileName = kv.Value;
            var c = new CharacterScript(name, fileName);
            allCharacters.Add(name, c);
            unseenCharacters.Add(name, c);
        }
    }

    void FillAllRolodex() {
        // TODO create the neccicary entries for the rolodex
        var parent = GameObject.Find("Scroll Content").transform;

        int i=0;
        float y=-60f; // Start near top of rolodex
        foreach(var kv in allCharacters) {
            // The x position is either left, middle, or center
            var x = 0f;
            if (i%3==0) x = -300;
            else if (i%3==2) x = 300;

            var character = kv.Value;
            var go = GameObject.Instantiate(
                rolodexEntry, parent.position, Quaternion.identity, parent);
            go.transform.localPosition =  new Vector3(x, y, 0);
            FillRolodexEntry(go, character);

            // Move to next row
            if (i%3==2) y -= 110;

            i++;
        }
    }

    public void FillRolodexEntry(GameObject go, CharacterScript c) {
        // Fill a single rolodex entry with character info
        go.transform.Find("Portrait").GetComponent<RawImage>().texture = c.portrait;
        go.transform.Find("Name").GetComponent<Text>().text = c.name;
        go.transform.Find("Personality").GetComponent<Text>().text = c.personality;
        go.transform.Find("Species").GetComponent<Text>().text = c.species;
        go.transform.Find("Job").GetComponent<Text>().text = c.job;

        if (!unseenCharacters.ContainsKey(c.name)) {
            go.transform.Find("Mystery Cover").gameObject.SetActive(false);
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
            names.Add(name, words[1].TrimEnd());
        }
        return names;
    }

    public static Texture2D LoadPortrait(string fileName) {
        // Load this character's portrait from the portraits folder
        // TODO
        fileName = "portraits/"+fileName;
        var t = Resources.Load<Texture2D>(fileName);
        Debug.Assert(t != null, "Loading " + fileName + " was null");
        return t;
    }
    public static string LoadBio(string fileName) {
        // Load this character's bio from the bios folder
        fileName = "bio/"+fileName;
        var t = Resources.Load<TextAsset>(fileName);
        Debug.Assert(t != null, "Loading " + fileName + " was null");
        return t.text;
    }

    public CharacterScript GetUnseenCharacter() {
        // Give out a new character - noting them as now seen, and thus
        // in their rolleddex
        // TODO for now, just get all their info, cuz whatever
        var c = unseenCharacters.ElementAt(Random.Range(0, unseenCharacters.Count)).Value;
        unseenCharacters.Remove(c.name);
        UpdateRolledex();
        return c;
    }

    public void UpdateRolledex() {
        // Go through the rolledex and only show info for the ones we have
        foreach(Transform child in GameObject.Find("Scroll Content").transform) {
            var name = child.Find("Name").GetComponent<Text>().text;
            if (!unseenCharacters.ContainsKey(name)) {
                child.Find("Mystery Cover").gameObject.SetActive(false);
            }
        }
    }

    public int RolodexCount() {
        // Return number of characters the player has seen
        return allCharacters.Count - unseenCharacters.Count;
    }

    void Update() {
    }
}
