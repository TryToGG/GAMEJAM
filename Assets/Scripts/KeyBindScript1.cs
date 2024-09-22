using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindScript1 : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>(); //saving keybinds and controls ex)move up, W
    public TextMeshProUGUI up, left, down, right, cycle, place;
    private GameObject currentkey;

    void Start()
    {
        //keys.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W")));
        // Use this if you want to use the saving function
        //changing the string(ex-"UpArrow") into KeyCode in order to put it in the dictionary

        keys.Add("Up", KeyCode.W);
        keys.Add("Left", KeyCode.A);
        keys.Add("Down", KeyCode.S);
        keys.Add("Right", KeyCode.D);
        keys.Add("Cycle Prop", KeyCode.Z);
        keys.Add("Place Prop", KeyCode.X);

        up.text = keys["Up"].ToString();
        left.text = keys["Left"].ToString();
        down.text = keys["Down"].ToString();
        right.text = keys["Right"].ToString();
        cycle.text = keys["Cycle Prop"].ToString();
        place.text = keys["Place Prop"].ToString();
    }

    private void OnGUI() // sensitively catches key input
    {
        if (currentkey != null)
        {
            Event e = Event.current;
            if(e.isKey) //if the event is key press
            {
                keys[currentkey.name] = e.keyCode; //go to the dictionary and find the original key
                currentkey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString(); //change the text to a new key
                currentkey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked) //clicked == the button that is clicked
    {
        currentkey = clicked; //currentkey is something like "Up" (Button's name)

    }

    //I tried to save this keys next time, but it somehow didn't worked properly, and got afraid of crashing the default key inputs, so I'll just comment it
    //public void SaveKeys()
    //{
    //    foreach (var key in keys) //referring dictionary keys
    //    {
    //        PlayerPrefs.SetString(key.Key, key.Value.ToString()); //<string, Keycode>
    //    }

    //    PlayerPrefs.Save();
    //}
}
