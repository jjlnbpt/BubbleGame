using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [field: SerializeField] public EventReference backgroundMusic { get; private set; }
    [field: SerializeField] public EventReference popEvent { get; private set; }
    [field: SerializeField] public EventReference spawnEvent { get; private set; }

    public EventInstance background;
    private List<EventInstance> events;

    public static AudioManager instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        events = new List<EventInstance>();

        background = CreateInstance(backgroundMusic);
        background.start();
        background.setParameterByName("MusicIntensity", 0);
    }

    // Generic method for creating an instance 
    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }

    // Creates an instance of a bubble popping event
    public EventInstance CreatePopInstance()
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(popEvent);
        events.Add(eventInstance);
        return eventInstance;
    }

    // Creates an instance of a bubble spawning event
    public EventInstance CreateSpawnInstance()
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(spawnEvent);
        events.Add(eventInstance);
        return eventInstance;
    }

    private void OnDestroy()
    {
        CleanUp();
    }

    // Stops all events that are playing
    private void CleanUp()
    {
        background.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        foreach (EventInstance e in events)
        {
            e.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }
}
