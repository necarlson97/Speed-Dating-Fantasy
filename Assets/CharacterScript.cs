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

    // Poor man's enums
    List<string> allSpecies = new List<string>{
        "minotaur", 
        "undead", 
        "fairy", 
        "vampire", 
        "devil", 
        "angel", 
        "dwarf", 
        "elf", 
        "werewolf", 
        "orc", 
    };
    List<string> allJobs = new List<string>{
        "soldier", 
        "teacher",  
        "sailor",  
        "baker",  
        "builder",  
        "tailor",  
        "musician",  
        "merchant",  
        "farmer",  
        "mayor",  
    };
    List<string> allPersonalitiets = new List<string>{
        "comedic", 
        "serious", 
        "kindly", 
        "dasdardly", 
        "artistic", 
        "simple", 
        "wise", 
        "fearsome", 
        "shy", 
        "proud", 
    };

    public CharacterScript(string name, string fileName) {
        string[] words = fileName.ToLower().Split('_');
        this.name = name;
        this.species = words[0];
        this.job = words[1];
        this.personality = words[2];
        this.portrait = CharacterCEOScript.LoadPortrait(fileName);
        this.bio = CharacterCEOScript.LoadBio(fileName).Trim();

        Debug.Assert(allSpecies.Contains(species), "Bad species: "+job+" for "+name);
        Debug.Assert(allJobs.Contains(job), "Bad job: "+job+" for "+name);
        Debug.Assert(allPersonalitiets.Contains(personality), "Bad personaliy: "+job+" for "+name);
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
