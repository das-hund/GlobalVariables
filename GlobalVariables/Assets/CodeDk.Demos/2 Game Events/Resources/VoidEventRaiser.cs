using CodeDk;
using UnityEngine;

public class VoidEventRaiser : MonoBehaviour
{
    public GlobalVoidGameEvent eventToRaise;

    public float eventsPerSecond = 100;
    public int eventsRaised = 0;

    public void Update()
    {
        int eventCount = (int)(eventsPerSecond * Time.time) - eventsRaised;

        for (int i = 0; i < eventCount; i++)
        {
            eventToRaise.RaiseEvent();
        }

        eventsRaised += eventCount;
    }
}
