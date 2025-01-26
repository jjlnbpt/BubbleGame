using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [field: SerializeField] public EventReference backgroundMusic { get; private set; }
    [field: SerializeField] public EventReference popEvent { get; private set; }
    [field: SerializeField] public EventReference spawnEvent { get; private set; }

    public EventInstance background;
    private List<EventInstance> events;

    public static AudioManager instance { get; private set; }

    private FMOD.DSP m_dsp;
    private FMOD.DSP_PARAMETER_FFT m_fftparam;

    public int sampleCount = 1028;
    public float[] samples;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        events = new List<EventInstance>();

        FMODUnity.RuntimeManager.CoreSystem.createDSPByType(FMOD.DSP_TYPE.FFT, out m_dsp);
        m_dsp.setParameterInt((int)FMOD.DSP_FFT.WINDOWTYPE, (int)FMOD.DSP_FFT_WINDOW.RECT);
        m_dsp.setParameterInt((int)FMOD.DSP_FFT.WINDOWSIZE, sampleCount * 2);

        background = CreateInstance(backgroundMusic);
        background.start();
        background.setParameterByName("MusicIntensity", 0);

        background.getChannelGroup(out FMOD.ChannelGroup channelGroup);
        channelGroup.addDSP(0, m_dsp);

        samples = new float[sampleCount];
    }

    private void Update()
    {
        GetSpectrumData();
    }

    private void GetSpectrumData()
    {
        System.IntPtr data;
        uint length;

        m_dsp.getParameterData(2, out data, out length);
        m_fftparam = (FMOD.DSP_PARAMETER_FFT)Marshal.PtrToStructure(data, typeof(FMOD.DSP_PARAMETER_FFT));

        if (m_fftparam.numchannels == 0)
        {
            background.getChannelGroup(out FMOD.ChannelGroup channelGroup);
            channelGroup.addDSP(0, m_dsp);

        }
        else if (m_fftparam.numchannels >= 1)
        {
            // Sample loop
            for (int s = 0; s < sampleCount; s++)
            {
                float totalChannelData = 0.0f;
                // Channel loop
                for (int c = 0; c < m_fftparam.numchannels; c++)
                {
                    totalChannelData += m_fftparam.spectrum[c][s];
                    samples[s] = totalChannelData / m_fftparam.numchannels;
                }
            }
        }

    }

    // Generic method for creating an instance 
    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }

    // Creates an instance of a bubble popping event
    public EventInstance CreatePopInstance(int combo)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(popEvent);
        events.Add(eventInstance);

        int pitch = Mathf.Min(combo, 15);
        eventInstance.setParameterByName("PopPitch", pitch);
        Debug.Log(combo);
        eventInstance.start();

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
        background.release();
        foreach (EventInstance e in events)
        {
            e.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            e.release();
        }
    }
}
