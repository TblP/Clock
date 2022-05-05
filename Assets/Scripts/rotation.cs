using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class rotation : MonoBehaviour
{
    public Transform second;
    public Transform minute;
    public Transform hour;
    public int n;
    public const float hourtodeg = 360f / 12f, minutToDeg = 360f / 60f, secToDeg = 360f / 60f;
    public Text timer2;
    public int hrs,mnt,sec;
    private float timer;

    public getreqst _getreqst;
    private void Start()
    {
        hrs = _getreqst.curtime.Hours;
        mnt = _getreqst.curtime.Minutes;
        timer = _getreqst.curtime.Seconds;
        n = 1;
        StartCoroutine(timercheck());
    }

    void Update()
    {
        second.localRotation = Quaternion.Euler(0f, 0f, (float)sec * -secToDeg);
        minute.localRotation = Quaternion.Euler(0f, 0f, (float)mnt * -minutToDeg);
        hour.localRotation = Quaternion.Euler(0f, 0f, (float)hrs * -hourtodeg);
        timer2.text = hrs + ":" + mnt;
        clock();
    }

    void clock()
    {
        timer += 1 * Time.deltaTime;
        sec = (int)timer;
        if (timer >= 59)
        {
            timer = 0;
            mnt += 1;
        }

        if (mnt == 59)
        {
            mnt = 0;
            hrs += 1;
        }

        if (hrs == 24)
        {
            hrs = 0;
        }
    }
    IEnumerator timercheck()
    {
        yield return new WaitForSeconds(n);
        n = 3600;
        againcheck();
        StartCoroutine(timercheck());
    }

    private void againcheck()
    {
        hrs = _getreqst.curtime.Hours;
        mnt = _getreqst.curtime.Minutes;
        timer = _getreqst.curtime.Seconds;
    }
}
