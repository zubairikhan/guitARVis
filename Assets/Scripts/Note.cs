using System;

public class Note
{
    public int Midi {get; private set;}
    public string Name{get; private set;}
    public int StringNum{get; private set;}
    public int Fret{get; private set;}
    public int Velocity{get; private set;}
    public double StartTime{get; private set;}
    public double EndTime{get; private set;}
    public double NoteDuration {
        get { return EndTime == 0 ? 0 : EndTime - StartTime; }
    }

    public DateTime StartTimeStamp{get; private set;}
    public DateTime EndTimeStamp{get; private set;}
    
    

    public Note(int midi, string name, int stringNum, int fret, int velocity, double startTime, DateTime startTimeStamp) {
        this.Midi = midi;
        this.Name = name;
        this.StringNum = stringNum;
        this.Fret = fret;
        this.Velocity = velocity;
        this.StartTime = startTime;
        this.StartTimeStamp = startTimeStamp;
    }

    public void SetEndTime(double endTime){
        this.EndTime = endTime;
    }
}
