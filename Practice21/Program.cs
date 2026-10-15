using System;
using System.Threading;

var alarmClock = new AlarmClock();

alarmClock.TriggeredAlarm += MessagePrinter;
alarmClock.TriggeredAlarm += Logger;
alarmClock.setAlarm();

alarmClock.TriggeredAlarm -= MessagePrinter;
alarmClock.setAlarm();

void MessagePrinter(object? sender, TriggeredAlarmEventArgs e)
{
    Console.WriteLine($"Alarm Triggered at {e.TriggeredAt:HH:mm:ss}!");
}

void Logger(object? sender, TriggeredAlarmEventArgs e)
{
    Console.WriteLine($"Logger - Set at: {e.SetAt:HH:mm:ss}, Triggered at: {e.TriggeredAt:HH:mm:ss}");
}

public class TriggeredAlarmEventArgs : EventArgs
{
    public DateTime SetAt { get; }
    public DateTime TriggeredAt { get; }

    public TriggeredAlarmEventArgs(DateTime setAt, DateTime triggeredAt)
    {
        SetAt = setAt;
        TriggeredAt = triggeredAt;
    }
}


public class AlarmClock
{
    public DateTime AlarmTime { get; private set; }
    public DateTime SetTime { get; private set; }

    public event EventHandler<TriggeredAlarmEventArgs>? TriggeredAlarm;

    public void setAlarm()
    {
        Console.WriteLine("Set alarm (seconds):");

        if (int.TryParse(Console.ReadLine(), out int seconds))
        {
            SetTime = DateTime.Now;
            AlarmTime = SetTime.AddSeconds(seconds);
            Console.WriteLine($"Alarm set for: {AlarmTime:HH:mm:ss}");

            Thread.Sleep(seconds * 1000);

            TriggeredAlarm?.Invoke(this, new TriggeredAlarmEventArgs(SetTime, AlarmTime));
        }
    }
}

// AlarmClock є класом видавцем всередині якого є подія TriggerAlarm. MessagePrinter та Logger є методами підписниками які чекають на сигнал від видавця. В даному прикладі використовується слабка зв'язаність, принцип який диктує що клас видавник (подія) та методи підписники (дія) не знають напряму один про одного й тільки спілкуються через делегат який зберігає список посилань неа методи.