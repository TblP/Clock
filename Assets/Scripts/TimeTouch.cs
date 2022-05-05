using System;
using UnityEngine;

public class TimeTouch : MonoBehaviour
{
    public Alarm _alarm;
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hour"))
        {
            _alarm.timeH = Int32.Parse(other.gameObject.name);
        }
    }
}
