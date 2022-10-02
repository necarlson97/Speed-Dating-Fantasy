using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneScript : MonoBehaviour {
    
    CharacterCEOScript ccs;
    SunScript sun;
    CharacterScript currentCharacter;

    GameObject readyScreen;
    GameObject conversationScreen;

    public AudioClip wrongAnswer;
    public AudioClip hitAnswer;
    public AudioClip rightAnswer;
    public AudioClip matched;

    void Start() {
        sun = GameObject.Find("Sun").GetComponent<SunScript>();
        ccs = GameObject.Find("CharacterCEO").GetComponent<CharacterCEOScript>();

        readyScreen = GameObject.Find("Ready");
        conversationScreen = GameObject.Find("Conversation");

        readyScreen.SetActive(true);
        conversationScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        // TODO just for testing
        if (Input.GetKeyDown("0")) {
            NextChatacter();
        }
    }

    public void NextChatacter() {
        // If it is our first time, begin the conversation
        sun.Reset();
        if (currentCharacter == null) {
            readyScreen.SetActive(false);
            conversationScreen.SetActive(true);
        } else {
            // No sun for the first one, only after
            sun.StartTimer();
        }

        // Spawn a new enemy
        GameObject.Find("Arena").GetComponent<ArenaScript>().Spawn();

        currentCharacter = ccs.GetUnseenCharacter();

        var t = conversationScreen.transform;
        t.Find("Portrait").GetComponent<RawImage>().texture = currentCharacter.portrait;
        t.Find("Name").GetComponent<Text>().text = currentCharacter.name;
        t.Find("Bio").GetComponentInChildren<Text>().text = currentCharacter.bio;

        // Dont want to disable, makes hard to find, so just scaling to 0 for now
        var bubbles = new List<string> { "Q1", "Q2", "Q3", "A1", "A2", "A3" };
        foreach (var b in bubbles) {
            t.Find(b).localScale = Vector3.zero;
        }

        PlayAudio(matched);
        FirstQuestion();
    }


    // Gifts depend on the charater's species
    // dictonary of species: good, bad
    Dictionary<string, string[]> gifts = new Dictionary<string, string[]>() {
        { "minotaur", new[] {"hay", "brain"} },
        { "undead", new[] {"brain", "fire"} },
        { "fairy", new[] {"mushroom", "ingot"} },
        { "vampire", new[] {"blood", "mushroom"} },
        { "devil", new[] {"fire", "crusifix"} },
        { "angel", new[] {"crusifix", "blood"} },
        { "dwarf", new[] {"ingot", "hay"} },
        { "elf", new[] {"peppermint", "bone"} },
        { "werewolf", new[] {"bone", "goblin"} },
        { "orc", new[] {"goblin", "peppermint"} },
    };

    // Ideology depends on the charater's job
    // dictonary of job: good, bad
    Dictionary<string, string[]> ideologies = new Dictionary<string, string[]>() {
        { "soldier", new[] {"fight the enemy", "enjoy sweet treats"} },
        { "teacher",  new[] {"love learning", "serve the economy"} },
        { "sailor",  new[] {"respect the sea", "work the land"} },
        { "baker",  new[] {"enjoy sweet treats", "respect the sea"} },
        { "builder",  new[] {"build a better tomorrow", "clothe yourself well"} },
        { "tailor",  new[] {"clothe yourself well", "keep music at heart"} },
        { "musician",  new[] {"keep music at heart", "build a better tomorrow"} },
        { "merchant",  new[] {"serve the economy", "love learning"} },
        { "farmer",  new[] {"work the land", "serve the economy"} },
        { "mayor",  new[] {"guid the community", "fight the enemy"} },
    };

    // Personality depends on the charater's... personality!
    // dictonary of personality: good, bad
    Dictionary<string, string[]> personalities = new Dictionary<string, string[]>() {
        { "comedic", new[] {"comedic", "serious"} },
        { "serious", new[] {"serious", "comedic"} },
        { "kindly", new[] {"kindly", "dasdardly"} },
        { "dasdardly", new[] {"dasdardly", "kindly"} },
        { "artistic", new[] {"artistic", "simple"} },
        { "simple", new[] {"simple", "artistic"} },
        { "wise", new[] {"wise", "fearsome"} },
        { "fearsome", new[] {"fearsome", "wise"} },
        { "shy", new[] {"shy", "proud"} },
        { "proud", new[] {"proud", "shy"} },
    };

    public void FirstQuestion() {
        // Player is getting ready to ask their first question
        var options = gifts[currentCharacter.species];

        var top = options[0];
        var bottom = options[1];
        if (Random.value > 0.5f) {
            top = options[1];
            bottom = options[0];
        }

        GameObject.Find("Up Text").GetComponent<Text>().text = top;
        GameObject.Find("Down Text").GetComponent<Text>().text = bottom;

        var nqt = GameObject.Find("Next Q").GetComponent<Text>();
        nqt.text = "I bought you a gift... It's a ";
    }
    public void SecondQuestion() {
        var options = ideologies[currentCharacter.job];

        var top = options[0];
        var bottom = options[1];
        if (Random.value > 0.5f) {
            top = options[1];
            bottom = options[0];
        }

        GameObject.Find("Up Text").GetComponent<Text>().text = top;
        GameObject.Find("Down Text").GetComponent<Text>().text = bottom;

        var nqt = GameObject.Find("Next Q").GetComponent<Text>();
        nqt.text = "I think it's always important to ";
    }
    public void ThirdQuestion() {
        var options = personalities[currentCharacter.personality];

        var top = options[0];
        var bottom = options[1];
        if (Random.value > 0.5f) {
            top = options[1];
            bottom = options[0];
        }

        GameObject.Find("Up Text").GetComponent<Text>().text = top;
        GameObject.Find("Down Text").GetComponent<Text>().text = bottom;

        var nqt = GameObject.Find("Next Q").GetComponent<Text>();
        nqt.text = "Can I say, you seem ";
    }

    public void SubmitAnswer(string s) {
        // Try sending 1st, 2nd or 3rd message,
        // depedning on what is still not sent
        var t = conversationScreen.transform;
        if (t.Find("Q1").localScale == Vector3.zero) {
            FirstAnswer(s);
        } else if (t.Find("Q2").localScale == Vector3.zero) {
            SecondAnswer(s);
        } else if (t.Find("Q3").localScale == Vector3.zero) {
            ThirdAnswer(s);
        } else {
            Debug.Assert(false, "Not sure what question: "+s);
        }
    }

    public void FirstAnswer(string s) {
        // Player has chosen what to ask for, and the charaacter
        // will respond with yay or nay

        var question = GameObject.Find("Next Q").GetComponent<Text>().text;
        SetMessage("Q1", question + s);

        var goodAnswer = gifts[currentCharacter.species][0];
        Debug.Log(s + " vs " + goodAnswer);
        if (s == goodAnswer) {
            SecondQuestion();
            Invoke("FirstThanks", 0.5f);
            sun.Reset(true);
            PlayAudio(hitAnswer);
        } else {
            SetMessage("A1", "Ew...");
            Invoke("Failure", 0.5f);
        }
    }
    public void SecondAnswer(string s) {
        var question = GameObject.Find("Next Q").GetComponent<Text>().text;
        SetMessage("Q2", question + s);

        var goodAnswer = ideologies[currentCharacter.job][0];
        Debug.Log(s + " vs " + goodAnswer);
        if (s == goodAnswer) {
            ThirdQuestion();
            Invoke("SecondThanks", 0.5f);
            sun.Reset(true);
            PlayAudio(hitAnswer);
        } else {
            SetMessage("A2", "Eh...");
            Invoke("Failure", 0.5f);
        }
    }
    public void ThirdAnswer(string s) {
        var question = GameObject.Find("Next Q").GetComponent<Text>().text;
        SetMessage("Q3", question + s);

        var goodAnswer = personalities[currentCharacter.personality][0];
        Debug.Log(s + " vs " + goodAnswer);
        if (s == goodAnswer) {
            GameObject.Find("Up Text").GetComponent<Text>().text = "nice";
            GameObject.Find("Down Text").GetComponent<Text>().text = "nice";
            var nqt = GameObject.Find("Next Q").GetComponent<Text>();
            nqt.text = "well done";

            Invoke("ThirdThanks", 0.1f);
            Invoke("NextChatacter", 1f);
            sun.Reset();
            PlayAudio(hitAnswer);
        } else {
            SetMessage("A3", "Not really...");
            Invoke("Failure", 0.5f);
        }
    }

    public void FirstThanks() {
        // TODO particles, sounds
        SetMessage("A1", "I love it!");
        PlayAudio(rightAnswer);
    }
    public void SecondThanks() {
        // TODO particles, sounds
        SetMessage("A2", "I agree!");
        PlayAudio(rightAnswer);
    }
    public void ThirdThanks() {
        // TODO particles, sounds
        SetMessage("A3", "You know me!");
        PlayAudio(rightAnswer);
    }

    public void Failure() {
        // Player has run out of time, or sent a bad message
        // Swap them to the game over screen, let them know they got some rolladex stuff,
        // and reset

        // Show whatever character they died on
        // TODO is it possible to die without character?
        if (currentCharacter != null) {
            var go = GameObject.Find("Loss Rolodex").gameObject;
            ccs.FillRolodexEntry(go, currentCharacter);
        }

        // TODO reset everything!
        currentCharacter = null;
        GameObject.Find("Up Text").GetComponent<Text>().text = "Ready?";
        GameObject.Find("Down Text").GetComponent<Text>().text = "Ready?";
        GameObject.Find("Next Q").GetComponent<Text>().text = "";
        sun.Reset();
        GameObject.Find("Arena").GetComponent<ArenaScript>().Reset();

        PlayAudio(wrongAnswer);
        GameObject.Find("Main Camera").GetComponent<CameraScript>().GoTo("Up Left");
    }

    public void OutOfTime() {
        // TODO set some stuff at the top / bottom, and
        // on failure screen
        Failure();
    }

    public void SetMessage(string b, string s) {
        // Set the text for a specific question / answer
        var t = conversationScreen.transform;
        var bubble = t.Find(b);
        bubble.localScale = new Vector3(1, 1, 1);
        bubble.GetComponentInChildren<Text>().text = s;
        bubble.GetComponentInChildren<ParticleSystem>().Play();
    }

    public void UpText() {
        // Player chose the option at the top of the arena
        if (currentCharacter == null) {
            NextChatacter();
            return;
        }
        var answer = GameObject.Find("Up Text").GetComponent<Text>().text;

        SubmitAnswer(answer);
    }

    public void DownText() {
        // Player chose the option at the bottom of the arena
        if (currentCharacter == null) {
            NextChatacter();
            return;
        }
        GameObject.Find("Player").GetComponent<PlayerScript>().Recenter();
        var answer = GameObject.Find("Down Text").GetComponent<Text>().text;
        SubmitAnswer(answer);
    }

    public void PlayAudio(AudioClip c){
        var source = GetComponentInChildren<AudioSource>();
        source.pitch = Random.Range(0.9f, 1.1f);
        source.clip = c;
        source.Play();
    }
}
