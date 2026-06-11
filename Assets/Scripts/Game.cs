using UnityEngine;

public class Game: MonoBehaviour{
    private static bool playing = false;
    public static bool Playing
    {
        private set
        {
            playing = value;
        }
        get
        {
            return playing;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private static void Play(){
        playing = true;
    }
}