using System;
using UnityEngine;

public class WallClock : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;
    public Transform secondHand;

    void Update()
    {
        DateTime currentTime = DateTime.Now;

        float second = currentTime.Second;
        float minute = currentTime.Minute + second / 60f;
        float hour = (currentTime.Hour % 12) + minute / 60f;

        secondHand.localRotation = Quaternion.Euler(0, 0, -second * 6f);
        minuteHand.localRotation = Quaternion.Euler(0, 0, -minute * 6f);
        hourHand.localRotation = Quaternion.Euler(0, 0, -hour * 30f);
    }
}