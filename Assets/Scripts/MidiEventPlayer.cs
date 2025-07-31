using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidiEventPlayer: MonoBehaviour
{
    private List<TimedMidiEvent> _eventsToPlay;
    private Coroutine _playbackCoroutine;
    private MidiManager _midiManager;
    [SerializeField] GameObject recordingLight;
    private MeshRenderer _recordingLightMeshRenderer;

    void Start()
    {
        _midiManager = FindObjectOfType<MidiManager>();
        _recordingLightMeshRenderer = recordingLight.GetComponent<MeshRenderer>();
    }
    

    public void PlayEvents(List<TimedMidiEvent> events)
    {
        if (_playbackCoroutine != null)
        {
            StopCoroutine(_playbackCoroutine);
        }

        _eventsToPlay = new List<TimedMidiEvent>(events);
        _playbackCoroutine = StartCoroutine(PlayEventsWithTiming());
        ChangeRecordingLight(Color.green);
    }

    public void StopEvents()
    {
        if (_playbackCoroutine != null)
        {
            StopCoroutine(_playbackCoroutine);
            ChangeRecordingLight(Color.white);
        }
    }

    private void ChangeRecordingLight(Color color)
    {
        _recordingLightMeshRenderer.material.color = color;
    }

    private IEnumerator PlayEventsWithTiming()
    {
        if (_eventsToPlay == null || _eventsToPlay.Count == 0)
        {
            yield break;
        }

        // Sort by time offset just in case
        _eventsToPlay.Sort((a, b) => a.TimeOffset.CompareTo(b.TimeOffset));

        var startTime = Time.time;
        var firstEventTime = _eventsToPlay[0].TimeOffset.TotalSeconds;

        foreach (var timedEvent in _eventsToPlay)
        {
            // Calculate when this event should fire
            float eventTime = (float)(timedEvent.TimeOffset.TotalSeconds - firstEventTime);
            float currentElapsed = Time.time - startTime;

            // Wait until it's time for this event
            if (currentElapsed < eventTime)
            {
                yield return new WaitForSeconds(eventTime - currentElapsed);
            }

            // Process the event
            _midiManager.ProcessNotesForPlayback(timedEvent.Event);
        }
    }
}