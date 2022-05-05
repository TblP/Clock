using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Alarm : MonoBehaviour
{
    private Camera myCam;
    private Vector3 screenPos;
    private float angleOffset;
    public Collider2D Hour;
    public Collider2D Minute;
    public bool h, m, start;
    public int timeH;
    private int timeM;
    public float step;
    public Text timer;
    private bool AlarmStatus;
    public bool AmPmSwitch;
    public DropChoice _DropChoice;
    public AudioSource _audio;
    bool switchinput;
    public int fulltimeH;
    public Button _switch;
    public Text _ampmbtn;
    public wakemeup _AWakemeup;

    public rotation _Rotation;
    
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        myCam = Camera.main;
        timeH = 0;
        timeM = 0;
    }

    private void Update()
    {
        if (!AlarmStatus)
        {
            ArrowRotate();
        }

        TimeChoice();
        alarmSignal();
        
        if (!switchinput & !AlarmStatus & _AWakemeup.check)
        {
            _DropChoice._hour.interactable = false;
            _DropChoice._minute.interactable = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true); 
        }

        if (switchinput & !AlarmStatus & _AWakemeup.check)
        {
            _DropChoice._hour.interactable = true;
            _DropChoice._minute.interactable = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);  
        }

        if (transform.GetChild(0).gameObject.activeSelf == false)
        {
            timeH = _DropChoice._hour.value;
            timeM = _DropChoice._minute.value;
        }
        
        if (AmPmSwitch)
        {
            _ampmbtn.text = "PM";
        }
        else
        {
            _ampmbtn.text = "AM";
        }

        pmtime();
    }

    void ArrowRotate()
    {

        if (Input.touchCount == 0)
        {
            m = false;
            h = false;
        }
        Vector3 mousePos = myCam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if(Hour == Physics2D.OverlapPoint(mousePos))
            {
                h = !h;
            }

            if (h)
            {
                screenPos = myCam.WorldToScreenPoint(transform.position);
                Vector3 vec3 = Input.mousePosition - screenPos;
                angleOffset = (Mathf.Atan2(Hour.transform.right.y, Hour.transform.right.x) - Mathf.Atan2(vec3.y, vec3.x)) * Mathf.Rad2Deg;
            }
            
            if(Minute == Physics2D.OverlapPoint(mousePos))
            {
                m = !m;
            }

            if (m)
            {
                screenPos = myCam.WorldToScreenPoint(transform.position);
                Vector3 vec3 = Input.mousePosition - screenPos;
                angleOffset = (Mathf.Atan2(Minute.transform.right.y, Minute.transform.right.x) - Mathf.Atan2(vec3.y, vec3.x)) * Mathf.Rad2Deg;
            }
        }

        if (h)
        {
            Vector3 vec3 = Input.mousePosition - screenPos;
            float angle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
            Hour.transform.localRotation = Quaternion.Euler(0f, 0f, angleOffset + angle);
        }

        if (m)
        {
            Vector3 vec3 = Input.mousePosition - screenPos;
            float angle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
            Minute.transform.localRotation = Quaternion.Euler(0f, 0f, angle + angleOffset);
        }
        
        
    }

    void TimeChoice()
    {
        if (step > 0)
        {
            start = true;
            
        }

        if (m)
        {
            step = Minute.transform.eulerAngles.z;
            step = 360 - step;
            timeM = 0;
        }
        if (start & !m)
        {
            if (step > 0)
            {
                step -= 6;
                timeM++;
            }
            if(step <= 0)
            {
                timeM -= 1;
                start = false;
            }
        }
    }

    void alarmSignal()
    {
        if (!AmPmSwitch)
        {
            if (timeH == _Rotation.hrs & timeM == _Rotation.mnt & AlarmStatus & !start)
            {
                if(!_audio.isPlaying)
                    _audio.Play();
            }
        }

        if (AmPmSwitch)
        {
            if (fulltimeH == _Rotation.hrs  & timeM == _Rotation.mnt & AlarmStatus & !start)
            {
                if(!_audio.isPlaying)
                    _audio.Play();
            }
        }
        
    }

    public void ChoiceInput()
    {
        switchinput = !switchinput;
    }
    
    public void acceptAlarm()
    {
        settime();
        _switch.interactable = false;
        _DropChoice._hour.interactable = false;
        _DropChoice._minute.interactable = false;
        AlarmStatus = true;
    }

    public void deleteAlarm()
    {
        timer.text = String.Format("{0}:{1}", "HH", "MM");
        timeH = 0;
        timeM = 0;
        Hour.transform.rotation = quaternion.identity;
        Minute.transform.rotation = quaternion.identity;
        AlarmStatus = false;
        _switch.interactable = true;
    }

    public void AmPm()
    {
        AmPmSwitch = !AmPmSwitch;
        settime();

    }

    void settime()
    {
        if (!AmPmSwitch)
        {
            timer.text = String.Format("{0}:{1}", timeH, timeM);
        }
        if (AmPmSwitch)
        {
            timer.text = String.Format("{0}:{1}", fulltimeH, timeM);
        }
    }

    void pmtime()
    {
        switch (timeH)
        {
            case 0:
                fulltimeH = 12;
                break;
            case 1:
                fulltimeH = 13;
                break;
            case 2:
                fulltimeH = 14;
                break;
            case 3:
                fulltimeH = 15;
                break;
            case 4:
                fulltimeH = 16;
                break;
            case 5:
                fulltimeH = 17;
                break;
            case 6:
                fulltimeH = 18;
                break;
            case 7:
                fulltimeH = 19;
                break;
            case 8:
                fulltimeH = 20;
                break;
            case 9:
                fulltimeH = 21;
                break;
            case 10:
                fulltimeH = 22;
                break;
            case 11:
                fulltimeH = 23;
                break;
        }
    }
}
