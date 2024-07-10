using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;
    private EventInstance MainMusic;
    private EventInstance BonusZone_Cam;
    private EventInstance BonusZone_Paul;
    private EventInstance MoveRight;
    private EventInstance Victory_Cam;
    private EventInstance Victory_Paul;
    private EventInstance Reverse;
    private EventInstance SlowDown;
    private EventInstance MoveLeft;
    private EventInstance SpeedUp;
    private EventInstance Inventory;


    bool audioResumed = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

    }

    void Start()
    {
        MainMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Musique");
        BonusZone_Cam = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/BonusZone_Cam");
        MoveRight = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/MoveRight");
        BonusZone_Paul = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/BonusZone_Paul");
        Victory_Cam = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Victory_Cam");
        Victory_Paul = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Victory_Paul");
        Reverse = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Reverse");
        MoveLeft = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/MoveLeft");
        SlowDown = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/SlowDown");
        SpeedUp = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/SpeedUp");
        Inventory = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Inventory");


        MainMusic.start();

        ResumeAudio();


    }

    public void playBonusZone_Cam()
    {
        BonusZone_Cam.start();
    }

    public void playBonusZone_Paul()
    {
        BonusZone_Paul.start();
    }

    public void playSlowDown()
    {
        SlowDown.start();
    }


    public void playSpeedUp()
    {
        SpeedUp.start();
    }
    public void playMoveLeft()
    {
        MoveLeft.start();
    }

    public void playReverse()
    {
        Reverse.start();
    }
    public void playMoveRight()
    {
        MoveRight.start();
    }
    public void playVictory_Cam()
    {
        Victory_Cam.start();
    }
    public void playVictory_Paul()
    {
        Victory_Paul.start();
    }
    public void playInventory()
    {
        Inventory.start();
    }

    public void playRandomEnemySound(int minRange, int maxRange)
    {
        StartCoroutine(EnemySoundRandom(minRange, maxRange));
    }

    IEnumerator EnemySoundRandom(int minRange, int maxRange)
    {
        yield return new WaitForSeconds(Random.Range(minRange, maxRange));
        BonusZone_Paul.start();
    }



    public void ResumeAudio()
    {
        if (!audioResumed)
        {
            var result = FMODUnity.RuntimeManager.CoreSystem.mixerSuspend();
            Debug.Log(result);
            result = FMODUnity.RuntimeManager.CoreSystem.mixerResume();
            Debug.Log(result);
            audioResumed = true;
        }
    }
}
