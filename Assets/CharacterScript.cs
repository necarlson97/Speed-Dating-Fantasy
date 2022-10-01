using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript {
    // Holds the info for a specific character
    internal string name;
    internal string species;
    internal string job;
    internal string personality;
    internal string bio;
    internal Texture2D portrait;

    public CharacterScript(string name, string fileName) {
        string[] words = fileName.Split('_');
        this.name = name;
        this.species = words[0];
        this.job = words[1];
        this.personality = words[2];
        this.portrait = CharacterCEOScript.LoadPortrait(fileName);
        this.bio = CharacterCEOScript.LoadBio(fileName);
    }
    // public CharacterScript(string name, string fileName, Iamge portrait) {
    //     string[] words = fileName.Split('_');
    //     this.name = name;
    //     this.species = words[0];
    //     this.job = words[1];
    //     this.personality = words[2];
    //     this.portrait = portrait;
    // }
    // public CharacterScript(string n, string s, string j, string p, Iamge portrait) {
    //     this.name = n;
    //     this.species = s;
    //     this.job = j;
    //     this.personality = p;
    //     this.portrait = portrait;
    // }
}
