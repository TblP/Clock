using System;
using UnityEngine;
using UnityEngine.UI;

public class wakemeup : MonoBehaviour
{
    public GameObject Clock;

    public GameObject Alarm;

    public Button Accept;
    public Button Delete;
    public Button swith;
    
    public bool check;
    // Start is called before the first frame update
    private void Start()
    {
        check = false;
        swith.interactable = false;
        Accept.interactable = false;
        Delete.interactable = false;
    }

    public void Switch()
    {
        check = !check;

        if (!check)
        {
            Alarm.SetActive(false);
            Clock.SetActive(true);
            Accept.interactable = false;
            Delete.interactable = false;
            swith.interactable = false;
        }
        else
        {
            Clock.SetActive(false);
            Alarm.SetActive(true);
            Accept.interactable = true;
            Delete.interactable = true;
            swith.interactable = true;
        }
    }
}
