
using System;
using UnityEngine;

[Serializable]
public struct Unit 
{
    RegStatus regStatus;

    float Eat;
    float Sleep;
    float Seat;

    public Unit(RegStatus regStatus, float eat, float sleep, float seat)
    {
        this.regStatus = regStatus;
        Eat = eat;
        Sleep = sleep;
        Seat = seat;
    }

    public void MiusStatus(float deltatime) {
        Eat -= UnityEngine.Random.Range(0,1) * deltatime;
        Sleep -= UnityEngine.Random.Range(0, 1) * deltatime;
        Seat -= UnityEngine.Random.Range(0, 1) * deltatime;
    }
}
