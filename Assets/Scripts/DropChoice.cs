
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropChoice : MonoBehaviour
{

    public Dropdown _minute;
    public List<string> dropOpt;
    public Dropdown _hour;
    // Start is called before the first frame update
    void Start()
    {
        _hour.interactable = false;
        _minute.interactable = false;
        for (int i = 0; i < 60; i++)
        {
            dropOpt.Add(i + "");
        }
        _minute.AddOptions(dropOpt);
        dropOpt.Clear();
        for (int i = 0; i < 12; i++)
        {
            dropOpt.Add(i + "");
        }
        _hour.AddOptions(dropOpt);
    }
}
