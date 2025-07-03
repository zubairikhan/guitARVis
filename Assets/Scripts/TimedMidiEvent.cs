using Melanchall.DryWetMidi.Core;
using System;

public class TimedMidiEvent
{
    public MidiEvent Event { get; }
    public TimeSpan TimeOffset { get; }

    public TimedMidiEvent(MidiEvent midiEvent, TimeSpan timeOffset)
    {
        Event = midiEvent;
        TimeOffset = timeOffset;
    }
}
