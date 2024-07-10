using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FmodPreloader : MonoBehaviour
{

    [SerializeField]
    float timeBeforeLoad;
    // List of Banks to load
    [FMODUnity.BankRef]
    public List<string> Banks;

    float loadingStartTime;
    // The name of the scene to load and switch to
    public string Scene = null;

    public void Start()
    {
        StartCoroutine(LoadGameAsync());
    }

    void FixedUpdate()
    {

        // Update the loading indication
    }

    IEnumerator LoadGameAsync()
    {
        loadingStartTime = Time.time;
        // Start an asynchronous operation to load the scene
        AsyncOperation async = SceneManager.LoadSceneAsync(Scene);

        // Don't lead the scene start until all Studio Banks have finished loading
        async.allowSceneActivation = false;

        // Iterate all the Studio Banks and start them loading in the background
        // including the audio sample data
        foreach (var bank in Banks)
        {
            FMODUnity.RuntimeManager.LoadBank(bank, true);
        }

        // Keep yielding the co-routine until all the Bank loading is done
        while (FMODUnity.RuntimeManager.AnyBankLoading())
        {
            yield return null;
        }

        while (Time.time < loadingStartTime + timeBeforeLoad)
        {
            yield return null;
        }
        // Allow the scene to be activated. This means that any OnActivated() or Start()
        // methods will be guaranteed that all FMOD Studio loading will be completed and
        // there will be no delay in starting events
        async.allowSceneActivation = true;

        // Keep yielding the co-routine until scene loading and activation is done.
        while (!async.isDone)
        {
            yield return null;
        }

    }
}
